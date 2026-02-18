using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class CreateArticulMatrService
    {
        private readonly DbService _dbService;
        private readonly FileLogger _logger = new FileLogger();

        public CreateArticulMatrService()
        {
            _dbService = new DbService(new DatabaseHelper());
            //_dbHelper = new DatabaseHelper();
        }
        public async Task<BindingList<CreateArticulMatrModel>> GetMatrForArticulAsync()
        {
            try
            {
                var list = await _dbService
                    .GetListAsync<CreateArticulMatrModel>(
                        "select * from dbo.view_createarticul_matr_articul",
                        new { });

                return new BindingList<CreateArticulMatrModel>(list);

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
        }
    }
}
