using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    public class TeamWorkDataService : ITeamWorkDataService
    {
        private readonly ArtNormService _artNormService;
        private readonly DbService _dbService;

        public TeamWorkDataService(ArtNormService artNormService, DbService dbService)
        {
            _artNormService = artNormService;
            _dbService = dbService;
        }

        public async Task<ArtNormN> LoadAnnDataAsync(int annId)
        {
            return await _artNormService.GetArtNormDataById(annId);
        }

        public async Task SaveAnnDataAsync(ArtNormN data)
        {
            await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, data);
        }
    }

    public class TeamWorkUIService : ITeamWorkUIService
    {
        private readonly TeamWork_AdvanceTW _form;

        public TeamWorkUIService(TeamWork_AdvanceTW form)
        {
            _form = form;
        }

        public void RefreshAllGrids()
        {
            _form?.Invoke(new System.Action(() =>
            {
                _form.RefreshAllGridsForService();
            }));
        }

        public void HighlightChangedControls()
        {
            // Заглушка: конкретная реализация подсветки останется в форме
        }

        public void UpdateFormTitle(string title)
        {
            if (_form != null && !_form.IsDisposed)
            {
                _form.Text = title;
            }
        }
    }

    public class TeamWorkValidationService : ITeamWorkValidationService
    {
        private readonly HashSet<(int N, int N1)> _seen = new HashSet<(int N, int N1)>();
        public ValidationResult ValidateOperationNumbers(List<NormRasz> operations)
        {
            if (operations == null) return ValidationResult.Success;
            _seen.Clear();
            foreach (var op in operations)
            {
                var key = (op.N, op.N1);
                if (!_seen.Add(key))
                {
                    return new ValidationResult($"Дублирующийся номер операции: {op.N}.{op.N1}");
                }
            }
            return ValidationResult.Success;
        }

        public bool HasDuplicateNumbers(int n, int n1)
        {
            var key = (n, n1);
            return _seen.Contains(key);
        }
    }
}


