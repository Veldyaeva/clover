using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.interfaces;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SewingProduction.form
{
    public partial class SpravZeh : CustomForm, IDataUpdatableForm
    {
        private readonly SpravZehDataService _spravZehDataService;
        private readonly ServiceBroker _serviceBroker;
        //чтобы перейти к нужной строке в таблице:
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        bool flagAddDown = false; //если добавили поле в таблицу
        bool flagStartListening = false; //вкл прослушки
        //Таймер для уведомления о сохранении:
        private Timer timer;
        string _tableSQL;

        public SpravZeh(UserClass user, string tableSQL, string rusNameTableSQL) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _spravZehDataService = new SpravZehDataService(dbHelper);
            _serviceBroker = new ServiceBroker(this);
            ThemeManager.UpdateTheme(this);
            //Таймер
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            _tableSQL = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;

        }
        public SpravZeh()
        {
            InitializeComponent();
        }
        private void SpravZeh_Load(object sender, EventArgs e)
        {
            //gridControlSprav.InitializeAccess(_user, this.Name, new List<string> { _tableSQL });
            comboBoxVidProizv_Enter(sender, e);
            _serviceBroker.StartBroker();
        }
        #region service broker
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm(string _table = null)
        {
            gridControlSprav_Load(null, EventArgs.Empty);
        }
        #endregion
        //Загрузка грида:
        private void gridControlSprav_Load(object sender, EventArgs e)
        {
            spravList.DataSource = _spravZehDataService.GetZehList();
            gridViewZeh.Columns[0].Visible = false;
            gridViewZeh.BestFitColumns();
            gridViewZeh.OptionsView.ColumnAutoWidth = true;
            if (!flagStartListening)
            {
                _serviceBroker.StartListening("idZeh,nameZeh,address,idProizv", "ZehList");
                flagStartListening = _serviceBroker.GetFlagStartListening();
            }
        }
        //Загрузка комбобокса виды производства:
        private void comboBoxVidProizv_Enter(object sender, EventArgs e)
        {
            System.Data.DataTable tableVidProizv = new System.Data.DataTable();
            tableVidProizv = _spravZehDataService.GetNameProizvFromSpVidProizv();
            comboBoxVidProizv.Items.Clear();
            foreach (DataRow row in tableVidProizv.Rows)
            {
                comboBoxVidProizv.Items.Add(row[0].ToString());
            }
        }
        //Кнопка добавить:
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Добавить";
                GridView gridView = (GridView)gridControlSprav.MainView;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = false;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                // Код = последнему коду в таблице + 1
                textBoxKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount - 1)[0]) + 1).ToString();
                textBoxName.Text = "";
                textBoxName.ReadOnly = false;
                textBoxAdres.Text = "";
                textBoxAdres.ReadOnly = false;
                comboBoxVidProizv.Text = "";
                comboBoxVidProizv.Enabled = true;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Кнопка редактировать:
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Редактировать";
                // Получаем доступ к GridView
                GridView gridView = gridControlSprav.MainView as GridView;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = true;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                textBoxName.Text = gridView.GetFocusedRowCellValue("Название").ToString();
                textBoxName.ReadOnly = false;
                textBoxAdres.Text = gridView.GetFocusedRowCellValue("Адрес").ToString();
                textBoxAdres.ReadOnly = false;
                comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("Производство").ToString();
                comboBoxVidProizv.Enabled = true;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Клик на грид:
        private void gridControlSprav_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Просмотр";
                // Сохраняем индекс строки
                GridView gridView = gridControlSprav.MainView as GridView;
                currentRowIndex = gridView.FocusedRowHandle;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = false;
                simpleButtonAddOtm.Visible = false;
                simpleButtonAddSave.Visible = false;
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                textBoxName.Text = gridView.GetFocusedRowCellValue("Название").ToString();
                textBoxName.ReadOnly = true;
                textBoxAdres.Text = gridView.GetFocusedRowCellValue("Адрес").ToString();
                textBoxAdres.ReadOnly = true;
                //comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("nameProizv") != DBNull.Value ? gridView.GetFocusedRowCellValue("nameProizv").ToString() : "";
                if (gridView.GetFocusedRowCellValue("Производство") != DBNull.Value)
                    comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("Производство").ToString();
                else comboBoxVidProizv.SelectedIndex = -1;
                comboBoxVidProizv.Enabled = false;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        private void gridControlSprav_KeyUp(object sender, KeyEventArgs e)
        {
            gridControlSprav_Click(sender, e);
        }
        //Кнопка Отмена:
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }
        //Редактирование таблице:
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _spravZehDataService.UpdateRowZehList(e.Column.FieldName, e.Value, gridViewZeh.GetDataRow(e.RowHandle)[0]);
        }
        //Удалить запись:
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            currentRowIndex = gridViewZeh.FocusedRowHandle;
            string textCol = textBoxName.Text;
            string kodCol = textBoxKod.Text;
            string message = "Удаление ЦЕХА приведет к удалению бригад и оборудования в этом цеху, Вы уверены что хотите удалить '" + textCol + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _spravZehDataService.DeleteZehList(kodCol);
                AddTab.TabPages[0].PageVisible = false;
            }
        }
        //Кнопка сохранить:
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            switch (xtraTabPageAdd.Text)
            {
                case "Добавить":
                    {
                        _spravZehDataService.InsertZehList(textBoxName.Text, textBoxAdres.Text, comboBoxVidProizv.Text);
                        flagAddDown = true;
                        break;
                    }
                case "Редактировать":
                    {
                        _spravZehDataService.UpdateZehList(textBoxName.Text, textBoxAdres.Text, comboBoxVidProizv.Text, textBoxKod.Text);
                        break;
                    }
                default:
                    break;
            }
            labelSave.Text = "Сохранено!";
            labelSave.Visible = true;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSave.Visible = false; // Скрываем лейбл
            timer.Stop(); // Останавливаем таймер
        }
        // ЛейблЛинк виды производств:
        private void linkLabelVid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                var form = new SpravForAll("spVidProizv", rusNameTableSQL: "Справочник Вид произв", red: false);
                mainForm.OpenForm(form, sender);
            }
        }
        // Закрытие формы:
        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serviceBroker.StopListening();
        }

    }
    public class SpravZehDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public SpravZehDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public DataTable GetZehList()
        {
            string query = $@"SELECT idZeh,nameZeh AS 'Название',nameProizv AS 'Производство' ,address AS 'Адрес'
                            FROM ZehList
                            LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv";
            return _dbHelper.ExecuteQuery(query);
        }
        public void InsertZehList(string name, string adres, string VidProizv)
        {
            string query = $@"INSERT INTO zehList (nameZeh,address,idProizv)
                                              VALUES (@name,@adres,
                                               (SELECT idProizv FROM spVidProizv WHERE nameProizv = @VidProizv))";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@name", name }, { "@adres", adres }, { "@VidProizv", VidProizv } });
        }
        public void UpdateZehList(string name, string adres, string VidProizv, string kod)
        {
            string query = $@"UPDATE zehList
                                   SET nameZeh = @name,
                                       address = @adres,
                                       idProizv = (SELECT idProizv FROM spVidProizv WHERE nameProizv = @VidProizv)
                                   WHERE idZeh =  @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@name", name }, { "@adres", adres }, { "@VidProizv", VidProizv }, { "@kod", kod } });
        }
        public void UpdateRowZehList(string eFieldName, object eValue, object ekod)
        {
            string query = $@"UPDATE zehList
                              SET {eFieldName} = '{eValue}'
                              WHERE idZeh =  {ekod}";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@ekod", ekod } });
        }
        public void DeleteZehList(string kodCol)
        {
            string query = $@" DELETE FROM OborudBrig WHERE idZeh = @kodCol
                               DELETE FROM spBrig WHERE idZeh = @kodCol
                               DELETE FROM ZehList WHERE idZeh = @kodCol";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodCol", kodCol } });
        }
        public DataTable GetNameProizvFromSpVidProizv()
        {
            string query = $"SELECT nameProizv FROM spVidProizv";
            return _dbHelper.ExecuteQuery(query);
        }
    }
}
