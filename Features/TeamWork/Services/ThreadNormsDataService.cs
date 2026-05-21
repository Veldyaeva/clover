using Dapper;
using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{

    internal sealed class ThreadNormsDataService
    {
        private readonly DbService _dbService;

        public ThreadNormsDataService(DbService dbService, ILogger logger)
        {
            _dbService = dbService;
        }

        public Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
        {

            return _dbService.GetListFromProcedureAsync<ThreadNormRow>(
    "dbo.ThreadNorms_LoadRows",
    new { ZeroNormOnly = zeroNormOnly });
        }
        public async Task<int> SaveAsync(ThreadNormDbRow row)
        {
            var p = new DynamicParameters();
            p.AddDynamicParams(new
            {
                row.id,
                row.men,
                row.tg_id_n,
                row.ta_id,
                row.norm,
                row.kod_dr,
                row.kod3,
                row.kod_art,
                row.date_change
            });
            return await _dbService.ExecuteScalarProcedureAsync<int>(
                "cfn.ThreadNorms_Save",p
               );
        }
        public Task DeleteAsync(ThreadNormRow row)
        {
            var p = new DynamicParameters();
            p.Add("@id", row.id);
            return _dbService.ExecuteSpWithStatusAsync(
                "cfn.ThreadNorms_Delete",
                p);
        }
    }
}
