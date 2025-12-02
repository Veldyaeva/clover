using DevExpress.XtraGrid.Views.BandedGrid;
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
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
        }

        public ExpansionState Capture(BandedGridView masterView3)
        {
            if (masterView3 == null || masterView3.DataRowCount == 0)
                return ExpansionState.Empty;

            var machines = new HashSet<string>();
            var artNom = new HashSet<(string MachineKey, string ArtKey, int? Nom)>();

            for (int i = 0; i < masterView3.DataRowCount; i++)
            {
                if (!masterView3.GetMasterRowExpanded(i))
                    continue;

                if (masterView3.GetRow(i) is not KnitterPZVModel machineRow)
                    continue;

                var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                machines.Add(machineKey);

                if (masterView3.GetDetailView(i, 0) is BandedGridView detailView)
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

        public void Restore(BandedGridView masterView3, ExpansionState state)
        {
            if (state == null || masterView3 == null)
                return;

            masterView3.BeginUpdate();
            try
            {
                for (int i = 0; i < masterView3.DataRowCount; i++)
                {
                    if (masterView3.GetRow(i) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    if (!state.MachineKeys.Contains(machineKey))
                        continue;

                    masterView3.SetMasterRowExpanded(i, true);

                    if (masterView3.GetDetailView(i, 0) is not BandedGridView detailView)
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
                masterView3.EndUpdate();
            }
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


