using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.interfaces;
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
    public class OborudBrigDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public OborudBrigDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        #region oborudBrig
        public DataTable GetZehListFromOborudBrig()
        {
            string query = $@"SELECT nameZeh AS 'Цех', nameProizv AS 'Вид производства', address AS 'Адрес' 
                                          FROM ZehList
                                          LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetSpBrigFromOborudBrig(string nameZeh, int countVievZeh)
        {
            string query = $@"SELECT n_brig AS 'Номер', brig AS 'Бригада' FROM spBrig WHERE idZeh ";
            if (countVievZeh < 1)
                query += " IS NOT NULL";
            else
                if (nameZeh == null)
                query += " IS NOT NULL";
            else
                query += " = (SELECT idZeh FROM ZehList WHERE nameZeh = @nameZeh)";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@nameZeh", nameZeh } });
        }
        /// <summary>
        /// Запрос на получение швейного оборудования в зависимости от вида производства
        /// </summary>
        /// <param name="getVid">Вид производства (Швейное/Вязальное...)</param>
        /// <param name="getCount">Кол-во строчек</param>
        /// <param name="getZeh">Выбранный цех</param>
        /// <returns></returns>
        public DataTable GetSpOborudShv(string getVid, int getCount, string getZeh)
        {
            switch (getVid)
            {
                case "Швейный":
                    getVid = " vid_shp ";
                    break;
                case "Вязальный":
                    getVid = " vid_vzp ";
                    break;
                case "Носочный":
                    getVid = " vid_np ";
                    break;
                case "Раскройный":
                    getVid = " vid_rz ";
                    break;
                default:
                    getVid = " 3 ";
                    break;
            }
            string query = $@"SELECT spoborudshv.text_ob AS 'Оборудование', COALESCE(OborudBrig.count, 0) AS 'Кол-во', 
                                     CASE 
                                         WHEN {getVid} = 1 THEN 'Основное'
                                         WHEN {getVid} = 2 THEN 'Дополнительное'
                                         WHEN {getVid} = 3 THEN 'Другое'
                                     END AS 'Вид'
                                  FROM spOborudShv 
                                  LEFT JOIN OborudBrig ON spoborudshv.kod_ob = OborudBrig.kod_ob 
                                  AND OborudBrig.idZeh ";
            if (getCount < 1)
                query += " IS NOT NULL";
            else
                if (getZeh == null)
                query += " IS NOT NULL";
            else
                query += " = (SELECT idZeh FROM ZehList WHERE nameZeh = @getZeh)";
            query += $@" WHERE spoborudshv.kod_ob IS NOT NULL AND COALESCE(spoborudshv.arhiv, 0) = 0 
                        AND {getVid} > 0
                        ORDER BY CASE WHEN COALESCE(OborudBrig.count, 0) > 0 THEN 1 ELSE 0 END DESC, text_ob ASC";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@getZeh", getZeh } });
        }
        /// <summary>
        /// Обновление/добавление оборудования в цеху
        /// </summary>
        /// <param name="getOb">Оборудование</param>
        /// <param name="getZeh">Имя цеха</param>
        /// <param name="getValue">Кол-во</param>
        public void UpdateOborudBrig(string getOb, string getZeh, object getValue)
        {
            string get_kod_ob = $" (SELECT kod_ob FROM spoborudshv WHERE text_ob = '{getOb}') ";
            string get_idZeh = $" (SELECT idZeh FROM ZehList WHERE nameZeh = '{getZeh}') ";

            //Обновляем, если такой записи нет то добавляем:
            string query = $@" UPDATE OborudBrig SET count = @getValue
                                  WHERE kod_ob = {get_kod_ob} AND idZeh = {get_idZeh}
                                    IF @@ROWCOUNT = 0
                                    BEGIN
                                        INSERT INTO OborudBrig (kod_ob, idZeh, count)
                                        VALUES ({get_kod_ob}, {get_idZeh}, @getValue);
                                    END";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@getValue", getValue } });
        }
        #endregion
    }

}
