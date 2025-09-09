using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class AdminForm : CustomForm
    {
        private readonly AdminFormDataService _adminFormDataService;
        DatabaseHelper dbHelper = new DatabaseHelper();
        private readonly UserClass _user;
        public AdminForm(UserClass user) : base(user)
        {
            InitializeComponent();
            _adminFormDataService = new AdminFormDataService(dbHelper);
            _user = user;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
        #region Формы
        private async void customGridControlForms_Load(object sender, EventArgs e)
        {
            await Forms_Load();
        }
        private async Task Forms_Load()
        {
            int eFocusedRowHandle = gridViewForms.FocusedRowHandle;
            bindingSourceForms.DataSource = await _adminFormDataService.GetProjectForms();
            gridViewForms.FocusedRowHandle = eFocusedRowHandle;
            repositoryItemLookUpEditCreator.DataSource = await _adminFormDataService.GetUser();
        }
        private void customCheckBoxMyForm_CheckedChanged(object sender, EventArgs e)
        {
            string filter;
            filter = customCheckBoxMyForm.Checked ? $"[UserName] = '{_user.UserName}'" : "";
            gridViewForms.ActiveFilterString = filter;
            Objects_Load();
        }
        private async void customGridControlForms_Click(object sender, EventArgs e)
        {
            Objects_Load();
        }
        private void customGridControlForms_KeyUp(object sender, KeyEventArgs e)
        {
            Objects_Load();
        }
        private void customButtonFormAdd_Click(object sender, EventArgs e)
        {
            gridViewForms.AddNewRow();
        }
        private void gridViewForms_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridViewForms.GridControl.BeginInvoke(new Action(() =>
            {
                if (gridViewForms.IsValidRowHandle(e.RowHandle))
                {
                    gridViewForms.FocusedRowHandle = e.RowHandle;
                    gridViewForms.ShowPopupEditForm();
                }
            }));
        }
        private async void gridViewForms_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = ((DataRowView)e.Row).Row;

            if (row == null) return;

            int id = row["ProjectFormsID"] != DBNull.Value ? Convert.ToInt32(row["ProjectFormsID"]) : 0;

            string nameForm = row["NameForm"]?.ToString() ?? "";
            string nameFormRus = row["NameFormRus"]?.ToString() ?? "";
            int creatorID = row["CreatorID"] != DBNull.Value ? Convert.ToInt32(row["CreatorID"]) : _user.UserId;


            try
            {
                if (id > 0)
                {
                    _adminFormDataService.UpdateProjectForms("NameForm", nameForm, id);
                    _adminFormDataService.UpdateProjectForms("NameFormRus", nameFormRus, id);
                    _adminFormDataService.UpdateProjectForms("CreatorID", creatorID, id);
                }
                else
                {
                    int newId = await _adminFormDataService.InsertProjectForms(nameForm, nameFormRus, _user.UserId);
                    row["ProjectFormsID"] = newId;
                    //await _adminFormDataService.InsertObjectForm(nameForm, nameFormRus, "CustomForm", _user.UserId, newId);
                }
                await Forms_Load();
                await Objects_Load();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("UQ_NameForm"))
                {
                    MessageBox.Show($"Форма с именем \"{nameForm}\" уже существует. Имя должно быть уникальным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                gridViewForms.DeleteRow(gridViewForms.FocusedRowHandle);
            }
        }

        private async void gridViewForms_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            //await Forms_Load();
        }
        private async void customButtonDeleteForm_Click(object sender, EventArgs e)
        {
            int eID = Convert.ToInt32(gridViewForms.GetFocusedRowCellValue(gridViewForms.Columns["ProjectFormsID"]));
            string NameFormRus = gridViewForms.GetFocusedRowCellValue(gridViewForms.Columns["NameFormRus"]).ToString();
            string message = "Удаление ФОРМЫ приведет к удалению объектов, Вы уверены что хотите удалить '" + NameFormRus + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                _adminFormDataService.DeleteProjectForms(eID);
            await Forms_Load();
        }
        #endregion
        #region Объекты
        private async void customGridControlObject_Load(object sender, EventArgs e)
        {
            Objects_Load();
        }
        private async Task Objects_Load()
        {
            try
            {
                if (gridViewForms != null && gridViewForms.RowCount > 0 && gridViewForms.Columns != null && gridViewForms.Columns.Count > 0)
                {
                    int FormID = (int)gridViewForms.GetFocusedRowCellValue(gridViewForms.Columns["ProjectFormsID"]);
                    bindingSourceObject.DataSource = await _adminFormDataService.GetObjectForm(FormID);
                }
            }
            catch
            {
                bindingSourceObject.Clear();
            }
        }
        private void customButton1_Click(object sender, EventArgs e)
        {
            gridViewObject.AddNewRow();
        }
        private void gridViewObject_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridViewObject.GridControl.BeginInvoke(new Action(() =>
            {
                if (gridViewObject.IsValidRowHandle(e.RowHandle))
                {
                    gridViewObject.FocusedRowHandle = e.RowHandle;
                    gridViewObject.ShowPopupEditForm();
                }
            }));
        }
        private async void gridViewObject_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var rowView = e.Row as DataRowView;
            if (rowView == null)
            {
                Console.WriteLine("rowView null");
                return;
            }
            var row = rowView.Row;
            if (row == null)
            {
                Console.WriteLine("row null");
                return;
            }

            int id = row["ObjectID"] != DBNull.Value ? Convert.ToInt32(row["ObjectID"]) : 0;

            int formID = Convert.ToInt32(gridViewForms.GetFocusedRowCellValue("ProjectFormsID"));
            string objectName = row["ObjectName"]?.ToString();
            string objectNameRus = row["ObjectNameRus"]?.ToString();
            string objectType = row["ObjectType"]?.ToString();

            if (id > 0)
            {
                Console.WriteLine($"RowUpdated для объекта (ID = {id})");
                _adminFormDataService.UpdateObjectForm("ObjectName", objectName, id);
                _adminFormDataService.UpdateObjectForm("ObjectNameRus", objectNameRus, id);
                _adminFormDataService.UpdateObjectForm("ObjectType", objectType, id);
            }
            else
            {
                int newId = await _adminFormDataService.InsertObjectForm(objectName, objectNameRus, objectType, _user.UserId, formID);
                row["ObjectID"] = newId;
                Console.WriteLine("Добавлен новый объект с ID = " + newId);
            }

            await Objects_Load();
        }

        private async void customButton2_Click(object sender, EventArgs e)
        {
            object val = gridViewObject.GetFocusedRowCellValue("ObjectID");
            if (val == null || val == DBNull.Value)
            {
                MessageBox.Show("Объект не выбран или не сохранён.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int eID = Convert.ToInt32(val);
            _adminFormDataService.DeleteObjectForm(eID);
            await Objects_Load();
        }
        #endregion

        private async void customButtonLoadObject_Click(object sender, EventArgs e)
        {
            if (gridViewForms.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите форму.");
                return;
            }

            int formID = Convert.ToInt32(gridViewForms.GetFocusedRowCellValue("ProjectFormsID"));
            string formName = gridViewForms.GetFocusedRowCellValue("NameForm")?.ToString();

            System.Data.DataTable dbObjects = await _adminFormDataService.GetObjectForm(formID);

            var scanner = new FormScanner(_adminFormDataService, _user);
            System.Data.DataTable updated = await scanner.CheckMissingObjectsAsyncAndAdd(formName, formID, dbObjects);

            bindingSourceObject.DataSource = updated;
            gridViewObject.RefreshData();
        }

        private async void customButtonLoadForm_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dbForms = await _adminFormDataService.GetProjectForms();
                var scanner = new FormScanner(_adminFormDataService, _user);
                System.Data.DataTable updatedForms = await scanner.CheckMissingFormsAsyncAndAdd(dbForms);
                bindingSourceForms.DataSource = updatedForms;
                gridViewForms.RefreshData();
                MessageBox.Show("Проверка и добавление форм завершено.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке форм: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewForms_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool isMissing = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Missing"));
                bool isAdded = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Added"));

                if (isMissing)
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.ForeColor = Color.DarkRed;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (isAdded)
                {
                    e.Appearance.BackColor = Color.Honeydew;
                    e.Appearance.ForeColor = Color.DarkGreen;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private void gridViewObject_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool isMissing = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "MissingObj"));
                bool isAdded = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "AddedObj"));

                if (isMissing)
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.ForeColor = Color.DarkRed;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (isAdded)
                {
                    e.Appearance.BackColor = Color.Honeydew;
                    e.Appearance.ForeColor = Color.DarkGreen;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        #region menu
        private async void customButtonLoadMenu_Click(object sender, EventArgs e)
        {
            if (gridViewForms.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите форму!");
                return;
            }

            int formID = Convert.ToInt32(gridViewForms.GetFocusedRowCellValue("ProjectFormsID"));
            string formName = gridViewForms.GetFocusedRowCellValue("NameForm")?.ToString();

            if (formName != "SpMainForm")
            {
                MessageBox.Show("Выберите основную форму!");
                return;
            }

            var mainForm = Application.OpenForms.OfType<SpMainForm>().FirstOrDefault();
            if (mainForm == null)
            {
                MessageBox.Show("Главная форма не найдена.");
                return;
            }


            var menuStrip = mainForm.MainMenuStrip;
            if (menuStrip == null)
            {
                MessageBox.Show("MenuStrip не найден.");
                return;
            }

            var scanner = new MenuScanner(_adminFormDataService, _user);
            await scanner.ScanAndInsertMenuAsync(menuStrip, formID);

            await Objects_Load();
            MessageBox.Show("Пункты меню успешно добавлены в базу данных.");
        }
        #endregion

        private void customButtonOpen_Click(object sender, EventArgs e)
        {
            if (gridViewForms.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите форму для открытия.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string formName = gridViewForms.GetFocusedRowCellValue("NameForm")?.ToString();

            if (string.IsNullOrWhiteSpace(formName))
            {
                MessageBox.Show("Не удалось получить имя формы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Находим главную форму
            var mainForm = Application.OpenForms.OfType<SpMainForm>().FirstOrDefault();
            if (mainForm == null)
            {
                MessageBox.Show("Главная форма не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Используем FormScanner для создания формы
            var scanner = new FormScanner(_adminFormDataService, _user);
            var formType = scanner.GetAllFormNamesInProject().FirstOrDefault(name => name == formName);

            if (formType == null)
            {
                MessageBox.Show($"Форма '{formName}' не найдена в проекте.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем форму
            var formInstance = scanner.CreateFormInstance(Type.GetType(formName) ?? AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == formName));

            if (formInstance == null)
            {
                MessageBox.Show($"Не удалось создать экземпляр формы '{formName}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mainForm.OpenForm(formInstance, sender);
        }
    }

    public class AdminFormDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public AdminFormDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<System.Data.DataTable> GetUser()
        {
            string query = $@" SELECT UserName, UserID AS CreatorID FROM Users";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { });
        }
        #region Формы
        public async Task<System.Data.DataTable> GetProjectForms()
        {
            string query = $@"
                SELECT pf.ProjectFormsID, pf.NameForm, pf.NameFormRus, UserName, pf.CreatorID FROM ProjectForms pf
                LEFT JOIN Users u ON pf.CreatorID = u.UserID";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { });
        }
        public async void UpdateProjectForms(string eColumn, object eValue, int eId)
        {
            string query = $@"
                UPDATE ProjectForms SET {eColumn} = @eValue
                WHERE ProjectFormsID = @eId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@eId", eId } });
        }
        public async Task<int> InsertProjectForms(string eNameForm, string eNameFormRus, int CreatorID)
        {
            string query = $@"
                INSERT INTO ProjectForms (NameForm,NameFormRus,CreatorID)
                OUTPUT INSERTED.ProjectFormsID
                VALUES (@eNameForm,@eNameFormRus,@CreatorID)";
            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@eNameForm", eNameForm },
                { "@eNameFormRus", eNameFormRus },
                { "@CreatorID", CreatorID }
            });
            await InsertObjectForm(eNameForm, eNameFormRus, "CustomForm", CreatorID, result);
            return Convert.ToInt32(result);
        }
        public async void DeleteProjectForms(int eId)
        {
            string query = $@"
                DELETE ProjectForms 
                WHERE ProjectFormsID = @eId;
                DELETE ObjectForm 
                WHERE FormID = @eId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eId", eId } });
        }

        #endregion
        #region Объекты
        public async Task<System.Data.DataTable> GetObjectForm(int FormID)
        {
            string query = $@"
                SELECT obf.ObjectID,obf.ObjectName,obf.ObjectNameRus,obf.ObjectType,u.UserName FROM objectForm obf
                LEFT JOIN ProjectForms pf ON pf.ProjectFormsID = obf.FormID
                LEFT JOIN Users u ON pf.CreatorID = u.UserID
                WHERE FormID = @FormID";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@FormID", FormID } });
        }
        public async void UpdateObjectForm(string eColumn, object eValue, int eId)
        {
            string query = $@"
                UPDATE objectForm SET {eColumn} = @eValue
                WHERE ObjectID = @eId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@eId", eId } });
        }
        public async Task<int> InsertObjectForm(string objectName, string objectNameRus, string objectType, int creatorID, int formID)
        {
            string query = @"
            INSERT INTO ObjectForm (ObjectName, ObjectNameRus, ObjectType, CreatorID, FormID)
            OUTPUT INSERTED.ObjectID
            VALUES (@ObjectName, @ObjectNameRus, @ObjectType, @CreatorID, @FormID)";

            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@ObjectName", objectName },
                { "@ObjectNameRus", objectNameRus },
                { "@ObjectType", objectType },
                { "@CreatorID", creatorID },
                { "@FormID", formID }
            });

            SetPravaForObject(result);
            return Convert.ToInt32(result);
        }
        public async void DeleteObjectForm(int eId)
        {
            string query = $@"
                DELETE ObjectForm 
                WHERE ObjectID = @eId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eId", eId } });
        }
        private async void SetPravaForObject(int newId)
        {
            AllRoleDataService allRoleDataService = new AllRoleDataService(_dbHelper);
            await allRoleDataService.UpdateRoleObjectMode(1, newId, 2);
            Console.WriteLine("Назначены права администратору, объект:" + newId.ToString());
        }
        #endregion
        #region menu
        public async Task<bool> ObjectExists(int formId, string objectName)
        {
            string query = @"
                SELECT COUNT(*) FROM ObjectForm
                WHERE FormID = @FormID AND ObjectName = @ObjectName";

            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@FormID", formId },
                { "@ObjectName", objectName }
            });

            return Convert.ToInt32(result) > 0;
        }
        #endregion
    }

}
