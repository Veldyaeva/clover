using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Core.Services;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingProductionPlanning : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        private readonly ProrabotkiService _prorabotkiService;
        private readonly MatrixService _matrixService;
        public FormManager _formManager;
        private List<VyazPlanView> _currentVyazPlanViewData = new List<VyazPlanView>();
        private BindingList<VyazPlanView> _vyazPlanViewBindingList;
        private BindingSource _vyazPlanViewBindingSource;
        private List<VyazPlanView> vyazPlanViewData = new List<VyazPlanView>();
        private List<ArtPrFioProgr> _currentArtPrFioProgrViewData = new List<ArtPrFioProgr>();
        private BindingList<ArtPrFioProgr> _artPrFioProgrViewBindingList;
        private BindingSource _artPrFioProgrViewBindingSource;
        private List<PlanSezonZadanyView> _currentPlanSezonZadanyViewData = new List<PlanSezonZadanyView>();
        private BindingList<PlanSezonZadanyView> _planSezonZadanyViewBindingList;
        private BindingSource _planSezonZadanyViewBindingSource;
        private List<KnitMachineList> _currentKnitMachineListData = new List<KnitMachineList>();
        private BindingList<KnitMachineList> _knitMachineListBindingList;
        private BindingSource _knitMachineListBindingSource;

        private List<PlanSezonZadKnitMachineList> _currentPlanSezonZadKnitMachineListData = new List<PlanSezonZadKnitMachineList>();
        private BindingList<PlanSezonZadKnitMachineList> _planSezonZadKnitMachineListBindingList;
        private BindingSource _planSezonZadKnitMachineListBindingSource;

        private List<ArtPrKnitMachineView> _currentArtPrKnitMachineViewPr1Data = new List<ArtPrKnitMachineView>();
        private BindingList<ArtPrKnitMachineView> _artPrKnitMachineViewPr1BindingList;
        private BindingSource _artPrKnitMachineViewPr1BindingSource;

        private List<ArtPrKnitMachineView> _currentArtPrKnitMachineViewPr2Data = new List<ArtPrKnitMachineView>();
        private BindingList<ArtPrKnitMachineView> _artPrKnitMachineViewPr2BindingList;
        private BindingSource _artPrKnitMachineViewPr2BindingSource;

        private List<ArtPrKnitMachineView> _currentArtPrKnitMachineViewRecom1Data = new List<ArtPrKnitMachineView>();
        private BindingList<ArtPrKnitMachineView> _artPrKnitMachineViewRecom1BindingList;
        private BindingSource _artPrKnitMachineViewRecom1BindingSource;

        private List<ArtPrKnitMachineView> _currentArtPrKnitMachineViewRecom2Data = new List<ArtPrKnitMachineView>();
        private BindingList<ArtPrKnitMachineView> _artPrKnitMachineViewRecom2BindingList;
        private BindingSource _artPrKnitMachineViewRecom2BindingSource;


        public KnittingProductionPlanning()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();
            _prorabotkiService = new ProrabotkiService(_dbHelper);
            _matrixService = new MatrixService(_dbHelper);

            Form mainForm = Application.OpenForms["SpMainForm"];
            MenuStrip mainMenu = mainForm.MainMenuStrip;
            _formManager = new FormManager(mainForm, mainMenu, _user);

            ThemeManager.UpdateTheme(this);
        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                var vyazPlanViewTask = Task.Run(() =>
                {
                    _vyazPlanViewBindingList = new BindingList<VyazPlanView>();
                    _vyazPlanViewBindingSource = new BindingSource { DataSource = _vyazPlanViewBindingList };
                });
                var artPrFioProgrTask = Task.Run(() =>
                {
                    _artPrFioProgrViewBindingList = new BindingList<ArtPrFioProgr>();
                    _artPrFioProgrViewBindingSource = new BindingSource { DataSource = _artPrFioProgrViewBindingList };
                });
                var planSezonZadanyTask = Task.Run(() =>
                {
                    _planSezonZadanyViewBindingList = new BindingList<PlanSezonZadanyView>();
                    _planSezonZadanyViewBindingSource = new BindingSource { DataSource = _planSezonZadanyViewBindingList };
                });
                var knitMachineListTask = Task.Run(() =>
                {
                    _knitMachineListBindingList = new BindingList<KnitMachineList>();
                    _knitMachineListBindingSource = new BindingSource { DataSource = _knitMachineListBindingList };
                });
                var artPrKnitMachineViewPr1Task = Task.Run(() =>
                {
                    _artPrKnitMachineViewPr1BindingList = new BindingList<ArtPrKnitMachineView>();
                    _artPrKnitMachineViewPr1BindingSource = new BindingSource { DataSource = _artPrKnitMachineViewPr1BindingList };
                });
                var artPrKnitMachineViewPr2Task = Task.Run(() =>
                {
                    _artPrKnitMachineViewPr2BindingList = new BindingList<ArtPrKnitMachineView>();
                    _artPrKnitMachineViewPr2BindingSource = new BindingSource { DataSource = _artPrKnitMachineViewPr2BindingList };
                });
                var artPrKnitMachineViewRecom1Task = Task.Run(() =>
                {
                    _artPrKnitMachineViewRecom1BindingList = new BindingList<ArtPrKnitMachineView>();
                    _artPrKnitMachineViewRecom1BindingSource = new BindingSource { DataSource = _artPrKnitMachineViewRecom1BindingList };
                });
                var artPrKnitMachineViewRecom2Task = Task.Run(() =>
                {
                    _artPrKnitMachineViewRecom2BindingList = new BindingList<ArtPrKnitMachineView>();
                    _artPrKnitMachineViewRecom2BindingSource = new BindingSource { DataSource = _artPrKnitMachineViewRecom2BindingList };
                });

                await Task.WhenAll(vyazPlanViewTask, artPrFioProgrTask, planSezonZadanyTask, knitMachineListTask
                        , artPrKnitMachineViewPr1Task, artPrKnitMachineViewPr2Task, artPrKnitMachineViewRecom1Task, artPrKnitMachineViewRecom2Task);

                #region описание comboBox "Список машин"
                comboBoxKnitMachineList.DataSource = _knitMachineListBindingSource;
                comboBoxKnitMachineList.SelectedIndex = -1;
                comboBoxKnitMachineList.ValueMember = "kmlID";
                comboBoxKnitMachineList.DisplayMember = "kmlNumber";
                #endregion

                #region описание gridControlVyazPlan "оперативное планирование"
                gridControlVyazPlan.DataSource = _vyazPlanViewBindingSource;
                gridColumnVyazPlanNn.FieldName = "Nn";
                gridColumnVyazPlanNomZad.FieldName = "NomZad";
                gridColumnVyazPlanDateZap.FieldName = "DateZap";
                gridColumnVyazPlanNameSbit.FieldName = "NameSbit";
                gridColumnVyazPlanDateCdPlan.FieldName = "DateCdPlan";
                gridColumnVyazPlanArticul.FieldName = "Articul";
                gridColumnVyazPlanGrup.FieldName = "Grup";
                gridColumnVyazPlanNameVyazClass.FieldName = "NameVyazClass";
                gridColumnVyazPlanSekVyaz.FieldName = "SekVyaz";
                gridColumnVyazPlanZvetTkan.FieldName = "ZvetTkan";
                gridColumnVyazPlanKol.FieldName = "Kol";
                gridColumnVyazPlanSekVyazAll.FieldName = "SekVyazAll";
                gridColumnVyazPlanSyncSelection.FieldName = "SyncSelection";
                gridColumnVyazPlanKmlNumber.FieldName = "KmlNumber";
                gridColumnVyazPlanPszkmPlanDateFrom.FieldName = "DateZapPlanFrom";
                gridColumnVyazPlanPszkmPlanDateTo.FieldName = "DateZapPlanTo";

                _gridHelper.AutoRowFilterConfig(gridViewVyazPlan as GridView, 1);

                // 1. Настраиваем стандартный MultiSelect
                gridViewVyazPlan.OptionsSelection.MultiSelect = true;  // Включаем множественный выбор
                gridViewVyazPlan.OptionsBehavior.AutoUpdateTotalSummary = true;
                gridViewVyazPlan.OptionsView.ShowIndicator = false;   // Скрываем стандартный индикатор
                //gridViewVyazPlan.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = false;
                //gridViewVyazPlan.OptionsSelection.ShowCheckBoxSelectorInGroupRow = false;

                // 2. Создаем кастомный столбец с чекбоксами
                //var checkColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                //checkColumn.FieldName = "IsSelected";
                //checkColumn.Caption = " ";
                //checkColumn.VisibleIndex = 0;  // Ставим на первое место
                //checkColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                gridViewVyazPlan.IndicatorWidth = 0;
                gridColumnVyazPlanSyncSelection.UnboundDataType = typeof(bool);

                // Создаем RepositoryItemCheckEdit для отображения чекбоксов
                //var checkEdit = new RepositoryItemCheckEdit();
                //gridControl1.RepositoryItems.Add(checkEdit);
                //checkColumn.ColumnEdit = checkEdit;

                //gridView1.Columns.Add(checkColumn);

                // 3. Обновляем состояние чекбоксов при выделении строк
                gridViewVyazPlan.SelectionChanged += (s, e) =>
                {
                    #region "старый обработчик"
                    //gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["kmlNumber"];
                    //gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["SyncSelection"];
                    //gridViewVyazPlan.BeginUpdate();
                    //try
                    //{
                    //    //for (int i = 0; i < gridViewVyazPlan.RowCount; i++)
                    //    //{
                    //    //    bool isSelected = gridViewVyazPlan.IsRowSelected(i);
                    //    //    gridViewVyazPlan.SetRowCellValue(i, gridColumnVyazPlanSyncSelection, isSelected);
                    //    //}
                    //    foreach (int rowHandle in gridViewVyazPlan.GetSelectedRows())
                    //    {
                    //        gridViewVyazPlan.SetRowCellValue(rowHandle, gridColumnVyazPlanSyncSelection, true);
                    //        gridViewVyazPlan.PostEditor();
                    //        //gridViewVyazPlan.UpdateCurrentRow();
                    //    }

                    //    // Сбрасываем чекбоксы у невыделенных строк
                    //    for (int i = 0; i < gridViewVyazPlan.RowCount; i++)
                    //    {
                    //        if (!gridViewVyazPlan.IsRowSelected(i))
                    //        {
                    //            gridViewVyazPlan.SetRowCellValue(i, gridColumnVyazPlanSyncSelection, false);
                    //            gridViewVyazPlan.PostEditor();
                    //            //gridViewVyazPlan.UpdateCurrentRow();
                    //        }
                    //    }
                    //}
                    //finally
                    //{
                    //    gridViewVyazPlan.EndUpdate();

                    //}
                    //gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["SyncSelection"];
                    #endregion
                    #region "новый обработчик"
                    // Устанавливаем фокус на нужные колонки
                    gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["kmlNumber"];
                    gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["SyncSelection"];

                    gridViewVyazPlan.BeginUpdate();
                    try
                    {
                        // Проверка выделения по IDVyazClass (новая логика)
                        if (gridViewVyazPlan.SelectedRowsCount > 0)
                        {
                            var firstSelectedId = gridViewVyazPlan.GetRowCellValue(gridViewVyazPlan.GetSelectedRows()[0], "IDVyazClass");
                            var invalidSelections = new List<int>();

                            foreach (int rowHandle in gridViewVyazPlan.GetSelectedRows())
                            {
                                var currentId = gridViewVyazPlan.GetRowCellValue(rowHandle, "IDVyazClass");
                                if (!object.Equals(currentId, firstSelectedId))
                                {
                                    invalidSelections.Add(rowHandle);
                                }
                            }

                            // Если есть недопустимые выделения, снимаем их
                            if (invalidSelections.Count > 0)
                            {
                                foreach (int rowHandle in invalidSelections)
                                {
                                    gridViewVyazPlan.UnselectRow(rowHandle);
                                }

                                XtraMessageBox.Show("Можно выделять только строки с одинаковым IDVyazClass",
                                                   "Ограничение выделения",
                                                   MessageBoxButtons.OK,
                                                   MessageBoxIcon.Information);
                                return; // Прерываем обработку, так как выделение изменилось
                            }
                        }

                        // Ваша оригинальная логика обновления SyncSelection
                        foreach (int rowHandle in gridViewVyazPlan.GetSelectedRows())
                        {
                            gridViewVyazPlan.SetRowCellValue(rowHandle, gridColumnVyazPlanSyncSelection, true);
                            gridViewVyazPlan.PostEditor();
                        }

                        // Сбрасываем чекбоксы у невыделенных строк
                        for (int i = 0; i < gridViewVyazPlan.RowCount; i++)
                        {
                            if (!gridViewVyazPlan.IsRowSelected(i))
                            {
                                gridViewVyazPlan.SetRowCellValue(i, gridColumnVyazPlanSyncSelection, false);
                                gridViewVyazPlan.PostEditor();
                            }
                        }
                    }
                    finally
                    {
                        gridViewVyazPlan.EndUpdate();
                        gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["SyncSelection"];
                    }
                    #endregion
                };

                // 4. Обрабатываем клик по чекбоксу для выделения/снятия строки
                gridViewVyazPlan.RowCellClick += (s, e) =>
                {
                    if (e.Column == gridColumnVyazPlanSyncSelection)
                    {
                        bool newValue = !(bool)(gridViewVyazPlan.GetRowCellValue(e.RowHandle, gridColumnVyazPlanSyncSelection) ?? false);
                        gridView1.SetRowCellValue(e.RowHandle, gridColumnVyazPlanSyncSelection, newValue);

                        if (newValue)
                        {
                            gridViewVyazPlan.SelectRow(e.RowHandle);
                        }
                        else
                        {
                            gridViewVyazPlan.UnselectRow(e.RowHandle);
                        }
                    }
                };

                //// Подписываемся на событие изменения данных
                //// В конструкторе или методе инициализации
                //gridViewVyazPlan.ActiveFilter.Nodes.CollectionChanged += (s, e) =>
                //{
                //    if (gridViewVyazPlan.RowCount > 0)
                //    {
                //        gridViewVyazPlan_FocusedRowChanged?.Invoke(
                //            gridViewVyazPlan,
                //            new FocusedRowChangedEventArgs(gridViewVyazPlan.FocusedRowHandle, -1)
                //        );
                //    }
                //};

                //// При программном изменении фильтра
                //gridViewVyazPlan.ActiveFilterCriteria = newCriteria;
                //gridViewVyazPlan.RefreshData();
                #endregion

                #region описание gridControlArtPrFioProgr "проработки"
                gridControlArtPrFioProgr.DataSource = _artPrFioProgrViewBindingSource;
                gridColumnArtPrFioProgrArticul.FieldName = "art_pr";
                gridColumnArtPrFioProgrRazm.FieldName = "razm";
                #endregion

                #region описание gridControlPlanSezonZadanyRazmKol "количество по размерам"
                gridControlPlanSezonZadanyRazmKol.DataSource = _planSezonZadanyViewBindingSource;
                gridColumnPlanSezonZadanyRazmKolRazm.FieldName = "razm";
                gridColumnPlanSezonZadanyRazmKolKol.FieldName = "kol";
                #endregion

                #region описание gridControlArtPrKnitMachineViewPr1 "в.м ПР основн."
                gridControlArtPrKnitMachineViewPr1.DataSource = _artPrKnitMachineViewPr1BindingSource;
                gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                #endregion
                #region описание gridControlArtPrKnitMachineViewPr2 "в.м ПР вспомог."
                gridControlArtPrKnitMachineViewPr2.DataSource = _artPrKnitMachineViewPr2BindingSource;
                gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                #endregion
                #region описание gridControlArtPrKnitMachineViewRecom1 "в.м ПР основн."
                gridControlArtPrKnitMachineViewRecom1.DataSource = _artPrKnitMachineViewRecom1BindingSource;
                gridColumnArtPrKnitMachineViewRecom1KmlNumber.FieldName = "kmlNumber";
                gridColumnArtPrKnitMachineViewRecom1AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                gridColumnArtPrKnitMachineViewRecom1AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                #endregion
                #region описание gridControlArtPrKnitMachineViewRecom2 "в.м ПР вспомог."
                gridControlArtPrKnitMachineViewRecom2.DataSource = _artPrKnitMachineViewRecom2BindingSource;
                gridColumnArtPrKnitMachineViewRecom2KmlNumber.FieldName = "kmlNumber";
                gridColumnArtPrKnitMachineViewRecom2AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                gridColumnArtPrKnitMachineViewRecom2AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                #endregion

                pictureBoxEskiz.DataBindings.Add("ImageLocation", _vyazPlanViewBindingSource, nameof(VyazPlanView.PictPath), true, DataSourceUpdateMode.Never);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }


        private async void KnittingProductionPlanning_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                await LoadVyazPlanDataAsync();

                // устанавливаем фильтр: запланированные задания, которые не распределены по В/М
                //_vyazPlanViewBindingSource.Filter = "DateZapPlanFrom is null";
                gridViewVyazPlan.ActiveFilter.Clear(); // очищаем старые фильтры

                gridViewVyazPlan.ActiveFilter.Add(
                    gridViewVyazPlan.Columns["DateZapPlanFrom"],
                    new ColumnFilterInfo("[DateZapPlanFrom] = null")
                );
                gridViewVyazPlan.SortInfo.Add(
                    new DevExpress.XtraGrid.Columns.GridColumnSortInfo(
                        gridViewVyazPlan.Columns["NomZad"],
                        DevExpress.Data.ColumnSortOrder.Ascending
                    )
                );
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingProductionPlanning");
            }
        }

        private async Task LoadVyazPlanDataAsync()
        {
            try
            {
                _vyazPlanViewBindingSource.Clear();
                _vyazPlanViewBindingSource.ResetBindings(false);
                //List<VyazPlanView> vyazPlanViewData = await _vyazService.GetVyazPlanView();
                //var vyazPlanViewData = await _vyazService.GetVyazPlanView();
                vyazPlanViewData = await _vyazService.GetVyazPlanView();
                //_vyazPlanViewBindingList = vyazPlanViewData;
                //_vyazPlanViewBindingList = await _vyazService.GetVyazPlanView();
                if (vyazPlanViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные VyazPlanView", "LoadVyazPlanDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentVyazPlanViewData = vyazPlanViewData;                // Обновляем текущую модель
                        _vyazPlanViewBindingSource.DataSource = _currentVyazPlanViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные VyazPlanView успешно загружены", "LoadVyazPlanDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _vyazPlanViewBindingList.Add(vyazPlanViewData[0]);
                    _vyazPlanViewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные VyazPlanView", "LoadVyazPlanDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных VyazPlanView");
            }
        }

        private async Task LoadArtPrFioProgrViewDataAsync(string nn)
        {
            try
            {
                _artPrFioProgrViewBindingSource.Clear();
                _artPrFioProgrViewBindingSource.ResetBindings(false);
                var artPrFioProgrViewData = await _prorabotkiService.GetArtPrFioProgr(nn, 1);
                if (artPrFioProgrViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtPrFioProgrView", "LoadArtPrFioProgrViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentArtPrFioProgrViewData = artPrFioProgrViewData;                // Обновляем текущую модель
                        _artPrFioProgrViewBindingSource.DataSource = _currentArtPrFioProgrViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ArtPrFioProgrView успешно загружены", "LoadArtPrFioProgrViewDataAsync");
                    _artPrFioProgrViewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtPrFioProgrView", "LoadArtPrFioProgrViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtPrFioProgrView");
            }
        }

        private async Task LoadPlanSezonZadanyViewDataAsync(string nomZad)
        {
            try
            {
                _planSezonZadanyViewBindingSource.Clear();
                _planSezonZadanyViewBindingSource.ResetBindings(false);
                var planSezonZadanyViewData = await _matrixService.GetPlanSezonZadanyRazmKolByNomZad(nomZad);
                if (planSezonZadanyViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные view_plan_sezon_zadany", "LoadPlanSezonZadanyViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentPlanSezonZadanyViewData = planSezonZadanyViewData;                // Обновляем текущую модель
                        _planSezonZadanyViewBindingSource.DataSource = _currentPlanSezonZadanyViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные view_plan_sezon_zadany успешно загружены", "LoadPlanSezonZadanyViewDataAsync");
                    _planSezonZadanyViewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные view_plan_sezon_zadany", "LoadPlanSezonZadanyViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных view_plan_sezon_zadany");
            }
        }
        private async Task LoadKnitMachineListDataAsync(int idVyazClass)
        {
            try
            {
                _knitMachineListBindingSource.Clear();
                _knitMachineListBindingSource.ResetBindings(false);
                var knitMachineListData = await _vyazService.GetKnitMachineListByClassID(idVyazClass);
                if (knitMachineListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные knitMachineList", "LoadKnitMachineListDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentKnitMachineListData = knitMachineListData;                // Обновляем текущую модель
                        _knitMachineListBindingSource.DataSource = _currentKnitMachineListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные knitMachineList успешно загружены", "LoadKnitMachineListDataAsync");
                    _knitMachineListBindingSource.ResetBindings(false);
                    comboBoxKnitMachineList.SelectedIndex = -1;
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные knitMachineList", "LoadKnitMachineListDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных knitMachineList");
            }
        }
        private async Task LoadArtPrKnitMachineViewPr1DataAsync(string nn)
        {
            try
            {
                _artPrKnitMachineViewPr1BindingSource.Clear();
                _artPrKnitMachineViewPr1BindingSource.ResetBindings(false);
                int typeVyazKM = 1;
                string vidVyazKM = "pr";
                var artPrKnitMachineViewPr1Data = await _vyazService.GetArtPrKnitMachineByKodMatr(nn, typeVyazKM, vidVyazKM);
                if (artPrKnitMachineViewPr1Data != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentArtPrKnitMachineViewPr1Data = artPrKnitMachineViewPr1Data;                // Обновляем текущую модель
                        _artPrKnitMachineViewPr1BindingSource.DataSource = _currentArtPrKnitMachineViewPr1Data; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ArtPrKnitMachineView успешно загружены", "LoadArtPrKnitMachineViewPr1DataAsync");
                    _artPrKnitMachineViewPr1BindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtPrKnitMachineView");
            }
        }
        private async Task LoadArtPrKnitMachineViewPr2DataAsync(string nn)
        {
            try
            {
                _artPrKnitMachineViewPr2BindingSource.Clear();
                _artPrKnitMachineViewPr2BindingSource.ResetBindings(false);
                int typeVyazKM = 2;
                string vidVyazKM = "pr";
                var artPrKnitMachineViewPr2Data = await _vyazService.GetArtPrKnitMachineByKodMatr(nn, typeVyazKM, vidVyazKM);
                if (artPrKnitMachineViewPr2Data != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentArtPrKnitMachineViewPr2Data = artPrKnitMachineViewPr2Data;                // Обновляем текущую модель
                        _artPrKnitMachineViewPr2BindingSource.DataSource = _currentArtPrKnitMachineViewPr2Data; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ArtPrKnitMachineView успешно загружены", "LoadArtPrKnitMachineViewPr1DataAsync");
                    _artPrKnitMachineViewPr2BindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtPrKnitMachineView");
            }
        }
        private async Task LoadArtPrKnitMachineViewRecom1DataAsync(string nn)
        {
            try
            {
                _artPrKnitMachineViewRecom1BindingSource.Clear();
                _artPrKnitMachineViewRecom1BindingSource.ResetBindings(false);
                int typeVyazKM = 1;
                string vidVyazKM = "recom";
                var artPrKnitMachineViewRecom1Data = await _vyazService.GetArtPrKnitMachineByKodMatr(nn, typeVyazKM, vidVyazKM);
                if (artPrKnitMachineViewRecom1Data != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentArtPrKnitMachineViewRecom1Data = artPrKnitMachineViewRecom1Data;                // Обновляем текущую модель
                        _artPrKnitMachineViewRecom1BindingSource.DataSource = _currentArtPrKnitMachineViewRecom1Data; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ArtPrKnitMachineView успешно загружены", "LoadArtPrKnitMachineViewPr1DataAsync");
                    _artPrKnitMachineViewRecom1BindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtPrKnitMachineView");
            }
        }
        private async Task LoadArtPrKnitMachineViewRecom2DataAsync(string nn)
        {
            try
            {
                _artPrKnitMachineViewRecom2BindingSource.Clear();
                _artPrKnitMachineViewRecom2BindingSource.ResetBindings(false);
                int typeVyazKM = 2;
                string vidVyazKM = "recom";
                var artPrKnitMachineViewRecom2Data = await _vyazService.GetArtPrKnitMachineByKodMatr(nn, typeVyazKM, vidVyazKM);
                if (artPrKnitMachineViewRecom2Data != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentArtPrKnitMachineViewRecom2Data = artPrKnitMachineViewRecom2Data;                // Обновляем текущую модель
                        _artPrKnitMachineViewRecom2BindingSource.DataSource = _currentArtPrKnitMachineViewRecom2Data; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ArtPrKnitMachineView успешно загружены", "LoadArtPrKnitMachineViewPr1DataAsync");
                    _artPrKnitMachineViewRecom2BindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtPrKnitMachineView", "LoadArtPrKnitMachineViewPr1DataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtPrKnitMachineView");
            }
        }
        private void LoadList<T>(List<T> sourceList, BindingList<T> targetList, string idFieldName)
        {
            foreach (var item in sourceList)
            {
                var idProp = typeof(T).GetProperty(idFieldName);
                targetList.Add(item);
            }
        }

        private void KnitMachineStatusUpdate(int rowHandle)
        {
            var selectedItem = (VyazPlanView)gridViewVyazPlan.GetRow(rowHandle);
            switch (selectedItem.pszkmID, selectedItem.KmlID)
            {
                case (0, 0):  // Оба ID = 0
                    selectedItem.IsNew = false;
                    selectedItem.IsModified = false;
                    selectedItem.IsDeleted = false;
                    break;

                case ( > 0, 0):  // xPszkmID > 0 и xKmlID = 0
                    selectedItem.IsNew = false;
                    selectedItem.IsModified = false;
                    selectedItem.IsDeleted = true;
                    break;

                case (0, > 0):  // xPszkmID = 0 и xKmlID > 0
                    selectedItem.IsNew = true;
                    selectedItem.IsModified = false;
                    selectedItem.IsDeleted = false;
                    break;

                case ( > 0, > 0):  // Оба ID > 0
                    selectedItem.IsNew = false;
                    selectedItem.IsModified = true;
                    selectedItem.IsDeleted = false;
                    break;

                default:  // Все остальные случаи (например, отрицательные значения)
                    Console.WriteLine($"Не определен тип обновления строки для задания {selectedItem.NomZad} по артикулу {selectedItem.Articul} класс вязания {selectedItem.NameVyazClass}");
                    break;
            }
        }
        private void SimpleButtonSaveVyazStatusUpdate()
        {
            // Получаем список строк с флагом IsNew = true или IsModified = true
            var filteredListNew = vyazPlanViewData
                .Where(x => x.IsNew || x.IsModified || x.IsDeleted)
                .Select(x => new PlanSezonZadKnitMachineList
                {
                    pszkmPszNom = x.NomZad,
                    pszkmKmlID = x.KmlID,
                    pszkmID = x.pszkmID,
                    pszkmKnitClass = x.IDVyazClass,
                    pszkmPlanDateFrom = x.DateZapPlanFrom,
                    IsNew = x.IsNew,
                    IsModified = x.IsModified,
                    IsDeleted = x.IsDeleted
                })
                .ToList();
            simpleButtonSaveVyaz.Enabled = filteredListNew.Count > 0;
            simpleButtonSaveVyaz.Refresh();
        }

        private async void gridViewVyazPlan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var selectedRow = _vyazPlanViewBindingSource.Current as VyazPlanView;
            if (selectedRow != null)
            {
                Task artPrFioProgrDataViewLoadTask = LoadArtPrFioProgrViewDataAsync(selectedRow.Nn);
                Task planSezonZadanyViewLoadTask = LoadPlanSezonZadanyViewDataAsync(selectedRow.NomZad);
                Task knitMachineListLoadTask = LoadKnitMachineListDataAsync(selectedRow.IDVyazClass);
                Task artPrKnitMachinePr1LoadTask = LoadArtPrKnitMachineViewPr1DataAsync(selectedRow.Nn);
                Task artPrKnitMachinePr2LoadTask = LoadArtPrKnitMachineViewPr2DataAsync(selectedRow.Nn);
                Task artPrKnitMachineRecom1LoadTask = LoadArtPrKnitMachineViewRecom1DataAsync(selectedRow.Nn);
                Task artPrKnitMachineRecom2LoadTask = LoadArtPrKnitMachineViewRecom2DataAsync(selectedRow.Nn);
                await Task.WhenAll(artPrFioProgrDataViewLoadTask, planSezonZadanyViewLoadTask, knitMachineListLoadTask
                    , artPrKnitMachinePr1LoadTask, artPrKnitMachinePr2LoadTask, artPrKnitMachineRecom1LoadTask, artPrKnitMachineRecom2LoadTask);

                comboBoxKnitMachineList.SelectedValue = selectedRow.KmlID;
            }
            else
            {
                comboBoxKnitMachineList.SelectedValue = -1;
            }
        }

        private async void simpleButtonSaveVyaz_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем список строк с флагом IsNew = true или IsModified = true
                var filteredListNew = vyazPlanViewData
                    .Where(x => x.IsNew || x.IsModified || x.IsDeleted)
                    .Select(x => new PlanSezonZadKnitMachineList
                    {
                        pszkmPszNom = x.NomZad,
                        pszkmKmlID = x.KmlID,
                        pszkmID = x.pszkmID,
                        pszkmKnitClass = x.IDVyazClass,
                        pszkmlSeconds = x.SekVyaz * x.Kol,
                        pszkmPlanDateFrom = x.DateZapPlanFrom,
                        IsNew = x.IsNew,
                        IsModified = x.IsModified,
                        IsDeleted = x.IsDeleted
                    })
                    .ToList();
                // Преобразуем в BindingList
                if (filteredListNew.Count > 0)
                {

                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PlanSezonZadKnitMachineList>(connection, filteredListNew, "plan_sezon_zad_knitMachine", new[] { "pszkmID" });
                        var keys = filteredListNew
                        .Select(x => new { x.pszkmKnitClass, x.pszkmPszNom })
                        .ToList();

                        var conditions = string.Join(" OR ", keys.Select((x, i) =>
                            $"(pszkmKnitClass = @Class{i} AND pszkmPszNom = @Nom{i})"));

                        var parameters = new DynamicParameters();
                        for (int i = 0; i < keys.Count; i++)
                        {
                            parameters.Add($"Class{i}", keys[i].pszkmKnitClass);
                            parameters.Add($"Nom{i}", keys[i].pszkmPszNom);
                        }

                        var query = $@"
                            SELECT pszkmID, pszkmKnitClass, pszkmPszNom, pszkmPlanDateFrom, pszkmPlanDateTo
                            FROM plan_sezon_zad_knitMachine
                            WHERE {conditions}";

                        var inserted = await connection.QueryAsync<(int pszkmID, int pszkmKnitClass, string pszkmPszNom, DateTime? pszkmPlanDateFrom, DateTime? pszkmPlanDateTo)>(query, parameters);

                        foreach (var item in inserted)
                        {
                            var match = vyazPlanViewData.FirstOrDefault(x =>
                                x.IDVyazClass == item.pszkmKnitClass && x.NomZad == item.pszkmPszNom);

                            if (match != null)
                            {
                                //match.pszkmID = item.pszkmID != null ? item.pszkmID : 0;
                                match.pszkmID = item.pszkmID;
                                match.DateZapPlanFrom = item.pszkmPlanDateFrom;
                                match.DateZapPlanTo = item.pszkmPlanDateTo;
                                match.IsNew = false;
                                match.IsModified = false;
                                match.IsDeleted = false;
                            }
                        }

                        if (filteredListNew == null)
                        {
                            filteredListNew.Clear();
                        }
                        filteredListNew = vyazPlanViewData
                        .Where(x => x.IsDeleted)
                        .Select(x => new PlanSezonZadKnitMachineList
                        {
                            pszkmPszNom = x.NomZad,
                            pszkmKmlID = x.KmlID,
                            pszkmID = x.pszkmID,
                            pszkmKnitClass = x.IDVyazClass,
                            pszkmlSeconds = x.SekVyaz * x.Kol,
                            pszkmPlanDateFrom = x.DateZapPlanFrom,
                            IsNew = x.IsNew,
                            IsModified = x.IsModified,
                            IsDeleted = x.IsDeleted
                        })
                        .ToList();
                        foreach (var item in filteredListNew)
                        {
                            var match = vyazPlanViewData.FirstOrDefault(x =>
                                x.IDVyazClass == item.pszkmKnitClass && x.NomZad == item.pszkmPszNom);

                            if (match != null)
                            {
                                match.pszkmID = 0;
                                match.IsNew = false;
                                match.IsModified = false;
                                match.IsDeleted = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Внимание! Нет данных для сохранения!");
                    return;
                }

                filteredListNew.Clear();

                //// 4. Обновляем ID обратно в оригинальный список
                //foreach (var inserted in filteredListNew)
                //{
                //    var original = vyazPlanViewData.FirstOrDefault(x =>
                //        x.pszkmKmlID == inserted.pszkmKmlID && x.NomZad == inserted.pszkmPszNom);

                //    if (original != null)
                //    {
                //        original.pszkmID = inserted.pszkmID;
                //        original.IsNew = false;
                //        original.IsModified = false;
                //    }
                //}
                filteredListNew.Clear();
                //_planSezonZadKnitMachineListBindingList.Clear();
                //_planSezonZadKnitMachineListBindingSource.Clear();

                // Обновляем UI после сохранения
                await this.InvokeAsync(() =>
                {
                    gridControlVyazPlan.RefreshDataSource();
                });
                SimpleButtonSaveVyazStatusUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                throw;
            }
        }

        private void simpleButtonSetKnitMachine_Click(object sender, EventArgs e)
        {
            if (gridViewVyazPlan.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("Внимание! Нет выбранных заданий для привязки В/М!");
                return;
            }
            if (comboBoxKnitMachineList.SelectedIndex == -1)
            {
                MessageBox.Show("Внимание! Не выбрана В/М для привязки!");
                return;
            }

            foreach (var rowHandle in gridViewVyazPlan.GetSelectedRows())
            {
                var selectedMachine = _knitMachineListBindingSource.Current as KnitMachineList;
                var selectedItem = (VyazPlanView)gridViewVyazPlan.GetRow(rowHandle);
                if (selectedItem.KmlID != selectedMachine.kmlID)
                {
                    selectedItem.KmlID = selectedMachine.kmlID;
                    selectedItem.KmlNumber = selectedMachine.kmlNumber;
                    selectedItem.SyncSelection = false;
                    gridViewVyazPlan.UnselectRow(rowHandle);
                    KnitMachineStatusUpdate(rowHandle);
                }
            }
            _vyazPlanViewBindingSource.ResetBindings(false);
            SimpleButtonSaveVyazStatusUpdate();
        }

        private void gridControlArtPrKnitMachineViewRecom1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonClearKnitMachine_Click(object sender, EventArgs e)
        {
            if (gridViewVyazPlan.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("Внимание! Нет выбранных заданий для очистки В/М!");
                return;
            }

            foreach (var rowHandle in gridViewVyazPlan.GetSelectedRows())
            {
                //var selectedMachine = _knitMachineListBindingSource.Current as KnitMachineList;
                var selectedItem = (VyazPlanView)gridViewVyazPlan.GetRow(rowHandle);
                selectedItem.KmlID = 0;
                selectedItem.KmlNumber = "0";
                selectedItem.DateZapPlanFrom = null;
                selectedItem.DateZapPlanTo = null;
                selectedItem.SyncSelection = false;
                gridViewVyazPlan.UnselectRow(rowHandle);
                KnitMachineStatusUpdate(rowHandle);
            }
            _vyazPlanViewBindingSource.ResetBindings(false);
            SimpleButtonSaveVyazStatusUpdate();
        }
        public void OpenForm(Form form, object sender = null)
        {
            _formManager.OpenForm(form, sender);
        }
        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            ////KnittingMachinesLoading knittingMachinesLoading = new KnittingMachinesLoading();
            ////knittingMachinesLoading.MdiParent = this;
            ////knittingMachinesLoading.Show();


            ////KnittingMachinesLoading FDI = new KnittingMachinesLoading();
            ////DialogResult result = FDI.ShowDialog();

            //int xIDVyazClass = 0;
            //var selectedRow = _vyazPlanViewBindingSource.Current as VyazPlanView;
            //if (selectedRow != null && selectedRow.IDVyazClass != 0)
            //{
            //    xIDVyazClass = selectedRow.IDVyazClass;
            //}
            //else
            //{
            //    xIDVyazClass = -1;
            //}
            //KnittingMachinesLoading FDI = new KnittingMachinesLoading(xIDVyazClass);

            //DialogResult result = FDI.ShowDialog();
            //// Обработка результата, возвращенного модальной формой
            //if (result == DialogResult.OK)
            //{
            //    // Действия при успешном завершении работы модальной формы
            //    //MessageBox.Show("OK");
            //}
            //else
            //{
            //    // Действия при отмене или другом результате
            //    //MessageBox.Show("Cancel");
            //}

            int xIDVyazClass = 0;
            var selectedRow = _vyazPlanViewBindingSource.Current as VyazPlanView;
            if (selectedRow != null && selectedRow.IDVyazClass != 0)
            {
                xIDVyazClass = selectedRow.IDVyazClass;
            }
            else
            {
                xIDVyazClass = -1;
            }
            OpenForm(new KnittingMachinesLoading(xIDVyazClass), sender);
        }

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            //KnittingProductionPlanningReport FDI = new KnittingProductionPlanningReport();

            //DialogResult result = FDI.ShowDialog();
            //// Обработка результата, возвращенного модальной формой
            //if (result == DialogResult.OK)
            //{
            //    // Действия при успешном завершении работы модальной формы
            //    //MessageBox.Show("OK");
            //}
            //else
            //{
            //    // Действия при отмене или другом результате
            //    //MessageBox.Show("Cancel");
            //}
            OpenForm(new KnittingProductionPlanningReportParameters(), sender);
        }
    }
}
