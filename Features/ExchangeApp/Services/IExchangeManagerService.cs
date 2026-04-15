using ExchangeApp.Data;
using ExchangeApp.Models;
using SewingProduction.Helpers;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ExchangeApp.Services
{
    public interface IExchangeManagerService
    {
        Task<List<CompanyItem>> GetCompaniesAsync();
        Task<List<ExportTypeItem>> GetExportTypesAsync();

        Task<List<ExchangeDocumentItem>> GetDocumentsAsync(
            int companyId,
            DateTime dateFrom,
            DateTime dateTo,
            string exportTypeCode);

        Task<List<ExportBatchItem>> GetBatchesAsync(int companyId);

        Task<long> RunPrimaryExportAsync(
            string exportTypeCode,
            int companyId,
            DateTime dateFrom,
            DateTime dateTo,
            string userName);

        Task<long> CreateRequestAndRunAsync(
            string exportTypeCode,
            ExportRunMode mode,
            int companyId,
            IReadOnlyCollection<long> documentIds,
            string userName);
    }
}