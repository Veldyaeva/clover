using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExchangeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeApp.Services
{
    public sealed class FakeExchangeManagerService : IExchangeManagerService
    {
        private readonly List<ExportBatchItem> _batches = new List<ExportBatchItem>();
        private long _nextBatchId = 1000;

        public Task<List<CompanyItem>> GetCompaniesAsync()
        {
            return Task.FromResult(new List<CompanyItem>
            {
                new CompanyItem { CompanyId = 14188, CompanyName = "14188 - ЭЙС" },
                new CompanyItem { CompanyId = 14188, CompanyName = "8306 - Клевер" },
                new CompanyItem { CompanyId = 14188, CompanyName = "7666 - Эксперимент" },
                new CompanyItem { CompanyId = 14188, CompanyName = "63684 - МАЙ" },
                new CompanyItem { CompanyId = 10001, CompanyName = "10001 - Тестовая организация" }
            });
        }

        public Task<List<ExportTypeItem>> GetExportTypesAsync()
        {
            return Task.FromResult(new List<ExportTypeItem>
            {
                new ExportTypeItem { ExportTypeId = 1, Code = "akt_furn", Name = "Акт фурнитуры" },
                new ExportTypeItem { ExportTypeId = 2, Code = "pfizf", Name = "ПФИЗФ" },
                new ExportTypeItem { ExportTypeId = 3, Code = "kfizf", Name = "КФИЗФ" }
            });
        }

        public Task<List<ExchangeDocumentItem>> GetDocumentsAsync(int companyId, DateTime dateFrom, DateTime dateTo, string exportTypeCode)
        {
            var result = Enumerable.Range(1, 25)
                .Select(i => new ExchangeDocumentItem
                {
                    IsSelected = false,
                    DocumentId = i,
                    SourceDocType = "furnit_n_akt",
                    SourceDocId = $"DOC-{companyId}-{i:0000}",
                    SourceDocDate = dateFrom.Date.AddDays(i % 10),
                    SourceCompanyId = companyId,
                    LastLoadStatus = i % 5 == 0 ? "error" : "loaded",
                    NeedsExport = i % 3 == 0,
                    NeedsReexport = i % 4 == 0
                })
                .ToList();

            return Task.FromResult(result);
        }

        public Task<List<ExportBatchItem>> GetBatchesAsync(int companyId)
        {
            return Task.FromResult(_batches
                .OrderByDescending(x => x.ExportBatchId)
                .ToList());
        }

        public Task<long> RunPrimaryExportAsync(string exportTypeCode, int companyId, DateTime dateFrom, DateTime dateTo, string userName)
        {
            var id = _nextBatchId++;

            _batches.Add(new ExportBatchItem
            {
                ExportBatchId = id,
                BatchNo = $"{exportTypeCode.ToUpper()}_{DateTime.Now:yyyyMMdd_HHmmss}",
                ExportTypeCode = exportTypeCode,
                Reason = "primary",
                Status = "ready",
                DocumentCount = 25,
                RowCount = 120,
                CreatedAt = DateTime.Now
            });

            return Task.FromResult(id);
        }

        public Task<long> CreateRequestAndRunAsync(string exportTypeCode, ExportRunMode mode, int companyId, IReadOnlyCollection<long> documentIds, string userName)
        {
            var id = _nextBatchId++;

            _batches.Add(new ExportBatchItem
            {
                ExportBatchId = id,
                BatchNo = $"{exportTypeCode.ToUpper()}_{DateTime.Now:yyyyMMdd_HHmmss}",
                ExportTypeCode = exportTypeCode,
                Reason = mode == ExportRunMode.Delta ? "delta" : "reexport",
                Status = "ready",
                DocumentCount = documentIds.Count,
                RowCount = documentIds.Count * 3,
                CreatedAt = DateTime.Now
            });

            return Task.FromResult(id);
        }
    }
}