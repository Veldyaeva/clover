using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
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
        private Dictionary<(string TaskNum, string MachineKey), List<KnitterPZVModel>> _byTaskMachine;
        private Dictionary<string, List<KnitterPZVModel>> _machineArtNomMaster;
        private Dictionary<(string MachineKey, string ArtKey, int? Nom), List<KnitterPZVModel>> _machineArtNomGroups;
        private Dictionary<(string TaskNum, string MachineKey, string ArtKey, int? Nom, int? Pach), List<KnitterPZVModel>> _taskMachineArtNomPachGroups;
        public IReadOnlyList<KnitterPZVModel> AllRows => _allRows as IReadOnlyList<KnitterPZVModel> ?? new List<KnitterPZVModel>();

        /// <summary>
        /// Привязывает данные и настраивает поведение мастер/деталь.
        /// </summary>
        /// <param name="masterView3">Мастер-уровень (машины).</param>
        /// <param name="advBandedGridView1">Деталь-уровень: операции по машине.</param>
        /// <param name="bindingSource">Источник данных, привязанный к GridControl.</param>
        /// <param name="rows">Плоский список строк плана.</param>
        /// <param name="clearTabs">Если true — очищает временно pzvTab у входных строк.</param>
        public void BindGroupDetails(
            BandedGridView masterView3,
            AdvBandedGridView advBandedGridView1,
            System.Windows.Forms.BindingSource bindingSource,
            List<KnitterPZVModel> rows,
            bool clearTabs = true)
        {
            var expansionState = GridStateHelper.CaptureMasterDetailExpansion<KnitterPZVModel, (string TaskNum, string MachineKey), string>(
                masterView3,
                row => (
                    KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad),
                    KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber)),
                (detailView, groupRowHandle) => detailView.GetGroupRowValue(groupRowHandle)?.ToString() ?? string.Empty);

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
                // Группировка: сперва по заданию (taskNum), затем по номеру машины
                _byTaskMachine = _allRows
                    .GroupBy(r => (KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad), KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber)))
                    .ToDictionary(g => g.Key, g => g.ToList());

                _machineArtNomGroups = _allRows
                    .GroupBy(r => (KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber), KnitterPlanUtils.NormalizeArtKey(r.pzvArticul), r.pzvNom))
                    .ToDictionary(g => g.Key, g => g.ToList());

                _taskMachineArtNomPachGroups = _allRows
                    .GroupBy(r => (
                        TaskNum: KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad),
                        MachineKey: KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber),
                        ArtKey: KnitterPlanUtils.NormalizeArtKey(r.pzvArticul),
                        Nom: r.pzvNom,
                        Pach: (int?)r.n_pach))
                    .ToDictionary(g => g.Key, g => g.ToList());

                var masterData = _byTaskMachine.Values.Select(list =>
                {
                    var baseRow = list.First();

                    var master = new KnitterPZVModel
                    {
                        // копируем ключевые поля, которые нужны в шапке
                        pzvNomZad = baseRow.pzvNomZad,
                        kmlNumber = baseRow.kmlNumber,
                        pzvArticul = baseRow.pzvArticul,
                        pzvMod = baseRow.pzvMod,
                        pzvNom = baseRow.pzvNom,
                        koefObServ = baseRow.koefObServ,
                        name_class = baseRow.name_class,
                        pzvKmlID = baseRow.pzvKmlID
                    };

                    //  назначено = pzvChasNazn
                    master.pzvChasNazn = Math.Round(list.Sum(r => r.pzvChasNazn), 2);

                    //  факт = pzvNChasi
                    master.pzvNChasi = Math.Round(list.Sum(r => r.pzvNChasi ?? 0m), 2);

                    return master;
                }).ToList();
                if (bindingSource.DataSource is BindingList<KnitterPZVModel> bl)
                {
                    ApplyMasterDelta(bl, masterData);
                    // Модель не уведомляет об изменениях свойств (нет INotifyPropertyChanged),
                    // поэтому после in-place обновления нужно явно сообщить привязке о refresh.
                    bindingSource.ResetBindings(metadataChanged: false);
                    masterView3?.RefreshData();
                    advBandedGridView1?.RefreshData();
                }
                else
                {
                    bindingSource.DataSource = new BindingList<KnitterPZVModel>(masterData);
                }

                //
                masterView3.MasterRowGetRelationCount -= Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName -= Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList -= Master_MasterRowGetChildList;
                masterView3.MasterRowGetRelationCount += Master_MasterRowGetRelationCount;
                masterView3.MasterRowGetRelationName += Master_MasterRowGetRelationName;
                masterView3.MasterRowGetChildList += Master_MasterRowGetChildList;
                masterView3.OptionsDetail.EnableMasterViewMode = true;
                masterView3.OptionsDetail.ShowDetailTabs = false;
            }
            finally
            {
                grid?.EndUpdate();
            }

            GridStateHelper.RestoreMasterDetailExpansion<KnitterPZVModel, (string TaskNum, string MachineKey), string>(
                masterView3,
                expansionState,
                row => (
                    KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad),
                    KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber)),
                (detailView, groupRowHandle) => detailView.GetGroupRowValue(groupRowHandle)?.ToString() ?? string.Empty);
        }

        private static string MasterKey(KnitterPZVModel m) =>
    $"{KnitterPlanUtils.NormalizeTaskNum(m.pzvNomZad)}|{KnitterPlanUtils.NormalizeMachineKey(m.kmlNumber)}";

        private static void ApplyMasterDelta(BindingList<KnitterPZVModel> target, List<KnitterPZVModel> fresh)
        {
            var targetByKey = target.ToDictionary(MasterKey);
            var freshByKey = fresh.ToDictionary(MasterKey);

            // remove missing
            for (int i = target.Count - 1; i >= 0; i--)
            {
                var key = MasterKey(target[i]);
                if (!freshByKey.ContainsKey(key))
                    target.RemoveAt(i);
            }

            // update existing + add new
            foreach (var f in fresh)
            {
                var key = MasterKey(f);
                if (targetByKey.TryGetValue(key, out var t))
                {
                    // обновляем поля (ВАЖНО: не заменяем ссылку!)
                    t.pzvNomZad = f.pzvNomZad;
                    t.kmlNumber = f.kmlNumber;
                    t.pzvArticul = f.pzvArticul;
                    t.pzvMod = f.pzvMod;
                    t.pzvNom = f.pzvNom;
                    t.koefObServ = f.koefObServ;
                    t.name_class = f.name_class;
                    t.pzvKmlID = f.pzvKmlID;
                    t.pzvChasNazn = f.pzvChasNazn;
                    t.pzvNChasi = f.pzvNChasi;
                }
                else
                {
                    target.Add(f);
                }
            }
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
                expanded = baseRows.SelectMany(row => _allRows.Where(x =>
                    KnitterPlanUtils.NormalizeTaskNum(x.pzvNomZad) == KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad) &&
                    KnitterPlanUtils.NormalizeMachineKey(x.kmlNumber) == KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber)));
            }
            else if (view.Name == "bandedGridView1")
            {
                expanded = baseRows.SelectMany(row => _allRows.Where(x =>
                    KnitterPlanUtils.NormalizeTaskNum(x.pzvNomZad) == KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad) &&
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

            // Двухуровневый режим: возвращаем операции (детали) для выбранной пары (задание + машина)
            var machineKey = KnitterPlanUtils.NormalizeMachineKey(head.kmlNumber);
            var taskNum = KnitterPlanUtils.NormalizeTaskNum(head.pzvNomZad);
            var result = new List<KnitterPZVModel>();
            var seenOperations = new HashSet<string>();

            if (_taskMachineArtNomPachGroups != null)
            {
                foreach (var kv in _taskMachineArtNomPachGroups)
                {
                    if (!string.Equals(kv.Key.TaskNum, taskNum, StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!string.Equals(kv.Key.MachineKey, machineKey, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var pachRows = kv.Value;
                    foreach (var parentRow in pachRows)
                    {
                        if (parentRow?.nrModels == null || parentRow.nrModels.Count == 0)
                            continue;

                        foreach (var nr in parentRow.nrModels)
                        {
                            var signature = $"{parentRow.pzvID}_{parentRow.n_pach}_{nr.nrN}_{nr.nrN1}_{nr.nr_kod_proizv}_{nr.nr_kod_ob}";
                            if (!seenOperations.Add(signature))
                                continue;

                            var operationRow = KnitterPlanUtils.CreateOperationRow(parentRow, nr);
                            operationRow.n_pach = parentRow.n_pach;
                            operationRow.razm = parentRow.razm;
                            result.Add(operationRow);
                        }
                    }
                }
            }

            // Протаскиваем коэф. обслуживания из мастера, если он отсутствует в строках операций
            if (head.koefObServ.HasValue)
            {
                foreach (var row in result)
                {
                    if (!row.koefObServ.HasValue)
                        row.koefObServ = head.koefObServ;
                }
            }
            // сортировка операций внутри группы
            result = result
                .OrderBy(r => r.nrN ?? int.MaxValue)
                .ThenBy(r => r.nrN1 ?? 0)
                .ThenBy(r => r.pzvID)
                .ToList();


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

        // Захват/восстановление развёрнутости вынесены в общий GridStateHelper
    }
}
