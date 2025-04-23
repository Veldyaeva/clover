// TeamWork.Articles.cs
using System.Threading.Tasks;
using System;
using SewingProduction.Models;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SewingProduction.Helpers;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;

namespace SewingProduction.Forms
{
    public partial class TeamWork
    {
        // Вторая вкладка — "Работа с артикулами"

        /// <summary>
        /// Загрузка вкладки "текущие работы" (MyDataAnn)
        /// </summary>
        private async Task CurrentWorks_Load()
        {
            //загрузка артикулов для увязки (актуальные и предварительные)
            await MyDataArtLoad();
            //загрузка  таблицы РТ для увязки (текущие работы)
            await MyDataAnnLoad();
            //norm_rasz
            await NormRaszLoad();
        }
        private async Task MyDataArtLoad()
        {
            try
            {
                // Fetch unbound articles
                DataTable relatedData = await _artNormService.GetRelatedSpArt();
                await _logger.LogEventAsync($"Related Data Count: {relatedData.Rows.Count}", "MyDataArtLoad");

                if (relatedData != null && relatedData.Rows.Count > 0)
                {
                    BindingList<MyDataART> artDataList = new BindingList<MyDataART>();

                    foreach (DataRow row in relatedData.Rows)
                    {
                        try
                        {
                            artDataList.Add(MapDataRowToMyDataART(row));
                        }
                        catch (Exception ex)
                        {
                            await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                            MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ;
                    }

                    try
                    {
                        customGridControl1.DataSource = artDataList; 
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка приведения artDataList.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private MyDataART MapDataRowToMyDataART(DataRow row)
        {
            if (row == null || row.Table == null)
            {
                Console.WriteLine("Ошибка: передан пустой DataRow или у него отсутствует таблица!");
                return null;
            }

            // Проверяем, содержит ли DataRow нужные колонки
            bool hasKod = row.Table.Columns.Contains("Kod");
            bool hasArticul = row.Table.Columns.Contains("Articul");
            bool hasGrup = row.Table.Columns.Contains("Grup");
            bool hasMod = row.Table.Columns.Contains("Mod");

            // Логируем список доступных колонок (для отладки)
            Console.WriteLine("Доступные колонки в DataRow:");
            foreach (DataColumn col in row.Table.Columns)
            {
                Console.WriteLine($" {col.ColumnName}");
            }

            return new MyDataART
            {
                Kod = hasKod && row["Kod"] != DBNull.Value ? Convert.ToInt32(row["Kod"]) : 0,
                Articul = hasArticul && row["Articul"] != DBNull.Value ? row["Articul"].ToString() : string.Empty,
                Group = hasGrup && row["Grup"] != DBNull.Value ? row["Grup"].ToString() : string.Empty,
                Model = hasMod && row["Mod"] != DBNull.Value ? row["Mod"].ToString() : string.Empty,
                IsChecked = false // Default value
            };
        }
        private async Task MyDataAnnLoad()
        {
            try
            {
                int kod = GetSelectedKodFromGrid(customGridControl1);
                List<ArtNormN> artNormNs = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);

                if (artNormNs != null)
                {
                    var relatedMyDataAnn = ConvertToMyDataAnn(artNormNs);
                    customGridControl3.DataSource = relatedMyDataAnn;
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            }

            //BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();
            //// Заполняем myDataList данными из DataTable 
            //foreach (MyDataANN row in relatedData)
            //{
            //    try
            //    {
            //        myDataList.Add(new MyDataANN
            //        {
            //            AnnId = row.AnnId,
            //            Kod = row.Kod,
            //            Articul = row.Articul,
            //            Status = row.Status,
            //            Stat = row.Stat,
            //            Group = row.Group,
            //            Model = row.Model,
            //            IsChecked = false
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            //        MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    ;
            //}
            //customGridControl2.DataSource = myDataList;
        }
        /// <summary>
        /// Загружает список разделений труда (РТ) для указанного артикула или кода.
        /// </summary>
        /// <param name="kod">Код артикула</param>
        /// <param name="articul">Название артикула</param>
        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        /// 
        private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        {
            try
            {
                List<ArtNormN> relatedData;
                bool loadAll = loadAllCheckBox.Checked;
                // Если включен чекбокс "Загрузить все"
                //relatedData = //ConvertDataTableToList<ArtNormN>(await _artNormService.GetArtNormDataCurrent(kod, loadAll));
                relatedData = await _artNormService.GetArtNormDataCurrent(kod, loadAll);
                if (!loadAll)
                { // Если артикул содержит "-", фильтруем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
                        if (partialData != null)
                            relatedData.AddRange(partialData);
                    }
                }

                BindingList<MyDataANN> myDataList = ConvertToMyDataAnn(relatedData);

                await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
                return myDataList;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new BindingList<MyDataANN>(); // Возвращаем пустой список в случае ошибки
            }

        }

        private int GetSelectedKodFromGrid(GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                return Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod"));
            }
            return 0;
        }
        async Task NormRaszLoad()
        {
            int annId = 0;
            var view = customGridControl2.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
            }
            var relatedRasz = await _artNormService.GetRelatedNormRasz(annId);
            normraszBindingSource1.DataSource = relatedRasz;
            customGridControl3.DataSource = normraszBindingSource1;
        }

        ///// <summary>
        ///// загрузка norm_rasz
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private async void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView8, e.FocusedRowHandle, "annId", 0);
            // await LoadRelatedData(annId);
            await GridHelper.LoadGridControlDataAsync(customGridControl6, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));

            //int annId = 0;
            //var view = gridView8;//customGridControl2.MainView as GridView;
            //if (view != null)
            //{
            //    annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "AnnId"));
            //}

            //var relatedData = _artNormService.GetRelatedNormRasz(annId);
            //normraszBindingSource1.DataSource = relatedData;
            //customGridControl3.DataSource = normraszBindingSource1;

        }

        private async Task bindArticulToNewRow(int selectedAnnId, int newId)
        {
            var oldRaszList = await _artNormService.GetRelatedNormRasz(selectedAnnId);
            //           await _artNormService.UpdateAnnId("norm_rasz", selectedAnnId.AnnID, "annId", newId.AnnID);
            foreach (var rasz in oldRaszList)
            {
                var copy = CloneHelper.CloneAndAssignNewAnnId(rasz, newId, TableNames.RaszId);
                await _artNormService.SaveEntityAsync(TableNames.Rasz, TableNames.RaszId, copy);
            }
            await _artNormService.UpdateFieldAsync(TableNames.Rask, "annId", newId, "annId", selectedAnnId);
            await _artNormService.UpdateFieldAsync(TableNames.Kont,"annId", newId, "annId", selectedAnnId);
            await _artNormService.UpdateFieldAsync(TableNames.Obr, "annId", newId, "annId", selectedAnnId);

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
            SafeInvoke(view.GridControl, () =>
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
            });
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


        /// <summary>
        /// Привязывает выбранные артикулы к выбранному разделению труда (РТ).
        /// </summary>
        private async void BindButton_Click(object sender, EventArgs e)
        {
            try
            {
                GridView artView = customGridControl1.MainView as GridView;
                GridView annView = customGridControl2.MainView as GridView;

                if (artView == null || annView == null)
                {
                    MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int selectedArt = -1;
                int selectedAnn = -1;
                MyDataART selectedArtRow = null;
                MyDataANN selectedAnnRow = null;
                // Получаем выбранные артикулы
                BindingList<MyDataART> artDataSource = artView.DataSource as BindingList<MyDataART>;
                foreach (var row in artDataSource)
                {
                    if (row.IsChecked)
                    {
                        selectedArt = row.Kod;
                        selectedArtRow = row;
                        break;
                    }
                }

                // Получаем выбранное разделение труда
                BindingList<MyDataANN> annDataSource = annView.DataSource as BindingList<MyDataANN>;
                foreach (var row in annDataSource)
                {
                    if (row.IsChecked)
                    {
                        selectedAnn = row.AnnId;
                        selectedAnnRow = row;
                        break;
                    }
                }

                // Проверяем, выбраны ли оба элемента
                if (selectedArt == -1 || selectedAnn == -1)
                {
                    MessageBox.Show("Выберите артикул и разделение труда для привязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Подтверждение привязки
                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите привязать артикул {selectedArtRow.Articul.TrimEnd()} к разделению труда {selectedAnnRow.Articul.TrimEnd()}?",
                    "Подтверждение привязки",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) return;

                // Выполняем привязку через сервис
                _artNormService.UpdateAnnIdinArticul(selectedArt, selectedAnn);

                // Обновляем UI
                if (selectedArtRow != null && selectedAnnRow != null)
                {
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
                    artView.RefreshData();
                    annView.RefreshData();
                    customGridControl1.MainView.RefreshData();
                    // customGridControl1.MainView;

                    customGridControl2.MainView.RefreshData();
                }

                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArt} привязан к РТ {selectedAnn}");
                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SearchArticlesButton_Click(object sender, EventArgs e)
        {
            // TODO: Поиск по артикулам
        }

        private void ButtonAddArticle_Click(object sender, EventArgs e)
        {
            // TODO: Добавление нового артикула
        }

        private void ButtonEditArticle_Click(object sender, EventArgs e)
        {
            // TODO: Редактирование артикула
        }

        private void ButtonDeleteArticle_Click(object sender, EventArgs e)
        {
            // TODO: Удаление артикула
        }

        private void ANNgridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // TODO: обработка изменения выделенной строки артикула
        }
    }
}
