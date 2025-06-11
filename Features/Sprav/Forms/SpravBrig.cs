using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using static SewingProduction.form.SettingsForm;
using SewingProduction.Helpers;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.form
{
    public partial class SpravBrig : CustomForm, IDataUpdatableForm
    {
        private readonly SpravBrigDataService _spravBrigDataService;
        private readonly ServiceBroker _serviceBroker;
        //чтобы перейти к нужной строке в таблице:
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        bool flagAddDown = false; //если добавили поле в таблицу
        bool flagStartListening = false; //вкл прослушки
        string _tableSQL;
        //Таймер для уведомления о сохранении:
        private Timer timer;
        public SpravBrig(UserClass user, string tableSQL, string rusNameTableSQL) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _spravBrigDataService = new SpravBrigDataService(dbHelper);
            _serviceBroker = new ServiceBroker(this);
            ThemeManager.UpdateTheme(this);
            //Таймер
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            //Имя формы:
            this.Text = rusNameTableSQL;
            _tableSQL = tableSQL;
        }
        public SpravBrig()
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _spravBrigDataService = new SpravBrigDataService(dbHelper);
            _serviceBroker = new ServiceBroker(this);
        }
        private void SpravBrig_Load(object sender, EventArgs e)
        {
            //gridControlSprav.InitializeAccess(_user, this.Name, new List<string> { _tableSQL });
            gridControlSprav.InitializeAccess(_user, this.Name);
            //Загрузка комбобокса:
            comboBoxZeh_Enter(sender, e);
            _serviceBroker.StartBroker();
        }
        #region service broker
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm()
        {
            gridControlSprav_Load(null, EventArgs.Empty);
        }
        #endregion
        //Загрузка грида:
        private void gridControlSprav_Load(object sender, EventArgs e)
        {
            spravList.DataSource = _spravBrigDataService.GetSpBrig();
            //gridView1.Columns[0].Visible = false;
            //gridView1.Columns["Номер"].Width = 100;
            gridView1.OptionsView.ColumnAutoWidth = true;
            if (!flagStartListening)
            {
                _serviceBroker.StartListening("id_brig,idZeh,n_brig,brig", "SpBrig");
                flagStartListening = _serviceBroker.GetFlagStartListening();
            }
        }
        //Загрузка комбобокса список цехов:
        private void comboBoxZeh_Enter(object sender, EventArgs e)
        {
            System.Data.DataTable tableVidProizv = new System.Data.DataTable();
            tableVidProizv = _spravBrigDataService.GetNameZehFromZehList();
            comboBoxZeh.Items.Clear();
            foreach (DataRow row in tableVidProizv.Rows)
            {
                comboBoxZeh.Items.Add(row[0].ToString());
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
                textBoxBrig.Text = "";
                textBoxBrig.ReadOnly = false;
                textBoxNBrig.Text = "";
                textBoxNBrig.ReadOnly = false;
                comboBoxZeh.Text = "";
                comboBoxZeh.Enabled = true;
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
                textBoxKod.Text = gridView.GetFocusedRowCellValue(id_brig).ToString();
                textBoxBrig.Text = gridView.GetFocusedRowCellValue("brig").ToString();
                textBoxBrig.ReadOnly = false;
                textBoxNBrig.Text = gridView.GetFocusedRowCellValue("n_brig").ToString();
                textBoxNBrig.ReadOnly = false;
                comboBoxZeh.Text = gridView.GetFocusedRowCellValue("nameZeh").ToString();
                comboBoxZeh.Enabled = true;
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
                textBoxKod.Text = gridView.GetFocusedRowCellValue("id_brig").ToString();
                textBoxBrig.Text = gridView.GetFocusedRowCellValue("brig").ToString();
                textBoxBrig.ReadOnly = true;
                textBoxNBrig.Text = gridView.GetFocusedRowCellValue("n_brig").ToString();
                textBoxNBrig.ReadOnly = true;
                ////comboBoxZeh.Text = gridView.GetFocusedRowCellValue("Цех") != DBNull.Value ? gridView.GetFocusedRowCellValue("Цех").ToString() : "";
                if (gridView.GetFocusedRowCellValue("nameZeh") != DBNull.Value)
                    comboBoxZeh.Text = gridView.GetFocusedRowCellValue("nameZeh").ToString();
                else comboBoxZeh.SelectedIndex = -1;
                comboBoxZeh.Enabled = false;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Кнопки вверх/вниз:
        private void gridControlSprav_KeyUp(object sender, KeyEventArgs e)
        {
            gridControlSprav_Click(sender, e);
        }
        //Кнопка Отмена:
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }
        //Редактирование в таблице:
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _spravBrigDataService.UpdateRowSpBrig(e.Column.FieldName, e.Value, gridView1.GetDataRow(e.RowHandle)[0]);

        }
        //Удалить запись:
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControlSprav.MainView as GridView;
            currentRowIndex = gridView.FocusedRowHandle;
            string textCol = textBoxBrig.Text;
            string kodCol = textBoxKod.Text;
            string message = "Вы уверены что хотите удалить '" + textCol + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _spravBrigDataService.DeleteSpBrig(kodCol);
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
                        _spravBrigDataService.InsertSpBrig(textBoxBrig.Text, textBoxNBrig.Text, comboBoxZeh.Text);
                        flagAddDown = true;
                        break;
                    }
                case "Редактировать":
                    {
                        _spravBrigDataService.UpdateSpBrig(textBoxBrig.Text, textBoxNBrig.Text, comboBoxZeh.Text, textBoxKod.Text);
                        break;
                    }
                default:
                    break;
            }
            labelSave.Text = "Сохранено!";
            labelSave.Visible = true;
            timer.Start();
            gridControlSprav_Click(sender, e);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSave.Visible = false; // Скрываем лейбл
            timer.Stop(); // Останавливаем таймер
        }
        //ЛейблЛинк Справочник Цехов:
        private void linkLabelVid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravZeh)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravZeh f = new SpravZeh(_user, "ZehList", "Справочник Цехов");
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        //Закрытие формы:
        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serviceBroker.StopListening();
        }

    }
    public class SpravBrigDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public SpravBrigDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public DataTable GetSpBrig()
        {
            string query = $@" SELECT id_brig,n_brig,brig,nameZeh
                            FROM spBrig
                            LEFT JOIN ZehList ON ZehList.idZeh = spBrig.idZeh ";
            return _dbHelper.ExecuteQuery(query);
        }
        public void InsertSpBrig(string brig, string nBrig, string zeh)
        {
            string query = $@"INSERT INTO spBrig (brig,n_brig,idZeh)
                              VALUES (@brig,@nBrig,
                              (SELECT idZeh FROM ZehList WHERE nameZeh = @zeh))";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@brig", brig } , { "@nBrig", nBrig } , { "@zeh", zeh } });
        }
        public void UpdateSpBrig(string brig, string nBrig, string zeh, string kod)
        {
            string query = $@"UPDATE spBrig
                              SET brig = @brig,
                                n_brig = @nBrig,
                                idZeh = (SELECT idZeh FROM ZehList WHERE nameZeh = @zeh)
                              WHERE id_brig =  @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@brig", brig }, { "@nBrig", nBrig }, { "@zeh", zeh }, { "@kod", kod } });
        }
        public void UpdateRowSpBrig(string eFieldName, object eValue, object ekod)
        {
            string query = $@"UPDATE spBrig
                              SET {eFieldName} = @eValue
                              WHERE id_brig =  @ekod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@ekod", ekod } });
        }
        public void DeleteSpBrig(string kodCol)
        {
            string query = $"DELETE FROM spBrig WHERE id_brig = @kodCol";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodCol", kodCol } });
        }
        public DataTable GetNameZehFromZehList()
        {
            string query = $"SELECT nameZeh FROM ZehList";
            return _dbHelper.ExecuteQuery(query);
        }
    }
}