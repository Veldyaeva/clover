using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис для подготовки данных отчета технологической схемы разделения труда
    /// </summary>
    public class WorkDivisionReportService
    {
        private readonly ArtNormRepository _artNormService;
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly ILogger _logger = new FileLogger();

        public WorkDivisionReportService(ArtNormRepository artNormService, DatabaseHelperSQL dbHelper)
        {
            _artNormService = artNormService;
            _dbHelper = dbHelper;
        }
    }
}