using SewingProduction.Models;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Services
{
    public interface ITeamWorkDataService
    {
        /// <summary>
        /// Извлекает код из данных артикула с валидацией и логированием
        /// </summary>
        /// <param name="artData">Данные артикула</param>
        /// <param name="context">Контекст для логирования</param>
        /// <returns>Код как число или 0 в случае ошибки</returns>
        Task<int> GetKodFromArtDataAsync(MyDataART artData, string context = "GetKodFromArtData");

        /// <summary>
        /// Получает текущий выбранный артикул из BindingSource
        /// </summary>
        /// <param name="bindingSource">Источник данных</param>
        /// <returns>Текущий артикул или null</returns>
        MyDataART GetCurrentArtData(BindingSource bindingSource);
    }
}
