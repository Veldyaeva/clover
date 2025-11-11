using DevExpress.XtraGrid.Views.BandedGrid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Строит иерархию данных и настраивает master-detail связки для гридов.
    /// Держит состояние группировок и выбранных строк.
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

        public void BindGroupDetails(
            BandedGridView masterView3,
            BandedGridView bandedGridView1,
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

                var masterData = _byMachine.Values.Select(list => list.First()).ToList();
                bindingSource.DataSource = masterData;

                // wire top-level view
                masterView3.MasterRowGetRelationCount -= Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName -= Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList -= Master_MasterRowGetChildList;
                masterView3.MasterRowGetRelationCount += Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName += Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList += Master_MasterRowGetChildList;
                masterView3.OptionsDetail.EnableMasterViewMode = true;
                masterView3.OptionsDetail.AllowOnlyOneMasterRowExpanded = false;
                masterView3.OptionsDetail.ShowDetailTabs = false;

                // wire second-level view (bandedGridView1 -> advBandedGridView1)
                bandedGridView1.MasterRowGetRelationCount -= Detail_MasterRowGetRelationCount;
                bandedGridView1.MasterRowGetRelationName -= Detail_MasterRowGetRelationName;
                bandedGridView1.MasterRowGetChildList -= Detail_MasterRowGetChildList;
                bandedGridView1.MasterRowGetRelationCount += Detail_MasterRowGetRelationCount;
                bandedGridView1.MasterRowGetRelationName += Detail_MasterRowGetRelationName;
                bandedGridView1.MasterRowGetChildList += Detail_MasterRowGetChildList;
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

        public IEnumerable<KnitterPZVModel> GetRowsForViewSelection(BandedGridView view)
        {
            if (_allRows == null || view == null)
                return Enumerable.Empty<KnitterPZVModel>();

            var baseRows = GetRowsFromView(view).ToList();
            if (!baseRows.Any())
                return Enumerable.Empty<KnitterPZVModel>();

            IEnumerable<KnitterPZVModel> expanded;
            if (view.Name == "bandedGridView3")
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x => NormalizeMachineKey(x.kmlNumber) == NormalizeMachineKey(row.kmlNumber)));
            }
            else if (view.Name == "bandedGridView1")
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
            var view = sender as BandedGridView;
            var head = (KnitterPZVModel)view?.GetRow(e.RowHandle);
            if (head == null)
            {
                e.ChildList = new List<KnitterPZVModel>();
                return;
            }

            var machineKey = NormalizeMachineKey(head.kmlNumber);
            if (_machineArtNomMaster != null && _machineArtNomMaster.TryGetValue(machineKey, out var childRows))
                e.ChildList = childRows;
            else
                e.ChildList = new List<KnitterPZVModel>();
        }

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
            var bandedView = sender as BandedGridView;
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

        private static IEnumerable<KnitterPZVModel> GetRowsFromView(BandedGridView view)
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

        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        private static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
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

        // Захват/восстановление развёрнутости вынесены в GridExpansionService
    }
}

