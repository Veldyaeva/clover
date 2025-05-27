using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.form.UserDistribution;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public class FormScanner
    {
        private readonly AdminFormDataService _adminFormDataService;
        private readonly UserClass _user;

        public FormScanner(AdminFormDataService dataService, UserClass user)
        {
            _adminFormDataService = dataService;
            _user = user;
        }

        public async Task ScanAndSave(string formClassName, int formId, DataTable existingObjects)
        {
            Type formType = FindFormTypeByName(formClassName);
            if (formType == null)
            {
                MessageBox.Show($"Форма '{formClassName}' не найдена.");
                return;
            }

            Form formInstance = CreateFormInstance(formType);
            if (formInstance == null)
            {
                MessageBox.Show($"Форма '{formClassName}' не содержит подходящего конструктора (UserClass или без параметров).");
                return;
            }

            // ❗ НЕ ВЫЗЫВАЕМ Show или Load
            // Просто сканируем контролы
            var controls = GetAllControls(formInstance);

            foreach (var ctrl in controls)
            {
                string objectName = ctrl.Name;
                if (string.IsNullOrWhiteSpace(objectName)) continue;

                string objectType = ctrl.GetType().Name;
                string baseText = string.IsNullOrWhiteSpace(ctrl.Text) ? "" : ctrl.Text.Trim();
                string objectNameRus = GetRussianNameForObject(baseText, objectType);

                bool alreadyExists = existingObjects.AsEnumerable()
                    .Any(row => row["ObjectName"].ToString() == objectName);

                if (!alreadyExists)
                {
                    await _adminFormDataService.InsertObjectForm(
                        objectName,
                        objectNameRus,
                        objectType,
                        _user.UserId,
                        formId
                    );
                }
            }

            MessageBox.Show($"Сканирование формы '{formClassName}' завершено.");
        }
        private string GetRussianNameForObject(string baseText, string objectType)
        {
            string prefix;

            switch (objectType)
            {
                case "CustomButton": case "Button":
                    prefix = "Кнопка";
                    break;
                case "CustomLabel": case "Label":
                    prefix = "Метка";
                    break;
                case "CustomTextBox": case "TextBox": 
                case "CustomMaskedTextBox": case "MaskedTextBox":
                    prefix = "Поле";
                    break;
                case "CustomComboBox": case "ComboBox":
                    prefix = "Раскрывающийся список";
                    break;
                case "CustomCheckBox": case "CheckBox":
                    prefix = "Галочка";
                    break;
                case "CustomGridControl": case "GridControl":
                    prefix = "Таблица";
                    break;
                case "CustomGroupBox":  case "GroupBox":
                    prefix = "Группа";
                    break;
                case "CustomToolStripMenuItem":  case "ToolStripMenuItem":
                    prefix = "Элемент меню";
                    break;
                default:
                    prefix = "Объект";
                    break;
            }

            if (string.IsNullOrWhiteSpace(baseText))
            {
                return prefix;
            }
            else
            {
                return prefix + " \"" + baseText + "\"";
            }
        }

        private Form CreateFormInstance(Type formType)
        {
            try
            {
                // Пытаемся найти пустой конструктор
                ConstructorInfo defaultCtor = formType.GetConstructor(Type.EmptyTypes);
                if (defaultCtor != null)
                {
                    return (Form)defaultCtor.Invoke(null);
                }
                // Пытаемся найти конструктор с UserClass
                ConstructorInfo ctorWithUser = formType.GetConstructor(new[] { typeof(UserClass) });
                if (ctorWithUser != null)
                {
                    return (Form)ctorWithUser.Invoke(new object[] { _user });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания формы: {ex.Message}");
            }

            return null;
        }

        private Type FindFormTypeByName(string formClassName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t =>
                    t.IsSubclassOf(typeof(Form)) &&
                    t.Name == formClassName);
        }

        private List<Control> GetAllControls(Control root)
        {
            List<Control> controls = new List<Control>();
            foreach (Control ctrl in root.Controls)
            {
                controls.Add(ctrl);
                if (ctrl.HasChildren)
                    controls.AddRange(GetAllControls(ctrl));
            }
            return controls;
        }
        #region load form
        public List<string> GetAllFormNamesInProject()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    typeof(CustomForm).IsAssignableFrom(t) &&
                    t.IsClass &&
                    !t.IsAbstract
                )
                .Select(t => t.Name)
                .Distinct()
                .ToList();
        }
        public async Task<DataTable> CheckMissingFormsAsyncAndAdd(DataTable dbForms)
        {
            var knownFormNames = GetAllFormNamesInProject();

            // Добавляем колонки если их нет
            if (!dbForms.Columns.Contains("Missing"))
                dbForms.Columns.Add("Missing", typeof(bool));
            if (!dbForms.Columns.Contains("Added"))
                dbForms.Columns.Add("Added", typeof(bool));

            var dbFormNames = dbForms.AsEnumerable()
                .Select(r => r["NameForm"].ToString())
                .ToHashSet();

            // Отметка тех, кто в БД, но не в проекте
            foreach (DataRow row in dbForms.Rows)
            {
                string nameForm = row["NameForm"]?.ToString();
                if (!knownFormNames.Contains(nameForm))
                {
                    row["Missing"] = true;
                    row["Added"] = false;
                    row["NameFormRus"] = (row["NameFormRus"]?.ToString() ?? "") + " ⚠ отсутствует";
                }
                else
                {
                    row["Missing"] = false;
                    row["Added"] = false;
                }
            }

            // Те, кто есть в проекте, но не в БД
            foreach (string name in knownFormNames)
            {
                if (!dbFormNames.Contains(name))
                {
                    // Добавляем в БД
                    int newId = await _adminFormDataService.InsertProjectForms(name, name, _user.UserId);

                    // И в DataTable
                    var newRow = dbForms.NewRow();
                    newRow["ProjectFormsID"] = newId;
                    newRow["NameForm"] = name;
                    newRow["NameFormRus"] = "✅ добавлено из проекта";
                    newRow["UserName"] = _user.UserName;
                    newRow["Missing"] = false;
                    newRow["Added"] = true;
                    dbForms.Rows.Add(newRow);
                }
            }

            return dbForms;
        }
        #endregion
        #region load object
        public async Task<DataTable> CheckMissingObjectsAsyncAndAdd(string formClassName, int formId, DataTable dbObjects)
        {
            Type formType = FindFormTypeByName(formClassName);
            if (formType == null)
            {
                MessageBox.Show($"Форма '{formClassName}' не найдена.");
                return dbObjects;
            }

            Form formInstance = CreateFormInstance(formType);
            if (formInstance == null)
            {
                MessageBox.Show($"Форма '{formClassName}' не создана.");
                return dbObjects;
            }

            if (!dbObjects.Columns.Contains("MissingObj"))
                dbObjects.Columns.Add("MissingObj", typeof(bool));
            if (!dbObjects.Columns.Contains("AddedObj"))
                dbObjects.Columns.Add("AddedObj", typeof(bool));

            var knownControls = GetAllControls(formInstance)
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .ToList();

            var dbObjectNames = dbObjects.AsEnumerable()
                .Select(r => r["ObjectName"].ToString())
                .ToHashSet();

            // Отметка тех, кто в БД, но не в форме
            foreach (DataRow row in dbObjects.Rows)
            {
                string objectName = row["ObjectName"]?.ToString();
                bool found = knownControls.Any(c => c.Name == objectName);
                row["MissingObj"] = !found;
                row["AddedObj"] = false;

                if (!found)
                {
                    row["ObjectNameRus"] = (row["ObjectNameRus"]?.ToString() ?? "") + " ⚠ отсутствует";
                }
            }

            // Добавление тех, кто есть на форме, но не в БД
            foreach (var ctrl in knownControls)
            {
                if (!dbObjectNames.Contains(ctrl.Name))
                {
                    string objectType = ctrl.GetType().Name;
                    string baseText = string.IsNullOrWhiteSpace(ctrl.Text) ? "" : ctrl.Text.Trim();
                    string objectNameRus = GetRussianNameForObject(baseText, objectType);

                    int newId = await _adminFormDataService.InsertObjectForm(
                        ctrl.Name,
                        objectNameRus,
                        objectType,
                        _user.UserId,
                        formId
                    );

                    var newRow = dbObjects.NewRow();
                    newRow["ObjectID"] = newId;
                    newRow["ObjectName"] = ctrl.Name;
                    newRow["ObjectNameRus"] = "✅ добавлено из формы";
                    newRow["ObjectType"] = objectType;
                    newRow["UserName"] = _user.UserName;
                    newRow["MissingObj"] = false;
                    newRow["AddedObj"] = true;
                    dbObjects.Rows.Add(newRow);
                }
            }

            return dbObjects;
        }

        #endregion
    }
}
