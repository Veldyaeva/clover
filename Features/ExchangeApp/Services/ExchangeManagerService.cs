using ExchangeApp.Data;
using ExchangeApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeApp.Services
{
    public sealed class ExchangeManagerService : IExchangeManagerService
    {
        private readonly ExchangeRepository _repository;

        public ExchangeManagerService(ExchangeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<CompanyItem>> GetCompaniesAsync()
            => _repository.GetCompaniesAsync();

        public Task<List<ExportTypeItem>> GetExportTypesAsync()
            => _repository.GetExportTypesAsync();

        public Task<List<ExchangeDocumentItem>> GetDocumentsAsync(int companyId, DateTime dateFrom, DateTime dateTo, string exportTypeCode)
            => _repository.GetDocumentsAsync(companyId, dateFrom, dateTo, exportTypeCode);

        public Task<List<ExportBatchItem>> GetBatchesAsync(int companyId)
            => _repository.GetBatchesAsync(companyId);

        public Task<long> RunPrimaryExportAsync(string exportTypeCode, int companyId, DateTime dateFrom, DateTime dateTo, string userName)
        {
            return exportTypeCode switch
            {
                "akt_furn" => _repository.RunAktFurnPrimaryAsync(companyId, dateFrom, dateTo, userName),
                _ => throw new NotSupportedException($"Первичная выгрузка для типа '{exportTypeCode}' пока не реализована.")
            };
        }

        public Task<long> CreateRequestAndRunAsync(string exportTypeCode, ExportRunMode mode, int companyId, IReadOnlyCollection<long> documentIds, string userName)
        {
            return exportTypeCode switch
            {
                "akt_furn" => _repository.RunAktFurnByRequestAsync(companyId, mode, documentIds, userName),
                _ => throw new NotSupportedException($"Догрузка/перевыгрузка для типа '{exportTypeCode}' пока не реализована.")
            };
        }
    }
}