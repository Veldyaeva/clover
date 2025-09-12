using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Extensions;
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

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork : CustomForm
    {
        private void LoadGridSettings()
        {
            try
            {
                // Загружаем настройки размера и положения формы
                _formSettingsHelper.LoadFormSettings(this, "TeamWorkFormLayout.xml");

                // Включаем автоматическое сохранение настроек для всех CustomGridControl
                this.EnableAutoGridSettings(true);

                // Загружаем настройки для обычных GridControl (не CustomGridControl)
                _gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                _gridHelper.LoadGridViewSettings(gridView1, "gridView1Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView4, "gridView4Layout.xml");
                _gridHelper.LoadGridViewSettings(normRaszTab, "gridView6Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView_unboundArts, "gridView_unboundArtsLayout.xml");
                _gridHelper.LoadGridViewSettings(gridView_wdToBind, "gridView_wdToBindLayout.xml");
                _gridHelper.LoadGridViewSettings(gridViewPreArch, "gridViewPreArchLayout.xml");
                _gridHelper.LoadGridViewSettings(gridViewNZP, "gridViewNZPLayout.xml");
                _gridHelper.LoadGridViewSettings(gridView_binded, "gridView_bindedLayout.xml");

                // Добавляем другие гриды, если они есть
                if (gridViewRaskrTW != null)
                    _gridHelper.LoadGridViewSettings(gridViewRaskrTW, "gridViewRaskrTWLayout.xml");
                if (gridView5 != null)
                    _gridHelper.LoadGridViewSettings(gridView5, "gridView5Layout.xml");
                if (normKontTab != null)
                    _gridHelper.LoadGridViewSettings(normKontTab, "gridView2Layout.xml");
                if (normRaskArt != null)
                    _gridHelper.LoadGridViewSettings(normRaskArt, "gridView3Layout.xml");
                if (gridView8 != null)
                    _gridHelper.LoadGridViewSettings(gridView8, "gridView8Layout.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек интерфейса");
            }
        }
        private void SaveGridSettings()
        {
            try
            {
                // Сохраняем настройки размера и положения формы
                _formSettingsHelper.SaveFormSettings(this, "TeamWorkFormLayout.xml");

                // Сохраняем настройки split container
        //        _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl2, "splitContainer2Layout.xml");

   
                this.SaveAllGridSettings();

                // Сохраняем настройки для обычных GridControl (не CustomGridControl)
                _gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                _gridHelper.SaveGridViewSettings(gridView1, "gridView1Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView4, "gridView4Layout.xml");
                _gridHelper.SaveGridViewSettings(normRaszTab, "gridView6Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView_unboundArts, "gridView_unboundArtsLayout.xml");
                _gridHelper.SaveGridViewSettings(gridView_wdToBind, "gridView_wdToBindLayout.xml");
                _gridHelper.SaveGridViewSettings(gridViewPreArch, "gridViewPreArchLayout.xml");
                _gridHelper.SaveGridViewSettings(gridViewNZP, "gridViewNZPLayout.xml");
                _gridHelper.SaveGridViewSettings(gridView_binded, "gridView_bindedLayout.xml");

                // Добавляем другие гриды, если они есть
                if (gridViewRaskrTW != null)
                    _gridHelper.SaveGridViewSettings(gridViewRaskrTW, "gridViewRaskrTWLayout.xml");
                if (gridView5 != null)
                    _gridHelper.SaveGridViewSettings(gridView5, "gridView5Layout.xml");
                if (normKontTab != null)
                    _gridHelper.SaveGridViewSettings(normKontTab, "gridView2Layout.xml");
                if (normRaskArt != null)
                    _gridHelper.SaveGridViewSettings(normRaskArt, "gridView3Layout.xml");
                if (gridView8 != null)
                    _gridHelper.SaveGridViewSettings(gridView8, "gridView8Layout.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек интерфейса");
            }
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
                    // Условие: dateUpdate IS NULL OR дата dateUpdate = сегодня (без времени)
                    // Необходимо, чтоб пользователь видел записи, с которыми работал сегодня
                    var updateIsEmpty = new UnaryOperator(UnaryOperatorType.IsNull, new OperandProperty("dateUpdate"));
                    
                    // Сравниваем диапазон: от начала дня до конца дня
                    var todayStart = DateTime.Today; // 00:00:00
                    var todayEnd = DateTime.Today.AddDays(1).AddTicks(-1); // 23:59:59.9999999
                    
                    var updateIsToday = new GroupOperator(
                        GroupOperatorType.And,
                        new BinaryOperator("dateUpdate", todayStart, BinaryOperatorType.GreaterOrEqual),
                        new BinaryOperator("dateUpdate", todayEnd, BinaryOperatorType.LessOrEqual)
                    );
                    
                    var updateCondition = new GroupOperator(
                        GroupOperatorType.Or,
                        updateIsEmpty,
                        updateIsToday
                    );

                    var excludeArchived = new BinaryOperator("status", (int)Status.Archive, BinaryOperatorType.NotEqual);
                    archiveCheckBox.Checked = false;
                    archiveCheckBox.Enabled = false;
                    var notDescribedFilter = new GroupOperator(GroupOperatorType.And, updateCondition, excludeArchived);

                    if (statusCriteria != null)
                        statusCriteria = new GroupOperator(GroupOperatorType.And, statusCriteria, notDescribedFilter);
                    else
                        statusCriteria = notDescribedFilter;
                }
                else archiveCheckBox.Enabled = true;
                if (statusCriteria != null)
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
                    new BinaryOperator("Status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                );
            }

            return null;
        }
        #region Загрузка данных LoadGridControlData

        private async void LoadGridImage(PictureBox pictureBox, int? annId = null, int? kod = null)
        {
            string imagePath = null;
            try
            {
                imagePath = await _artNormService.GetImage(annId, kod);
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
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения по пути '{imagePath ?? "NULL"}' для annId = {annId}");
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
                
                // Автоматически переходим на первую строку результатов фильтрации
                //view.BeginInvoke(new Action(() =>
                //{
                    try
                    {
                        if (view.DataRowCount > 0)
                        {
                            int firstVisibleRow = view.GetVisibleRowHandle(0);
                            if (view.IsValidRowHandle(firstVisibleRow))
                            {
                                view.FocusedRowHandle = firstVisibleRow;
                                view.MakeRowVisible(firstVisibleRow);
                                
                                // Логируем действие
                                _logger?.LogEventAsync($"Автоматический переход на первую строку после применения фильтра по annId {_annId}. Всего строк: {view.DataRowCount}", "LoadGridControlData");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogErrorAsync(ex, "Ошибка при автоматическом переходе на первую строку после применения фильтра по annId");
                    }
              //  }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка фильтрации данных для annId = {_annId}");
            }
        }
        #endregion

        public static MyDataANN ToMyDataANN(ArtNormN ann)
        {
            if (ann == null) return null;
            return new MyDataANN
            {
                AnnID = ann.AnnID,
                //Kod = ann.Kod,
                Articul = ann.Articul,
                Status = ann.Status,
                grup = ann.grup,
                mod = ann.Mod,
                dateUpdate = ann.dateUpdate,
            };
        }

    }
}
