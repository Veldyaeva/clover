using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

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