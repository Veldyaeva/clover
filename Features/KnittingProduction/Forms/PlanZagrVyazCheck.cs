using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using SewingProduction.Core.helpers;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Helpers.GridHelper;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyazCheck : CustomForm, IThemeable
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly SprMonthService _sprMonthService;
        private readonly VyazService _vyazService;

        //private CancellationTokenSource? _loadCts;
        private readonly DebouncedLoader _loader = new(delayMs: 300);

        private BindingSource _sprMonthBindingSource;
        private BindingSource _podrVyazBindingSource;
        private BindingSource _pzvСheckBindingSource;


        public PlanZagrVyazCheck()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();

            _sprMonthService = new SprMonthService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);

            ThemeManager.UpdateTheme(this);
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                #region comboBoxMonthList
                _sprMonthBindingSource = new BindingSource
            {
                DataSource = new BindingList<SprMonth>()
            };

            comboBoxMonthList.DataSource = _sprMonthBindingSource;
            comboBoxMonthList.SelectedIndex = -1;
            comboBoxMonthList.ValueMember = "kod";
            comboBoxMonthList.DisplayMember = "name_cl";
            #endregion

            #region comboBoxPodrVyazList
            _podrVyazBindingSource = new BindingSource
            {
                DataSource = new BindingList<PodrVyaz>()
            };

            comboBoxPodrVyazList.DataSource = _podrVyazBindingSource;
            comboBoxPodrVyazList.SelectedIndex = -1;
            comboBoxPodrVyazList.ValueMember = "kod_vyaz";
            comboBoxPodrVyazList.DisplayMember = "text_vyaz";
            #endregion

            #region описание gridControlVyazPlan "оперативное планирование"
            _pzvСheckBindingSource = new BindingSource
            {
                DataSource = new BindingList<PzvCheck>()
            };
            gridControlPzvCheck.DataSource = _pzvСheckBindingSource;
            gridPzvCheckColumnTabTab.FieldName = "tabTab";
            gridPzvCheckColumnTabFio.FieldName = "tabFioSokr";
            gridPzvCheckColumnTabFio.Width = 90;
            gridViewPzvCheck.OptionsView.EnableAppearanceEvenRow = false;
            gridViewPzvCheck.OptionsView.EnableAppearanceOddRow = false;

            gridViewPzvCheck.OptionsView.RowAutoHeight = true;


            _gridHelper.AutoRowFilterConfig(gridViewPzvCheck as GridView, 1);

                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private void CreateDayColumns(GridView _gridView)
        {
            try
            {
                _gridView.BeginUpdate();
                _gridView.GroupSummary.Clear();

                string _xGridName = _gridView.Name;
                int _xMonth = Convert.ToInt32(comboBoxMonthList.SelectedValue);
                int _xYear = Convert.ToInt32(spinEditYear.Value);
                if (_xMonth == 0 || _xYear == 0)
                {
                    _xMonth = DateTime.Now.Month;
                    _xYear = DateTime.Now.Year;
                }
                DateTime currentDate = DateTime.Now;
                int daysInMonth = DateTime.DaysInMonth(_xYear, _xMonth);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    GridColumn dayColumn = new GridColumn();
                    string fieldName = $"pzvTab{day.ToString("00")}";
                    dayColumn.Name = $"{_xGridName}ColumnPzvTab{day.ToString("00")}";
                    dayColumn.FieldName = fieldName;
                    dayColumn.Caption = day.ToString();
                    dayColumn.Visible = true;
                    dayColumn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    dayColumn.OptionsColumn.AllowEdit = false;
                    _gridView.Columns.Add(dayColumn);

                    _gridView.GroupSummary.Add(
                        new GridGroupSummaryItem
                        {
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            FieldName = $"pzv{day.ToString("00")}",
                            ShowInGroupColumnFooter = dayColumn,
                            DisplayFormat = "0.00;-0.00;"
                        });
                    _gridView.GroupSummary.Add(
                        new GridGroupSummaryItem
                        {
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            FieldName = $"tab{day.ToString("00")}",
                            ShowInGroupColumnFooter = dayColumn,
                            DisplayFormat = "0.00;-0.00;"
                        });
                    //string _xNumFormat = "{0:0.00;-0.00;}";
                    ////string _xNumFormat = "{0:n2}";
                    //foreach (GridGroupSummaryItem gsi in _gridView.GroupSummary)
                    //{
                    //    gsi.DisplayFormat = _xNumFormat;
                    //}
                }
                GridColumn itogColumnTabChasiOf = new GridColumn();
                itogColumnTabChasiOf.FieldName = "tabChasiOf";
                itogColumnTabChasiOf.Caption = "Итого час по таб.";
                itogColumnTabChasiOf.Visible = true;
                itogColumnTabChasiOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
                itogColumnTabChasiOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                itogColumnTabChasiOf.OptionsColumn.AllowEdit = false;
                _gridView.Columns.Add(itogColumnTabChasiOf);
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.None,
                        FieldName = $"",
                        ShowInGroupColumnFooter = itogColumnTabChasiOf,
                        DisplayFormat = "0.00;-0.00;"
                    });
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        FieldName = $"tabChasiOf",
                        ShowInGroupColumnFooter = itogColumnTabChasiOf,
                        DisplayFormat = "0.00;-0.00;"
                    });

                GridColumn itogColumnPztChasiOf = new GridColumn();
                itogColumnPztChasiOf.FieldName = "pztChasiOf";
                itogColumnPztChasiOf.Caption = "Итого час по МЛ";
                itogColumnPztChasiOf.Visible = true;
                itogColumnPztChasiOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
                itogColumnPztChasiOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                itogColumnPztChasiOf.OptionsColumn.AllowEdit = false;
                _gridView.Columns.Add(itogColumnPztChasiOf);
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        FieldName = $"pztChasiOf",
                        ShowInGroupColumnFooter = itogColumnPztChasiOf,
                        DisplayFormat = "0.00;-0.00;"
                    });
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.None,
                        FieldName = $"",
                        ShowInGroupColumnFooter = itogColumnPztChasiOf,
                        DisplayFormat = "0.00;-0.00;"
                    });

                GridColumn itogColumnProcentOf = new GridColumn();
                itogColumnProcentOf.FieldName = "procentOf";
                itogColumnProcentOf.Caption = "% выраб.";
                itogColumnProcentOf.Visible = true;
                itogColumnProcentOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
                itogColumnProcentOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                itogColumnProcentOf.OptionsColumn.AllowEdit = false;
                _gridView.Columns.Add(itogColumnProcentOf);
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.None,
                        FieldName = $"",
                        ShowInGroupColumnFooter = itogColumnProcentOf,
                        DisplayFormat = "0.00;-0.00;"
                    });
                _gridView.GroupSummary.Add(
                    new GridGroupSummaryItem
                    {
                        SummaryType = DevExpress.Data.SummaryItemType.None,
                        FieldName = $"",
                        ShowInGroupColumnFooter = itogColumnProcentOf,
                        DisplayFormat = "0.00;-0.00;"
                    });

                _gridView.BestFitColumns();
                _gridView.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка CreateDayColumns: {ex.Message}");
            }
        }
        private void RemoveDayColumns(GridView _gridView)
        {
            try
            {
                _gridView.BeginUpdate();

                if (_gridView == null) return;

                for (int i = _gridView.Columns.Count - 1; i >= 0; i--)
                {
                    GridColumn col = _gridView.Columns[i];
                    string fieldName = col.FieldName;

                    if (string.IsNullOrEmpty(fieldName))
                        continue;

                    // Дневные колонки pzvTab01..pzvTab31
                    if (fieldName.StartsWith("pzvTab"))
                    {
                        _gridView.Columns.RemoveAt(i);
                        continue;
                    }

                    // Итоговые колонки
                    if (fieldName == "tabChasiOf"
                        || fieldName == "pztChasiOf"
                        || fieldName == "procentOf")
                    {
                        _gridView.Columns.RemoveAt(i);
                    }
                }

                _gridView.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка RemoveDayColumns: {ex.Message}");
            }
        }
        private Task SetStatusAsync(string text)
            => this.UI(() => labelStatus.Text = text);

        private Task SetLoadingAsync(bool isLoading)
            => this.UI(() =>
            {
                labelStatus.Text = isLoading ? "Загрузка…" : "Готово";
                Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            });

        private async Task LoadSprMonthDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        await SetLoadingAsync(true);

                        // ❌ НЕ очищаем ComboBox здесь

                        var bs = await _sprMonthService.GetSprMonth(token);

                        await this.UI(() =>
                        {
                            comboBoxMonthList.BeginUpdate();
                            _sprMonthBindingSource.DataSource = bs.DataSource;
                            comboBoxMonthList.EndUpdate();
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadSprMonthDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки spr_month");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }

        private async Task LoadPodrVyazDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        await SetLoadingAsync(true);

                        await this.UI(() =>
                        {
                            comboBoxPodrVyazList.BeginUpdate();
                            _podrVyazBindingSource.DataSource = new BindingList<PodrVyaz>();
                            comboBoxPodrVyazList.EndUpdate();
                            comboBoxPodrVyazList.SelectedIndex = -1;
                        });

                        var bs = await _vyazService.GetPodrVyaz(token);

                        await this.UI(() =>
                        {
                            comboBoxPodrVyazList.BeginUpdate();
                            _podrVyazBindingSource.DataSource = bs.DataSource;
                            comboBoxPodrVyazList.EndUpdate();
                            comboBoxPodrVyazList.SelectedIndex = -1;
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadPodrVyazDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки podr_vyaz");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }

        private async Task LoadPzvCheckDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        gridViewPzvCheck.ShowLoadingPanel();
                        await SetLoadingAsync(true);

                        //await this.UI(() =>
                        //{
                        //    gridControlPzvCheck.BeginUpdate();
                        //    _podrVyazBindingSource.DataSource = new BindingList<PodrVyaz>();
                        //    gridControlPzvCheck.EndUpdate();
                        //});
                        //MessageBox.Show($" {comboBoxPodrVyazList.SelectedIndex} ");
                        int _xPodrID = Convert.ToInt32(comboBoxPodrVyazList.SelectedValue);
                        int _xMonth = Convert.ToInt32(comboBoxMonthList.SelectedValue);
                        int _xYear = Convert.ToInt32(spinEditYear.Value);
                        var bs = await _vyazService.GetPzvCheck(_xPodrID, _xMonth, _xYear, token);

                        await this.UI(() =>
                        {
                            gridControlPzvCheck.BeginUpdate();
                            _pzvСheckBindingSource.DataSource = bs.DataSource;
                            gridControlPzvCheck.EndUpdate();
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                        gridViewPzvCheck.HideLoadingPanel();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadPzvCheckDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки pzvCheck");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }
        private async void PlanZagrVyazCheck_Load(object sender, EventArgs e)
        {
            //InitializeBindings();
            try
            {
                RemoveDayColumns(gridViewPzvCheck);
                CreateDayColumns(gridViewPzvCheck);
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);


                await LoadSprMonthDataAsync();

                comboBoxMonthList.SelectedValue = DateTime.Now.Month;
                spinEditYear.Value = DateTime.Now.Year;

                await LoadPodrVyazDataAsync();
                comboBoxPodrVyazList.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка PlanZagrVyazCheck_Load: {ex.Message}");
            }
        }

        private async void comboBoxMonthList_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _loader.CancelUser();          // ⛔ отменяем предыдущие загрузки
                RemoveDayColumns(gridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                CreateDayColumns(gridViewPzvCheck);
                //await LoadPodrVyazDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка comboBoxMonthList_SelectedValueChanged: {ex.Message}");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            { 
                _loader.CancelLifetime();
                _loader.Dispose();
                base.OnFormClosed(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка OnFormClosed: {ex.Message}");
            }
        }

        private void simpleButtonPrevMonth_Click(object sender, EventArgs e)
        {
            try
            { 
                DateTime _prevMonth = Convert.ToDateTime($"01.{comboBoxMonthList.SelectedValue}.{spinEditYear.Value}");
                _prevMonth = _prevMonth.AddMonths(-1);

                comboBoxMonthList.SelectedValue = _prevMonth.Month;
                spinEditYear.Value = _prevMonth.Year;

                RemoveDayColumns(gridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                CreateDayColumns(gridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка simpleButtonPrevMonth_Click: {ex.Message}");
            }
        }

        private void simpleButtonNextMonth_Click(object sender, EventArgs e)
        {
            try
            { 
                DateTime _nextMonth = Convert.ToDateTime($"01.{comboBoxMonthList.SelectedValue}.{spinEditYear.Value}");
                _nextMonth = _nextMonth.AddMonths(1);

                comboBoxMonthList.SelectedValue = _nextMonth.Month;
                spinEditYear.Value = _nextMonth.Year;

                RemoveDayColumns(gridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                CreateDayColumns(gridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка simpleButtonNextMonth_Click: {ex.Message}");
            }
        }

        private void buttonGetPzvCheck_Click(object sender, EventArgs e)
        {
            try
            { 
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                else
                {
                    MessageBox.Show("Выберите подразеделение и повторите попытку");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка buttonGetPzvCheck_Click: {ex.Message}");
            }
        }

        private void comboBoxMonthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxPodrVyazList.SelectedIndex != -1)
            //{
            //    LoadPzvCheckDataAsync();
            //}
        }

        private void spinEditYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                RemoveDayColumns(gridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                CreateDayColumns(gridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка spinEditYear_ValueChanged: {ex.Message}");
            }
        }

        private void gridViewPzvCheck_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName.StartsWith("d"))
            {
                string dayNumber = e.Column.FieldName.Substring(1);
                if (int.TryParse(dayNumber, out int day) && day >= 1 && day <= 31)
                {
                    string markColumnName = $"dd{day.ToString("00")}"; ;
                    object markValue = gridViewPzvCheck.GetRowCellValue(e.RowHandle, markColumnName);

                    if (markValue != null)
                    {
                        string[] workingValue = { "1", "11", "8" };
                        bool IsWorkingDay = workingValue.Any(v => markValue.ToString().Contains(v));
                        bool IsHoliday = markValue.ToString().Contains("/3");
                        bool IsOrderBasedLeave = markValue.ToString().Contains("/4");
                        if (IsHoliday) { e.Appearance.BackColor = Color.FromArgb(128, 128, 255); }
                        else
                        {
                            if (IsOrderBasedLeave) { e.Appearance.BackColor = Color.FromArgb(255, 128, 64); }
                            else
                            {
                                if (IsWorkingDay) { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
                                else
                                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
                            }
                        }
                    }
                }
            }
        }

    }
}