using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Base;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.DataAccess.Sql;
using DevExpress.Utils;
using DevExpress.Mvvm.Native;
using System.Drawing;
using DevExpress.XtraExport.Helpers;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.CodeParser;
using System.Collections;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.DataAccess.Native.Data;
using System.Threading;
using System.Globalization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.Xpo.DB.Helpers;
using SewingProduction.Properties;

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
            var dbHelper = new DatabaseHelper(Properties.Settings.Default.ACEConnectionString);
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
            this.fioTableAdapter.Fill(this.aCEDataSet.fio);
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
                    kod = Convert.ToInt32(view.GetRowCellValue(0, "kod"));
                }


                //string query = $"select dbo.getFileEskizForKodd({kodd}) as pathpict ";
                //var dt = ShowRelatedDataAce(query);
                //if (dt != null)
                //{
                //    pictureBox1.Image = Image.FromFile(((DataTable)dt).Rows[0]["pathpict"].ToString());

                //}
                LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
                LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
                LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
                LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
                LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));
                var view_nzp = customGridControl5.MainView as GridView;
                //if (view_nzp != null)
                //{
                //    for (int i=0; i<view_nzp.RowCount; i++)
                //    {
                //        DataRow ewwewqe = (DataRow)view_nzp.GetRow(i);
                //        int nzp = Convert.ToInt32(ewwewqe["kolNZP"]);
                //    }
                //}
                LoadGridControlData(pictureBox1, kod);


                commentRichTextBox.Text = (string)view.GetRowCellValue(0, "komment");
                customComboBox1.SelectedValue = view.GetRowCellValue(0, "constr");
                customComboBox2.SelectedValue = view.GetRowCellValue(0, "diz");

                //применение фильтров к таблице разделений
                filterTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных в список разделений труда: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CurrentWorks_Load();

        }

        private bool IsDataTableLoaded(GridView view)
        {
            if (view.DataSource is BindingSource bindingSource && bindingSource.DataSource is DataTable dataTable)
            {
                return dataTable.Rows.Count > 0;
            }
            return false;
        }


        private void gridControl2_Load(object sender, EventArgs e)
        {

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
                        commentRichTextBox.Text = GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "komment", "");
                        customComboBox1.SelectedValue = GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "constr", 0);
                        //customComboBox2.SelectedValue = view.GetRowCellValue(e.FocusedRowHandle, "diz");
                        customComboBox2.SelectedValue = GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "diz", 0);
                        //int annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annId"));
                        int annId = GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "annId", 0);
                        UpdateRelatedData(annId);
                        int kod = GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "annId", 0);
                        LoadGridControlData(pictureBox1, kod);

                    }
                }
                catch (Exception ex)
                {
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
        private void artnormnBindingSource_DataSourceChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// обновляет данные в связанных таблицах
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
            //int nzp = 0;
            //var view_nzp = customGridControl5.MainView as GridView;
            //if (view_nzp != null)
            //{
            //    for (int i = 0; i < view_nzp.RowCount; i++)
            //    {
            //        DataRowView ewwewqe = (DataRowView)view_nzp.GetRow(i);
            //        int? ewwerrw = Convert.ToInt32(ewwewqe["kolNZP"]);

            //        nzp = ewwerrw ?? 0;
            //    }
            //}
            //if (nzp > 0)
            //{ customButton7.Enabled = false; }

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
                MessageBox.Show($"Ошибка при обновлении NZP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Загрузка данных в связанные таблицы с помощью фильтров
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
        /// отвязать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            var view = customGridControl5.MainView as GridView;
            if (view != null)
            {
                int[] selectedRows = view.GetSelectedRows();
                int annId = 0;
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
                    { art = row.kod;
                        row.binded_art = bindedAnn.articul;
                    }
                }
            }
            else
            {
                MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (ann != 0 && art != 0)
            {
                //bindedRow.binded_art = bindedAnn.model;
                _artNormService.UpdateAnnId(art, ann);
                art_view.RefreshData();
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

        /// <summary>
        /// архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton10_Click(object sender, EventArgs e)
        {
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
                var relatedData = _artNormService.GetRelatedspArt(0);
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
                    catch { };
                }

                customGridControl1.DataSource = artDataList;

                //РТ для увязки
                relatedData = _artNormService.GetArtNormDataCurrent();

                BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();
                // Заполняем myDataList данными из DataTable 
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
                    }
                    catch { };
                }
                customGridControl2.DataSource = myDataList;

                //norm_rasz
                int annId = 0;
                var view = customGridControl2.MainView as GridView;
                if (view != null)
                {
                    annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
                }

                relatedData = _artNormService.GetRelatedNormRasz(annId);
                normraszBindingSource1.DataSource = relatedData;
                customGridControl3.DataSource = normraszBindingSource1;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки данных в текущие работы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        //загрузка norm_rasz 
        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = 0;
            var view = gridView8;//customGridControl2.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "AnnId"));
            }

            var relatedData = _artNormService.GetRelatedNormRasz(annId);
            normraszBindingSource1.DataSource = relatedData;
            customGridControl3.DataSource = normraszBindingSource1;

        }

        //обработка клика на заголовке, 
        private void gridView8_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == gridView8.Columns["IsChecked"])
            {
                int rowHandle = e.RowHandle;
                MyDataANN data = gridView8.GetRow(rowHandle) as MyDataANN;

                if (data != null)
                {
                    data.IsChecked = (bool)e.Value;
                    if (data.IsChecked)
                    { // Обходим все строки и устанавливаем IsChecked в false для остальных
                        for (int i = 0; i < gridView8.RowCount; i++)
                        {
                            if (i != rowHandle)
                            {
                                MyDataANN otherData = gridView8.GetRow(i) as MyDataANN;
                                if (otherData != null)
                                {
                                    if (otherData.IsChecked)
                                        otherData.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                gridView8.RefreshData();
            }

        }

        //обработка клика на заголовке
        private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == gridView7.Columns["IsChecked"])
            {
                int rowHandle = e.RowHandle;
                MyDataART data = gridView7.GetRow(rowHandle) as MyDataART;

                if (data != null)
                {
                    data.IsChecked = (bool)e.Value;
                    if (data.IsChecked)
                    { // Обходим все строки и устанавливаем IsChecked в false для остальных
                        for (int i = 0; i < gridView7.RowCount; i++)
                        {
                            if (i != rowHandle)
                            {
                                MyDataART otherData = gridView7.GetRow(i) as MyDataART;
                                if (otherData != null)
                                {
                                    if (otherData.IsChecked)
                                        otherData.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                gridView7.RefreshData();
            }

        }

        #region headerCheckBox
        private bool isHeaderChecked = false; // Состояние CheckBox в заголовке
        private Dictionary<GridView, bool> gridViewStates = new Dictionary<GridView, bool>();//словарь состояний CheckBox
        //отрисовка чекБокса в заголовке столбца
        private void gridView8_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            // DrawCheckBox(e);
        }

        private void gridView7_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            // DrawCheckBox(e);
        }
        private void gridView9_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            //DrawCheckBox(e);
        }
        private void DrawCheckBox(ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName == "IsChecked") // Убедимся, что это нужный столбец
            {
                // Очищаем стандартную отрисовку заголовка
                e.Handled = true;

                // Отрисовка заголовка
                e.Painter.DrawObject(e.Info);

                // Отрисовка CheckBox
                // Получаем размеры области заголовка
                Rectangle rect = e.Bounds;
                CheckBoxRenderer.DrawCheckBox(
                    e.Graphics,
                    new Point(rect.Left + (rect.Width - 16) / 2, rect.Y + (rect.Height / 2) - 8),   // Рассчитываем позицию чекбокса по центру
                    isHeaderChecked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal
                );
            }
        }
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
        private void AddCheckBoxToHeader(GridView gridView)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit headerCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            GridColumn column = gridView.Columns["IsChecked"];
            column.OptionsColumn.AllowEdit = true;

            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.ColumnEdit = headerCheckEdit;


        }
        //Обработка нажатия на заголовке чекБоксов
        private void CheckBoxMouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (!hitInfo.InColumn || hitInfo.Column.FieldName != "IsChecked") return;
            isHeaderChecked = !isHeaderChecked;
            //UpdateAllRows(view, isHeaderChecked);
            //view.RefreshData();
            // Получаем или устанавливаем состояние для текущего GridView
            if (!gridViewStates.TryGetValue(view, out bool isChecked))
            {
                isChecked = false; // Значение по умолчанию, если GridView еще не был обработан
            }

            isChecked = !isChecked;
            gridViewStates[view] = isChecked; // Сохраняем новое состояние

            UpdateAllRows(view, isChecked);
            view.RefreshData();
        }

        //обновление статуса CheckBox у всех строк таблицы
        private void UpdateAllRows(GridView view, bool isChecked)
        {
            IList dataSource = view.DataSource as IList;
            if (dataSource == null) return;

            foreach (var item in dataSource)
            {
                if (item != null)
                {
                    var property = item.GetType().GetProperty("IsChecked");
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(item, isChecked);
                    }
                }
            }
        }
        //переопределяем метод сортировки кликом на заголовке столбца. Хотелось бы оставить, конечно
        private void gridView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                e.Handled = true; // Отменяем сортировку по колонке "IsChecked"
            }
        }

        #endregion
        #endregion


        private void customComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customComboBox1.Text = customComboBox1.Text.ToString().TrimEnd(' ');
        }

        private void customComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            customComboBox2.Text = customComboBox2.Text.ToString().TrimEnd(' ');
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

        private void GridButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

        private void customButton6_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            TeamWork_AdvanceTW teamWork_Advance = new TeamWork_AdvanceTW();
            teamWork_Advance.ShowDialog();

        }
    }

    //Источник данных РТ для увязки
    public class MyDataANN : INotifyPropertyChanged
    {
        public int annId { get; set; }
        public int kod { get; set; }
        public string articul { get; set; }
        public int status { get; set; }
        public string group { get; set; }
        public string model { get; set; }

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

        //код из 7 цифр
        public int kod { get; set; }
        public string group { get; set; }
        public string model { get; set; }

        //связанный артикул. очищается после завершения сеанса
        public string binded_art { get; set; }

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

    //класс для работы с БД
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    //класс для обработки SQL
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;

        public ArtNormService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable GetArtNormData()
        {
            string query = "SELECT SUBSTRING(kod,1,7) as kod, annId, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id";
            return _dbHelper.ExecuteQuery(query);
        }
        /// <summary>
        /// загрузка артикулов для увязки. Статус != архивное
        /// </summary>
        /// <returns>Возвращает таблицу артикулов</returns>
        public DataTable GetArtNormDataCurrent()
        {
            //string query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status<3";
            string query = "SELECT * FROM artNormNView WHERE kod IN (SELECT annId FROM ACE_backup.dbo.View_sp_articul WHERE kodd_rt LIKE '@kod')";
            return _dbHelper.ExecuteQuery(query);
        }

        public void ResetAnnId(int spArticul)
        {
            // string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod IN (SELECT kod FROM view_sp_articul WHERE kodd_rt = @kod)";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul } });
        }

        public void UpdateAnnId(int spArticul, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul + "%" }, { "@annId", annId } });
        }

        // Получение связанных данных

        public DataTable GetRelatedNormRasz(int annId)
        {
            string query = "SELECT annId, n, n1, razryd, text, sek, kod, kod_o, kod_ob FROM norm_rasz WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public DataTable GetRelatedNormRask(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek  FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormKont(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek FROM norm_kont WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormDopObr(int annId)
        {
            string query = "SELECT annId, sek_p, sek_p_tamp, sek_v, sek_stra FROM norm_dop_obr WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelDesigner(int tab)
        {
            
            string query = "SELECT fio, tab FROM fio where tab = @tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        /// <summary>
        /// Получение связанных данных из sp_articul
        /// </summary>
        /// <param name="annId"></param>
        /// <returns></returns>
        public DataTable GetRelatedspArt(int annId)
        {
            //string query = "";
            //if (annId == 0)
            //{ query = "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, FROM sp_articul WHERE annID IS NULL"; }
            //else if (annId>0)
            //{ query = $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID = @annId"; }
            string query = annId == 0
            //? "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL"
            //: $"SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID = @annId";//kod as trueKod, SUBSTRING(kod,1,7) as kod
            ? "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL" //"EXEC dbo.GetNZPByKoddRT @annId"
            : "EXEC dbo.GetNZPByKoddRT @annId";


            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public DataTable GetImage(int kod)
        {
            string query = "select dbo.getFileEskizForKodd(@kod) as pathpict ";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kod", kod } });
        }


    }
}