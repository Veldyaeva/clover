using NLog;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Helpers
{
    public class ArtNormController : IArtNormController
    {
        private readonly ArtNormRepository _service;
        private readonly ILogger _logger;

        public ArtNormController(ArtNormRepository service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task LoadCurrentDataAsync(int kod)
        {
            try
            {
                var result = await _service.GetArtNormDataCurrent(kod, true);
                // Дополнительная логика отображения в UI
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки текущих данных");
            }
        }

        public async Task LoadAnnByArticulAsync(int kod, string articul)
        {
            try
            {
                var result = await _service.GetArtNormDataCurrent(kod, false);
                if (articul.Contains("-"))
                {
                    string shortArt = articul.Split('-')[0];
                    var part = await _service.GetArtNormDataCurrent(shortArt);
                    result.AddRange(part);
                }

                // Конвертация и отображение
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки ANN по артикулу");
            }
        }

        public async Task<bool> InsertNormRaszAsync(NormRasz normRasz)
        {
            try
            {
                int insertedId = await _service.InsertNormRaszAsync(normRasz);
                return insertedId > 0;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка добавления norm_rasz");
                return false;
            }
        }

        public async Task<bool> InsertNormRaskAsync(NormRask normRask)
        {
            try
            {
                int insertedId = await _service.InsertNormRaskAsync(normRask);
                return insertedId > 0;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка добавления norm_rask");
                return false;
            }
        }
    }

}
