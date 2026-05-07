using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Models;
using System.ComponentModel;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW
    {
        private ITeamWorkPresenter _presenter;

        private void LogContractsSuppressed(string context, System.Exception ex = null)
        {
            _ = _logger.LogErrorAsync(ex ?? new System.Exception("Suppressed exception"), $"Contracts suppressed: {context}");
        }

        Control ITeamWorkView.AsControl => this;
        void ITeamWorkView.BindRasz(BindingList<NormRasz> rasz, BindingSource raszBindingSource) { gridControlRasz.DataSource = raszBindingSource; }
        void ITeamWorkView.BindRask(BindingList<NormRask> rask, BindingSource raskBindingSource) { gridControlRaskr.DataSource = raskBindingSource; }
        void ITeamWorkView.BindKont(BindingList<NormKont> kont, BindingSource kontBindingSource) { gridControlKont.DataSource = kontBindingSource; }
        void ITeamWorkView.BindAnn(ArtNormN ann, BindingSource annBindingSource) { bindingSource1.DataSource = ann; }
        void ITeamWorkView.RefreshAll()
        {
            try
            {
                gridControlRasz.RefreshDataSource();
                gridControlRaskr.RefreshDataSource();
                gridControlKont.RefreshDataSource();
            }
            catch (System.Exception ex)
            {
                LogContractsSuppressed("ITeamWorkView.RefreshAll", ex);
            }
        }

        void ITeamWorkView.ShowInfo(string message)
        {
            try { _uiService?.ShowStatus(message); }
            catch (System.Exception ex) { LogContractsSuppressed("ITeamWorkView.ShowInfo", ex); }
        }

        void ITeamWorkView.ShowError(string message)
        {
            try { MessageBox.Show(message, "??????", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (System.Exception ex) { LogContractsSuppressed("ITeamWorkView.ShowError", ex); }
        }
    }
}
