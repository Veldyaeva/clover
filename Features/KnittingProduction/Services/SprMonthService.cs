using Dapper;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Services
{
    public class SprMonthService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();

        #region
        /// <summary>
        /// Инициализирует новый экземпляр dbService.
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public SprMonthService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }
        #endregion
        public async Task<BindingSource> GetSprMonth(CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                const string query = @"select * from spr_month order by kod";
                var command = new CommandDefinition(query, new { }, cancellationToken: cancellationToken);

                var list = (await connection
                    .QueryAsync<SprMonth>(command))
                    .AsList();

                return new BindingSource
                {
                    DataSource = new BindingList<SprMonth>(list)
                };
            }
            catch (OperationCanceledException)
            {
                // отмена — НЕ ошибка
                return new BindingSource
                {
                    DataSource = new BindingList<SprMonth>()
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении данных spr_month");
                return new BindingSource
                {
                    DataSource = new BindingList<SprMonth>()
                };
            }
        }
    }
}
