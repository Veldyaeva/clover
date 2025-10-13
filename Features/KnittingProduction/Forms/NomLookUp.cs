using DevExpress.CodeParser;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Core.Services;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CardByNom.Services;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class NomLookUp : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly CardByNomService _cardByNomService;
        private readonly DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private List<RasNomList> _currentRasNomListData = new List<RasNomList>();
        private List<RasNomList> rasNomListData = new List<RasNomList>();
        private BindingList<RasNomList> _rasNomListBindingList;
        private BindingSource _rasNomListBindingSource;
        public NomLookUp(string _nomZadany, string _ko, string _articul)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            var xNomZadany = _nomZadany;
            var xKo = _ko;

            switch (_ko.Trim().Length, _nomZadany.Trim().Length)
            {
                case ( > 0, 0):
                    customLabel1.Text = "Артикул";
                    customTextBoxEx1.Text = _articul;
                    break;
                case (0, > 0):
                    customLabel1.Text = "№ задания";
                    customTextBoxEx1.Text = _nomZadany;
                    break;
                default:
                    customLabel1.Text = "ошибка";
                    customTextBoxEx1.Text = "Повторите поиск";
                    break;
            }
        }

        private async void LoadRasNomListByNomZadany(string _nomZadany)
        {
            try
            {
                _rasNomListBindingSource.Clear();
                _rasNomListBindingSource.ResetBindings(false);
                var rasNomListData = await _dbService.GetListAsync<RasNomList>("dbo.GetNomListByKodOrNomZadany @xNomZadany = '{_nomZadany}'", new { });
                if (rasNomListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RasNomList", "LoadRasNomListDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                        _currentRasNomListData = rasNomListData;                // Обновляем текущую модель
                        _rasNomListBindingSource.DataSource = _currentRasNomListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные RasNomList успешно загружены", "LoadRasNomListDataAsync");
                    //RasCard.Enabled = true ;
                    _rasNomListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RasNomList", "LoadRasNomListDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RasNomList");
            }
        }

        private void LoadNomListByArticul(string _ko)
        {

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var rasNomListTask = Task.Run(() =>
                {
                    _rasNomListBindingList = new BindingList<RasNomList>();
                    _rasNomListBindingSource = new BindingSource { DataSource = _rasNomListBindingList };
                });

                await Task.WhenAll(rasNomListTask);

                #region описание gridControlRasNomList "список расчетов по номеру задания или артикулу"
                gridControlRasNomList.DataSource = _rasNomListBindingSource;
                gridRasNomListColumnNom.FieldName = "Nn";
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
                        gridViewRasNomList.SetRowCellValue(e.RowHandle, gridColumnVyazPlanSyncSelection, newValue);

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
        private void customTextBoxEx1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //}
        }
    }
}
