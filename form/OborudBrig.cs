using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.VisualEffects;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace SewingProduction.form
{
    public partial class OborudBrig : CustomForm
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;

        private SqlDependency sqlDependency;
        private SqlConnection connection;
        bool flagStartListening = false; //вкл прослушки
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        public OborudBrig()
        {
            InitializeComponent();
        }
        private void OborudBrig_Load_1(object sender, EventArgs e)
        {

        }
        //Обнолвение таблиц при активации вкладки:
        private void OborudBrig_Activated(object sender, EventArgs e)
        {
            gridZeh_Load(sender, e);
            gridBrig_Load(sender, e);
            gridOborud_Load(sender, e);
        }

        private void gridBrig_Click(object sender, EventArgs e)
        {
            //gridOborud_Load(sender, e);
        }
        //Выбор цеха в таблице цехов:
        private void gridZeh_Click(object sender, EventArgs e)
        {
            gridBrig_Load(sender, e);
            gridOborud_Load(sender, e);
        }
        private void gridZeh_KeyUp(object sender, KeyEventArgs e)
        {
            gridZeh_Click(sender, e);
        }
        //Таблица цехов:
        private void gridZeh_Load(object sender, EventArgs e)
        {
            try
            {
                using (var connectionSELECT = new SqlConnection(connectionString))
                {
                    string queryList = $@"SELECT nameZeh AS 'Цех', nameProizv AS 'Вид производства', address AS 'Адрес' 
                                          FROM ZehList
                                          LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(queryList, connectionSELECT);
                    System.Data.DataTable tableList = new System.Data.DataTable();
                    dataAdapter.Fill(tableList);
                    bindingZeh.DataSource = tableList;
                    GridView gridView = gridZeh.MainView as GridView;
                    gridView.OptionsBehavior.Editable = false;
                    gridView.BestFitColumns();
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"ZehList SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }

        }

        //Таблица бригад:
        private void gridBrig_Load(object sender, EventArgs e)
        {
            try
            {
                using (var connectionSELECT = new SqlConnection(connectionString))
                {
                    GridView gridViewZeh = gridZeh.MainView as GridView;
                    string queryList = $@"SELECT n_brig AS 'Номер', brig AS 'Бригада' FROM spBrig WHERE idZeh ";
                    if (gridViewZeh.Columns.Count < 1)
                        queryList += " IS NOT NULL";
                    else
                        if (gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString() == null)
                        queryList += " IS NOT NULL";
                    else
                        queryList += " = (SELECT idZeh FROM ZehList WHERE nameZeh = '" +
                        gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString() + "')";
                    //используя подключение отправляем запрос БД:
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(queryList, connectionSELECT);
                    //Создаем в памяти таблицу:
                    System.Data.DataTable tableList = new System.Data.DataTable();
                    //Добавляем ответ сервера в таблицу:
                    dataAdapter.Fill(tableList);
                    //Закгрузка в таблицу грида:
                    bindingBrig.DataSource = tableList;
                    // Получаем доступ к GridView
                    GridView gridView = gridBrig.MainView as GridView;
                    //Запрет на редактирование
                    gridView.OptionsBehavior.Editable = false;
                    gridView.BestFitColumns();
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"Brig SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
        }


        //Таблица оборудования в цехе:
        private void gridOborud_Load(object sender, EventArgs e)
        {
            try
            {
                GridView gridView = gridOborud.MainView as GridView;
                string currentOb = "";
                if (gridView.FocusedRowHandle >= 0)
                {
                    currentOb = gridView.GetRowCellValue(gridView.FocusedRowHandle, "Оборудование").ToString();
                    //currentRowIndex = gridView.FocusedRowHandle;
                }
                using (var connectionSELECT = new SqlConnection(connectionString))
                {
                    GridView gridViewZeh = gridZeh.MainView as GridView;
                    string getVid = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Вид производства"]).ToString();
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
                    string queryList = $@"SELECT spoborudshv.text_ob AS 'Оборудование', COALESCE(OborudBrig.count, 0) AS 'Кол-во', 
                                                CASE 
                                                    WHEN {getVid} = 1 THEN 'Основное'
                                                    WHEN {getVid} = 2 THEN 'Дополнительное'
                                                    WHEN {getVid} = 3 THEN 'Другое'
                                                END AS 'Вид'
                                          FROM spoborudshv 
                                          LEFT JOIN OborudBrig ON spoborudshv.kod_ob = OborudBrig.kod_ob 
                                          AND OborudBrig.idZeh ";
                    if (gridViewZeh.Columns.Count < 1)
                        queryList += " IS NOT NULL";
                    else
                        if (gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString() == null)
                        queryList += " IS NOT NULL";
                    else
                        queryList += " = (SELECT idZeh FROM ZehList WHERE nameZeh = '" +
                        gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString() + "')";
                    queryList += " WHERE spoborudshv.kod_ob IS NOT NULL AND COALESCE(spoborudshv.arhiv, 0) = 0 ";
                    queryList += $" AND {getVid} > 0";
                    queryList += " ORDER BY CASE WHEN COALESCE(OborudBrig.count, 0) > 0 THEN 1 ELSE 0 END DESC, text_ob ASC";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(queryList, connectionSELECT);
                    System.Data.DataTable tableList = new System.Data.DataTable();
                    dataAdapter.Fill(tableList);
                    bindingOborud.DataSource = tableList;
                    gridView.BestFitColumns();
                    int rowHandle = gridView.LocateByValue("Оборудование", currentOb);
                    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    {
                        gridView.FocusedRowHandle = rowHandle;
                        gridView.MakeRowVisible(rowHandle);
                    }
                }
                if (!flagStartListening)
                {
                    // Запуск отслеживания изменений для соединения с базой данных
                    SqlDependency.Start(connectionString);
                    // Начинаем прослушивание
                    StartListening();
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"spoborudshv SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try 
            { 
                using (SqlConnection connectionCell = new SqlConnection(connectionString))
                {
                    //Получаем код оборудования по названию из таблицы оборудований:
                    GridView gridViewOborud = gridOborud.MainView as GridView;
                    string get_kod_ob = $@" (SELECT kod_ob FROM spoborudshv WHERE text_ob =
                                      '{gridViewOborud.GetFocusedRowCellValue(gridViewOborud.Columns["Оборудование"]).ToString()}') ";
                    //Получаем код цеха по названию из таблицы цехов:
                    GridView gridViewZeh = gridZeh.MainView as GridView;
                    string get_idZeh = $@" (SELECT idZeh FROM ZehList WHERE nameZeh = 
                                      '{gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString()}') ";
                    connectionCell.Open();
                    //Обновляем, если такой записи нет то добавляем:
                    string sql = $@" UPDATE OborudBrig SET count = {e.Value} 
                                  WHERE kod_ob = {get_kod_ob} AND idZeh = {get_idZeh}
                                    IF @@ROWCOUNT = 0
                                    BEGIN
                                        INSERT INTO OborudBrig (kod_ob, idZeh, count)
                                        VALUES ({get_kod_ob}, {get_idZeh}, {e.Value});
                                    END";
                    SqlCommand command = new SqlCommand(sql, connectionCell);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"OborudBrig SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
        }

        private void labelZeh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            SpravZeh f = new SpravZeh("ZehList", "Справочник Цехов");
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        private void linkLabelBrig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravBrig)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravBrig f = new SpravBrig("spBrig", "Справочник Бригад");
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        public void StartListening()
        {
            try
            {
                flagStartListening = true;
                // Остановка предыдущего прослушивания, если оно было активно:
                StopListening();
                // SQL-запрос
                string queryOborudList = $"SELECT idOB,idZeh,kod_ob,count FROM dbo.OborudBrig";
                // Создание соединения с базой данных
                connection = new SqlConnection(connectionString);
                // Открытие соединения
                connection.Open();
                // Создание команды для выполнения SQL-запроса
                SqlCommand command = new SqlCommand(queryOborudList, connection);
                // Создание зависимости, чтобы отслеживать изменения
                sqlDependency = new SqlDependency(command);
                // Подписка на событие изменения
                sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                // Выполнение команды
                command.ExecuteReader();

            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting listener: {ex.Message}");
            }
        }
        public void StopListening()
        {
            // Закрываем подключение
            if (connection != null)
            {
                connection.Close();
            }
        }
        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            // Строка состояния:
            Debug.WriteLine($"Notification received: Type={e.Type}, Info={e.Info}, Source={e.Source}");
            // Проверка есть ли уведомления
            if (e.Type == SqlNotificationType.Change)
            {
                Debug.WriteLine("Data was changed");
                // Обновление UI через Invoke
                if (this.IsHandleCreated)
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Обновили таблицу
                        gridOborud_Load(sender, e);
                    });

            }
            // Возобновляем прослушивание
            StartListening();
        }

    }
}
