using DevExpress.Data.Filtering;
using DevExpress.Entity.Model.DescendantBuilding.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;


namespace SewingProduction.form
{
    /// <summary>
    /// Общий класс работы в бригадах
    /// </summary>
    public partial class TeamWork : CustomForm
    {
        private readonly ArtNormService _artNormService;



        public TeamWork()
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper("ace");//Properties.Settings.Default.ACEConnectionString);
            _artNormService = new ArtNormService(dbHelper);
            UpdateTheme(this);
        }
        /// <summary>
        /// загрузка формы разделений труда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamWorkForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.fio". При необходимости она может быть перемещена или удалена.
            //this.fioTableAdapter.Fill(this.aCEDataSet.fio);
            Thread splashThread = new Thread(() =>
            {
                SplashScreen splash = new SplashScreen();
                splash.ShowDialog();
                Thread.Sleep(2000); // Пример задержки - 2 секунды

            });
            splashThread.Start(); // Запускаем сплэш

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Ошибка загрузки данных");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                splashThread.Abort(); // Закрываем сплэш
            }
        }

        private void LoadData()
        {
            try
            {
                int annId = 0;
                int kod = 0;
                var data = _artNormService.GetArtNormData();
                artnormnBindingSource.DataSource = data;
                gridControl2.DataSource = artnormnBindingSource;

                GridView view = gridControl2.MainView as GridView;
                if (view != null)
                {
                    annId = Convert.ToInt32(view.GetRowCellValue(0, "annId"));
                    kod = Convert.ToInt32(view.GetRowCellValue(1, "kod"));
                }

                LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
                Logger.LogEvent(annId.ToString(), "таблица загружена");
                LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
                Logger.LogEvent(annId.ToString(), "norm_rasz");
                LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
                LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
                LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));
                var view_nzp = customGridControl5.MainView as GridView;

                LoadGridControlData(pictureBox1, kod);


                commentRichTextBox.Text = (string)view.GetRowCellValue(0, "komment");
                customComboBox1.SelectedValue = view.GetRowCellValue(0, "constr");
                customComboBox2.SelectedValue = view.GetRowCellValue(0, "diz");

                //применение фильтров к таблице разделений
                filterTable();

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ошибка загрузки данных в список разделений труда: {ex.Message}");

                MessageBox.Show($"Ошибка загрузки данных в список разделений труда: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CurrentWorks_Load();

        }
        #region работа с окном разделений труда
        private bool IsDataTableLoaded(GridView view)
        {
            if (view.DataSource is BindingSource bindingSource && bindingSource.DataSource is DataTable dataTable)
            {
                return dataTable.Rows.Count > 0;
            }
            return false;
        }

        /// <summary>
        /// Обработка смены фокуса строки в таблице разделений. При смене фокуса меняются данные в связанных таблицах швейных и раскройных операций.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0 && IsDataTableLoaded(gridView3))
            {
                try
                {
                    //var view = gridControl2.MainView as GridView;
                    //if (view != null)
                    var view = gridView3;
                    if (gridView3 != null)
                    {
                        commentRichTextBox.Text = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "komment", "");
                        int f = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "constr", 0);
                        customComboBox1.SelectedValue = f;// CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "constr", 0);
                        //customComboBox2.SelectedValue = view.GetRowCellValue(e.FocusedRowHandle, "diz");
                        customComboBox2.SelectedValue = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "diz", 0);
                        //int annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annId"));
                        int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "annId", 0);
                        UpdateRelatedData(annId);
                        int kod = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "annId", 0);
                        LoadGridControlData(pictureBox1, kod);

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, $"Ошибка при загрузке данных: {ex.Message}");
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private int selectedRowHandle = -1;

        private void gridControl2_Leave(object sender, EventArgs e)
        {
            selectedRowHandle = gridView3.FocusedRowHandle;
        }

        private void gridControl2_GotFocus(object sender, EventArgs e)
        {
            if (selectedRowHandle >= 0)
            {
                gridView1.FocusedRowHandle = selectedRowHandle;
                gridView1.SelectRow(selectedRowHandle);
                selectedRowHandle = -1; //Сбрасываем после восстановления выделения
            }
        }


        /// <summary>
        /// обновление данных в связанных таблицах
        /// </summary>
        /// <param name="annId">идентификатор разделения труда</param>
        private void UpdateRelatedData(int annId)
        {

            LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
            LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
            LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
            LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
            LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));
            UpdateNZPStatus();
        }
        private void UpdateNZPStatus()
        {
            try
            {
                int nzp = 0;
                var viewNzp = customGridControl5.MainView as GridView;

                if (viewNzp != null)
                {
                    for (int i = 0; i < viewNzp.RowCount; i++)
                    {
                        object cellValue = viewNzp.GetRowCellValue(i, "kolNZP");

                        if (cellValue != DBNull.Value && cellValue != null && int.TryParse(cellValue.ToString(), out int parsedValue))
                        {
                            nzp = parsedValue;
                        }
                    }
                }

                customButton7.Enabled = nzp <= 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ошибка обновления статуса НПЗ: {ex.Message}");
                MessageBox.Show($"Ошибка при обновлении NZP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Загрузка данных в связанные таблицы с помощью фильтров
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="source"></param>
        /// <param name="_annId"></param>
        private void LoadGridControlData(GridControl grid, BindingSource source, int _annId)//DataTable data)
        {
            //// Привязываем данные к BindingSource
            //source.DataSource = data;

            //// Привязываем BindingSource к GridControl
            //grid.DataSource = source;

            //// Обновляем визуализацию данных
            //grid.RefreshDataSource();
            string filter = "annId = " + _annId;
            GridView view = (GridView)grid.Views[0];
            view.BeginUpdate();
            view.ActiveFilterString = filter;
            view.EndUpdate();

        }
        /// <summary>
        /// Загрузка картинки
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="_annId"></param>
        private void LoadGridControlData(PictureBox pictureBox, int _annId)
        {
            var dt = _artNormService.GetImage(_annId);
            if (dt != null)
            {
                //pictureBox1.Image = Image.FromFile(((DataTable)dt).Rows[0]["pathpict"].ToString());
                pictureBox.ImageLocation = dt.Rows[0]["pathpict"].ToString();
            }

        }

        /// <summary>
        /// загрузка данных в связанные таблицы с помощью запросов из БД
        /// </summary>
        /// <param name="grid">Имя табл</param>
        /// <param name="source"></param>
        /// <param name="data"></param>
        private void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        {
            // Привязываем данные к BindingSource
            source.DataSource = data;

            // Привязываем BindingSource к GridControl
            grid.DataSource = source;

            // Обновляем визуализацию данных
            grid.RefreshDataSource();
        }

        /// <summary>
        /// нажатие кнопки "отвязать артикул от РТ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            var view = customGridControl5.MainView as GridView;
            if (view != null)
            {
                int[] selectedRows = view.GetSelectedRows();
                //int annId = 0;
                //var focusedRow = view.GetFocusedRow();
                {
                    if (selectedRows != null)
                    {
                        int kod = Convert.ToInt32(view.GetRowCellValue(selectedRows[0], "kodd_rt"));
                        // annId = Convert.ToInt32(view.GetRowCellValue(selectedRows[0], "annId"));
                        _artNormService.ResetAnnId(kod);
                    }
                }
                //   LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));

                //sparticulBindingSource.ResetBindings(true);
                //gridView10.RefreshData();
                //customGridControl5.RefreshDataSource();
                //customGridControl5.Refresh();
                //customGridControl5.Update();

                MessageBox.Show("Записи обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// привязка выбранных артикулов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindButton_Click(object sender, EventArgs e)
        {
            GridView art_view = customGridControl1.MainView as GridView;
            GridView ann_view = customGridControl2.MainView as GridView;
            int art = 0;
            int ann = 0;
            MyDataART bindedRow = new MyDataART();
            MyDataANN bindedAnn = new MyDataANN();
            if ((art_view != null) && (ann_view != null))
            {
                //выбираем отмеченные РТ
                BindingList<MyDataANN> AnnDataSource = ann_view.DataSource as BindingList<MyDataANN>;
                foreach (MyDataANN row in AnnDataSource)
                {
                    if (row.IsChecked)
                    {
                        ann = row.annId;
                        bindedAnn = row;
                    }
                }
                //выбираем отмеченные галкой артикулы
                BindingList<MyDataART> ArtDataSource = art_view.DataSource as BindingList<MyDataART>;
                foreach (MyDataART row in ArtDataSource)
                {
                    if (row.IsChecked)
                    {
                        art = row.kod;
                        row.binded_art = bindedAnn.articul;
                    }
                }
            }
            else
            {
                MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //NewForm dialog = new NewForm();
            DialogResult result = MessageBox.Show($"Вы хотите увязать выбранный артикул {art} с разделением труда {ann}?" , "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//dialog.ShowDialog();
            if (result == DialogResult.No) { return;  }
            if (ann != 0 && art != 0)
            {
                //bindedRow.binded_art = bindedAnn.model;
                _artNormService.UpdateAnnId(art, ann);
                art_view.RefreshData();
                Logger.LogEvent("Привязка завершена", "Привязка завершена, успех");

                MessageBox.Show("Привязка завершена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { MessageBox.Show("Выберите значение для увязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }



        /// <summary>
        /// поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = "";//searchComboBox1.Text;
            string columnName = GetSelectedColumnName();
            if (!string.IsNullOrEmpty(columnName))
            {
                gridView3.ActiveFilterCriteria = new FunctionOperator(
                    FunctionOperatorType.Contains,
                    new OperandProperty(columnName),
                    new OperandValue(searchText));
            }
        }

        private string GetSelectedColumnName()
        {
            if (kode.Checked) return "kod";
            if (articul.Checked) return "articul";
            if (model.Checked) return "mod";
            if (group.Checked) return "grup";
            return string.Empty;
        }

        /// <summary>
        /// редактировать РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton5_Click(object sender, EventArgs e)
        {
            if (gridView3.FocusedRowHandle >= 0)
                gridView3.ShowPopupEditForm();
        }

        /// <summary>
        /// применение фильтров к таблице разделений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            filterTable();
        }

        /// <summary>
        ///предварительные-актуальные-архивные - загрузка по статусам
        /// не описанные - sec_shv=0 & arh = 0
        /// </summary>
        ///
        private void filterTable()
        {
            string filterString = "";
            if (customCheckBox1.Checked) filterString += "status = 1";
            if (customCheckBox2.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + "status = 2";
            if (customCheckBox3.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + "status = 3";
            if (filterString.Length > 0) filterString = "(" + filterString + ")";
            if (SortBox.Checked) filterString += (filterString.Length > 0 ? " AND " : "") + string.Format("([sek_shv] = {0} AND [status] < {1})", 0, 3);// статус 3 - архивный;
            gridView3.BeginUpdate();
            gridView3.ActiveFilterString = filterString;
            gridView3.EndUpdate();
        }

        //фильтр
        private void customButton12_Click(object sender, EventArgs e)
        {
            string filterString = filterTextBox1.Text.TrimEnd(' ');//searchControl1.Text;//
            string columnName = GetSelectedColumnName();
            if (filterString.Length > 0)
            {
                if (!string.IsNullOrEmpty(columnName))
                {
                    gridView3.ActiveFilterCriteria = new FunctionOperator(
                        FunctionOperatorType.Contains,
                        new OperandProperty(columnName),
                        new OperandValue(filterString));
                }
            }

        }
        private void gridView3_ColumnFilterChanged(object sender, EventArgs e)
        {
            //  searchControl1.ClearFilter();
        }

        /// <summary>
        /// архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton10_Click(object sender, EventArgs e)
        {
           // ArchiveAndCopyRT(); ;
            //проверка на наличие НЗП

            //если нет НЗП
            //базовое РТ в архив

            //есть НЗП
            //базовое РТ в предварительный архив


            //проверять НЗП при открытии формы, если обнулилось - можно отправляь в архив по кнопке, новому РТ присвоить статус "актуальное"
            //редактирование копии РТ, если НЗП, РТ "предварительное", если нет НЗП, РТ "актуальное"
            var view = gridView3;
            if (view != null)
            {
                int[] selectedRows = view.GetSelectedRows();
                int annId = 0;
                {
                    if (selectedRows != null)
                    {
                        annId = Convert.ToInt32(view.GetRowCellValue(selectedRows[0], "annId"));
                    }
                }

                TeamWork_ArchAndCopy teamWork_ArchAndCopy = new TeamWork_ArchAndCopy(annId);
                teamWork_ArchAndCopy.ShowDialog();
            }
            //новому РТ привязываем артикулы старого
            //операции старого РТ удаляем из загруза бригад (кроме ВЗП)



        }

        private void customComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  customComboBox1.Text = customComboBox1.Text.ToString().TrimEnd(' ');
        }

        private void customComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // customComboBox2.Text = customComboBox2.Text.ToString().TrimEnd(' ');
        }

        private void customCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            string filterString = "";
            if (customCheckBox6.Checked) filterString += "status = 1";
            if (customCheckBox5.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + "status = 2";

            gridView8.BeginUpdate();
            gridView8.ActiveFilterString = filterString;
            gridView8.EndUpdate();

        }

        private void GridButton_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            string filterString = filterTextBox1.Text.TrimEnd(' ');
            string columnName = GetSelectedColumnName();
            if (filterString.Length > 0)
            {
                if (!string.IsNullOrEmpty(columnName))
                {
                    gridView3.ActiveFilterCriteria = new FunctionOperator(
                        FunctionOperatorType.Contains,
                        new OperandProperty(columnName),
                        new OperandValue(filterString));
                }
            }

        }

        private void searchControl1_QueryIsSearchColumn(object sender, QueryIsSearchColumnEventArgs args)
        {
            string colName = GetSelectedColumnName();
            if (args.FieldName != colName)
                args.IsSearchColumn = false;
        }

        private void search_CheckedChanged(object sender, EventArgs e)
        {

            searchControl1.ClearFilter();

        }
        #endregion

        #region текущие работы - требуют увязки
        /// <summary>
        /// Загрузка вкладки "текущие работы"
        /// </summary>
        void CurrentWorks_Load()
        {
            //загрузка  таблицы РТ для увязки (текущие работы)
            try
            {
                //артикулы для увязки
                DataTable relatedData = _artNormService.GetRelatedspArt(0);
                BindingList<MyDataART> artDataList = new BindingList<MyDataART>();
                // Заполняем myDataList данными из DataTable 
                foreach (DataRow row in relatedData.Rows)
                {
                    try
                    {
                        artDataList.Add(new MyDataART
                        {
                            kod = Convert.ToInt32(row["kod"]),
                            articul = row["articul"].ToString(),
                            group = row["grup"].ToString(),
                            model = row["mod"].ToString(),
                            // binded_art = row["binded_art"].ToString(),
                            IsChecked = false
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                        MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Привязка данных к таблице
                customGridControl1.DataSource = artDataList;

                if (artDataList.Count > 0)
                {
                    // Загрузка работ по первому артикулу
                    BindingList<MyDataANN> myDataList = LoadWorksbyArt(artDataList[0].kod, artDataList[0].articul);
                    customGridControl2.DataSource = myDataList;

                    // Загрузка norm_rasz
                    int annId = 0;
                    var view = customGridControl2.MainView as GridView;
                    if (view != null && view.RowCount > 0)
                    {
                        annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
                    }

                    DataTable normRaszData = _artNormService.GetRelatedNormRasz(annId);
                    normraszBindingSource1.DataSource = normRaszData;
                    customGridControl3.DataSource = normraszBindingSource1;
                    DataTable bindedArts = _artNormService.GetRelatedspArt(annId);
                }
            }
            //    customGridControl1.DataSource = artDataList;

            //    BindingList<MyDataANN> myDataList = LoadWorksbyArt(artDataList[0].kod, artDataList[0].articul);

            //    //привязка источника данных к таблице
            //    customGridControl2.DataSource = myDataList;

            //    //norm_rasz
            //    int annId = 0;
            //    var view = customGridControl2.MainView as GridView;
            //    if (view != null)
            //    {
            //        annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
            //    }

            //    relatedData = _artNormService.GetRelatedNormRasz(annId);
            //    normraszBindingSource1.DataSource = relatedData;
            //    customGridControl3.DataSource = normraszBindingSource1;
            //}
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки данных в текущие работы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает данные для "текущих работ" на основе кода и артикула.
        /// Если чекбокс customCheckBox4 активен, загружает все данные, иначе фильтрует по выбранному артикулу.
        /// </summary>
        /// <param name="kod">Код для фильтрации</param>
        /// <param name="articul">Артикул для фильтрации</param>
        /// <returns>Список данных для отображения в таблице</returns>
        private BindingList<MyDataANN> LoadWorksbyArt(int kod, string articul)
        {
            DataTable relatedData = new DataTable();
            // Если customCheckBox4 выбран, загружаем все работы
            if (customCheckBox4.Checked)
            {
                relatedData = _artNormService.GetArtNormDataCurrent(kod, true); // Загружаем все данные
            }
            else
            {
                // Если чекбокс не выбран, фильтруем данные по выбранному артикулу и коду
                relatedData = _artNormService.GetArtNormDataCurrent(kod, false); // Загружаем данные по коду
                                                                                 // Фильтруем по первой части артикула (до дефиса)
                int k = articul.IndexOf("-");
                if (k > 0)
                {
                    DataTable dataTable = _artNormService.GetArtNormDataCurrent(articul.Substring(0, k));
                    relatedData.Merge(dataTable); // Объединяем данные по части артикула
                }
            }

            // Создаём и заполняем BindingList для отображения в GridControl
            BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();
            string q ="";
            string w ="";
            string e ="";
            string r ="";
            string t ="";
            string y ="";
            string u = "";
            string i = "";
            int index = 0;
            foreach (DataRow row in relatedData.Rows)
            {
                try
                {
                    myDataList.Add(new MyDataANN
                    {
                        annId = Convert.ToInt32(row["annId"]),
                        kod = Convert.ToInt32(row["kod"]),
                        articul = row["articul"].ToString(),
                        status = Convert.ToInt32(row["status"]),
                        stat = row["stat"].ToString(),
                        group = row["grup"].ToString(),
                        model = row["mod"].ToString(),
                        IsChecked = false
                     });

                    // q = myDataList[index].annId.ToString().TrimEnd(' ');
                     w = myDataList[index].kod.ToString();
                    // e = myDataList[index].articul.ToString().TrimEnd(' ');
                    // r = myDataList[index].status.ToString().TrimEnd(' ');
                    // t = myDataList[index].stat.ToString().TrimEnd(' ');
                    // y = myDataList[index].group.ToString().TrimEnd(' ');
                    // u = myDataList[index].model.ToString().TrimEnd(' ');
                    // i = myDataList[index].IsChecked.ToString();
                    index++;
                    //Logger.LogEvent($"annId= {q},kod={w},art={e},status={r},stat={t},group={y},mod={u}, checked={i}");
                 }
                catch (Exception  ex)
                 {
                    Logger.LogError(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}, annId= {q},kod={w},art={e},status={r},stat={t},group={y},mod={u}, checked {i}");
                    MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return myDataList;
        }


        /// <summary>
        /// загрузка norm_rasz 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = 0;
            var view = gridView8;//customGridControl2.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annId"));
            }

            var relatedData = _artNormService.GetRelatedNormRasz(annId);
            normraszBindingSource1.DataSource = relatedData;
            customGridControl3.DataSource = normraszBindingSource1;

        }
        #endregion
        #region headerCheckBox

        /// <summary>
        /// Обработчик изменения значения в ячейке (IsChecked) для gridView8.
        /// Устанавливает IsChecked в true только для одной строки, сбрасывая остальные.
        /// </summary>
        private void gridView8_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                UpdateExclusiveCheck<MyDataANN>(gridView8, e.RowHandle);
            }
        }

        /// <summary>
        /// Обработчик изменения значения в ячейке (IsChecked) для gridView7.
        /// Устанавливает IsChecked в true только для одной строки, сбрасывая остальные.
        /// </summary>
        private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                UpdateExclusiveCheck<MyDataART>(gridView7, e.RowHandle);
            }
        }

        /// <summary>
        /// Устанавливает IsChecked = true только для одной строки, сбрасывая остальные в false.
        /// </summary>
        /// <param name="gridView">GridView, в котором выполняется изменение</param>
        /// <param name="rowHandle">Индекс строки, где пользователь установил флаг</param>
        private void UpdateExclusiveCheck<T>(GridView gridView, int rowHandle) where T : class
        {
            var selectedData = gridView.GetRow(rowHandle) as T;
            if (selectedData is ICheckable checkableSelectedData)
            {
                checkableSelectedData.IsChecked = true;

                // Сбрасываем флаг у остальных строк
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (i != rowHandle)
                    {
                        var otherData = gridView.GetRow(i) as T;
                        if (otherData is ICheckable checkableOtherData && checkableOtherData.IsChecked)
                        {
                            checkableOtherData.IsChecked = false;
                        }
                    }
                }
            }
            gridView.RefreshData();
        }

        /// <summary>
        /// Интерфейс для моделей с флагом IsChecked
        /// </summary>
        public interface ICheckable
        {
            bool IsChecked { get; set; }
        }

        /// <summary>
        /// Модель MyDataANN с реализацией интерфейса ICheckable
        /// </summary>
        public class MyDataANN : ICheckable
        {
            public int annId { get; set; }
            public int kod { get; set; }
            public string articul { get; set; }
            public int status { get; set; }
            public string group { get; set; }
            public string model { get; set; }
            public string stat { get; set; }
            public bool IsChecked { get; set; }
        }

        /// <summary>
        /// Модель MyDataART с реализацией интерфейса ICheckable
        /// </summary>
        public class MyDataART : ICheckable
        {
            public string articul { get; set; }
            public int kod { get; set; }
            public string group { get; set; }
            public string model { get; set; }
            public string binded_art { get; set; }
            public bool IsChecked { get; set; }
        }
        ///// <summary>
        ///// обработка клика на заголовке, 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridView8_CellValueChanged(object sender, CellValueChangedEventArgs e)
        //{
        //    if (e.Column == gridView8.Columns["IsChecked"])
        //    {
        //        int rowHandle = e.RowHandle;
        //        MyDataANN data = gridView8.GetRow(rowHandle) as MyDataANN;

        //        if (data != null)
        //        {
        //            data.IsChecked = (bool)e.Value;
        //            if (data.IsChecked)
        //            { // Обходим все строки и устанавливаем IsChecked в false для остальных
        //                for (int i = 0; i < gridView8.RowCount; i++)
        //                {
        //                    if (i != rowHandle)
        //                    {
        //                        MyDataANN otherData = gridView8.GetRow(i) as MyDataANN;
        //                        if (otherData != null)
        //                        {
        //                            if (otherData.IsChecked)
        //                                otherData.IsChecked = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        gridView8.RefreshData();
        //    }
        //}

        ///// <summary>
        ///// обработка клика на заголовке
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        //{
        //    if (e.Column == gridView7.Columns["IsChecked"])
        //    {
        //        int rowHandle = e.RowHandle;
        //        MyDataART data = gridView7.GetRow(rowHandle) as MyDataART;

        //        if (data != null)
        //        {
        //            data.IsChecked = (bool)e.Value;
        //            if (data.IsChecked)
        //            { // Обходим все строки и устанавливаем IsChecked в false для остальных
        //                for (int i = 0; i < gridView7.RowCount; i++)
        //                {
        //                    if (i != rowHandle)
        //                    {
        //                        MyDataART otherData = gridView7.GetRow(i) as MyDataART;
        //                        if (otherData != null)
        //                        {
        //                            if (otherData.IsChecked)
        //                                otherData.IsChecked = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        gridView7.RefreshData();
        //    }
        //}

        private void checkedChange(GridView view, int rowHandle, bool value)
        {
            MyDataANN data = view.GetRow(rowHandle) as MyDataANN;

            if (data != null)
            {
                data.IsChecked = value;
                if (data.IsChecked)
                { // Обходим все строки и устанавливаем IsChecked в false для остальных
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (i != rowHandle)
                        {
                            MyDataANN otherData = view.GetRow(i) as MyDataANN;
                            if (otherData != null)
                            {
                                if (otherData.IsChecked)
                                    otherData.IsChecked = false;
                            }
                        }
                    }
                }
            }
        }
        private bool isHeaderChecked = false; // Состояние CheckBox в заголовке
        private Dictionary<GridView, bool> gridViewStates = new Dictionary<GridView, bool>();//словарь состояний CheckBox
        /// <summary>
        /// это для множественного выбора
        /// </summary>
        /// <typeparam name="MyDataART"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        private List<MyDataART> GetCheckedRows<MyDataART>(GridView view)
        {
            BindingList<MyDataART> dataSource = view.DataSource as BindingList<MyDataART>;
            if (dataSource == null) return new List<MyDataART>(); // Обработка null
            List<MyDataART> list = new List<MyDataART>();
            foreach (MyDataART row in dataSource)
            {
                //if (row.IsChecked = true)
                {
                    list.Add(row);

                }
            }
            return new List<MyDataART>();//
                                         //dataSource.Where(item => item.IsChecked).ToList();
        }

        ///// <summary>
        ///// Обработка нажатия на заголовке чекБоксов
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void CheckBoxMouseDown(object sender, MouseEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view == null) return;
        //    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

        //    if (!hitInfo.InColumn || hitInfo.Column.FieldName != "IsChecked") return;
        //    isHeaderChecked = !isHeaderChecked;
        //    //UpdateAllRows(view, isHeaderChecked);
        //    //view.RefreshData();
        //    // Получаем или устанавливаем состояние для текущего GridView
        //    if (!gridViewStates.TryGetValue(view, out bool isChecked))
        //    {
        //        isChecked = false; // Значение по умолчанию, если GridView еще не был обработан
        //    }

        //    isChecked = !isChecked;
        //    gridViewStates[view] = isChecked; // Сохраняем новое состояние

        //    UpdateAllRows(view, isChecked);
        //    view.RefreshData();
        //}

        ///// <summary>
        ///// обновление статуса CheckBox у всех строк таблицы
        ///// </summary>
        ///// <param name="view"></param>
        ///// <param name="isChecked"></param>
        //private void UpdateAllRows(GridView view, bool isChecked)
        //{
        //    IList dataSource = view.DataSource as IList;
        //    if (dataSource == null) return;

        //    foreach (var item in dataSource)
        //    {
        //        if (item != null)
        //        {
        //            var property = item.GetType().GetProperty("IsChecked");
        //            if (property != null && property.CanWrite)
        //            {
        //                property.SetValue(item, isChecked);
        //            }
        //        }
        //    }
        //}

        #endregion

        /// <summary>
        /// Загрузка формы Предварительное РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            TeamWork_AdvanceTW teamWork_Advance = new TeamWork_AdvanceTW();
            teamWork_Advance.ShowDialog();

        }
        /// <summary>
        /// Смена фокуса в таблице артикулов для увязки. Выбор подходящих РТ для увязки с выбранным артикулом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView7_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int kodd_rt = 0;
            string articul = "";
            var view = gridView7;
            // DataTable relatedData = new DataTable();
            BindingList<MyDataANN> relatedData = new BindingList<MyDataANN>();
            if (!customCheckBox4.Checked)
            {
                if (view != null)
                {
                    kodd_rt = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "kod"));
                    articul = view.GetRowCellValue(e.FocusedRowHandle, "articul").ToString();
                }
                relatedData = LoadWorksbyArt(kodd_rt, articul);
            }
            else
            {
                relatedData = LoadWorksbyArt(0, "");
            }

            //привязка источника данных к таблице
            customGridControl2.DataSource = relatedData;//myDataList;

            
            normraszBindingSource1.DataSource = _artNormService.GetRelatedNormRasz(kodd_rt); 
            customGridControl3.DataSource = normraszBindingSource1;
        }

        private void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            BindingList<MyDataANN> myDataList = null;
            int i = gridView7.FocusedRowHandle;
            if (customCheckBox4.Checked)
            {
                myDataList = LoadWorksbyArt(0, "");
            }
            else
            {
                myDataList = LoadWorksbyArt(Convert.ToInt32(gridView7.GetRowCellValue(i, "kod")), gridView7.GetRowCellValue(i, "articul").ToString());
            }
            customGridControl2.DataSource = myDataList;
        }

        private void customButton6_Click(object sender, EventArgs e)
        {
            GridView view = gridView3;
            if (view == null) return;

            view.AddNewRow(); // Добавляем новую строку
            using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW())
            {
                if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                {
                    //сохраняем
                }
                else
                {
                    //отменяем
                }
            }
            //view.ShowPopupEditForm(); // Открываем окно редактирования
        }
        #region arch+copy
        /// <summary>
        /// Архивирование РТ с возможностью создания копии
        /// </summary>
        private void ArchiveAndCopyRT()
        {
            //var view = gridView3;
            //if (view == null || view.SelectedRowsCount == 0)
            //{
            //    MessageBox.Show("Выберите запись для архивирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //int annId = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "annId"));
            //bool hasNZP = _artNormService.CheckNZP(annId);

            //if (!hasNZP || (hasNZP && !_artNormService.HasAssignedOperations(annId)))
            //{
            //    // Создаем копию РТ со статусом "актуальное"
            //    int newAnnId = _artNormService.CreateCopyRT(annId, "актуальное");
            //    _artNormService.UpdateRTStatus(annId, "архив");
            //    _artNormService.ReassignArticles(annId, newAnnId);
            //    _artNormService.RemoveOperationsFromBrigades(annId);
            //    MessageBox.Show("РТ успешно архивировано и скопировано.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    // Создаем копию РТ со статусом "предварительный"
            //    int newAnnId = _artNormService.CreateCopyRT(annId, "предварительный");
            //    _artNormService.UpdateRTStatus(annId, "предварительный архив");
            //    _artNormService.ReassignArticles(annId, newAnnId);
            //    _artNormService.RemoveOperationsFromBrigades(annId);
            //    MessageBox.Show("РТ с НЗП перемещено в предварительный архив и создана копия.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// Обновление данных по НЗП и перевод РТ в архив
        /// </summary>
        private void UpdateNZPAndArchive()
        {
            //var view = gridView3;
            //if (view == null || view.SelectedRowsCount == 0)
            //{
            //    MessageBox.Show("Выберите запись для архивирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //int annId = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "annId"));
            //if (_artNormService.CheckNZP(annId))
            //{
            //    MessageBox.Show("Невозможно отправить в архив: есть незавершенное производство.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //_artNormService.UpdateRTStatus(annId, "архив");
            //MessageBox.Show("РТ успешно отправлено в архив.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnArchiveCopy_Click(object sender, EventArgs e)
        {
            ArchiveAndCopyRT();
        }

        private void btnArchiveRT_Click(object sender, EventArgs e)
        {
            UpdateNZPAndArchive();
        }

        private void gridView3_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        private void copyButton_Click(object sender, EventArgs e)
        {

        }
    }
    #endregion


    /// <summary>
    /// Источник данных РТ для увязки
    /// </summary>
    public class MyDataANN : INotifyPropertyChanged
    {
        /// <summary>
        /// AnnId
        /// </summary>
        public int annId { get; set; }
        /// <summary>
        /// код из 7 цифр
        /// </summary>
        public int kod { get; set; }
        /// <summary>
        /// артикул
        /// </summary>
        public string articul { get; set; }
        /// <summary>
        /// статус (номер)
        /// </summary>
        public int status { get; set; }
        public string group { get; set; }
        public string model { get; set; }
        /// <summary>
        /// статус название
        /// </summary>
        public string stat { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Источник данных Артикулов для увязки
    /// </summary>
    public class MyDataART
    {
        public string articul { get; set; }

        /// <summary>
        /// код из 7 цифр
        /// </summary>
        public int kod { get; set; }
        /// <summary>
        /// группа
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// модель
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// связанный артикул. очищается после завершения сеанса
        /// </summary>
        public string binded_art { get; set; }

        private bool _isChecked;
        /// <summary>
        /// Метка в чек-боксе
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
