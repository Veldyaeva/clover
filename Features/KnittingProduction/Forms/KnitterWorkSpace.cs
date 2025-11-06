using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.CardByNom.Services;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlClient;
using Dapper;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly KnitterOrchestrator _orchestrator;
        private readonly BindingSource _planBindingSource = new BindingSource();
        private readonly DatabaseHelper _dbHelper;

        // Поля для группировки мастер-деталь
        private List<KnitterPZVModel> _allRows;
        private Dictionary<string, List<KnitterPZVModel>> _byMachine;
        private Dictionary<string, List<KnitterPZVModel>> _machineArtNomMaster;
        private Dictionary<(string MachineKey, string ArtKey, int? Nom), List<KnitterPZVModel>> _machineArtNomGroups;
        private Dictionary<(string MachineKey, string ArtKey, int? Nom, int? Pach), List<KnitterPZVModel>> _machineArtNomPachGroups;
        // Вью для третьего уровня (деталь детальной таблицы)
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;

        /// <summary>
        /// Инициализирует форму рабочего места вязальщика, настраивает источники данных и события.
        /// </summary>
        public KnitterWorkSpace()
        {
            try
        {
            InitializeComponent();
                dataLayoutControl1.DataSource = _planBindingSource;

                ConfigureAdvBandedGridColumns();

                //    dataLayoutControl1.RetrieveFields(new RetrieveFieldsParameters
                //    {
                //        DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
                //    });

                _dbHelper = new DatabaseHelper();
                var repo = new KnitterRepository(_dbHelper);
                _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

            PlanZagrVyazGridControl.DataSource = _planBindingSource;

                // === Конфигурация третьего уровня (по art+№рассчёта) ===
                advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView(PlanZagrVyazGridControl);
                advBandedGridView1.Name = "advBandedGridView1";
                advBandedGridView1.GridControl = PlanZagrVyazGridControl;
                advBandedGridView1.OptionsDetail.EnableMasterViewMode = false;

                var band2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand { Caption = "Операции (Art+Nom)" };
                var colN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "n", FieldName = "N", Visible = true };
                var colN1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "n1", FieldName = "N1", Visible = true };
                var colText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "наименование операции", FieldName = "nrText", Visible = true };
                band2.Columns.Add(colN);
                band2.Columns.Add(colN1);
                band2.Columns.Add(colText);
                advBandedGridView1.Bands.Add(band2);

                PlanZagrVyazGridControl.ViewCollection.Add(advBandedGridView1);

                if (PlanZagrVyazGridControl.LevelTree.Nodes.Count > 0)
                {
                    var level1 = PlanZagrVyazGridControl.LevelTree.Nodes[0];
                    var level2 = new DevExpress.XtraGrid.GridLevelNode
                    {
                        RelationName = "ArtNom",
                        LevelTemplate = advBandedGridView1
                    };
                    level1.Nodes.Add(level2);
                }
            this.Load += async (s, e) => await InitializeAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Первичная инициализация: загрузка справочника ФИО, установка дефолтного табеля (999) и автозагрузка плана.
        /// </summary>
        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _orchestrator.GetFioListAsync();
                fioList ??= new List<FioModel>();

                // Жестко выбираем табельный при загрузке формы
                const int defaultTab = 1438;
                bool hasDefault = fioList.Any(f => f.Tab == defaultTab);
                if (!hasDefault)
                {
                    string defaultFio = await _orchestrator.GetFioByTabAsync(defaultTab);
                    if (!string.IsNullOrWhiteSpace(defaultFio))
                    {
                        fioList.Insert(0, new FioModel { Tab = defaultTab, Fio = defaultFio });
                    }
                }

                FioGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Fio);
                FioGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                FioGridLookUpEdit.Properties.DataSource = fioList;

                if (fioList.Count > 0)
                {
                    FioGridLookUpEdit.EditValue = fioList.Any(f => f.Tab == defaultTab)
                        ? defaultTab
                        : fioList[0].Tab;
                }

                // По умолчанию табельный номер не выбран — требуется явный выбор пользователем
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///// <summary>
        ///// Перегружает данные плана для текущего выбранного табеля через LoadPzvAsync и биндинги в грид.
        ///// </summary>
        //private async Task ReloadGridForCurrentSelectionAsync(bool onlyActive = true)
        //{
        //    try
        //    {
        //        if (FioGridLookUpEdit.EditValue == null)
        //        {
        //            _planBindingSource.DataSource = null;
        //            PlanZagrVyazGridControl.RefreshDataSource();
        //            return;
        //        }

        //        if (!int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
        //        {
        //            return;
        //        }

        //        using (var conn = _dbHelper.GetConnection())
        //        {
        //            var plan = await LoadPzvAsync(conn, tab, onlyActive);
        //            BindGroupDetails(plan ?? new List<KnitterPZVModel>());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        /// <summary>
        /// Конфигурирует соответствие колонок грида полям модели <see cref="KnitterPZVModel"/>.
        /// </summary>
        private void ConfigureAdvBandedGridColumns()
        {
            // Map columns to KnitterPZVModel properties
            //bandedGridColumn1.FieldName = nameof(KnitterPZVModel.pzvID);
            //bandedGridColumn1.Caption = "ID";

            //bandedGridColumn2.FieldName = nameof(KnitterPZVModel.pzvDivision);
            //bandedGridColumn2.Caption = "Подразделение";

            //bandedGridColumn3.FieldName = nameof(KnitterPZVModel.pzvMod);
            //bandedGridColumn3.Caption = "Модель";

            //bandedGridColumn4.FieldName = nameof(KnitterPZVModel.pzvArticul);
            //bandedGridColumn4.Caption = "Артикул";

            //bandedGridColumn5.FieldName = nameof(KnitterPZVModel.pzvKol);
            //bandedGridColumn5.Caption = "Кол-во";

            //bandedGridColumn6.FieldName = nameof(KnitterPZVModel.pzvDateStart);
            //bandedGridColumn6.Caption = "Начало";
            //bandedGridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //bandedGridColumn6.DisplayFormat.FormatString = "g";

            //bandedGridColumn7.FieldName = nameof(KnitterPZVModel.pzvDateEnd);
            //bandedGridColumn7.Caption = "Окончание";
            //bandedGridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //bandedGridColumn7.DisplayFormat.FormatString = "g";

            //// Hide unused column if present
            //bandedGridColumn8.Visible = false;
        }

        /// <summary>
        /// Реализует каскадную группировку для мастер-деталь вью:
        /// 0 уровень — по вязальной машине (kmlNumber),
        /// 1 уровень — по сочетанию артикула и номера (pzvArticul, pzvNom),
        /// 2 уровень — по партии (n_pach).
        /// </summary>
        private void BindGroupDetails(List<KnitterPZVModel> rows, bool clearTabs = true)
        {
            var expansionState = CaptureExpansionState();

            _allRows = rows ?? new List<KnitterPZVModel>();

            if (clearTabs && _allRows != null)
            {
                foreach (var row in _allRows)
                {
                    if (row != null)
                    {
                        row.pzvTab = null;
                    }
                }
            }
            PlanZagrVyazGridControl.BeginUpdate();
            try
            {
            _byMachine = _allRows
                .GroupBy(r => NormalizeMachineKey(r.kmlNumber))
                .ToDictionary(g => g.Key, g => g.ToList());

            _machineArtNomGroups = _allRows
                .GroupBy(r => (NormalizeMachineKey(r.kmlNumber), NormalizeArtKey(r.pzvArticul), r.pzvNom))
                .ToDictionary(g => g.Key, g => g.ToList());

            _machineArtNomPachGroups = _allRows
                .GroupBy(r => (NormalizeMachineKey(r.kmlNumber), NormalizeArtKey(r.pzvArticul), r.pzvNom, (int?)r.n_pach))
                .ToDictionary(g => g.Key, g => g.ToList());

            _machineArtNomMaster = _byMachine.ToDictionary(
                kv => kv.Key,
                kv => kv.Value
                    .GroupBy(r => (NormalizeArtKey(r.pzvArticul), r.pzvNom))
                    .Select(g => g.First())
                    .ToList());

            // Уровень 0 — первая запись каждой вязальной машины
            var masterData = _byMachine.Values.Select(list => list.First()).ToList();
            _planBindingSource.DataSource = masterData;
            PlanZagrVyazGridControl.RefreshDataSource();

            // Настройка событий для мастер-деталь вью (первый уровень ArtNom на bandedGridView3)
            var master = bandedGridView3;

            // Подписка на события (удаляем старые подписки если есть)
            master.MasterRowGetRelationCount -= Master_MasterRowGetRelationCount;
            master.MasterRowGetRelationName -= Master_MasterRowGetRelationName;
            master.MasterRowGetChildList -= Master_MasterRowGetChildList;

            // Подписываемся заново
            master.MasterRowGetRelationCount += Master_MasterRowGetRelationCount;
            master.MasterRowGetRelationName += Master_MasterRowGetRelationName;
            master.MasterRowGetChildList += Master_MasterRowGetChildList;

            master.OptionsDetail.EnableMasterViewMode = true;
            master.OptionsDetail.AllowOnlyOneMasterRowExpanded = false; // Разрешаем раскрытие нескольких строк
                                                                        //для бОльшей производительности лучше запрещать

            // Настройка второго уровня (Items) - bandedGridView1 является мастером для advBandedGridView1
            var bandedGridView1Master = bandedGridView1;
            bandedGridView1Master.MasterRowGetRelationCount -= Detail_MasterRowGetRelationCount;
            bandedGridView1Master.MasterRowGetRelationName -= Detail_MasterRowGetRelationName;
            bandedGridView1Master.MasterRowGetChildList -= Detail_MasterRowGetChildList;

            bandedGridView1Master.MasterRowGetRelationCount += Detail_MasterRowGetRelationCount;
            bandedGridView1Master.MasterRowGetRelationName += Detail_MasterRowGetRelationName;
            bandedGridView1Master.MasterRowGetChildList += Detail_MasterRowGetChildList;

            bandedGridView1Master.OptionsDetail.EnableMasterViewMode = true;
            bandedGridView1Master.OptionsDetail.AllowOnlyOneMasterRowExpanded = false;
            bandedGridView1Master.OptionsDetail.AllowExpandEmptyDetails = true; // Разрешаем раскрытие даже при пустых деталях (для отладки)
            }
            finally
            {
                PlanZagrVyazGridControl.EndUpdate();
            }

            RestoreExpansionState(expansionState);
        }

        private void Master_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Master_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "ArtNom";
        }

        private void Master_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var head = (KnitterPZVModel)bandedGridView3.GetRow(e.RowHandle);
            if (head == null)
            {
                e.ChildList = new List<KnitterPZVModel>();
                return;
            }

            var machineKey = NormalizeMachineKey(head.kmlNumber);
            if (_machineArtNomMaster != null && _machineArtNomMaster.TryGetValue(machineKey, out var childRows))
            {
                e.ChildList = childRows;
            }
            else
            {
                e.ChildList = new List<KnitterPZVModel>();
            }
        }

        // === Вложенный уровень: по art + №рассчёта ===
        private void Detail_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Detail_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Items";
        }

        private void Detail_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            try
            {
                // Второй уровень Items: sender - это bandedGridView1 (BandedGridView)
                var bandedView = sender as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
                if (bandedView == null)
                {
                    e.ChildList = new List<KnitterPZVModel>();
                    return;
                }

                var head = bandedView.GetRow(e.RowHandle) as KnitterPZVModel;
                if (head == null)
                {
                    e.ChildList = new List<KnitterPZVModel>();
                    return;
                }

                var machineKey = NormalizeMachineKey(head.kmlNumber);
                var artKey = NormalizeArtKey(head.pzvArticul);
                var nomKey = head.pzvNom;

                var result = new List<KnitterPZVModel>();
                var seenOperations = new HashSet<string>();

                if (_machineArtNomGroups != null && _machineArtNomGroups.TryGetValue((machineKey, artKey, nomKey), out var groupRows))
                {
                    foreach (var groupRow in groupRows)
                    {
                        var pachKey = (machineKey, artKey, nomKey, (int?)groupRow.n_pach);

                        if (_machineArtNomPachGroups == null || !_machineArtNomPachGroups.TryGetValue(pachKey, out var pachRows))
                            continue;

                        foreach (var parentRow in pachRows)
                        {
                            if (parentRow?.nrModels == null || parentRow.nrModels.Count == 0)
                                continue;

                            foreach (var nr in parentRow.nrModels)
                            {
                                var signature = $"{parentRow.pzvID}_{groupRow.n_pach}_{nr.nrN}_{nr.nrN1}_{nr.nr_kod_proizv}_{nr.nr_kod_ob}";
                                if (!seenOperations.Add(signature))
                                    continue;

                                var operationRow = CreateOperationRow(parentRow, nr);
                                operationRow.n_pach = groupRow.n_pach;
                                operationRow.razm = parentRow.razm;
                                result.Add(operationRow);
                            }
                        }
                    }
                }

                e.ChildList = result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Detail_MasterRowGetChildList error: {ex.Message}");
                e.ChildList = new List<KnitterPZVModel>();
            }
        }

        public void LoadData(PlanZagrVyaz data)
        {
            _planBindingSource.DataSource = data;
        }

        /// <summary>
        /// Обработчик смены выбранного сотрудника: подгружает план по табельному номеру и отображает в гриде.
        /// </summary>
        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FioGridLookUpEdit.EditValue == null)
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    return;
                }

                //using (var conn = _dbHelper.GetConnection())
                //{
                ////    По умолчанию показываем только активные операции
                //    var plan = await LoadPzvAsync(conn, tab, onlyActive: true);
                //    BindGroupDetails(plan ?? new List<KnitterPZVModel>());
                //}
                List<KnitterPZVModel> plan = await _orchestrator.GetPlanByTabAsync(tab);

                // Реализуем группировку для мастер-деталь вью
                BindGroupDetails(plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<List<KnitterPZVModel>> LoadPzvAsync(SqlConnection conn, int? tab, bool onlyActive)
        {
            // важная опция для маппинга имён с подчёркиваниями
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var sql = "EXEC dbo.GetPlanZagrVyazNorm_ByTab @tab, @OnlyActive;";
            var map = new ConcurrentDictionary<int, KnitterPZVModel>();

            // Границы m-mapping: до колонки nrID — KnitterPZVModel, 
            // затем блок полей nrModel, затем с n_pach — rzvModel.
            var result = await conn.QueryAsync<KnitterPZVModel, nrModel, rzvModel, KnitterPZVModel>(
                sql,
                (pzv, nr, rzv) =>
                {
                    var parent = map.GetOrAdd(pzv.pzvID, _ =>
                    {
                        // списки уже инициализированы в модели
                        return pzv;
                    });

                    // nr всегда есть (JOIN), но защитимся от дублей
                    if (nr != null && !ContainsNr(parent.nrModels, nr))
                        parent.nrModels.Add(nr);

                    // rzv может отсутствовать (LEFT JOIN) — в этом случае n_pach будет 0
                    if (rzv != null && HasRzv(rzv) && !ContainsRzv(parent.rzvModels, rzv))
                        parent.rzvModels.Add(rzv);

                    return parent;
                },
                new { tab, OnlyActive = onlyActive ? 1 : 0 },
                splitOn: "nrID,n_pach",
                buffered: false // потоково, меньше пиков по памяти
            );

            // нам важны уникальные родители
            return map.Values.ToList();
        }

        // --- помощники для уникальности (чтобы из-за джойнов не плодить дубликаты) ---

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var rowsForUpdate = GetKnitterRowsForCurrentSelection().ToList();
                if (!rowsForUpdate.Any())
                {
                    XtraMessageBox.Show(this, "Выберите строки плана для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pzvIds = rowsForUpdate
                    .Select(r => r.pzvID)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (pzvIds.Count == 0)
                {
                    XtraMessageBox.Show(this, "Не удалось определить записи плана для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);

                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(selectedTab);
                BindGroupDetails(refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<KnitterPZVModel> GetKnitterRowsForCurrentSelection()
        {
            if (_allRows == null || PlanZagrVyazGridControl.FocusedView is not DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
                return Enumerable.Empty<KnitterPZVModel>();

            var baseRows = GetRowsFromView(view).ToList();
            if (!baseRows.Any())
                return Enumerable.Empty<KnitterPZVModel>();

            IEnumerable<KnitterPZVModel> expanded;

            if (ReferenceEquals(view, bandedGridView3))
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x => NormalizeMachineKey(x.kmlNumber) == NormalizeMachineKey(row.kmlNumber)));
            }
            else if (ReferenceEquals(view, bandedGridView1))
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x =>
                    NormalizeMachineKey(x.kmlNumber) == NormalizeMachineKey(row.kmlNumber) &&
                    NormalizeArtKey(x.pzvArticul) == NormalizeArtKey(row.pzvArticul) &&
                    x.pzvNom == row.pzvNom &&
                    x.n_pach == row.n_pach));
            }
            else
            {
                expanded = baseRows;
            }

            return expanded
                .Where(r => r != null && r.pzvID > 0)
                .GroupBy(r => r.pzvID)
                .Select(g => g.First());
        }

        private static IEnumerable<KnitterPZVModel> GetRowsFromView(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
        {
            int[] selectedHandles = view.GetSelectedRows();
            if (selectedHandles == null || selectedHandles.Length == 0)
            {
                if (view.FocusedRowHandle >= 0)
                    selectedHandles = new[] { view.FocusedRowHandle };
                else
                    return Enumerable.Empty<KnitterPZVModel>();
            }

            return selectedHandles
                .Select(view.GetRow)
                .OfType<KnitterPZVModel>();
        }

        private ExpansionState CaptureExpansionState()
        {
            if (bandedGridView3 == null || bandedGridView3.DataRowCount == 0)
                return ExpansionState.Empty;

            var machines = new HashSet<string>();
            var artNom = new HashSet<(string MachineKey, string ArtKey, int? Nom)>();

            for (int i = 0; i < bandedGridView3.DataRowCount; i++)
            {
                if (!bandedGridView3.GetMasterRowExpanded(i))
                    continue;

                if (bandedGridView3.GetRow(i) is not KnitterPZVModel machineRow)
                    continue;

                var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                machines.Add(machineKey);

                if (bandedGridView3.GetDetailView(i, 0) is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView detailView)
                {
                    for (int j = 0; j < detailView.DataRowCount; j++)
                    {
                        if (!detailView.GetMasterRowExpanded(j))
                            continue;

                        if (detailView.GetRow(j) is not KnitterPZVModel artRow)
                            continue;

                        artNom.Add((machineKey, NormalizeArtKey(artRow.pzvArticul), artRow.pzvNom));
                    }
                }
            }

            return new ExpansionState(machines, artNom);
        }

        private void RestoreExpansionState(ExpansionState state)
        {
            if (state == null || bandedGridView3 == null)
                return;

            bandedGridView3.BeginUpdate();
            try
            {
                for (int i = 0; i < bandedGridView3.DataRowCount; i++)
                {
                    if (bandedGridView3.GetRow(i) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    if (!state.MachineKeys.Contains(machineKey))
                        continue;

                    bandedGridView3.SetMasterRowExpanded(i, true);

                    if (bandedGridView3.GetDetailView(i, 0) is not DevExpress.XtraGrid.Views.BandedGrid.BandedGridView detailView)
                        continue;

                    detailView.BeginUpdate();
                    try
                    {
                        for (int j = 0; j < detailView.DataRowCount; j++)
                        {
                            if (detailView.GetRow(j) is not KnitterPZVModel artRow)
                                continue;

                            var artKey = NormalizeArtKey(artRow.pzvArticul);
                            var key = (machineKey, artKey, artRow.pzvNom);
                            if (!state.ArtNomKeys.Contains(key))
                                continue;

                            detailView.SetMasterRowExpanded(j, true);
                        }
                    }
                    finally
                    {
                        detailView.EndUpdate();
                    }
                }
            }
            finally
            {
                bandedGridView3.EndUpdate();
            }
        }

        private sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
        }
 
        private static KnitterPZVModel CreateOperationRow(KnitterPZVModel parent, nrModel nr)
        {
            var operationRow = new KnitterPZVModel
            {
                pzvID = parent.pzvID,
                pzvDivision = parent.pzvDivision,
                pzvMod = parent.pzvMod,
                pzvArticul = parent.pzvArticul,
                pzvKmlID = parent.pzvKmlID,
                kmlNumber = parent.kmlNumber,
                pzvNomZad = parent.pzvNomZad,
                pzvAnnID = parent.pzvAnnID,
                pzvNom = parent.pzvNom,
                pzvKol = parent.pzvKol,
                pzvSek = parent.pzvSek,
                pzvDateStart = parent.pzvDateStart,
                pzvDateEnd = parent.pzvDateEnd,
                pzvKolNazn = parent.pzvKolNazn,
                pzvTab = parent.pzvTab,
                n_pach = parent.n_pach,
                razm = parent.razm,
                sekEd_Effective = parent.sekEd_Effective,
                kol_Effective = parent.kol_Effective,
                nrN = nr?.nrN,
                nrN1 = nr?.nrN1,
                nrText = nr?.nrText,
                nrRazryd = nr?.nrRazryd,
                nrObor = nr?.nrObor,
                nr_kod_ob = nr?.nr_kod_ob,
                nr_kod_proizv = nr?.nr_kod_proizv
            };

            if (nr != null)
            {
                operationRow.nrModels = new BindingList<nrModel>(new List<nrModel>
                {
                    new nrModel
                    {
                        nr_kod_proizv = nr.nr_kod_proizv,
                        nrN = nr.nrN,
                        nrN1 = nr.nrN1,
                        nrRazryd = nr.nrRazryd,
                        nrText = nr.nrText,
                        nrObor = nr.nrObor,
                        nr_kod_ob = nr.nr_kod_ob,
                        kmlNumber = nr.kmlNumber
                    }
                });
            }

            return operationRow;
        }

        private static bool HasRzv(rzvModel r)
        {
            // при LEFT JOIN null уедет в 0; 0 для n_pach/код/кол в реальных данных не используется
            return r.n_pach != 0 || r.rzv_kod != 0 || r.rzv_kol != 0 || !string.IsNullOrEmpty(r.pach_kod) || !string.IsNullOrEmpty(r.razm);
        }

        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        private static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
        }

        private static bool ContainsNr(BindingList<nrModel> list, nrModel x)
        {
            // Обычно уникальность нормы внутри pzv — комбинация (nrN, nrN1, nr_kod_proizv, nr_kod_ob)
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.nrN == x.nrN && e.nrN1 == x.nrN1 && e.nr_kod_proizv == x.nr_kod_proizv && e.nr_kod_ob == x.nr_kod_ob)
                    return true;
            }
            return false;
        }

        private static bool ContainsRzv(BindingList<rzvModel> list, rzvModel x)
        {
            // для раскроя: пачка + код + размер
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.n_pach == x.n_pach && e.rzv_kod == x.rzv_kod && string.Equals(e.razm, x.razm, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var a = Block14Composer.Format(textEdit2.Text);

            textEdit3.Text = a.ToString();
        }
    }
}


