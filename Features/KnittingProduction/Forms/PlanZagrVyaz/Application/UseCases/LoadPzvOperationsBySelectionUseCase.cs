using Newtonsoft.Json;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Requests;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Services;
using System.Threading;
using System.Threading.Tasks;
using Formatting = Newtonsoft.Json.Formatting;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases
{
    public sealed class LoadPzvOperationsBySelectionUseCase
    {
        private readonly VyazService _vyazService;

        public LoadPzvOperationsBySelectionUseCase(VyazService vyazService)
        {
            _vyazService = vyazService;
        }

        public async Task<LoadPzvOperationsResult> ExecuteAsync(LoadPzvOperationsRequest request, CancellationToken ct = default)
        {
            string pachListJson = JsonConvert.SerializeObject(request.SelectedPachList, Formatting.Indented);

            var rows = await _vyazService.GetPZVOperListByPachList(pachListJson, request.VyazPodrKod);

            return new LoadPzvOperationsResult
            {
                Rows = rows,
                JsonPayload = pachListJson
            };
        }
    }
}