//using DevExpress.XtraEditors;
//using DevExpress.XtraGrid.Views.Grid;
//using SewingProduction.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SewingProduction.form;
//using SewingProduction.Interfaces;
//using SewingProduction.Models;
//using SewingProduction.Services;
//using DevExpress.Data.Filtering;
//using System.Windows.Forms;
//using System.Data;
//using DevExpress.XtraGrid;
//using System.ComponentModel;

//namespace SewingProduction.Forms
//{
//    public partial class TeamWork : CustomForm
//    {
//        private void LoadGridSettings()
//        {
//            // Load split container settings
//            _splitContainerHelper.LoadSplitContainerSettings(splitContainerControl3, "splitContainer3Layout.xml");

//            // Загружаем настройки для всех гридов
//            _gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
//            _gridHelper.LoadGridViewSettings(gridView1, "gridView1Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridView4, "gridView4Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridView6, "gridView6Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridView_unboundArts, "gridView7Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridView_wdToBind, "gridView8Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridViewPreArch, "gridView9Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridViewNZP, "gridView10Layout.xml");
////            _gridHelper.LoadGridViewSettings(gridView11, "gridView11Layout.xml");
//            _gridHelper.LoadGridViewSettings(gridView_binded, "gridView12Layout.xml");
//            //_gridHelper.LoadGridViewSettings(gridControl_unboundArts.MainView as GridView, "customGridControl1Layout.xml");
//            //_gridHelper.LoadGridViewSettings(gridControl_wdToBind.MainView as GridView, "customGridControl2Layout.xml");
//            //_gridHelper.LoadGridViewSettings(customGridControl3.MainView as GridView, "customGridControl3Layout.xml");
//            //_gridHelper.LoadGridViewSettings(gridControlPreArch.MainView as GridView, "customGridControl4Layout.xml");
//            //_gridHelper.LoadGridViewSettings(gridControlNZP.MainView as GridView, "customGridControl5Layout.xml");
//            //_gridHelper.LoadGridViewSettings(gridControl_binded.MainView as GridView, "customGridControl6Layout.xml");
//        }
//        private void SaveGridSettings()
//        {
//            _gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
//            _gridHelper.SaveGridViewSettings(gridView1, "gridView1Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridView4, "gridView4Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridView6, "gridView6Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridView_unboundArts, "gridView7Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridView_wdToBind, "gridView8Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridViewPreArch, "gridView9Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridViewNZP, "gridView10Layout.xml");
////            _gridHelper.SaveGridViewSettings(gridView11, "gridView11Layout.xml");
//            _gridHelper.SaveGridViewSettings(gridView_binded, "gridView12Layout.xml");
//            //_gridHelper.SaveGridViewSettings(gridControl_unboundArts.MainView as GridView, "customGridControl1Layout.xml");
//            //_gridHelper.SaveGridViewSettings(gridControl_wdToBind.MainView as GridView, "customGridControl2Layout.xml");
//            //_gridHelper.SaveGridViewSettings(customGridControl3.MainView as GridView, "customGridControl3Layout.xml");
//            //_gridHelper.SaveGridViewSettings(gridControlPreArch.MainView as GridView, "customGridControl4Layout.xml");
//            //_gridHelper.SaveGridViewSettings(gridControlNZP.MainView as GridView, "customGridControl5Layout.xml");
//            //_gridHelper.SaveGridViewSettings(gridControl_binded.MainView as GridView, "customGridControl6Layout.xml");

//            // Save split container settings
//            _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl2, "splitContainer2Layout.xml");
//            _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl3, "splitContainer3Layout.xml");
//        }
//        /// <summary>
//        /// Применяет фильтры к данным в gridView3
//        /// </summary>
//        private void filterTable()
//        {
//            try
//            {
//                // Сохраняем текущий фильтр поиска, если он есть
//                CriteriaOperator searchFilter = null;
//                if (ANNgridView.ActiveFilterCriteria is GroupOperator groupFilter)
//                {
//                    // Проверяем, есть ли фильтр поиска в группе операторов
//                    foreach (var criteria in groupFilter.Operands)
//                    {
//                        if (criteria is FunctionOperator functionOp &&
//                            functionOp.OperatorType == FunctionOperatorType.Contains)
//                        {
//                            searchFilter = criteria;
//                            break;
//                        }
//                    }
//                }
//                else if (ANNgridView.ActiveFilterCriteria is FunctionOperator functionFilter &&
//                         functionFilter.OperatorType == FunctionOperatorType.Contains)
//                {
//                    searchFilter = functionFilter;
//                }

//                // Создаем фильтры на основе состояния чекбоксов
//                CriteriaOperator statusCriteria = null;
//                GroupOperator statusGroup = null;

//                // Создаем фильтр по статусу
//                if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
//                {
//                    var statusFilters = new List<CriteriaOperator>();

//                    if (preliminaryCheckBox.Checked)
//                        statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

//                    if (actualCheckBox.Checked)
//                    {
//                        statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
//                        statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
//                    }

//                    if (archiveCheckBox.Checked)
//                        statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

//                    if (statusFilters.Count > 1)
//                    {
//                        statusGroup = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
//                        statusCriteria = statusGroup;
//                    }
//                    else if (statusFilters.Count == 1)
//                    {
//                        statusCriteria = statusFilters[0];
//                    }
//                }

//                // Добавляем фильтр по "Не описанные" если выбран
//                if (SortBox.Checked)
//                {
//                    var notDescribedFilter = new GroupOperator(
//                        GroupOperatorType.And,
//                        new BinaryOperator("sek_shv", 0),
//                        new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
//                    );

//                    if (statusCriteria != null)
//                    {
//                        statusCriteria = new GroupOperator(
//                            GroupOperatorType.And,
//                            statusCriteria,
//                            notDescribedFilter
//                        );
//                    }
//                    else
//                    {
//                        statusCriteria = notDescribedFilter;
//                    }
//                }

//                // Если есть и фильтр поиска, и фильтр статуса
//                if (searchFilter != null && statusCriteria != null)
//                {
//                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
//                        GroupOperatorType.And,
//                        searchFilter,
//                        statusCriteria
//                    );
//                }
//                else if (searchFilter != null)
//                {
//                    // Только фильтр поиска
//                    ANNgridView.ActiveFilterCriteria = searchFilter;
//                }
//                else if (statusCriteria != null)
//                {
//                    // Только фильтр статуса
//                    ANNgridView.ActiveFilterCriteria = statusCriteria;
//                }
//                else
//                {
//                    // Нет фильтров
//                    ANNgridView.ActiveFilterString = string.Empty;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogErrorAsync(ex, "Ошибка при применении фильтра");
//            }
//        }

//        private CriteriaOperator GetStatusFilter()
//        {
//            // Создаем фильтры на основе состояния чекбоксов
//            if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
//            {
//                var statusFilters = new List<CriteriaOperator>();

//                if (preliminaryCheckBox.Checked)
//                    statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

//                if (actualCheckBox.Checked)
//                {
//                    statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
//                    statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
//                }
//                if (archiveCheckBox.Checked)
//                    statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

//                if (statusFilters.Count > 1)
//                {
//                    return new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
//                }
//                else if (statusFilters.Count == 1)
//                {
//                    return statusFilters[0];
//                }
//            }

//            // Добавляем фильтр по "Не описанные" если выбран
//            if (SortBox.Checked)
//            {
//                return new GroupOperator(
//                    GroupOperatorType.And,
//                    new BinaryOperator("sek_shv", 0),
//                    new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
//                );
//            }

//            return null;
//        }
//        #region Загрузка данных LoadGridControlData

//        /// <summary>
//        /// Загружает изображение в PictureBox по идентификатору разделения труда.
//        /// </summary>
//        /// <param name="pictureBox">Целевой PictureBox</param>
//        /// <param name="kod">Идентификатор разделения труда</param>
//        private async void LoadGridControlData(PictureBox pictureBox, int kod)
//        {
//            string imagePath = null;
//            try
//            {
//                imagePath = await _artNormService.GetImage(kod);
//                if (!string.IsNullOrEmpty(imagePath))
//                {
//                    pictureBox.ImageLocation = imagePath;
//                }
//                else
//                {
//                    pictureBox.Image = null;
//                }
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения по пути '{imagePath ?? "NULL"}' для kod = {kod}");
//                pictureBox.Image = null;
//            }
//        }

//        /// <summary>
//        /// Применяет фильтр к GridView на основе annId.
//        /// </summary>
//        /// <param name="grid">GridControl, в котором нужно применить фильтр</param>
//        /// <param name="source">источник данных</param>
//        /// <param name="_annId">Идентификатор разделения труда</param>
//        private async void LoadGridControlData(GridControl grid, BindingSource source, int _annId)
//        {
//            try
//            {
//                string filter = "annId = " + _annId;
//                GridView view = (GridView)grid.Views[0];

//                view.BeginUpdate();
//                view.ActiveFilterString = filter;
//                view.EndUpdate();
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка фильтрации данных для annId = {_annId}");
//            }
//        }
//        #endregion
//        BindingList<MyDataANN> ConvertToMyDataAnn(List<ArtNormN> _relatedData)
//        {
//            // Преобразуем List<ArtNormN> в BindingList<MyDataANN>
//            BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

//            foreach (var item in _relatedData)
//            {
//                myDataList.Add(new MyDataANN
//                {
//                    AnnId = item.AnnID,
//                    Kod = item.Kod,
//                    Articul = item.Articul,
//                    Status = item.Status,
//                    grup = item.grup,
//                    Model = item.Mod,
//                    IsChecked = false
//                });
//            }

//            return myDataList;
//        }

//    }
//}
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.form;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid;
using System.ComponentModel;

namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private void LoadGridSettings()
        {
            // Load split container settings

            // Загружаем настройки для всех гридов
            _gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
            _gridHelper.LoadGridViewSettings(gridView1, "gridView1Layout.xml");
            _gridHelper.LoadGridViewSettings(gridView4, "gridView4Layout.xml");
            _gridHelper.LoadGridViewSettings(gridView6, "gridView6Layout.xml");
            _gridHelper.LoadGridViewSettings(gridView_unboundArts, "gridView7Layout.xml");
            _gridHelper.LoadGridViewSettings(gridView_wdToBind, "gridView8Layout.xml");
            _gridHelper.LoadGridViewSettings(gridViewPreArch, "gridView9Layout.xml");
            _gridHelper.LoadGridViewSettings(gridViewNZP, "gridView10Layout.xml");
            _gridHelper.LoadGridViewSettings(gridView_binded, "gridView12Layout.xml");
        }
        private void SaveGridSettings()
        {
            _gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
            _gridHelper.SaveGridViewSettings(gridView1, "gridView1Layout.xml");
            _gridHelper.SaveGridViewSettings(gridView4, "gridView4Layout.xml");
            _gridHelper.SaveGridViewSettings(gridView6, "gridView6Layout.xml");
            _gridHelper.SaveGridViewSettings(gridView_unboundArts, "gridView7Layout.xml");
            _gridHelper.SaveGridViewSettings(gridView_wdToBind, "gridView8Layout.xml");
            _gridHelper.SaveGridViewSettings(gridViewPreArch, "gridView9Layout.xml");
            _gridHelper.SaveGridViewSettings(gridViewNZP, "gridView10Layout.xml");
            //            _gridHelper.SaveGridViewSettings(gridView11, "gridView11Layout.xml");
            _gridHelper.SaveGridViewSettings(gridView_binded, "gridView12Layout.xml");
            // Save split container settings
            _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl2, "splitContainer2Layout.xml");
        }
        /// <summary>
        /// Применяет фильтры к данным в gridView3
        /// </summary>
        private void filterTable()
        {
            try
            {
                // Сохраняем текущий фильтр поиска, если он есть
                CriteriaOperator searchFilter = null;
                if (ANNgridView.ActiveFilterCriteria is GroupOperator groupFilter)
                {
                    // Проверяем, есть ли фильтр поиска в группе операторов
                    foreach (var criteria in groupFilter.Operands)
                    {
                        if (criteria is FunctionOperator functionOp &&
                            functionOp.OperatorType == FunctionOperatorType.Contains)
                        {
                            searchFilter = criteria;
                            break;
                        }
                    }
                }
                else if (ANNgridView.ActiveFilterCriteria is FunctionOperator functionFilter &&
                         functionFilter.OperatorType == FunctionOperatorType.Contains)
                {
                    searchFilter = functionFilter;
                }

                // Создаем фильтры на основе состояния чекбоксов
                CriteriaOperator statusCriteria = null;
                GroupOperator statusGroup = null;

                // Создаем фильтр по статусу
                if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
                {
                    var statusFilters = new List<CriteriaOperator>();

                    if (preliminaryCheckBox.Checked)
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                    if (actualCheckBox.Checked)
                    {
                        statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                    }

                    if (archiveCheckBox.Checked)
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

                    if (statusFilters.Count > 1)
                    {
                        statusGroup = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                        statusCriteria = statusGroup;
                    }
                    else if (statusFilters.Count == 1)
                    {
                        statusCriteria = statusFilters[0];
                    }
                }

                // Добавляем фильтр по "Не описанные" если выбран
                if (SortBox.Checked)
                {
                    var notDescribedFilter = new GroupOperator(
                        GroupOperatorType.And,
                        new BinaryOperator("sek_shv", 0),
                        new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                    );

                    if (statusCriteria != null)
                    {
                        statusCriteria = new GroupOperator(
                            GroupOperatorType.And,
                            statusCriteria,
                            notDescribedFilter
                        );
                    }
                    else
                    {
                        statusCriteria = notDescribedFilter;
                    }
                }

                // Если есть и фильтр поиска, и фильтр статуса
                if (searchFilter != null && statusCriteria != null)
                {
                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
                        GroupOperatorType.And,
                        searchFilter,
                        statusCriteria
                    );
                }
                else if (searchFilter != null)
                {
                    // Только фильтр поиска
                    ANNgridView.ActiveFilterCriteria = searchFilter;
                }
                else if (statusCriteria != null)
                {
                    // Только фильтр статуса
                    ANNgridView.ActiveFilterCriteria = statusCriteria;
                }
                else
                {
                    // Нет фильтров
                    ANNgridView.ActiveFilterString = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при применении фильтра");
            }
        }

        private CriteriaOperator GetStatusFilter()
        {
            // Создаем фильтры на основе состояния чекбоксов
            if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
            {
                var statusFilters = new List<CriteriaOperator>();

                if (preliminaryCheckBox.Checked)
                    statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                if (actualCheckBox.Checked)
                {
                    statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                    statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                }
                if (archiveCheckBox.Checked)
                    statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

                if (statusFilters.Count > 1)
                {
                    return new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                }
                else if (statusFilters.Count == 1)
                {
                    return statusFilters[0];
                }
            }

            // Добавляем фильтр по "Не описанные" если выбран
            if (SortBox.Checked)
            {
                return new GroupOperator(
                    GroupOperatorType.And,
                    new BinaryOperator("sek_shv", 0),
                    new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                );
            }

            return null;
        }
        #region Загрузка данных LoadGridControlData

        /// <summary>
        /// Загружает изображение в PictureBox по идентификатору разделения труда.
        /// </summary>
        /// <param name="pictureBox">Целевой PictureBox</param>
        /// <param name="kod">Идентификатор разделения труда</param>
        private async void LoadGridControlData(PictureBox pictureBox, int kod)
        {
            string imagePath = null;
            try
            {
                imagePath = await _artNormService.GetImage(kod);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    pictureBox.ImageLocation = imagePath;
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения по пути '{imagePath ?? "NULL"}' для kod = {kod}");
                pictureBox.Image = null;
            }
        }

        /// <summary>
        /// Применяет фильтр к GridView на основе annId.
        /// </summary>
        /// <param name="grid">GridControl, в котором нужно применить фильтр</param>
        /// <param name="source">источник данных</param>
        /// <param name="_annId">Идентификатор разделения труда</param>
        private async void LoadGridControlData(GridControl grid, BindingSource source, int _annId)
        {
            try
            {
                string filter = "annId = " + _annId;
                GridView view = (GridView)grid.Views[0];

                view.BeginUpdate();
                view.ActiveFilterString = filter;
                view.EndUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка фильтрации данных для annId = {_annId}");
            }
        }
        #endregion
    }
}
