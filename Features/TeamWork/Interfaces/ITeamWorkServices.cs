using SewingProduction.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
    }

    public interface ITeamWorkValidationService
    {
        ValidationResult ValidateOperationNumbers(List<NormRasz> operations);
        bool HasDuplicateNumbers(int n, int n1);
    }
}


