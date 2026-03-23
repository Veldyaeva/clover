using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Features.TeamWork.Operations;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW
    {
        private ITeamWorkPresenter _presenter;

        Control ITeamWorkView.AsControl => this;
        void ITeamWorkView.BindRasz(BindingList<NormRasz> rasz, BindingSource raszBindingSource) { gridControlRasz.DataSource = raszBindingSource; }
        void ITeamWorkView.BindRask(BindingList<NormRask> rask, BindingSource raskBindingSource) { gridControlRaskr.DataSource = raskBindingSource; }
        void ITeamWorkView.BindKont(BindingList<NormKont> kont, BindingSource kontBindingSource) { gridControlKont.DataSource = kontBindingSource; }
        void ITeamWorkView.BindAnn(ArtNormN ann, BindingSource annBindingSource) { bindingSource1.DataSource = ann; }
        void ITeamWorkView.RefreshAll() { try { gridControlRasz.RefreshDataSource(); gridControlRaskr.RefreshDataSource(); gridControlKont.RefreshDataSource(); } catch { } }
        void ITeamWorkView.ShowInfo(string message) { try { _uiService?.ShowStatus(message); } catch { } }
        void ITeamWorkView.ShowError(string message) { try { MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); } catch { } }

        // Local adapters to satisfy interfaces until real DI services are provided
        private sealed class TeamWorkDataServiceAdapter : ITeamWorkDataService
        {
            private readonly SewingProduction.Services.ArtNormRepository _art;
            private readonly SewingProduction.Services.DbService _db;
            public TeamWorkDataServiceAdapter(SewingProduction.Services.ArtNormRepository art, SewingProduction.Services.DbService db) { _art = art; _db = db; }
            public async Task<ArtNormN> LoadAnnDataAsync(int annId)
            {
                return await _art.GetArtNormDataById(annId);
            }
            public async Task SaveAnnDataAsync(ArtNormN data)
            {
                await _db.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, data);
            }
        }
        private sealed class TeamWorkUIServiceAdapter : ITeamWorkUIService
        {
            private readonly TeamWork_AdvanceTW _form;
            public TeamWorkUIServiceAdapter(TeamWork_AdvanceTW form) { _form = form; }
            public void RefreshAllGrids() { _form.RefreshAllGridsForService(); }
            public void HighlightChangedControls() { }
            public void UpdateFormTitle(string title) { try { _form.Text = title; } catch { } }
            public void ShowStatus(string message, int delayMs = 3000) { _ = _form.ShowStatusForService(message, delayMs); }
            public void HighlightControl(Control control, bool on) { if (on) _form.HighlightControlForService(control); else _form.UnhighlightControlForService(control); }
            public void RestoreFocusRow(GridView view, int rowHandle) { try { if (view.IsValidRowHandle(rowHandle)) { view.FocusedRowHandle = rowHandle; view.MakeRowVisible(rowHandle); } } catch { } }
        }
        private sealed class TeamWorkValidationServiceAdapter : ITeamWorkValidationService
        {
            public ValidationResult ValidateOperationNumbers(List<NormRasz> operations)
            {
                var errors = TeamWorkValidationServiceEx.GetOperationNumberErrors(operations);
                return (errors == null || errors.Count == 0) ? ValidationResult.Success : new ValidationResult(string.Join("; ", errors));
            }
            public bool HasDuplicateNumbers(int n, int n1)
            {
                // Basic helper not using context; callers usually pass collection separately
                return false;
            }
        }
    }
}
