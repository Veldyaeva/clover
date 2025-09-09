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

        /// <summary>
        /// Подготавливает данные для отчета по указанному AnnID
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        /// <returns>Данные для отчета</returns>
        public async Task<WorkDivisionReportData> PrepareReportDataAsync(int annId)
        {
            try
            {
                // Загружаем основные данные ANN
                var annData = await _artNormService.GetArtNormDataById(annId);
                if (annData == null)
                {
                    throw new ArgumentException($"Не найдены данные для AnnID: {annId}");
                }

                // Загружаем связанные данные
                var operations = await _artNormService.GetRelatedNormRasz(annId);
                var raskroyOps = await _artNormService.GetRelatedNormRask(annId);
                var kontrolOps = await _artNormService.GetRelatedNormKont(annId);

                // Загружаем данные о дизайнере и конструкторе
                var fioData = await _artNormService.GetRelDesigner();
                var designer = fioData?.FirstOrDefault(f => f.Tab == annData.Diz)?.Fio ?? "";
                var constructor = fioData?.FirstOrDefault(f => f.Tab == annData.Constr)?.Fio ?? "";

                // Создаем объект данных для отчета
                var reportData = new WorkDivisionReportData
                {
                    Articul = annData.Articul?.Trim() ?? "",
                    Grup = annData.grup?.Trim() ?? "",
                    Mod = annData.Mod?.Trim() ?? "",
                    Komment = annData.Komment?.Trim() ?? "",
                    DateCreate = annData.dateCreate,
                    Designer = designer,
                    Constructor = constructor,
                    Reco = annData.Reco?.Trim() ?? "",

                    // Основные показатели времени
                    Sek = annData.Sek,
                    SekShv = annData.SekShv,
                    SekVyaz = annData.SekVyaz,
                    SekVyaz5 = annData.SekVyaz5,
                    SekVyaz6 = annData.SekVyaz6,
                    SekVyaz7 = annData.SekVyaz7,
                    SekVyaz10 = annData.SekVyaz10,
                    SekVyaz12 = annData.SekVyaz12,
                    SekVyazo = annData.SekVyazo,
                    SekVyaz14 = annData.SekVyaz14,
                    SekVyaz70 = annData.SekVyaz70,
                    SekVyaz71 = annData.SekVyaz71,
                    SekVyaz72 = annData.SekVyaz72,
                    SekVyaz62 = annData.SekVyaz62,
                    SekVyaz57 = annData.SekVyaz57,
                    SekVyaz18 = annData.SekVyaz18,
                    SekKr = annData.SekKr,

                    // Операции
                    Operations = operations ?? new System.Collections.Generic.List<NormRasz>(),
                    RaskroyOperations = raskroyOps ?? new System.Collections.Generic.List<NormRask>(),
                    KontrolOperations = kontrolOps ?? new System.Collections.Generic.List<NormKont>()
                };

                // Рассчитываем дополнительные параметры
                CalculateAdditionalParameters(reportData);

                await _logger.LogEventAsync($"Подготовлены данные отчета для AnnID: {annId}", "WorkDivisionReportService.PrepareReportDataAsync");

                return reportData;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при подготовке данных отчета для AnnID: {annId}");
                throw;
            }
        }

        /// <summary>
        /// Рассчитывает дополнительные параметры для отчета
        /// </summary>
        /// <param name="reportData">Данные отчета</param>
        private void CalculateAdditionalParameters(WorkDivisionReportData reportData)
        {
            // Здесь можно добавить расчет дополнительных параметров
            // Например, такт потока, количество рабочих, расчетный выпуск в смену

            // Пример расчета такта потока (может потребоваться корректировка формулы)
            if (reportData.Sek > 0)
            {
                // Пример: такт потока = общее время / количество операций
                int operationsCount = reportData.Operations?.Count ?? 1;
                if (operationsCount == 0) operationsCount = 1;

                reportData.TaktPotoka = (decimal)reportData.Sek / operationsCount;
            }

            // Пример расчета расчетного выпуска в смену (480 минут в смену)
            if (reportData.Sek > 0)
            {
                reportData.RaschetVypusk = (int)(480 * 60 / reportData.Sek);
            }

            // Примерный расчет количества рабочих
            // (может потребоваться корректировка в зависимости от бизнес-логики)
            reportData.KolRab = reportData.Operations?.Count ?? 0;
        }
    }
}