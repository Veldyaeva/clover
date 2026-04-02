using DevExpress.Data;
using DevExpress.Xpf.Core.Native;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Core.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using System.ComponentModel;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz
    {
        private async Task LoadPlanTotalHoursByKnitMachineDataAsync()
        {
            try
            {
                _planTotalHoursByKnitMachineBindingSource.Clear();
                _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);

                planTotalHoursByKnitMachineData = await _vyazService.GetPlanTotalHoursByKnitMachine();

                if (planTotalHoursByKnitMachineData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _planTotalHoursByKnitMachineBindingSource.DataSource = planTotalHoursByKnitMachineData;
                    });

                    await _logger.LogEventAsync($"Данные PlanTotalHoursByKnitMachine успешно загружены", "LoadPlanTotalHoursByKnitMachineDataAsync");
                    _planTotalHoursByKnitMachineBindingList.Add(planTotalHoursByKnitMachineData[0]);
                    _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);
                    gridViewPlanTotalHoursByKnitMachine.ExpandAllGroups();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");
                }
                Debug.WriteLine($"LoadPlanTotalHoursByKnitMachineDataAsync completed");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PlanTotalHoursByKnitMachine");
            }
        }
        private async Task LoadZadanyListByMachineNewDataAsync(int kmlID)
        {
            try
            {
                _zadanyListNewBindingSource.Clear();
                _zadanyListNewBindingSource.ResetBindings(false);

                zadanyListNewData = await _vyazService.GetZadanyListByMachine(kmlID);

                if (zadanyListNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        gridControlZadanyList.BeginUpdate();
                        _zadanyListNewBindingSource.DataSource = zadanyListNewData;
                        gridControlZadanyList.EndUpdate();
                    });

                    await _logger.LogEventAsync($"Данные ZadanyListByMachine успешно загружены", "LoadZadanyListByMachineNewDataAsync");
                    _zadanyListNewBindingList.Add(zadanyListNewData[0]);
                    _zadanyListNewBindingSource.ResetBindings(false);

                    gridViewZadanyList.BeginSort();
                    gridViewZadanyList.ClearSorting();
                    gridViewZadanyList.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnYearPlan, ColumnSortOrder.Ascending));
                    gridViewZadanyList.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
                    gridViewZadanyList.EndSort();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");
                }
                Debug.WriteLine($"LoadZadanyListByMachineNewDataAsync completed");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ZadanyListByMachine");
            }
        }
        private async Task LoadRzvPachListByNomNewDataAsync(int nom, string nomZad)
        {
            try
            {
                _rzvPachListByNomNewBindingSource.Clear();
                _rzvPachListByNomNewBindingSource.ResetBindings(false);

                if (nom > 0)
                {
                    rzvPachListByNomNewData = await _vyazService.GetRzvPachListByNom(nom, nomZad);
                }
                else
                {
                    rzvPachListByNomNewData = null;
                }

                if (rzvPachListByNomNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _rzvPachListByNomNewBindingSource.DataSource = rzvPachListByNomNewData;
                    });

                    await _logger.LogEventAsync($"Данные RzvPachListByNom успешно загружены", "LoadRzvPachListByNomNewDataAsync");
                    _rzvPachListByNomNewBindingList.Add(rzvPachListByNomNewData[0]);
                    _rzvPachListByNomNewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");
                }
                Debug.WriteLine($"LoadRzvPachListByNomNewDataAsync completed");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RzvPachListByNom");
            }
        }
        private async Task LoadPZVOperListByPachListNewDataAsync(string pachList, int vyazPodrKod)
        {
            try
            {
                _pZVOperListByPachListNewBindingSource.Clear();
                _pZVOperListByPachListNewBindingSource.ResetBindings(false);

                if (pachList.Length > 0 && pachList != "[]" && pachList is not null)
                {
                    pZVOperListByPachListNewData = await _vyazService.GetPZVOperListByPachList(pachList, vyazPodrKod);
                }
                else
                {
                    pZVOperListByPachListNewData = null;
                }

                if (pZVOperListByPachListNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PZVOperListByPachListNew", "LoadPZVOperListByPachListNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _pZVOperListByPachListNewBindingSource.DataSource = pZVOperListByPachListNewData;
                    });

                    await _logger.LogEventAsync($"Данные PZVOperListByPachList успешно загружены", "LoadPZVOperListByPachListNewDataAsync");
                    _pZVOperListByPachListNewBindingList.Add(pZVOperListByPachListNewData[0]);
                    _pZVOperListByPachListNewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
                }
                Debug.WriteLine($"LoadPZVOperListByPachListNewDataAsync completed");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
            }
        }
        private async Task LoadSmenZadanyVyazDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        switch (vyazPodrKod)
                        {
                            case 1:
                                advBandedGridViewSmenZadany.ShowLoadingPanel();
                                break;
                            case 2:
                            case 3:
                                gridViewSmenZadanyOtp.ShowLoadingPanel();
                                break;
                        }

                        await SetLoadingAsync(true);
                        int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
                        int _xKodProizv = 1; // код производства 1 - вязальное производство
                        //int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
                        var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, vyazPodrKod, token);

                        await this.UI(() =>
                        {
                            gridControlSmenZadany.BeginUpdate();
                            _smenZadanyVyazBindingSource.DataSource = bs.DataSource;
                            if (vyazPodrKod == 1)
                            {
                                Application.Idle -= ExpandGroupsOnIdle;
                                Application.Idle += ExpandGroupsOnIdle;
                            }
                            gridControlSmenZadany.EndUpdate();
                            switch (vyazPodrKod)
                            {
                                case 1:
                                    advBandedGridViewSmenZadany.BeginSort();
                                    advBandedGridViewSmenZadany.ClearSorting();
                                    advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmaNumber, ColumnSortOrder.Ascending));
                                    advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmlNumber, ColumnSortOrder.Ascending));
                                    advBandedGridViewSmenZadany.EndSort();
                                    break;
                                case 2:
                                case 3:
                                    gridViewSmenZadanyOtp.BeginSort();
                                    gridViewSmenZadanyOtp.ClearSorting();
                                    gridViewSmenZadanyOtp.SortInfo.Add(new GridColumnSortInfo(gridSmenZadanyOtpColumnSzFio, ColumnSortOrder.Ascending));
                                    //gridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
                                    gridViewSmenZadanyOtp.EndSort();
                                    break;
                            }

                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadSmenZadanyVyazDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazDataAsync");
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

            switch (vyazPodrKod)
            {
                case 1:
                    advBandedGridViewSmenZadany.HideLoadingPanel();
                    break;
                case 2:
                case 3:
                    gridViewSmenZadanyOtp.HideLoadingPanel();
                    break;
            }

            Debug.WriteLine($"LoadSmenZadanyVyazDataAsync completed");
        }
        private async Task LoadSmenZadanyVyazNewDataAsync(int _xKodProizv)
        {
            try
            {
                await _loader.RunAsync(
                    async token =>
                    {
                        try
                        {
                            switch (vyazPodrKod)
                            {
                                case 1:
                                    advBandedGridViewSmenZadany.ShowLoadingPanel();
                                    break;
                                case 2:
                                case 3:
                                    gridViewSmenZadanyOtp.ShowLoadingPanel();
                                    break;
                            }

                            await SetLoadingAsync(true);
                            int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
                            //int _xKodProizv = 1; // код производства 1 - вязальное производство
                            //int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
                            var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, vyazPodrKod, token);

                            await this.UI(() =>
                            {
                                gridControlSmenZadany.BeginUpdate();
                                _smenZadanyVyazNewBindingSource.DataSource = bs.DataSource;
                                Application.Idle -= ExpandGroupsOnIdle;
                                Application.Idle += ExpandGroupsOnIdle;
                                //----------------------
                                var changes = GetChanges<SmenZadanyVyaz>(
                                    _smenZadanyVyazBindingSource,
                                    _smenZadanyVyazNewBindingSource,
                                    HashMode.ExcludeOnly,
                                    keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID", "szTab" },
                                    hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
                                );

                                ApplyChanges(
                                    _smenZadanyVyazBindingSource,
                                    changes,
                                    UpdateFieldsMode.ExcludeOnly,
                                    keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID", "szTab" },
                                    (GridView)gridControlSmenZadany.MainView,
                                    fields: new[] { "IsNew", "IsModified", "IsDeleted" }
                                );

                                RemoveMissingSmart(
                                    _smenZadanyVyazBindingSource,
                                    changes.Removed,
                                    gridControlSmenZadany
                                );
                                if (changes != null)
                                {
                                    changes.Added?.Clear();
                                    changes.Modified?.Clear();
                                    changes.Removed?.Clear();
                                    changes = null;
                                }
                                //------------------------------
                                if (vyazPodrKod == 1)
                                {
                                    Application.Idle -= ExpandGroupsOnIdle;
                                    Application.Idle += ExpandGroupsOnIdle;
                                }
                                gridControlSmenZadany.EndUpdate();

                                //----------------------
                                switch (vyazPodrKod)
                                {
                                    case 1:
                                        advBandedGridViewSmenZadany.BeginSort();
                                        advBandedGridViewSmenZadany.ClearSorting();
                                        advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmaNumber, ColumnSortOrder.Ascending));
                                        advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmlNumber, ColumnSortOrder.Ascending));
                                        advBandedGridViewSmenZadany.EndSort();
                                        break;
                                    case 2:
                                    case 3:
                                        gridViewSmenZadanyOtp.BeginSort();
                                        gridViewSmenZadanyOtp.ClearSorting();
                                        gridViewSmenZadanyOtp.SortInfo.Add(new GridColumnSortInfo(gridSmenZadanyOtpColumnSzFio, ColumnSortOrder.Ascending));
                                        //gridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
                                        gridViewSmenZadanyOtp.EndSort();
                                        break;
                                }
                                //----------------------

                                advBandedGridViewSmenZadany.TopRowIndex = TopRowIndexSmenZadany;
                            });

                            await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка LoadSmenZadanyVyazNewDataAsync (GetSmenZadanyVyaz + _smenZadanyVyazBindingSource update): {ex.Message}");
                        }
                    },
                    onError: async ex =>
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazNewDataAsync");
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

                switch (vyazPodrKod)
                {
                    case 1:
                        advBandedGridViewSmenZadany.HideLoadingPanel();
                        break;
                    case 2:
                    case 3:
                        gridViewSmenZadanyOtp.HideLoadingPanel();
                        break;
                }

                Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync completed");
            }
            catch (Exception ex) { Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync - {ex.ToString()}"); }
        }
        private async Task LoadNaryadZadanyVyazDataAsync(int tab, int kmlID)
        {
            // 1️ отменяем предыдущий запрос
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _loadCts = new CancellationTokenSource();

            var token = _loadCts.Token;

            try
            {
                // 2️ сразу очищаем грид + показываем загрузку
                await this.InvokeAsync(() =>
                {
                    gridControlNaryadZadany.BeginUpdate();
                    gridViewNaryadZadany.ShowLoadingPanel();

                    _naryadZadanyVyazBindingSource.DataSource =
                        new BindingList<NaryadZadanyVyaz>();
                });

                // 3️ долгий запрос
                var bs = await _vyazService.GetNaryadZadanyVyaz(tab, kmlID, vyazPodrKod, token);

                // если отменили — просто выходим
                if (token.IsCancellationRequested)
                    return;

                // 4️ привязываем результат
                await this.InvokeAsync(() =>
                {
                    _naryadZadanyVyazBindingSource.DataSource = bs.DataSource;
                });

                if (bs.Count == 0)
                {
                    await _logger.LogEventAsync(
                        "Данные NaryadZadanyVyaz не найдены",
                        nameof(LoadNaryadZadanyVyazDataAsync)
                    );
                }
                else
                {
                    await _logger.LogEventAsync(
                        "Данные NaryadZadanyVyaz успешно загружены",
                        nameof(LoadNaryadZadanyVyazDataAsync)
                    );
                }
                Debug.WriteLine($"LoadNaryadZadanyVyazDataAsync completed");
            }
            catch (OperationCanceledException)
            {
                // молча — это нормальный сценарий
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных NaryadZadanyVyaz");
            }
            finally
            {
                await this.InvokeAsync(() =>
                {
                    gridViewNaryadZadany.HideLoadingPanel();
                    gridControlNaryadZadany.EndUpdate();
                    gridViewNaryadZadany.ExpandAllGroups();
                });
            }
        }
        private async Task LoadPlanTotalQuantityByArticulDataAsync()
        {
            // 1️ отменяем предыдущий запрос
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _loadCts = new CancellationTokenSource();

            var token = _loadCts.Token;

            try
            {
                // 2️ сразу очищаем грид + показываем загрузку
                await this.InvokeAsync(() =>
                {
                    gridControlPlanTotalQuantityByArticul.BeginUpdate();
                    gridViewPlanTotalQuantityByArticul.ShowLoadingPanel();

                    _planTotalQuantityByArticulBindingSource.DataSource =
                        new BindingList<PlanTotalQuantityByArticul>();
                });

                // 3️ долгий запрос
                var bs = await _vyazService.GetPlanTotalQuantityByArticul(token);

                // если отменили — просто выходим
                if (token.IsCancellationRequested)
                    return;

                // 4️ привязываем результат
                await this.InvokeAsync(() =>
                {
                    _planTotalQuantityByArticulBindingSource.DataSource = bs.DataSource;
                });

                if (bs.Count == 0)
                {
                    await _logger.LogEventAsync(
                        "Данные GetPlanTotalQuantityByArticul не найдены",
                        nameof(LoadPlanTotalQuantityByArticulDataAsync)
                    );
                }
                else
                {
                    await _logger.LogEventAsync(
                        "Данные GetPlanTotalQuantityByArticul успешно загружены",
                        nameof(LoadPlanTotalQuantityByArticulDataAsync)
                    );
                }
                Debug.WriteLine($"LoadPlanTotalQuantityByArticulDataAsync completed");
            }
            catch (OperationCanceledException)
            {
                // молча — это нормальный сценарий
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных GetPlanTotalQuantityByArticul");
            }
            finally
            {
                await this.InvokeAsync(() =>
                {
                    gridViewPlanTotalQuantityByArticul.HideLoadingPanel();
                    gridControlPlanTotalQuantityByArticul.EndUpdate();
                    gridViewPlanTotalQuantityByArticul.ExpandAllGroups();
                });
            }
        }
        private async Task LoadArtNormNDataAsync(int annId, CancellationToken ct)
        {
            var data = await _anService.GetArtNormDataById(annId, ct); // добавь ct внутрь сервиса
            ct.ThrowIfCancellationRequested();

            await this.InvokeAsync(() =>
            {
                _artNormNBindingSource.DataSource = data == null
                    ? new List<ArtNormN>()
                    : new List<ArtNormN> { data };

                _artNormNBindingSource.ResetBindings(false);
            });
            Debug.WriteLine($"LoadArtNormNDataAsync completed for annId={annId}");
        }
        private async Task LoadNormRaszDataAsync(int nrId, CancellationToken ct)
        {
            var data = await _anService.GetRelatedNormRaszByID(nrId, ct);
            ct.ThrowIfCancellationRequested();

            await this.InvokeAsync(() =>
            {
                _normRaszBindingSource.DataSource = data == null
                    ? new List<NormRasz>()
                    : new List<NormRasz> { data };

                _normRaszBindingSource.ResetBindings(false);
            });
            Debug.WriteLine($"LoadNormRaszDataAsync completed for nrId={nrId}");
        }
        private async Task LoadKnitWorkingShiftSmenToMoveDataAsync(int _kmaID)
        {
            try
            {
                var sql = @$"
                            SELECT * 
                            FROM knitWorkingShiftNewCurrentSmen_view
                            WHERE kmaID <> {_kmaID}
                              AND dateShiftStart IS NOT NULL
                              AND dateShiftEnd IS NULL";

                var data = await _dbService.GetListAsync<KnitWorkingShiftSmen>(sql, null);

                _zonesCache = data ?? new List<KnitWorkingShiftSmen>();
                await _logger.LogEventAsync($"Данные KnitWorkingShiftSmenToMove успешно загружены", "LoadKnitWorkingShiftSmenToMoveDataAsync");
                Debug.WriteLine($"LoadKnitWorkingShiftSmenToMoveDataAsync completed with {_zonesCache.Count} records");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки зон");
                await _logger.LogEventAsync($"Не удалось найти данные KnitWorkingShiftSmenToMove", "LoadKnitWorkingShiftSmenToMoveDataAsync");
                _zonesCache = new();
            }
        }
    }
}
