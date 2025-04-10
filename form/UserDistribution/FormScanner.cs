using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.form.UserDistribution;

namespace SewingProduction.Helpers
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
    }
}
