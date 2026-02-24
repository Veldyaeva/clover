using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Сервис захвата и восстановления состояния развёрнутости master-detail для BandedGridView.
    /// Не привязан к форме; можно переиспользовать.
    /// </summary>
    public class GridExpansionService
    {
        public sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>(), new HashSet<(string MachineKey, string Header)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys, HashSet<(string MachineKey, string Header)> headerGroups)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
                HeaderGroups = headerGroups ?? new HashSet<(string MachineKey, string Header)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
            public HashSet<(string MachineKey, string Header)> HeaderGroups { get; }
        }

        public ExpansionState Capture(BandedGridView masterView3)
        {
            if (masterView3 == null || masterView3.DataRowCount == 0)
                return ExpansionState.Empty;

            var machines = new HashSet<string>();
            var artNom = new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            var headers = new HashSet<(string MachineKey, string Header)>();

            for (int i = 0; i < masterView3.DataRowCount; i++)
            {
                int rh = masterView3.GetVisibleRowHandle(i);
                if (rh < 0) continue;

                if (masterView3.IsMasterRow(rh) && masterView3.GetMasterRowExpanded(rh))
                {
                    if (masterView3.GetRow(rh) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    machines.Add(machineKey);

                    var detailView = masterView3.GetDetailView(rh, 0) as BandedGridView;
                    if (detailView != null)
                    {
                        // Art/Nom — развёрнутые группы по артикулу/ному (если есть такой уровень)
                        for (int j = 0; j < detailView.DataRowCount; j++)
                        {
                            if (!detailView.GetMasterRowExpanded(j))
                                continue;
                            if (detailView.GetRow(j) is not KnitterPZVModel artRow)
                                continue;
                            artNom.Add((machineKey, NormalizeArtKey(artRow.pzvArticul), artRow.pzvNom));
                        }

                        // Раскрытые группы по __Header (пачки) внутри detail
                        if (detailView is GridView gridDetail)
                        {
                            for (int j = 0; j < gridDetail.RowCount; j++)
                            {
                                int drh = gridDetail.GetVisibleRowHandle(j);
                                if (drh < 0) continue;
                                if (!gridDetail.IsGroupRow(drh) || gridDetail.GetRowLevel(drh) != 0)
                                    continue;
                                if (!gridDetail.GetRowExpanded(drh))
                                    continue;

                                var headerValue = gridDetail.GetGroupRowValue(drh)?.ToString();
                                if (!string.IsNullOrEmpty(headerValue))
                                    headers.Add((machineKey, headerValue));
                            }
                        }
                    }
                }
            }

            return new ExpansionState(machines, artNom, headers);
        }

        public void Restore(BandedGridView masterView3, ExpansionState state)
        {
            if (state == null || masterView3 == null)
                return;

            masterView3.BeginUpdate();
            try
            {
                for (int i = 0; i < masterView3.DataRowCount; i++)
                {
                    int rh = masterView3.GetVisibleRowHandle(i);
                    if (rh < 0) continue;

                    if (masterView3.GetRow(rh) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    if (!state.MachineKeys.Contains(machineKey))
                        continue;

                    masterView3.SetMasterRowExpanded(rh, true);

                    var detailView = masterView3.GetDetailView(rh, 0) as BandedGridView;
                    if (detailView == null)
                        continue;

                    detailView.BeginUpdate();
                    try
                    {
                        // Art/Nom — восстановление развёрнутых групп по артикулу/ному
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

                        // Группы по __Header (пачки): выставляем развёрнутость по сохранённому состоянию.
                        // Не вызываем CollapseAllGroups() — один detail view на все мастер-строки, иначе сбросим группы у предыдущей машины.
                        if (detailView is GridView gridDetail)
                        {
                            for (int j = 0; j < gridDetail.RowCount; j++)
                            {
                                int grh = gridDetail.GetVisibleRowHandle(j);
                                if (grh < 0) continue;
                                if (!gridDetail.IsGroupRow(grh) || gridDetail.GetRowLevel(grh) != 0)
                                    continue;

                                var headerVal = gridDetail.GetGroupRowValue(grh)?.ToString();
                                bool shouldExpand = !string.IsNullOrEmpty(headerVal) &&
                                    state.HeaderGroups.Contains((machineKey, headerVal));
                                gridDetail.SetRowExpanded(grh, shouldExpand);
                            }
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
                masterView3.EndUpdate();
            }
        }

        /// <summary>
        /// Находит handle групповой строки по значению группы (аналог FindGroupRow по header).
        /// </summary>
        private static int FindGroupRowByValue(GridView gridView, string headerValue)
        {
            if (gridView == null || string.IsNullOrEmpty(headerValue))
                return -1;
            for (int j = 0; j < gridView.RowCount; j++)
            {
                int rh = gridView.GetVisibleRowHandle(j);
                if (rh < 0) continue;
                if (!gridView.IsGroupRow(rh) || gridView.GetRowLevel(rh) != 0)
                    continue;
                var val = gridView.GetGroupRowValue(rh)?.ToString();
                if (string.Equals(val, headerValue, StringComparison.Ordinal))
                    return rh;
            }
            return -1;
        }

        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        private static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
        }
    }
}


