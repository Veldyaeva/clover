using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис для подготовки данных отчета технологической схемы разделения труда
    /// </summary>
    public class WorkDivisionReportService
    {
        private readonly ArtNormService _artNormService;
        private readonly DatabaseHelper _dbHelper;
        private readonly ILogger _logger = new FileLogger();

        public WorkDivisionReportService(ArtNormService artNormService, DatabaseHelper dbHelper)
        {
            _artNormService = artNormService;
            _dbHelper = dbHelper;
        }
    }
}