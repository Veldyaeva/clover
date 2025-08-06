using SewingProduction.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Services
{
    public class TeamWorkDataService : ITeamWorkDataService
    {
        private readonly HybridLogger _logger;

        public TeamWorkDataService(HybridLogger logger)
        {
            _logger = logger;
        }

        public async Task<int> GetKodFromArtDataAsync(MyDataART artData, string context = "GetKodFromArtData")
        {
            // Проверяем входные данные
            if (artData?.kodd_rt == null)
            {
                return 0;
            }

            // Пытаемся преобразовать строку в число
            if (int.TryParse(artData.kodd_rt, out int kodValue))
            {
                return kodValue;
            }

            // Логируем ошибку преобразования
            await _logger.LogEventAsync(
                $"Не удалось преобразовать Kod '{artData.kodd_rt}' в число.",
                context);

            return 0;
        }
        public MyDataART GetCurrentArtData(BindingSource bindingSource)
        {
            return bindingSource?.Current as MyDataART;
        }

    }
}
