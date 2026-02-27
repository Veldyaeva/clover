using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Сервис захвата и восстановления состояния развёрнутости master-detail для BandedGridView.
    ///
    /// Ключевая идея для стабильности:
    /// - НЕ используем DataRowCount + GetVisibleRowHandle (это только видимые строки).
    /// - Работаем по RowCount + IsDataRow/IsGroupRow.
    ///
    /// Art/Nom уровень отключён: в текущей модели второй master-иерархии нет,
    /// а попытка хранить/восстанавливать её даёт нестабильность.
    /// </summary>
    public class GridExpansionService
    {
        public sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(
                new HashSet<string>(),
                new HashSet<(string MachineKey, string Header)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string Header)> headerGroups)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                HeaderGroups = headerGroups ?? new HashSet<(string MachineKey, string Header)>();
            }

            /// <summary>Какие master-строки (машины) были раскрыты.</summary>
            public HashSet<string> MachineKeys { get; }

            /// <summary>Какие группы пачек (__Header) раскрыты внутри detail по каждой машине.</summary>
            public HashSet<(string MachineKey, string Header)> HeaderGroups { get; }
        }

        public ExpansionState Capture(BandedGridView masterView)
        {
            if (masterView == null || masterView.RowCount == 0)
                return ExpansionState.Empty;

            var machines = new HashSet<string>();
            var headers = new HashSet<(string MachineKey, string Header)>();

            // Идём по всем строкам View, отбираем только data rows.
            for (int rh = 0; rh < masterView.RowCount; rh++)
            {
                if (!masterView.IsDataRow(rh))
                    continue;

                if (!masterView.IsMasterRow(rh) || !masterView.GetMasterRowExpanded(rh))
                    continue;

                if (masterView.GetRow(rh) is not KnitterPZVModel machineRow)
                    continue;

                var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                if (string.IsNullOrEmpty(machineKey))
                    continue;

                machines.Add(machineKey);

                // Detail может быть GridView/AdvBandedGridView/BandedGridView — приводим к GridView.
                var detail = masterView.GetDetailView(rh, 0) as GridView;
                if (detail == null)
                    continue;

                // Группы пачек (__Header) внутри detail.
                for (int grh = 0; grh < detail.RowCount; grh++)
                {
                    if (!detail.IsGroupRow(grh) || detail.GetRowLevel(grh) != 0)
                        continue;

                    if (!detail.GetRowExpanded(grh))
                        continue;

                    var headerValue = detail.GetGroupRowValue(grh)?.ToString();
                    if (!string.IsNullOrEmpty(headerValue))
                        headers.Add((machineKey, headerValue));
                }
            }

            return new ExpansionState(machines, headers);
        }

        public void Restore(BandedGridView masterView, ExpansionState state)
        {
            if (state == null || masterView == null)
                return;

            masterView.BeginUpdate();
            try
            {
                // Идём по всем строкам, отбираем data rows.
                for (int rh = 0; rh < masterView.RowCount; rh++)
                {
                    if (!masterView.IsDataRow(rh))
                        continue;

                    if (masterView.GetRow(rh) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    if (string.IsNullOrEmpty(machineKey))
                        continue;

                    bool shouldExpandMachine = state.MachineKeys.Contains(machineKey);

                    if (masterView.IsMasterRow(rh))
                        masterView.SetMasterRowExpanded(rh, shouldExpandMachine);

                    if (!shouldExpandMachine)
                        continue;

                    // Detail может быть создан лениво — но после SetMasterRowExpanded(true) обычно уже доступен.
                    var detail = masterView.GetDetailView(rh, 0) as GridView;
                    if (detail == null)
                        continue;

                    detail.BeginUpdate();
                    try
                    {
                        // Восстанавливаем развёрнутость групп (__Header) по сохранённому состоянию.
                        // Не используем CollapseAllGroups — чтобы не ломать другие уровни/машины.
                        for (int grh = 0; grh < detail.RowCount; grh++)
                        {
                            if (!detail.IsGroupRow(grh) || detail.GetRowLevel(grh) != 0)
                                continue;

                            var headerVal = detail.GetGroupRowValue(grh)?.ToString();
                            bool shouldExpand = !string.IsNullOrEmpty(headerVal) &&
                                state.HeaderGroups.Contains((machineKey, headerVal));

                            detail.SetRowExpanded(grh, shouldExpand);
                        }
                    }
                    finally
                    {
                        detail.EndUpdate();
                    }
                }
            }
            finally
            {
                masterView.EndUpdate();
            }
        }

        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }
    }
}
