using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.interfaces;
using SewingProduction.Features.Sprav.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;

namespace SewingProduction.form
{
    /// <summary>
    /// Форма оборудования в бригадах (цехах)
    /// </summary>
    public partial class OborudBrig : CustomForm, IDataUpdatableForm
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        //string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        private readonly OborudBrigDataService _oborudBrigDataService;
        private readonly ServiceBroker _serviceBroker;
        private SqlDependency sqlDependency;
        private SqlConnection connection;
        bool flagStartListening = false; //вкл прослушки
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        public OborudBrig(UserClass user) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _oborudBrigDataService = new OborudBrigDataService(dbHelper);
            _serviceBroker = new ServiceBroker(this);
            ThemeManager.UpdateTheme(this);
        }
        #region service broker
        private void OborudBrig_Load_1(object sender, EventArgs e)
        {
            _serviceBroker.StartBroker();
        }
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm(string _table)
        {
            gridOborud_Load(null, EventArgs.Empty);
        }
        #endregion

        // Обнолвение таблиц при активации вкладки:
        private void OborudBrig_Activated(object sender, EventArgs e)
        {
            gridZeh_Load(sender, e);
            gridZeh_Click(sender, e);
        }
        // Клик на бригаду
        private void gridBrig_Click(object sender, EventArgs e)
        {
            gridOborud_Load(sender, e);
        }
        // Выбор цеха в таблице цехов:
        private void gridZeh_Click(object sender, EventArgs e)
        {
            gridViewZeh_FocusedRowChanged(sender, null);
            //gridBrig_Load(sender, e);
            //gridOborud_Load(sender, e);
        }
        private void gridViewZeh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridBrig_Load(sender, e);
            gridOborud_Load(sender, e);
        }
        // Управление стерлочками
        private void gridZeh_KeyUp(object sender, KeyEventArgs e)
        {
            gridZeh_Click(sender, e);
        }
        /// <summary>
        /// Таблица цехов:
        /// </summary>
        private void gridZeh_Load(object sender, EventArgs e)
        {
            bindingZeh.DataSource = _oborudBrigDataService.GetZehListFromOborudBrig();
            GridView gridView = gridZeh.MainView as GridView;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.BestFitColumns();

            gridView.Columns["Цех"].OptionsColumn.ReadOnly = true;
            gridView.Columns["Вид производства"].OptionsColumn.ReadOnly = true;
            gridView.Columns["Адрес"].OptionsColumn.ReadOnly = true;
        }
        /// <summary>
        /// Таблица бригад:
        /// </summary>
        private void gridBrig_Load(object sender, EventArgs e)
        {
            if (gridViewZeh != null && gridViewZeh.RowCount > 0 && gridViewZeh.Columns != null && gridViewZeh.Columns.Count > 0)
            {
                string nameZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();
                int countVievZeh = gridViewZeh.Columns.Count;
                bindingBrig.DataSource = _oborudBrigDataService.GetSpBrigFromOborudBrig(nameZeh, countVievZeh);
                // Получаем доступ к GridView
                GridView gridView = gridBrig.MainView as GridView;
                //Запрет на редактирование
                gridView.OptionsBehavior.Editable = false;
                gridView.BestFitColumns();
            }
        }

        // Таблица оборудования в цехе:
        private void gridOborud_Load(object sender, EventArgs e)
        {
            //если таблица с цехами не пустая
            if (gridViewZeh != null && gridViewZeh.RowCount > 0 && gridViewZeh.Columns != null && gridViewZeh.Columns.Count > 0)
            {
                GridView gridView = gridOborud.MainView as GridView;
                string currentOb = "";
                if (gridView.FocusedRowHandle >= 0)
                {
                    currentOb = gridView.GetRowCellValue(gridView.FocusedRowHandle, "Оборудование").ToString();
                    //currentRowIndex = gridView.FocusedRowHandle;
                }
                GridView gridViewZeh = gridZeh.MainView as GridView;
                string getVid = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Вид производства"]).ToString();
                string getZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();
                int getCount = gridViewZeh.Columns.Count;
                bindingOborud.DataSource = _oborudBrigDataService.GetSpOborudShv(getVid, getCount, getZeh);
                gridView.BestFitColumns();

                gridView.Columns["Оборудование"].OptionsColumn.ReadOnly = true;
                gridView.Columns["Кол-во"].OptionsColumn.ReadOnly = false;
                gridView.Columns["Вид"].OptionsColumn.ReadOnly = true;

                int rowHandle = gridView.LocateByValue("Оборудование", currentOb);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gridView.FocusedRowHandle = rowHandle;
                    gridView.MakeRowVisible(rowHandle);
                }
                if (!flagStartListening)
                {
                    _serviceBroker.StartListening("idOB, idZeh, kod_ob, count", "OborudBrig");
                    flagStartListening = _serviceBroker.GetFlagStartListening();
                }
            }
        }
        //Редактирование кол-ва оборудования
        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Получаем код оборудования по названию из таблицы оборудований:
            GridView gridViewOborud = gridOborud.MainView as GridView;
            string getOb = gridViewOborud.GetFocusedRowCellValue(gridViewOborud.Columns["Оборудование"]).ToString();

            //Получаем код цеха по названию из таблицы цехов:
            GridView gridViewZeh = gridZeh.MainView as GridView;
            string getZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();

            //Получаем отредактированное значение:
            object getValue = e.Value;

            _oborudBrigDataService.UpdateOborudBrig(getOb, getZeh, getValue);
        }
        /// <summary>
        /// Открытие справочника Цехов
        /// </summary>
        private void labelZeh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new SpravZeh(_user, "ZehList", "Справочник Цехов"), "цехаToolStripMenuItem");
            }
        }

        /// <summary>
        /// Открытие справочника Бригад
        /// </summary>
        private void linkLabelBrig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new SpravBrig(_user, "spBrig", "Справочник Бригад"), "бригадыToolStripMenuItem");
            }
        }

    }

}
