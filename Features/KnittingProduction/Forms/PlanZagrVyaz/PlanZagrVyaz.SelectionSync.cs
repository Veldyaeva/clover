using SewingProduction.Core.Class;
using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz
    {
        private void SyncSelectionUpdate()
        {
            gridViewZadanyList.PostEditor();
            gridViewZadanyList.UpdateCurrentRow();
            var selectedZad = _zadanyListBindingSource?.Current as PZVZadanyList;
            if (selectedZad == null)
                return;

            //bool isSelected = selectedZad.SyncSelection == 1;
            int isSelected = Convert.ToInt32(
                    gridViewZadanyList.GetFocusedRowCellValue("SyncSelection"));

            foreach (var row in _rzvPachListByNomBindingSource.List.OfType<RzvPachListByNom>())
            {
                if (row.nom == selectedZad.nom && row.nomZad == selectedZad.pszNom)
                    //row.SyncSelection = isSelected ? 1 : 0;
                    row.SyncSelection = isSelected;
            }
            _zadanyListBindingSource.ResetBindings(false);
            gridViewZadanyList.RefreshData();

            _rzvPachListByNomBindingSource.ResetBindings(false);
            //gridViewRzvPachListByNom.RefreshData();
            gridViewRzvPachListByNom.RefreshData();
        }
    }

    //private void SyncSelectionUpdate()
    //    {
    //        try
    //        {
    //            // обязательно после BeginInvoke: BindingSource уже обновлён
    //            gridViewZadanyList.PostEditor();
    //            gridViewZadanyList.UpdateCurrentRow();

    //            var selectedRow = _zadanyListBindingSource.Current as PZVZadanyList;
    //            if (selectedRow == null)
    //                return;

    //            int isSelected = Convert.ToInt32(
    //                gridViewZadanyList.GetFocusedRowCellValue("SyncSelection"));

    //            var recordsToUpdate = _rzvPachListByNomBindingSource.List
    //                .Cast<RzvPachListByNom>()
    //                .Where(record =>
    //                    record != null &&
    //                    record.nomZad == selectedRow.pszNom &&
    //                    record.nom == selectedRow.nom)
    //                .ToList();

    //            foreach (var record in recordsToUpdate)
    //                record.SyncSelection = isSelected;

    //            _zadanyListBindingSource.ResetBindings(false);
    //            gridViewZadanyList.RefreshData();

    //            _rzvPachListByNomBindingSource.ResetBindings(false);
    //            gridViewRzvPachListByNom.RefreshData();

    //            Debug.WriteLine($"SyncSelectionUpdate completed for NomZad='{selectedRow.pszNom}', Nom='{selectedRow.nom}', IsSelected={isSelected}");
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Ошибка обновления: {ex.Message}");
    //        }
    //    }
    }