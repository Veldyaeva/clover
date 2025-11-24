using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Interfaces
{
    public interface ITeamWorkDataService
    {
        Task<ArtNormN> LoadAnnDataAsync(int annId);
        Task SaveAnnDataAsync(ArtNormN data);
    }

    public interface ITeamWorkUIService
    {
        void RefreshAllGrids();
        void HighlightChangedControls();
        void UpdateFormTitle(string title);
        void ShowStatus(string message, int delayMs = 3000);
        void HighlightControl(System.Windows.Forms.Control control, bool on);
        void RestoreFocusRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle);
    }

    public interface ITeamWorkValidationService
    {
        ValidationResult ValidateOperationNumbers(List<NormRasz> operations);
        bool HasDuplicateNumbers(int n, int n1);
    }

    public interface ITeamWorkView
    {
        System.Windows.Forms.Control AsControl { get; }
        void BindRasz(System.ComponentModel.BindingList<SewingProduction.Models.NormRasz> rasz, System.Windows.Forms.BindingSource raszBindingSource);
        void BindRask(System.ComponentModel.BindingList<SewingProduction.Models.NormRask> rask, System.Windows.Forms.BindingSource raskBindingSource);
        void BindKont(System.ComponentModel.BindingList<SewingProduction.Models.NormKont> kont, System.Windows.Forms.BindingSource kontBindingSource);
        void BindAnn(SewingProduction.Models.ArtNormN ann, System.Windows.Forms.BindingSource annBindingSource);
        void RefreshAll();
        void ShowInfo(string message);
        void ShowError(string message);
    }

    public interface ITeamWorkPresenter
    {
        System.Threading.Tasks.Task InitializeAsync();
        void Attach();
        void Detach();
        System.Threading.Tasks.Task SaveAsync(bool closeAfterSave);
        System.Threading.Tasks.Task ImportFromBufferAsync();
        System.Threading.Tasks.Task DeleteSelectedAsync();
        System.Threading.Tasks.Task AddOperationAsync();
        void AttachPopupMenus(DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler raszHandler,
                              DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler kontHandler,
                              DevExpress.XtraGrid.Views.Grid.GridView kontView);
        void DetachPopupMenus();
    }
}







