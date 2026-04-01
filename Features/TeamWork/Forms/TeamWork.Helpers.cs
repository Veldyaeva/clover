using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using SewingProduction.Core.Extensions;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork : CustomForm
    {
        #region GridSettings
        private void LoadGridSettings()
        {
            try
            {
                // Загружаем настройки размера и положения формы
                _formSettingsHelper.LoadFormSettings(this, "TeamWorkFormLayout.xml");

                // Включаем автоматическое сохранение настроек для всех CustomGridControl
                this.EnableAutoGridSettings(true);

                //// Загружаем настройки для обычных GridControl (не CustomGridControl)
                //_gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                //_gridHelper.LoadGridViewSettings(gridViewRaszTW, "gridView1Layout.xml");
                //_gridHelper.LoadGridViewSettings(gridViewKontTW, "gridView4Layout.xml");
                //_gridHelper.LoadGridViewSettings(normRaszTab, "gridView6Layout.xml");
                //_gridHelper.LoadGridViewSettings(gridView_unboundArts, "gridView_unboundArtsLayout.xml");
                //_gridHelper.LoadGridViewSettings(gridView_wdToBind, "gridView_wdToBindLayout.xml");
                //_gridHelper.LoadGridViewSettings(gridViewPreArch, "gridViewPreArchLayout.xml");
                //_gridHelper.LoadGridViewSettings(gridViewNZP, "gridViewNZPLayout.xml");
                //_gridHelper.LoadGridViewSettings(gridView_binded, "gridView_bindedLayout.xml");

                //// Добавляем другие гриды, если они есть
                //if (gridViewRaskrTW != null)
                //    _gridHelper.LoadGridViewSettings(gridViewRaskrTW, "gridViewRaskrTWLayout.xml");
                //if (gridViewBindedArts != null)
                //    _gridHelper.LoadGridViewSettings(gridViewBindedArts, "gridView5Layout.xml");
                //if (normKontTab != null)
                //    _gridHelper.LoadGridViewSettings(normKontTab, "gridView2Layout.xml");
                //if (normRaskArt != null)
                //    _gridHelper.LoadGridViewSettings(normRaskArt, "gridView3Layout.xml");
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

                //// Сохраняем настройки для обычных GridControl (не CustomGridControl)
                //_gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                //_gridHelper.SaveGridViewSettings(gridViewRaszTW, "gridView1Layout.xml");
                //_gridHelper.SaveGridViewSettings(gridViewKontTW, "gridView4Layout.xml");
                //_gridHelper.SaveGridViewSettings(normRaszTab, "gridView6Layout.xml");
                //_gridHelper.SaveGridViewSettings(gridView_unboundArts, "gridView_unboundArtsLayout.xml");
                //_gridHelper.SaveGridViewSettings(gridView_wdToBind, "gridView_wdToBindLayout.xml");
                //_gridHelper.SaveGridViewSettings(gridViewPreArch, "gridViewPreArchLayout.xml");
                //_gridHelper.SaveGridViewSettings(gridViewNZP, "gridViewNZPLayout.xml");
                //_gridHelper.SaveGridViewSettings(gridView_binded, "gridView_bindedLayout.xml");

                //// Добавляем другие гриды, если они есть
                //if (gridViewRaskrTW != null)
                //    _gridHelper.SaveGridViewSettings(gridViewRaskrTW, "gridViewRaskrTWLayout.xml");
                //if (gridViewBindedArts != null)
                //    _gridHelper.SaveGridViewSettings(gridViewBindedArts, "gridView5Layout.xml");
                //if (normKontTab != null)
                //    _gridHelper.SaveGridViewSettings(normKontTab, "gridView2Layout.xml");
                //if (normRaskArt != null)
                //    _gridHelper.SaveGridViewSettings(normRaskArt, "gridView3Layout.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек интерфейса");
            }
        }
        /// <summary>
        /// Настраивает базовые параметры гридов
        /// </summary>
        private void InitializeGridSettings()
        {
            // Настройка гридов (общие настройки, не связанные с данными DataSource)
            if (gridControl_unboundArts?.MainView is GridView unboundArtsView)
            {
                ConfigureGridSelection(unboundArtsView, "unboundArts");
                unboundArtsView.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
                unboundArtsView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
            }

            if (gridControl_wdToBind?.MainView is GridView wdToBindView)
            {
                ConfigureGridSelection(wdToBindView, "wdToBind");
                wdToBindView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(gridControl_wdToBind, e);
            }

        }
        /// <summary>
        /// Применяет базовые настройки к гриду (сброс фильтров, сортировки и восстановление стандартных настроек)
        /// </summary>
        private void ApplyBaseGridSettings(GridView gridView, string gridName = "")
        {
            if (gridView == null) return;

            try
            {
                gridView.BeginUpdate();

                // Сбрасываем фильтры и сортировку
                gridView.ActiveFilter.Clear();
                gridView.ClearSorting();
                gridView.ClearGrouping();
                // Очищаем строку поиска
                gridView.ActiveFilterString = string.Empty;

                // Очищаем быстрый поиск (если есть)
                if (gridView.FindFilterText != null)
                {
                    gridView.FindFilterText = string.Empty;
                }

                // Очищаем автофильтры в колонках
                foreach (GridColumn column in gridView.Columns)
                {
                    column.FilterInfo = new ColumnFilterInfo();
                }

                // Применяем базовые настройки отображения
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.OptionsView.EnableAppearanceOddRow = true;
                gridView.OptionsView.ShowAutoFilterRow = true;
                gridView.OptionsView.ShowGroupPanel = false;
                gridView.OptionsView.ShowIndicator = true;

                // Для основного грида ANNgridView - специальные настройки
                if (gridView == ANNgridView)
                {
                    //gridView.OptionsView.ShowPreview = true;
                    //gridView.PreviewLineCount = 1;

                    // Восстанавливаем базовую сортировку
                    if (gridView.Columns["AnnID"] != null)
                    {
                        gridView.Columns["AnnID"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    }

                    // Восстанавливаем настройки колонки dateUpdate
                    RestoreDateUpdateColumnSettings(gridView);
                }

                // Настройки выбора для разных гридов
                ConfigureGridSelection(gridView, gridName);
            }
            finally
            {
                gridView.EndUpdate();
            }
        }

        #endregion

        #region Header Buttons


        private void InitHeaderButtonTags()
        {
            // layoutTkans — «Разделения труда»
            TagByCaption(layoutControlGroup8, new (string caption, string tag)[] {
                ("Добавить предварительное", "wd:add-prelim"),
                ("Редактировать РТ",          "wd:edit"),
                ("Дубль",                     "wd:clone"),
                ("Архив РТ",                  "wd:archive"),
                ("Печать",                    "wd:print"),
                ("Печать+",                   "wd:print-plus"),
            });

            // layoutControlGroup14 — блок увязки
            TagByCaption(layoutControlGroup14, new[] {
                ("Увязать",                   "bind:link"),
                ("Отвязать",                  "bind:unlink"),
                ("Проставить утверждение",   "bind:touch-update-date"),
                (" все РТ",                  "bind:show-all") // это check-button
            });

            // layoutControlGroup2 — «РТ для увязки»
            // Две кнопки уже имеют теги в дизайнере: btnArch, btnArt. Добавим недостающие, если будут.
            TagByCaption(layoutControlGroup2, new[] {
                ("Архив+копия",              "rt:arch-and-copy"),   // будет проставлен, если Tag ещё пуст
                ("Создать из артикула",      "rt:create-from-article")
            });

            // layoutControlGroup19 — Архив
            TagByCaption(layoutControlGroup19, new[] {
                ("Вернуть в актуальные",     "arch:restore") // если такая подпись есть
            });

            // Если есть group6 (создать из артикула) — можно пометить и её
            TagByCaption(layoutControlGroupPreArch, new[] {
                ("Р’ Р°СЂС…РёРІ",                   "prearch:archive")
            });

            if (layoutControlGroup6 != null)
                AutoTagAllButtons(layoutControlGroup6, "g6");
        }

        private void TagByCaption(LayoutControlGroup group, IEnumerable<(string caption, string tag)> map)
        {
            if (group == null || group.CustomHeaderButtons == null) return;

            foreach (var (caption, tag) in map)
            {
                var btn = group.CustomHeaderButtons
                               .OfType<GroupBoxButton>()
                               .FirstOrDefault(b => string.Equals(b.Caption, caption, StringComparison.OrdinalIgnoreCase));
                if (btn != null && (btn.Tag == null || string.IsNullOrWhiteSpace(btn.Tag.ToString())))
                {
                    btn.Tag = tag;
                }
            }
        }

        /// <summary>
        /// Проставляет авто-теги всем кнопкам, у которых Tag ещё пуст (slug из подписи)
        /// </summary>
        /// <param name="group"></param>
        /// <param name="prefix"></param>
        private void AutoTagAllButtons(LayoutControlGroup group, string prefix = null)
        {
            if (group == null || group.CustomHeaderButtons == null) return;
            foreach (var btn in group.CustomHeaderButtons.OfType<GroupBoxButton>())
            {
                if (btn.Tag != null && !string.IsNullOrWhiteSpace(btn.Tag.ToString())) continue;
                var slug = MakeSlug(btn.Caption);
                btn.Tag = string.IsNullOrEmpty(prefix) ? slug : $"{prefix}:{slug}";
            }
        }
        private static string MakeSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            var chars = text.Trim()
                            .ToLowerInvariant()
                            .Select(ch => char.IsLetterOrDigit(ch) ? ch : '-')
                            .ToArray();
            var raw = new string(chars);
            while (raw.Contains("--")) raw = raw.Replace("--", "-");
            return raw.Trim('-');
        }
        /// <summary>
        /// Поиск кнопки по тегу
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static GroupBoxButton FindButtonByTag(LayoutControlGroup group, string tag) =>
    group?.CustomHeaderButtons?.OfType<GroupBoxButton>()
         .FirstOrDefault(b => string.Equals(b.Tag as string, tag, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Grid Selection and Filters
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

        /// <summary>
        /// Восстанавливает фокус на записи с указанным AnnID
        /// </summary>
        private async Task<bool> RestoreFocusAsync(int annId)
        {
            try
            {
                if (TryFocusAndRefreshRowByAnnId(ANNgridView, annId))
                {
                    // Загружаем связанные данные для восстановленной записи
                    await LoadRelatedData(annId);
                    await _logger.LogEventAsync($"Фокус восстановлен на AnnID: {annId}", "RestoreFocus");
                    return true;
                }
                else
                {
                    await _logger.LogEventAsync($"Запись с AnnID: {annId} не найдена после перезагрузки", "RestoreFocus");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при восстановлении фокуса на AnnID: {annId}");
                return false;
            }
        }
        /// <summary>
        /// Настраивает параметры выбора для грида
        /// </summary>
        private void ConfigureGridSelection(GridView gridView, string gridName)
        {
            gridView.OptionsSelection.MultiSelect = false;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            // Можно добавить специфические настройки для разных гридов
            switch (gridName.ToLower())
            {
                case "unboundarts":
                case "wdtobind":
                    // Дополнительные настройки для этих гридов
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Применяет базовые фильтры к основному гриду
        /// </summary>
        private void ApplyBaseFilters()
        {
            try
            {
                if (ANNgridView == null) return;

                ANNgridView.BeginUpdate();

                // Применяем существующий метод фильтрации, если он есть
                filterTable();
            }
            finally
            {
                ANNgridView.EndUpdate();
            }
        }


        #endregion

        #region Загрузка данных LoadGridControlData

        private async Task LoadGridImage(PictureBox pictureBox, int? annId = null, int? kod = null)
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
        private async Task LoadGridControlData(GridControl grid, BindingSource source, int _annId)
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

        /// <summary>
        /// Проверяет конфигурацию всех гридов после инициализации
        /// </summary>
        private void VerifyGridConfigurations()
        {
            try
            {
                // Verify normRaskArt grid configuration
                if (customGridControl2?.MainView is GridView normRaskArtView)
                {
                    _logger?.LogEventAsync($"VerifyGridConfigurations: normRaskArt has {normRaskArtView.Columns.Count} columns", "VerifyGridConfigurations");
                    foreach (GridColumn col in normRaskArtView.Columns)
                    {
                        _logger?.LogEventAsync($"VerifyGridConfigurations: normRaskArt column '{col.Name}' - FieldName: '{col.FieldName}', Visible: {col.Visible}, Width: {col.Width}", "VerifyGridConfigurations");
                    }

                    // Verify data source binding
                    _logger?.LogEventAsync($"VerifyGridConfigurations: normRaskArt DataSource: {normRaskArtView.GridControl?.DataSource}", "VerifyGridConfigurations");

                    // Verify grid visibility and accessibility
                    _logger?.LogEventAsync($"VerifyGridConfigurations: customGridControl2.Visible={customGridControl2.Visible}, Enabled={customGridControl2.Enabled}", "VerifyGridConfigurations");
                    _logger?.LogEventAsync($"VerifyGridConfigurations: customGridControl2.Parent={customGridControl2.Parent?.Name}, Parent.Visible={customGridControl2.Parent?.Visible}", "VerifyGridConfigurations");
                }
                else
                {
                    _logger?.LogWarningAsync("VerifyGridConfigurations: customGridControl2 or its MainView is null", "VerifyGridConfigurations");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Error in VerifyGridConfigurations");
            }
        }


        #endregion

        #region Search and Restore
        /// <summary>
        /// Восстанавливает исходные данные после очистки поиска
        /// </summary>
        private async Task RestoreOriginalData()
        {
            try
            {
                bool showAll = showAllWD;
                //var showAll = ;
                //if (showAll != null)
                //{
                //    showAll.Checked;
                //}
                // Восстанавливаем данные для gridView_unboundArts
                await MyDataArtLoad();

                // Восстанавливаем данные для gridView_wdToBind в зависимости от состояния showAllWD
                if (showAll)
                {
                    var allWorks = await LoadWorksbyArt("");
                    if (allWorks != null)
                    {
                        // Заполняем текстовый статус для каждой записи
                        foreach (var item in allWorks)
                        {
                            item.Stat = StatusHelper.GetStatusText(item.Status);
                        }

                        _myDataAnnList.Clear();
                        _myDataAnnList.RaiseListChangedEvents = false;
                        foreach (var item in allWorks)
                        {
                            _myDataAnnList.Add(item);
                        }
                        _myDataAnnList.RaiseListChangedEvents = true;
                        _myDataAnnBindingSource.ResetBindings(false);
                    }
                }
                else
                {
                    // Если showAllWD не отмечен, очищаем данные
                    _myDataAnnList.Clear();
                    _myDataAnnBindingSource.ResetBindings(false);
                }

                gridView_wdToBind.RefreshData();
                gridView_unboundArts.RefreshData();

                await _logger.LogEventAsync("RestoreOriginalData: Исходные данные восстановлены", "RestoreOriginalData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при восстановлении исходных данных");
            }
        }
        /// <summary>
        /// Обработчик нажатия клавиш в searchControl1 для поиска артикулов (основная вкладка)
        /// </summary>
        private async void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await SearchArticulesByText();
            }
        }

        /// <summary>
        /// Обработчик изменения текста в searchControl1 для обновления при очистке
        /// </summary>
        private async void searchControl1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchControl = sender as SearchControl;
                if (searchControl == null) return;

                // Если текст очищен, возвращаем исходные данные
                if (string.IsNullOrEmpty(searchControl.Text?.Trim()))
                {
                    await _logger.LogEventAsync("searchControl1: Текст очищен, восстанавливаем исходные данные", "searchControl1_TextChanged");
                    await RestoreOriginalData();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в searchControl1_TextChanged");
            }
        }
        #endregion

        #region EditForm Management
        // Управление немодальной формой TeamWork_AdvanceTW
        private static readonly List<TeamWork_AdvanceTW> _openAdvanceForms = new List<TeamWork_AdvanceTW>();
        private static readonly object _lockObject = new object();

        private static bool HasOpenAdvanceForms()
        {
            lock (_lockObject)
            {
                // Очищаем закрытые формы из списка
                _openAdvanceForms.RemoveAll(form => form == null || form.IsDisposed);
                return _openAdvanceForms.Count > 0;
            }
        }

        /// <summary>
        /// Добавляет экземпляр TeamWork_AdvanceTW в список открытых форм
        /// </summary>
        /// <param name="form">Форма для добавления</param>
        private static void AddOpenAdvanceForm(TeamWork_AdvanceTW form)
        {
            lock (_lockObject)
            {
                if (form != null && !form.IsDisposed && !_openAdvanceForms.Contains(form))
                {
                    _openAdvanceForms.Add(form);
                }
            }
        }

        /// <summary>
        /// Удаляет экземпляр TeamWork_AdvanceTW из списка открытых форм
        /// </summary>
        /// <param name="form">Форма для удаления</param>
        private static void RemoveOpenAdvanceForm(TeamWork_AdvanceTW form)
        {
            lock (_lockObject)
            {
                _openAdvanceForms.Remove(form);
            }
        }

        /// <summary>
        /// Открывает TeamWork_AdvanceTW не в модальном режиме с проверкой на уже открытые экземпляры
        /// </summary>
        /// <param name="bufferWorkDivision">ID из буфера</param>
        /// <param name="mode">Режим работы</param>
        /// <param name="newId">ID новой записи</param>
        /// <param name="oldId">ID исходной записи</param>
        /// <param name="sourceAnnIdToCopyDetailsFrom">ID источника для копирования деталей</param>
        /// <param name="initialArtData">Начальные данные артикула</param>
        /// <param name="duplicateAnnData">Данные для дублирования</param>
        /// <returns>Созданная форма или существующая форма если уже есть открытые экземпляры</returns>
        public TeamWork_AdvanceTW OpenAdvanceFormNonModal(int bufferWorkDivision, int mode, int? newId = null, int? oldId = null, int? sourceAnnIdToCopyDetailsFrom = null, MyDataART initialArtData = null, ArtNormN duplicateAnnData = null)
        {
            try
            {
                // Проверяем, есть ли уже открытые экземпляры
                if (HasOpenAdvanceForms())
                {
                    // Получаем первый открытый экземпляр
                    TeamWork_AdvanceTW existingForm = null;
                    lock (_lockObject)
                    {
                        existingForm = _openAdvanceForms.FirstOrDefault(form => form != null && !form.IsDisposed);
                    }

                    if (existingForm != null)
                    {
                        string articul = existingForm.CurrentArticul;
                        if (!string.IsNullOrEmpty(articul))
                        {
                            string msg = string.IsNullOrWhiteSpace(articul) ? "Открыто разделение труда"
                            : $"Открыто разделение труда для артикула \"{articul}\"";

                            // Показать сплеш на 2 секунды
                            Task.Run(() =>
                            {
                                try
                                {
                                    SplashScreenHelper.ShowSplash(msg, textOnly: true);
                                    try
                                    {
                                        System.Threading.Thread.Sleep(2000);
                                    }
                                    catch
                                    {
                                        SplashScreenHelper.CloseSplash();
                                    }
                                }
                                catch (Exception splashEx)
                                {
                                    _ = _logger?.LogErrorAsync(splashEx, "OpenAdvanceFormNonModal: ошибка показа сплеша");
                                    SplashScreenHelper.CloseSplash();
                                }
                                finally
                                {
                                    SplashScreenHelper.CloseSplash();
                                }
                            });
                        }
                        SplashScreenHelper.CloseSplash();
                        existingForm.WindowState = FormWindowState.Normal;
                        existingForm.BringToFront();
                        existingForm.Activate();
                    }

                    return null;
                }
            }
            catch (ObjectDisposedException ode)
            {
                try
                {
                    _ = _logger?.LogErrorAsync(ode, "OpenAdvanceFormNonModal: обнаружены освобождённые формы, очищаю список и продолжаю");
                    lock (_lockObject)
                    {
                        _openAdvanceForms.RemoveAll(form => form == null || form.IsDisposed);
                    }
                }
                catch (Exception ex)
                {
                    _ = _logger?.LogErrorAsync(ex, "OpenAdvanceFormNonModal: ошибка при очистке списка форм после ObjectDisposedException");
                }
            }
            catch (InvalidOperationException ioe)
            {
                _ = _logger?.LogErrorAsync(ioe, "OpenAdvanceFormNonModal: ошибка при проверке открытых форм");
            }
            catch (Exception ex)
            {
                _ = _logger?.LogErrorAsync(ex, "OpenAdvanceFormNonModal: непредвиденная ошибка при проверке открытых форм");
            }


            var advanceForm = new TeamWork_AdvanceTW(_user, bufferWorkDivision, mode, newId, oldId, sourceAnnIdToCopyDetailsFrom, initialArtData, duplicateAnnData);

            // Добавляем в список открытых форм
            AddOpenAdvanceForm(advanceForm);

            // Подписываемся на событие закрытия формы
            advanceForm.FormClosed += (sender, e) =>
            {
                RemoveOpenAdvanceForm(advanceForm);
            };

            // Открываем форму не в модальном режиме
            advanceForm.Show();

            return advanceForm;
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

        #region Mode Management

        /// <summary>
        /// Обработчик изменения режима работы (обычный/комплект)
        /// </summary>
        private void ModeRadio_CheckedChanged_internal(object sender, EventArgs e)
        {
            try
            {
                if (!toggleSwitchKit.IsOn)
                {
                    SetNormalMode();
                }
                else if (toggleSwitchKit.IsOn)
                {
                    SetKitMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переключении режима: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Устанавливает обычный режим работы
        /// </summary>
        private void SetNormalMode()
        {
            // Отключаем множественный выбор
            ANNgridView.OptionsSelection.MultiSelect = false;
            ANNgridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            // Используем VisibleLogic для программного управления видимостью
            ButtonEditWd.VisibleLogic = true;
            ButtonEditOnlyAdv.VisibleLogic = true;
            ButtonArchAndCopyWd.VisibleLogic = true;
            ButtonDouble.VisibleLogic = true;
            ButtonCopyWd.VisibleLogic = true;

            // Скрываем элементы режима комплекта
            KITlabel.VisibleLogic = false;
            ButtonPreliminaryWd.VisibleLogic = false;

            // Управляем видимостью LayoutControlItem для режима комплекта
            if (layoutControlItem5 != null) // ButtonPreliminaryWd
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (layoutControlItem6 != null) // KITlabel
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            editBtns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // Устанавливаем тексты кнопок
            ButtonPreliminaryWd.Text = "добавить предварительное РТ";
            ButtonCopyWd.Text = "копировать РТ в буфер";

            // Очищаем буфер при переключении режима
            TeamWorkBuffer.ClearBuffer();

            // Взаимоисключаем кнопки редактирования: если доступна расширенная, скрываем обычную
            EnforceEditButtonsExclusivity();
        }

        /// <summary>
        /// Устанавливает режим создания комплекта
        /// </summary>
        private void SetKitMode()
        {
            // Включаем множественный выбор
            ANNgridView.OptionsSelection.MultiSelect = true;
            ANNgridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            // Используем VisibleLogic для программного управления видимостью
            // Скрываем большинство кнопок
            ButtonEditWd.VisibleLogic = false;
            ButtonEditOnlyAdv.VisibleLogic = false;
            ButtonArchAndCopyWd.VisibleLogic = false;
            ButtonDouble.VisibleLogic = false;

            // Показываем элементы режима комплекта
            ButtonPreliminaryWd.VisibleLogic = true;
            ButtonCopyWd.VisibleLogic = true;
            KITlabel.VisibleLogic = true;

            // Управляем видимостью LayoutControlItem для режима комплекта
            if (layoutControlItem5 != null) // ButtonPreliminaryWd
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (layoutControlItem6 != null) // KITlabel
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            editBtns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // Настраиваем внешний вид
            KITlabel.ForeColor = Color.Red;
            ButtonCopyWd.Text = "копировать комплект в буфер";
            ButtonPreliminaryWd.Text = "создать комплект";

            // Очищаем буфер при переключении режима
            TeamWorkBuffer.ClearBuffer();

            // На всякий случай поддержим консистентность взаимной видимости
            EnforceEditButtonsExclusivity();
        }

        #endregion

        /// <summary>
        /// Делает кнопки редактирования взаимоисключающимися:
        /// если видна расширенная (ButtonEditOnlyAdv), скрывает обычную (ButtonEditWd);
        /// иначе показывает обычную при наличии прав.
        /// </summary>
        private void EnforceEditButtonsExclusivity()
        {
            try
            {
                if (ButtonEditOnlyAdv == null || ButtonEditWd == null) return;

                // Итоговая видимость учитывает и права, и логику
                bool advancedVisible = ButtonEditOnlyAdv.Visible;
                if (advancedVisible)
                {
                    ButtonEditWd.VisibleLogic = false;
                }
                // Если расширенная скрыта — не трогаем состояние обычной, оно может управляться режимом
            }
            catch (Exception ex)
            {
                _ = _logger?.LogErrorAsync(ex, "EnforceEditButtonsExclusivity");
            }
        }

    }
    public static class DemoHelper
    {

        public static Image GetDeleteImage()
        {
            return GetImage(Brushes.Red);
        }

        public static Image GetEditImage()
        {
            return GetImage(Brushes.Green);
        }

        public static Image GetImage(Brush b)
        {
            Image img = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(b, new Rectangle(0, 0, img.Width - 1, img.Height - 1));
            }
            return img;
        }
    }

}
