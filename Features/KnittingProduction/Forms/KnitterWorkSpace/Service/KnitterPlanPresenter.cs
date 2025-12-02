using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Отвечает за построение и выдачу данных для GridControl в режиме мастер→деталь.
    /// В текущей версии работает в двух уровнях: 1) машина (bandedGridView3), 2) операции (advBandedGridView1).
    /// Сама “шапка” второго уровня отрисовывается во вью через группировку по вычисляемой колонке.
    /// </summary>
    public class KnitterPlanPresenter
    {
        private List<KnitterPZVModel> _allRows;
        private Dictionary<string, List<KnitterPZVModel>> _byMachine;
        private Dictionary<string, List<KnitterPZVModel>> _machineArtNomMaster;
        private Dictionary<(string MachineKey, string ArtKey, int? Nom), List<KnitterPZVModel>> _machineArtNomGroups;
        private Dictionary<(string MachineKey, string ArtKey, int? Nom, int? Pach), List<KnitterPZVModel>> _machineArtNomPachGroups;
        private readonly GridExpansionService _expansionService = new GridExpansionService();

        public IReadOnlyList<KnitterPZVModel> AllRows => _allRows as IReadOnlyList<KnitterPZVModel> ?? new List<KnitterPZVModel>();

        /// <summary>
        /// Привязывает данные и настраивает поведение мастер/деталь.
        /// </summary>
        /// <param name="masterView3">Мастер-уровень (машины).</param>
        /// <param name="bandedGridView1">Резервное представление второго уровня (не используется для событий).</param>
        /// <param name="advBandedGridView1">Деталь-уровень: операции по машине.</param>
        /// <param name="bindingSource">Источник данных, привязанный к GridControl.</param>
        /// <param name="rows">Плоский список строк плана.</param>
        /// <param name="clearTabs">Если true — очищает временно pzvTab у входных строк.</param>
        public void BindGroupDetails(
            BandedGridView masterView3,
           // BandedGridView bandedGridView1,
           GridView bandedGridView1,
            AdvBandedGridView advBandedGridView1,
            System.Windows.Forms.BindingSource bindingSource,
            List<KnitterPZVModel> rows,
            bool clearTabs = true)
        {
            var expansionState = _expansionService.Capture(masterView3);

            _allRows = rows ?? new List<KnitterPZVModel>();
            if (clearTabs && _allRows != null)
            {
                foreach (var row in _allRows)
                {
                    if (row != null)
                        row.pzvTab = null;
                }
            }

            var grid = masterView3?.GridControl;
            grid?.BeginUpdate();
            try
            {
                _byMachine = _allRows
                    .GroupBy(r => KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber))
                    .ToDictionary(g => g.Key, g => g.ToList());

                _machineArtNomGroups = _allRows
                    .GroupBy(r => (KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber), KnitterPlanUtils.NormalizeArtKey(r.pzvArticul), r.pzvNom))
                    .ToDictionary(g => g.Key, g => g.ToList());

                _machineArtNomPachGroups = _allRows
                    .GroupBy(r => (KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber), KnitterPlanUtils.NormalizeArtKey(r.pzvArticul), r.pzvNom, (int?)r.n_pach))
                    .ToDictionary(g => g.Key, g => g.ToList());

                _machineArtNomMaster = _byMachine.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value
                        .GroupBy(r => (KnitterPlanUtils.NormalizeArtKey(r.pzvArticul), r.pzvNom))
                        .Select(g => g.First())
                        .ToList());

                var masterData = _byMachine.Values.Select(list => list.First()).ToList();
                bindingSource.DataSource = masterData;

                //
                masterView3.MasterRowGetRelationCount -= Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName -= Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList -= Master_MasterRowGetChildList;
                masterView3.MasterRowGetRelationCount += Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName += Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList += Master_MasterRowGetChildList;
                masterView3.OptionsDetail.EnableMasterViewMode = true;
                masterView3.OptionsDetail.AllowOnlyOneMasterRowExpanded = false;
                masterView3.OptionsDetail.ShowDetailTabs = false;

                bandedGridView1.OptionsDetail.EnableMasterViewMode = true;
                bandedGridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = false;
                bandedGridView1.OptionsDetail.AllowExpandEmptyDetails = true;
                bandedGridView1.OptionsDetail.ShowDetailTabs = false;
            }
            finally
            {
                grid?.EndUpdate();
            }

            _expansionService.Restore(masterView3, expansionState);
        }

        /// <summary>
        /// Возвращает набор записей, соответствующий текущему выбору пользователя в указанном представлении.
        /// Для уровня машин разворачивает в набор всех строк этой машины.
        /// </summary>
        public IEnumerable<KnitterPZVModel> GetRowsForViewSelection(DevExpress.XtraGrid.Views.Base.ColumnView view)
        {
            if (_allRows == null || view == null)
                return Enumerable.Empty<KnitterPZVModel>();

            var baseRows = GetRowsFromView(view).ToList();
            if (!baseRows.Any())
                return Enumerable.Empty<KnitterPZVModel>();

            IEnumerable<KnitterPZVModel> expanded;
            if (view.Name == "bandedGridView3")
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x => KnitterPlanUtils.NormalizeMachineKey(x.kmlNumber) == KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber)));
            }
            else if (view.Name == "bandedGridView1")
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x =>
                    KnitterPlanUtils.NormalizeMachineKey(x.kmlNumber) == KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber) &&
                    KnitterPlanUtils.NormalizeArtKey(x.pzvArticul) == KnitterPlanUtils.NormalizeArtKey(row.pzvArticul) &&
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

        /// <summary>Единственная деталь у машины.</summary>
        private void Master_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        /// <summary>Имя детали для GridControl.</summary>
        private void Master_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "ArtNom";
        }

        /// <summary>
        /// Возвращает список операций для выбранной машины (второй уровень).
        /// </summary>
        private void Master_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var view = sender as BandedGridView;
            var head = (KnitterPZVModel)view?.GetRow(e.RowHandle);
            if (head == null)
            {
                e.ChildList = new List<KnitterPZVModel>();
                return;
            }

            // Двухуровневый режим: возвращаем сразу операции (детали) для выбранной машины
            var machineKey = KnitterPlanUtils.NormalizeMachineKey(head.kmlNumber);

            var result = new List<KnitterPZVModel>();
            var seenOperations = new HashSet<string>();

            if (_machineArtNomGroups != null)
            {
                foreach (var kv in _machineArtNomGroups)
                {
                    if (!string.Equals(kv.Key.MachineKey, machineKey, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var groupRows = kv.Value;
                    foreach (var groupRow in groupRows)
                    {
                        var artKey = KnitterPlanUtils.NormalizeArtKey(groupRow.pzvArticul);
                        var nomKey = groupRow.pzvNom;
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

                                var operationRow = KnitterPlanUtils.CreateOperationRow(parentRow, nr);
                                operationRow.n_pach = groupRow.n_pach;
                                operationRow.razm = parentRow.razm;
                                result.Add(operationRow);
                            }
                        }
                    }
                }
            }

            e.ChildList = result;
        }

        // Второго мастер-уровня больше нет — деталь формируется сразу в Master_* обработчике
        private static IEnumerable<KnitterPZVModel> GetRowsFromView(DevExpress.XtraGrid.Views.Base.ColumnView view)
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

        // Захват/восстановление развёрнутости вынесены в GridExpansionService
    }
}
