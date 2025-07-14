
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using SewingProduction.Core.interfaces;
using System.Collections.Generic;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using SewingProduction.Helpers;
using SewingProduction.Features.UserDistribution.Helpers;


namespace SewingProduction.form
{
    public partial class SpravOborud : CustomForm, IDataUpdatableForm
    {
        private readonly SpravOborudDataService _spravOborudDataService;
        private readonly ServiceBroker _serviceBroker;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        //если добавили поле в таблицу:
        bool flagAddDown = false;
        public SpravOborud(UserClass user) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _spravOborudDataService = new SpravOborudDataService(dbHelper);
            _serviceBroker = new ServiceBroker(this);
            ThemeManager.UpdateTheme(this);
        }

        private void SpravOborud_Load(object sender, EventArgs e)
        {
            _serviceBroker.StartBroker();
            label4.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label7.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label21.Text = "Спец. оборудование \n для оказания услуг";
            label22.Text = "Спец. оборудование \n для оказания услуг";
        }

        #region service broker
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm(string _table)
        {
            LoadData();
        }
        #endregion

        // Загрузка / обновление данных:
        private void LoadData()
        {
            oborudList.DataSource = _spravOborudDataService.GetSpOborudShv(checkEditArhiv.Checked);
            GridView gridView = oborudGrid.MainView as GridView;
            //Запрет на редактирование
            gridView.OptionsBehavior.Editable = false;
            // Если он открыт
            if (gridView != null)
            {
                // Создаем экземпляр CheckEdit
                RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit
                {
                    ValueChecked = 1,   // Значение для отмеченной галочки
                    ValueUnchecked = 0   // Значение для неотмеченной галочки
                };
                // Назначаем его столбцам
                gridView.Columns["show_for_plan"].ColumnEdit = checkEdit;
                gridView.Columns["spec_ob"].ColumnEdit = checkEdit;
                gridView.Columns["arhiv"].ColumnEdit = checkEdit;
                //gridView.Columns["pokaz"].OptionsColumn.AllowEdit = false; // Запрещаем редактирование

                gridView.OptionsView.ShowGroupPanel = false; // Панель группировки отображается
                gridView.GroupPanelText = ""; // Текст
                gridView.OptionsFind.AlwaysVisible = true; // Всегда показывать панель поиска
            }
        }

        // Загрузка таблицы:
        private void oborudGrid_Load(object sender, EventArgs e)
        {
            LoadData();
            _serviceBroker.StartListening("kod_ob,text_ob,text_ob_s,ko_ob_all,spec_ob,nastav,arhiv,no_spec,pokaz_sp,id_class,show_for_plan,vid_shp,vid_vzp,vid_np,vid_rz", "spoborudshv");
        }

        

        private bool GetCheckBoxValue(string columnName, GridView gridViewGet)
        {
            // Получаем текущее выделенное значение в указанной ячейке столбца
            object value = gridViewGet.GetFocusedRowCellValue(columnName);
            // Проверяем, если значение равно 1 (отмеченная галочка)
            return value != null && value.Equals(1); // Вернуть true, если галочка отмечена, иначе false
        }

        // Кнопка Редактировать
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
            AddTab.TabPages[1].PageVisible = true;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Получаем текущую выделенную строку в текстбокси и др
            textBoxRedKod.Text = gridView.GetFocusedRowCellValue("kod_ob").ToString();
            textBoxRedName.Text = gridView.GetFocusedRowCellValue("text_ob").ToString().Trim();
            textBoxRedSokrName.Text = gridView.GetFocusedRowCellValue("text_ob_s").ToString().Trim();
            // Заполняем комбобоксы:
            _spravOborudDataService.GetOborudShvOb(comboBoxRedGrup);
            _spravOborudDataService.GetSpOborudMachine(comboBoxRedVidm);
            _spravOborudDataService.GetMatrix_class(comboBoxRedClass);
            comboBoxRedGrup.Text = gridView.GetFocusedRowCellValue("text_ob_tip") != DBNull.Value ? gridView.GetFocusedRowCellValue("text_ob_tip").ToString() : "";
            comboBoxRedVidm.Text = gridView.GetFocusedRowCellValue("vidm") != DBNull.Value ? gridView.GetFocusedRowCellValue("vidm").ToString() : "";
            comboBoxRedClass.Text = gridView.GetFocusedRowCellValue("idClass") != DBNull.Value ? gridView.GetFocusedRowCellValue("idClass").ToString() : "";
            //comboBoxRedNastav.Text = gridView.GetFocusedRowCellValue("nastav") != DBNull.Value ? gridView.GetFocusedRowCellValue("nastav").ToString() : "";
            if (gridView.GetFocusedRowCellValue("nastav") != DBNull.Value)
                comboBoxRedNastav.Text = gridView.GetFocusedRowCellValue("nastav").ToString();
            else comboBoxRedNastav.SelectedIndex = -1;
            // comboBox group for proizv:
            comboBoxRedShp.Text = gridView.GetFocusedRowCellValue("vid_shp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_shp").ToString() : "нет";
            comboBoxRedVzp.Text = gridView.GetFocusedRowCellValue("vid_vzp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_vzp").ToString() : "нет";
            comboBoxRedNp.Text = gridView.GetFocusedRowCellValue("vid_np") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_np").ToString() : "нет";
            comboBoxRedRz.Text = gridView.GetFocusedRowCellValue("vid_rz") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_rz").ToString() : "нет";
            //checkBox:
            checkBoxRedShow.Checked = GetCheckBoxValue("show_for_plan", gridView);
            checkBoxRedSpec.Checked = GetCheckBoxValue("spec_ob", gridView);
            checkBoxRedArhiv.Checked = GetCheckBoxValue("arhiv", gridView);
        }

        // Кнопка Добавить
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = true;
            AddTab.TabPages[1].PageVisible = false;
            textBoxAddKod.Text = (_spravOborudDataService.GetLastId() + 1).ToString();
            textBoxAddName.Text = "";
            textBoxAddSokrName.Text = "";
            // Заполняем комбобоксы:
            _spravOborudDataService.GetOborudShvOb(comboBoxAddGrup);
            _spravOborudDataService.GetSpOborudMachine(comboBoxAddVidm);
            _spravOborudDataService.GetMatrix_class(comboBoxAddClass);
            comboBoxAddGrup.Text = "прочее";
            comboBoxAddVidm.Text = "Другое";
            comboBoxAddClass.Text = "";
            comboBoxAddNastav.Text = "";
            //Вид произв:
            comboBoxAddShp.Text = "нет";
            comboBoxAddVzp.Text = "нет";
            comboBoxAddNp.Text = "нет";
            comboBoxAddRz.Text = "нет";
            //скрыть и тд:
            checkBoxAddShow.Checked = false;
            checkBoxAddSpec.Checked = false;
            checkBoxAddArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Редактировать
        private void simpleButtonRedOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[1].PageVisible = false;
            textBoxRedKod.Text = "";
            textBoxRedName.Text = "";
            textBoxRedSokrName.Text = "";
            comboBoxRedGrup.Text = "";
            comboBoxRedVidm.Text = "";
            comboBoxRedClass.Text = "";
            comboBoxRedNastav.Text = "";
            checkBoxRedShow.Checked = false;
            checkBoxRedSpec.Checked = false;
            checkBoxRedArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Добавить
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
        }
        // Проверка заполения полей
        string proverkaZap(TextBox proverkaKod, TextBox proverkaName, TextBox proverkaSokrName, ComboBox proverkaGrup,
                        ComboBox proverkaShp, ComboBox proverkaVzp, ComboBox proverkaNp, ComboBox proverkaRz)
        {
            if (proverkaName.Text == "")
                return "Заполните поле 'Вид оборудования'!";
            if (proverkaSokrName.Text == "")
                return "Заполните поле 'Cокращенное наименование'!";
            if (proverkaGrup.Text == "")
                return "Заполните поле 'Группа оборудования'!";
            if (proverkaShp.Text == "нет" && proverkaVzp.Text == "нет" && proverkaNp.Text == "нет" && proverkaRz.Text == "нет")
                return "Выберите вид производства!";
            if (proverkaKod.Text == "0" || proverkaKod.Text == "")
                return "Ошибка, связанная с кодом записи, перезапустите программу и попробуйте снова.";
            return "OK";
        }
        // Кнопка Сохранить на вкладке Редактировать
        private void simpleButtonRedSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxRedKod, textBoxRedName, textBoxRedSokrName, comboBoxRedGrup,
                comboBoxRedShp, comboBoxRedVzp, comboBoxRedNp, comboBoxRedRz);
            if (proverka == "OK")
            {
                currentRowIndex = gridView1.FocusedRowHandle;
                _spravOborudDataService.UpdateSpOborudShv(textBoxRedName.Text, textBoxRedSokrName.Text, comboBoxRedClass.Text, 
                    comboBoxRedShp.SelectedIndex, comboBoxRedVzp.SelectedIndex, comboBoxRedNp.SelectedIndex, comboBoxRedRz.SelectedIndex,
                    comboBoxRedNastav.SelectedIndex, checkBoxRedShow.Checked, checkBoxRedSpec.Checked, checkBoxRedArhiv.Checked,
                    textBoxRedKod.Text, comboBoxRedGrup.Text, comboBoxRedVidm.Text);
                AddTab.TabPages[1].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Сохранить на вкладке Добавить
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxAddName, textBoxAddName, textBoxAddSokrName, comboBoxAddGrup,
                comboBoxAddShp, comboBoxAddVzp, comboBoxAddNp, comboBoxAddRz);
            if (proverka == "OK")
            {
                _spravOborudDataService.InsertSpOborudShv(textBoxAddName.Text, textBoxAddSokrName.Text, comboBoxAddClass.Text,
                    comboBoxAddShp.SelectedIndex, comboBoxAddVzp.SelectedIndex, comboBoxAddNp.SelectedIndex, comboBoxAddRz.SelectedIndex,
                    comboBoxAddNastav.SelectedIndex, checkBoxAddShow.Checked, checkBoxAddSpec.Checked, checkBoxAddArhiv.Checked,
                    textBoxAddKod.Text, comboBoxAddGrup.Text, comboBoxAddVidm.Text,
                    comboBoxAddClass.Text, comboBoxAddGrup.Text, comboBoxAddVidm.Text, textBoxAddKod.Text);
                // Флаг для перехода вниз
                flagAddDown = true;
                // Закрыть вкладку
                AddTab.TabPages[0].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Архив
        private void simpleButtonArhiv_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Получаем данные из ячейки
            string textObArh = gridView.GetFocusedRowCellValue("text_ob").ToString();
            int kodObArh = Convert.ToInt32(gridView.GetFocusedRowCellValue("kod_ob"));
            string message = "Вы уверены что хотите занести '" + textObArh + "' в архив?";
            var result = MessageBox.Show(message, "В архив?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _spravOborudDataService.SetArhiv(kodObArh);
            }
        }
        private void oborudGrid_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Отменяем если редакитруем:
            simpleButtonRedOtm_Click(sender, e);
        }

        private void checkEditArhiv_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
            GridView gridView = oborudGrid.MainView as GridView;
            //gridView.Columns["arhiv"].Visible = !gridView.Columns["arhiv"].Visible;
            //перенос столбца архив в конец:
            gridView.Columns["arhiv"].VisibleIndex = -(gridView.Columns["arhiv"].VisibleIndex - (gridView.Columns.Count - 2));
        }
        private void SpravOborud_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serviceBroker.StopBroker();
        }

    }
    public class SpravOborudDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public SpravOborudDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public void SetComboAllTableItems(System.Windows.Forms.ComboBox comboBox, string query)
        {
            System.Data.DataTable tableList = new System.Data.DataTable();
            tableList = _dbHelper.ExecuteQuery(query);
            comboBox.Items.Clear();
            //Загрузка в комбобокс:
            foreach (DataRow row in tableList.Rows)
            {
                comboBox.Items.Add(row[0].ToString());
            }
        }
        public System.Data.DataTable GetSpOborudShv(bool arhiv)
        {
            string query = $@"SELECT kod_ob ,spOborudShv.text_ob,text_ob_s,
                                            oborud_shv_ob.text_ob AS text_ob_tip,spec_ob,
                                            spOborudMachine.name AS vidm,pokaz_sp,matrix_class.caption AS idClass,show_for_plan,
                                            (CASE arhiv WHEN 1 THEN 1 ELSE 0 END) AS arhiv,
                                            (CASE nastav WHEN 1 THEN 'оверлок' WHEN 2 THEN 'плоскошовка' WHEN 3 THEN 'универсалка' ELSE NULL END) AS nastav,
                                            (CASE vid_shp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_shp,
                                            (CASE vid_vzp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_vzp,
                                            (CASE vid_np WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_np,
                                            (CASE vid_rz WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_rz
                                         FROM spOborudShv 
                                             LEFT JOIN oborud_shv_ob ON oborud_shv_ob.ko_ob_all = spOborudShv.ko_ob_all 
                                             LEFT JOIN spOborudMachine ON spOborudMachine.miniName = spOborudShv.no_spec 
                                             LEFT JOIN matrix_class ON matrix_class.id_class = spOborudShv.id_class
                                            {(arhiv ? "" : "WHERE arhiv IS NULL OR arhiv = 0")} ";
            return _dbHelper.ExecuteQuery(query);
        }
        public void GetOborudShvOb(ComboBox comboBox)
        {
            string query = $"SELECT text_ob FROM oborud_shv_ob ORDER by ko_ob_all ASC";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetSpOborudMachine(ComboBox comboBox)
        {
            string query = $"SELECT name FROM spOborudMachine";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetMatrix_class(ComboBox comboBox)
        {
            string query = $"SELECT caption FROM matrix_class";
            SetComboAllTableItems(comboBox, query);
        }
        public void UpdateSpOborudShv(string SOStext_ob, string text_ob_s, string id_class,
                    object vid_shp, object vid_vzp, object vid_np, object vid_rz,
                    object nastav, bool show_for_plan, bool spec_ob, bool arhiv,
                    string kod_ob, string OSOtext_ob, string name)
        {
            string query = $"UPDATE spOborudShv " +
                            $"SET text_ob = @SOStext_ob, " +
                                             $"text_ob_s = @text_ob_s, " +
                                             $"spOborudShv.ko_ob_all = oborud_shv_ob.ko_ob_all, " +
                                             $"no_spec = spOborudMachine.miniName, " +
                                             $"spOborudShv.id_class = (SELECT id_class FROM matrix_class WHERE caption = @id_class)," +
                                             $"vid_shp = @vid_shp, " +
                                             $"vid_vzp = @vid_vzp, " +
                                             $"vid_np = @vid_np, " +
                                             $"vid_rz = @vid_rz, " +
                                             $"nastav = @nastav, " +
                                             $"show_for_plan = @show_for_plan, " +
                                             $"spec_ob = @spec_ob, " +
                                             $"arhiv = @arhiv " +
                                         $" FROM spOborudShv,spOborudMachine,oborud_shv_ob " +
                                         $" WHERE kod_ob = @kod_ob" +
                                         $" AND oborud_shv_ob.text_ob = @OSOtext_ob " +
                                         $" AND spOborudMachine.name = @name ";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@SOStext_ob", SOStext_ob } , { "@text_ob_s", text_ob_s } , { "@id_class", id_class } ,
                                                    { "@vid_shp", vid_shp }, { "@vid_vzp", vid_vzp } ,{ "@vid_np", vid_np } ,{ "@vid_rz", vid_rz } ,
                                                    { "@nastav", nastav } ,{ "@show_for_plan", show_for_plan }, { "@spec_ob", spec_ob } ,{ "@arhiv", arhiv } ,
                                                    { "@kod_ob", kod_ob } ,{ "@OSOtext_ob", OSOtext_ob } ,{ "@name", name } });
        }
        public void InsertSpOborudShv(string SOStext_ob, string text_ob_s, string id_class,
                    object vid_shp, object vid_vzp, object vid_np, object vid_rz,
                    object nastav, bool show_for_plan, bool spec_ob, bool arhiv,
                    string kod_ob, string OSOtext_ob, string name,
                    string AddClass, string AddGrup, string AddVidm, string AddKod)
        {
            string klass;
            if (AddClass == "")
                klass = "NULL";
            else
                klass = "(SELECT id_class FROM matrix_class WHERE caption = @AddClass)";

            string query = $"INSERT INTO spOborudShv (text_ob, text_ob_s, ko_ob_all, no_spec, id_class, vid_shp," +
                                        $" vid_vzp, vid_np, vid_rz, nastav, show_for_plan, spec_ob,arhiv) " +
                                    $"VALUES (" +
                                        $"@SOStext_ob, " +
                                        $"@text_ob_s, " +
                                        $"(SELECT ko_ob_all FROM oborud_shv_ob WHERE text_ob = @AddGrup), " +
                                        $"(SELECT miniName FROM spOborudMachine WHERE name = @AddVidm), " +
                                        $"{klass} ," +
                                        $"@vid_shp," +
                                        $"@vid_vzp," +
                                        $"@vid_np," +
                                        $"@vid_rz," +
                                        $"@nastav, " +
                                        $"@show_for_plan, " +
                                        $"@spec_ob, " +
                                        $"@arhiv); " +
                                        $"EXEC dbo.add_columns_plan_proz_mg @obor_n = {AddKod};";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@SOStext_ob", SOStext_ob } , { "@text_ob_s", text_ob_s } , { "@id_class", id_class } ,
                                                    { "@vid_shp", vid_shp }, { "@vid_vzp", vid_vzp } ,{ "@vid_np", vid_np } ,{ "@vid_rz", vid_rz } ,
                                                    { "@nastav", nastav } ,{ "@show_for_plan", show_for_plan }, { "@spec_ob", spec_ob } ,{ "@arhiv", arhiv } ,
                                                    { "@kod_ob", kod_ob } ,{ "@OSOtext_ob", OSOtext_ob } ,{ "@name", name } ,
                                                    { "@AddClass", AddClass }  ,{ "@AddGrup", AddGrup }  ,{ "@AddVidm", AddVidm } });
        }
        public int GetLastId()
        {
            string query = "SELECT TOP 1 kod_ob FROM spOborudShv ORDER BY kod_ob DESC";
            System.Data.DataTable tableList = _dbHelper.ExecuteQuery(query);
            return (int)tableList.Rows[0]["kod_ob"];
        }
        public void SetArhiv(int kodObArh)
        {
            string query = $"UPDATE spOborudShv SET arhiv = 1 WHERE kod_ob = @kodObArh";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodObArh", kodObArh } });
        }
    }

}