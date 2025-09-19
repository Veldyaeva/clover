using DevExpress.XtraGrid.Views.Grid;
//using DevExpress.XtraGrid.Localization;
using SewingProduction.Core.interfaces;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.form
{
    public partial class SpravForAll : CustomForm, IDataUpdatableForm
    {
        private readonly SpravAllDataService _spravAllDataService;
        private readonly ServiceBroker _serviceBroker;
        int strForAdd;
        string columns;
        string whereSQL;
        string tableString;
        List<string> fieldsQueryListSQL;
        // Отслеживание изменений в базе данных:
        //private SqlDependency sqlDependency;
        // Соединение с бд:
        //private SqlConnection connection;
        //чтобы перейти к нужной строке в таблице:
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        bool flagAddDown = false; //если добавили поле в таблицу
        bool flagStartListening = false; //вкл прослушки
        private System.Windows.Forms.Label[] labels;
        private TextBox[] textBoxs;
        UserClass _user = new UserClass();
        //словари для рус названий столбцов:
        Dictionary<string, string> eng_rus = new Dictionary<string, string>();
        Dictionary<string, string> rus_eng = new Dictionary<string, string>();
        Dictionary<string, string> eng_type = new Dictionary<string, string>();
        Dictionary<string, int> rus_read = new Dictionary<string, int>();
        Dictionary<string, object> columnDefaults = new Dictionary<string, object>();
        //Таймер для уведомления о сохранении:
        private Timer timer;
        bool _servBrok = false;

        public SpravForAll(
            string tableSQL,
            string columnsSQL = "*",
            string whereSQL = "",
            string rusNameTableSQL = "",
            UserClass user = null,
            bool red = true,
            bool add = true,
            bool servBrok = true)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper();
            _spravAllDataService = new SpravAllDataService(dbHelper);
            _servBrok = servBrok;
            _serviceBroker = _servBrok == true ? new ServiceBroker(this) : null;
            ThemeManager.UpdateTheme(this);
            // Пользователь:
            _user = user;
            // Таймер
            timer = new Timer { Interval = 2000 };
            timer.Tick += Timer_Tick;

            // Таблица
            tableString = tableSQL;
            _spravAllDataService._tableString = tableSQL;
            columns = string.IsNullOrEmpty(columnsSQL) ? "*" : columnsSQL;
            this.whereSQL = whereSQL;

            // Имя формы
            this.Text = rusNameTableSQL;

            // Инициализация
            fieldsQueryListSQL = new List<string>();
            labels = new[] { labelKod, label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            textBoxs = new[] { textBoxKod, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };

            // Кнопки редактированить добавить
            simpleButtonRed.Enabled = red;
            simpleButtonAdd.Enabled = add;
        }
        public SpravForAll()
        {
            InitializeComponent();
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
        private void SpravForAll_Load(object sender, EventArgs e)
        {
            if (_user == null)
                Debug.WriteLine("[SpravForAll] ВНИМАНИЕ: пользователь не передан!");
            else
            {
                gridControlSprav.ObjectName = "gridControlSprav";
                gridControlSprav.InitializeAccess(_user, this.Name, new List<string> { tableString });
                if (_servBrok)
                    _serviceBroker.StartBroker();
                SpravForAll_V();
            }
        }
        private void SpravForAll_V()
        {
            var vButtonAcc = _spravAllDataService.LoadButton(_user.UserId);

            simpleButtonDel.Visible = simpleButtonDel.Enabled = GetMode(vButtonAcc, "simpleButtonDel") > 0;
            simpleButtonAdd.Visible = simpleButtonAdd.Enabled = GetMode(vButtonAcc, "simpleButtonAdd") > 0;
            simpleButtonRed.Visible = simpleButtonRed.Enabled = GetMode(vButtonAcc, "simpleButtonRed") > 0;

            gridView1.OptionsBehavior.Editable = simpleButtonRed.Enabled;
        }
        private int GetMode(System.Data.DataTable dt, string name) =>
        dt.AsEnumerable().FirstOrDefault(r => r["name"].ToString() == name)?["ModeID"] as int? ?? 0;

        //Рус нэйминг столбцов:
        private async System.Threading.Tasks.Task LoadRusNamesAsync()
        {
            try
            {
                System.Data.DataTable tableList = await _spravAllDataService.GetRusNameAsync();

                foreach (DataRow row in tableList.Rows)
                {
                    string name = row["name"].ToString();
                    eng_rus[name] = row["name_rus"].ToString();
                    rus_eng[row["name_rus"].ToString()] = name;
                    eng_type[name] = row["data_type"].ToString();
                    rus_read[row["name_rus"].ToString()] = (Int32)row["readonly"];

                    if (row.Table.Columns.Contains("default_value") && row["default_value"] != DBNull.Value)
                        columnDefaults[name] = row["default_value"];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading rus name: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки русских имен {ex.Message}");
            }
        }
        private async void gridControlSprav_Load(object sender, EventArgs e)
        {
            // Загружаем русские имена асинхронно
            await LoadRusNamesAsync();

            // Загружаем данные и запускаем прослушивание
            LoadData();
            if (_servBrok)
            {
                flagStartListening = true; // Устанавливаем флаг прослушки
                string columnsStr = string.Join(", ", fieldsQueryListSQL);
                _serviceBroker.StartListening(columnsStr, tableString);
            }
        }
        private void LoadData()
        {
            //gridControlSprav.InitializeAccess(_user, this.Name);
            System.Data.DataTable tableList = _spravAllDataService.GetRecord(columns, whereSQL);
            spravList.DataSource = tableList;
            fieldsQueryListSQL.Clear();
            // Получаем имена столбцов и добавляем их в список:
            foreach (System.Data.DataColumn column in tableList.Columns)
            {
                // Добавляем имя столбца в список
                fieldsQueryListSQL.Add(column.ColumnName);
            }
            //gridView1.Columns[0].Visible = false;

            // Изменяем заголовки столбцов
            for (int i = 0; i < tableList.Columns.Count; i++)
            {
                string englishName = tableList.Columns[i].Caption;
                // Проверяем, есть ли соответствующее русское имя в словаре
                if (eng_rus.TryGetValue(englishName, out string russianName))
                {
                    // Заменяем заголовок столбца на русское имя
                    gridView1.Columns[i].Caption = russianName;
                }
            }
            // Выравнивание столбцов
            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true;
            //перенос столбца архив в конец:
            //gridView.Columns["arhiv"].VisibleIndex = -(gridView.Columns["arhiv"].VisibleIndex - (gridView.Columns.Count - 2));

        }

        // Отображение лейблов и текстбоксов в нужном кол-е
        void labelAndTextBox()
        {
            GridView gridView = gridControlSprav.MainView as GridView;

            if (gridView != null)
            {
                int columnCount = gridView.Columns.Count;

                for (int i = 0; i < labels.Length; i++)
                {
                    if (i < columnCount) // Если индекс меньше количества колонок
                    {
                        // текст = колонке
                        labels[i].Text = gridView.Columns[i].Caption;
                        // Делаем метку видимой
                        //labels[i].Visible = true;
                        //textBoxs[i].Visible = true;
                        labels[i].Visible = gridView.Columns[i].Visible;
                        textBoxs[i].Visible = gridView.Columns[i].Visible;
                    }
                    else
                    {
                        // Скрываем метки, если нет данных
                        labels[i].Visible = false;
                        textBoxs[i].Visible = false;
                    }
                }
            }

        }
        //Кнопка добавить:
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gridView = (GridView)gridControlSprav.MainView;
                xtraTabPageAdd.Text = "Добавить";
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = false;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                labelAndTextBox();
                // Код = последнему коду в таблице + 1
                textBoxKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount - 1)[0]) + 1).ToString();
                for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                {
                    textBoxs[i].ReadOnly = false;
                    textBoxs[i].Text = "";
                }
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
                // Получаем доступ к GridView
                GridView gridView = gridControlSprav.MainView as GridView;
                xtraTabPageAdd.Text = "Редактировать";
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = true;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                labelAndTextBox();
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                {
                    //textBoxs[i].ReadOnly = false;
                    //textBoxs[i].Text = gridView.GetFocusedRowCellValue(gridView.Columns[i]).ToString();
                    var column = gridView.Columns[i];
                    textBoxs[i].ReadOnly = column.OptionsColumn.ReadOnly;
                    textBoxs[i].Text = gridView.GetFocusedRowCellValue(column)?.ToString();
                }
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
                labelAndTextBox();
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                {
                    textBoxs[i].ReadOnly = true;
                    textBoxs[i].Text = gridView.GetFocusedRowCellValue(gridView.Columns[i]).ToString();
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Кнопка Отмена:
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }
        //Кнопка сохранить:
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            switch (xtraTabPageAdd.Text)
            {
                case "Добавить":
                    {
                        _spravAllDataService.InsertRecord(fieldsQueryListSQL, textBoxs, eng_type, columnDefaults);
                        flagAddDown = true;
                        break;
                    }
                case "Редактировать":
                    {
                        _spravAllDataService.UpdateRecord(fieldsQueryListSQL, textBoxs, eng_type);
                        break;
                    }
                default:
                    break;
            }
            labelSave.Text = "Сохранено!";
            labelSave.Visible = true;
            timer.Start();
            if (!_servBrok)
                LoadData();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSave.Visible = false; // Скрываем лейбл
            timer.Stop(); // Останавливаем таймер
        }

        //Редактирование таблице:
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string englishName;
            if (rus_eng.TryGetValue(e.Column.FieldName, out string engName))
                englishName = engName;
            else
                englishName = e.Column.FieldName;
            _spravAllDataService.UpdateRowRecord(englishName, e.Value, fieldsQueryListSQL[0], gridView1.GetDataRow(e.RowHandle)[0]);
        }
        //Удалить запись:
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            try
            {
                currentRowIndex = gridView1.FocusedRowHandle;
                string textCol = gridView1.GetFocusedRowCellValue(gridView1.Columns[1]).ToString();
                object kodCol = gridView1.GetFocusedRowCellValue(gridView1.Columns[0]);
                string message = "Вы уверены что хотите удалить '" + textCol + "' ?";
                var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _spravAllDataService.DeleteRecord(fieldsQueryListSQL[0], kodCol);
                    AddTab.TabPages[0].PageVisible = false;
                }
                if (!_servBrok)
                    LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex.Message}");
                MessageBox.Show($"Ошибка {ex.Message}");
            }
        }

        //закрытие формы:
        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_servBrok)
                _serviceBroker.StopBroker();
        }
    }

    public class SpravAllDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public string _tableString { set; get; }
        public SpravAllDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public System.Data.DataTable GetRecord(string columnSQL, string whereSQL)
        {
            string query = $@"SELECT {columnSQL} FROM {_tableString}";
            query += whereSQL == "" ? "" : " WHERE " + whereSQL;
            return _dbHelper.ExecuteQuery(query);
        }
        public async System.Threading.Tasks.Task<System.Data.DataTable> GetRusNameAsync()
        {
            string query = @"SELECT acn.name, acn.name_rus, data_type, readonly, default_value
                                FROM dbo.all_column_name acn
                                INNER JOIN all_table_name atn
                                ON acn.id_atn = atn.id_atn
                                WHERE atn.name = @tableName
                                ORDER BY ORDINAL_POSITION";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tableName", _tableString } });
        }
        public System.Data.DataTable LoadButton(int UserID)
        {
            string query = @"SELECT acn.name , max(ModeID) AS ModeID FROM RoleColumn rc
                            LEFT JOIN all_column_name acn ON acn.id_acn = rc.ColumnID
                            LEFT JOIN all_table_name atn ON acn.id_atn = atn.id_atn
                            LEFT JOIN UserRoles ur ON rc.RoleID = ur.RoleID
                                WHERE acn.data_type = 'button' 
                                AND atn.name = @tableName
                                AND ur.UserID = @UserID
                                GROUP BY acn.name";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tableName", _tableString }, { "@UserID", UserID } });
        }
        public void InsertRecord(
            List<string> fieldsQueryListSQL,
            TextBox[] textBoxs,
            Dictionary<string, string> eng_type,
            Dictionary<string, object> columnDefaults)
        {
            // Формируем часть запроса с названиями столбцов
            string queryOborudAdd = "INSERT INTO " + _tableString + " (";
            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
            {
                queryOborudAdd += fieldsQueryListSQL[i];
                queryOborudAdd += (i == fieldsQueryListSQL.Count - 1) ? ")" : ", ";
            }

            // Формируем часть запроса со значениями
            queryOborudAdd += " VALUES (";
            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
            {
                string fieldName = fieldsQueryListSQL[i];
                string inputValue = textBoxs[i].Text.Trim();
                string valueToInsert = inputValue;

                // Если поле пустое — проверяем на default_value
                if (string.IsNullOrEmpty(inputValue) && columnDefaults.TryGetValue(fieldName, out object defaultVal))
                {
                    valueToInsert = defaultVal?.ToString() ?? "";
                }

                if (eng_type.TryGetValue(fieldName, out string fieldType))
                {
                    switch (fieldType.ToLower())
                    {
                        case "int":
                        case "bigint":
                            queryOborudAdd += string.IsNullOrEmpty(valueToInsert) ? "NULL" : valueToInsert;
                            break;

                        case "float":
                        case "decimal":
                        case "real":
                            queryOborudAdd += string.IsNullOrEmpty(valueToInsert) ? "NULL" : valueToInsert.Replace(',', '.');
                            break;

                        case "bit":
                            queryOborudAdd += (valueToInsert == "1" || valueToInsert.ToLower() == "true") ? "1" : "0";
                            break;

                        case "varchar":
                        case "nvarchar":
                        case "nchar":
                        case "char":
                        case "text":
                        default:
                            queryOborudAdd += $"'{valueToInsert.Replace("'", "''")}'"; // экранирование одинарной кавычки
                            break;
                    }
                }
                else
                {
                    queryOborudAdd += $"'{valueToInsert}'"; // если тип не определён
                }

                queryOborudAdd += (i == fieldsQueryListSQL.Count - 1) ? ")" : ", ";
            }

            // Выполнение запроса
            _dbHelper.ExecuteNonQuery(queryOborudAdd);
        }
        public void UpdateRecord(List<string> fieldsQueryListSQL, TextBox[] textBoxs, Dictionary<string, string> eng_type)
        {
            List<string> setParts = new List<string>();

            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
            {
                string column = fieldsQueryListSQL[i];
                if (eng_type.TryGetValue(column, out string type))
                {
                    string value = textBoxs[i].Text;

                    switch (type)
                    {
                        case "int":
                        case "float":
                        case "decimal":
                            value = value.Replace(',', '.'); // decimal point
                            setParts.Add($"{column} = {value}");
                            break;

                        case "string":
                        case "varchar":
                        case "nvarchar":
                        case "nchar":
                        case "char":
                            setParts.Add($"{column} = '{value.Replace("'", "''")}'"); // экранируем '
                            break;

                        default:
                            // если тип не поддержан — можно проигнорировать или кинуть исключение
                            break;
                    }
                }
            }

            if (setParts.Count == 0)
                throw new Exception("Нет данных для обновления");

            string query = $"UPDATE {_tableString} SET {string.Join(", ", setParts)} WHERE {fieldsQueryListSQL[0]} = {textBoxs[0].Text}";

            _dbHelper.ExecuteNonQuery(query);
        }

        public void UpdateRowRecord(string englishName, object eValue, string kod, object ekod)
        {
            string query = $"UPDATE {_tableString} SET {englishName} = @eValue WHERE {kod} = @ekod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@ekod", ekod } });
        }
        public void DeleteRecord(string kod, object ekod)
        {
            string query = $"DELETE FROM {_tableString} WHERE {kod} = @ekod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@ekod", ekod } });
        }
    }
}
