using Dapper;
using ExchangeApp.Models;
using Npgsql;
using NpgsqlTypes;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeApp.Data
{
    public sealed class ExchangeRepository
    {
        //private readonly string _connectionString;

        //public ExchangeRepository(string connectionString)
        //{
        //    _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        //}
        //private readonly DatabaseHelper _dbHelper;
        private readonly DataBaseHelperPostgreSQL _dbHelperPG;

        public ExchangeRepository()
        {
            _dbHelperPG = new DataBaseHelperPostgreSQL("cleverPG");
        }
        private NpgsqlConnection CreateConnection()
        {
            //return new NpgsqlConnection(_connectionString);
            return new NpgsqlConnection(_dbHelperPG.GetConnection().ToString());
        }

        public async Task<List<CompanyItem>> GetCompaniesAsync()
        {
            ////const string sql = @"
            ////    select
            ////        f.frm_id as company_id,
            ////        cast(f.frm_id as varchar) || ' - ' || coalesce(f.frm_naimen, '') as company_name
            ////    from mssql.firms f
            ////    where frm_id in (14188, 8306, 7666, 63684, 5914)
            ////    order by f.frm_id;";
            //const string sql = @"
            //    select
            //            f.""FRM_ID"" as company_id,
            //            cast(f.""FRM_ID"" as varchar) || ' - ' || coalesce(f.""FRM_Naimen"", '') as company_name
            //        from ""global"".""FIRMS"" f
            //        where f.""FRM_ID"" = 14188 or f.""FRM_ID"" = 8306 or f.""FRM_ID"" = 7666 or f.""FRM_ID"" = 63684
            //        order by f.""FRM_Naimen"";";
            //var result = new List<CompanyItem>();

            ////await using var cn = CreateConnection();
            //await using var cn = _dbHelperPG.GetConnection();
            ////await cn.OpenAsync();
            //if (cn.State != ConnectionState.Open)
            //    await cn.OpenAsync();

            //await using var cmd = new NpgsqlCommand(sql, cn);
            //await using var rd = await cmd.ExecuteReaderAsync();

            //while (await rd.ReadAsync())
            //{
            //    result.Add(new CompanyItem
            //    {
            //        CompanyId = rd.GetInt32(rd.GetOrdinal("company_id")),
            //        CompanyName = rd["company_name"]?.ToString()
            //    });
            //}

            //return result;


            using (var connection = _dbHelperPG.GetConnection())
            {
                string query = @"
                select
                        f.""FRM_ID"" as CompanyId,
                        f.""FRM_Naimen"" as CompanyName
                    from global_link.""FIRMS"" f
                    where f.""FRM_ID"" = 14188 or f.""FRM_ID"" = 8306 or f.""FRM_ID"" = 7666 or f.""FRM_ID"" = 63684
                    order by f.""FRM_Naimen"";";
                //cast(f.""FRM_ID"" as varchar) || ' - ' || coalesce(f.""FRM_Naimen"", '') as CompanyName
                var result = await connection.QueryAsync<CompanyItem>(query, new Dictionary<string, object> { });
                return result.ToList();
            }
        }

        public async Task<List<ExportTypeItem>> GetExportTypesAsync()
        {
            using (var connection = _dbHelperPG.GetConnection())
            {
                string query = @"
                select
                    export_type_id as ExportTypeId,
                    code as Code,
                    name as Name,
                    is_active as IsSelected
                from exchange1c.export_type
                where is_active = true
                order by name;";
                //cast(f.""FRM_ID"" as varchar) || ' - ' || coalesce(f.""FRM_Naimen"", '') as CompanyName
                var result = await connection.QueryAsync<ExportTypeItem>(query, new Dictionary<string, object> { });
                return result.ToList();
            }
            //const string sql = @"
            //    select
            //        export_type_id,
            //        code,
            //        name,
            //        is_active
            //    from exchange.export_type
            //    where is_active = true
            //    order by name;";

            //var result = new List<ExportTypeItem>();

            //await using var cn = CreateConnection();
            //await cn.OpenAsync();

            //await using var cmd = new NpgsqlCommand(sql, cn);
            //await using var rd = await cmd.ExecuteReaderAsync();

            //while (await rd.ReadAsync())
            //{
            //    result.Add(new ExportTypeItem
            //    {
            //        ExportTypeId = Convert.ToInt16(rd["export_type_id"]),
            //        Code = rd["code"]?.ToString(),
            //        Name = rd["name"]?.ToString(),
            //        IsSelected = false
            //    });
            //}

            //return result;
        }

        public async Task<List<ExchangeDocumentItem>> GetDocumentsAsync(int companyId, DateTime dateFrom, DateTime dateTo, string exportTypeCode)
        {
            using (var connection = _dbHelperPG.GetConnection())
            {
                string query = @"
                    select
                            dr.document_id as DocumentId,
                            dr.source_doc_type as SourceDocType,
                            dr.source_doc_id as SourceDocId,
                            dr.source_doc_date as SourceDocDate,
                            dr.source_company_id as SourceCompanyId,
                            dr.last_load_status as LastLoadStatus,
                            dr.needs_export as NeedsExport,
                            dr.needs_reexport as NeedsReexport,
                            0 as IsSelected
                        from exchange1c.document_registry dr
                        join exchange1c.export_type et
                          on et.export_type_id = dr.export_type_id
                        where et.code = @p_export_type_code
                          and (@p_company_id is null or dr.source_company_id = @p_company_id)
                          and (
                                dr.source_doc_date is null
                                or dr.source_doc_date::date between @p_date_from and @p_date_to
                              )
                        order by dr.source_doc_date desc nulls last, dr.source_doc_id;";
                //cast(f.""FRM_ID"" as varchar) || ' - ' || coalesce(f.""FRM_Naimen"", '') as CompanyName
                //var command = new CommandDefinition(query
                //    , new { p_export_type_codeTab = exportTypeCode
                //            , p_company_id = companyId
                //            , p_date_from = dateFrom
                //            , p_date_to = dateTo 
                //        });

                //var result = (await connection
                //    .QueryAsync<ExchangeDocumentItem>(command))
                //    .AsList();
                var result = await connection.QueryAsync<ExchangeDocumentItem>(query, new
                {
                    p_export_type_code = exportTypeCode,
                    p_company_id = companyId,
                    p_date_from = dateFrom,
                    p_date_to = dateTo
                });
                return result.ToList();
            }

//            const string sql = @"
//select
//    dr.document_id,
//    dr.source_doc_type,
//    dr.source_doc_id,
//    dr.source_doc_date,
//    dr.source_company_id,
//    dr.last_load_status,
//    dr.needs_export,
//    dr.needs_reexport
//from exchange.document_registry dr
//join exchange.export_type et
//  on et.export_type_id = dr.export_type_id
//where et.code = @p_export_type_code
//  and (@p_company_id is null or dr.source_company_id = @p_company_id)
//  and (
//        dr.source_doc_date is null
//        or dr.source_doc_date::date between @p_date_from and @p_date_to
//      )
//order by dr.source_doc_date desc nulls last, dr.source_doc_id;";

//            var result = new List<ExchangeDocumentItem>();

//            await using var cn = CreateConnection();
//            await cn.OpenAsync();

//            await using var cmd = new NpgsqlCommand(sql, cn);
//            cmd.Parameters.AddWithValue("p_export_type_code", exportTypeCode);
//            cmd.Parameters.AddWithValue("p_company_id", companyId);
//            cmd.Parameters.AddWithValue("p_date_from", dateFrom.Date);
//            cmd.Parameters.AddWithValue("p_date_to", dateTo.Date);

//            await using var rd = await cmd.ExecuteReaderAsync();

//            while (await rd.ReadAsync())
//            {
//                result.Add(new ExchangeDocumentItem
//                {
//                    IsSelected = false,
//                    DocumentId = Convert.ToInt64(rd["document_id"]),
//                    SourceDocType = rd["source_doc_type"]?.ToString(),
//                    SourceDocId = rd["source_doc_id"]?.ToString(),
//                    SourceDocDate = rd["source_doc_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(rd["source_doc_date"]),
//                    SourceCompanyId = rd["source_company_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(rd["source_company_id"]),
//                    LastLoadStatus = rd["last_load_status"]?.ToString(),
//                    NeedsExport = rd["needs_export"] != DBNull.Value && Convert.ToBoolean(rd["needs_export"]),
//                    NeedsReexport = rd["needs_reexport"] != DBNull.Value && Convert.ToBoolean(rd["needs_reexport"])
//                });
//            }

//            return result;
        }

        public async Task<List<ExportBatchItem>> GetBatchesAsync(int companyId)
        {
            using (var connection = _dbHelperPG.GetConnection())
            {
                string query = @"
                    select
                        eb.export_batch_id as ExportBatchId,
                        eb.batch_no as BatchNo,
                        et.code as ExportTypeCode,
                        eb.reason as Reason,
                        eb.status as Status,
                        eb.document_count as DocumentCount,
                        eb.row_count as RowCount,
                        eb.created_at as CreatedAt
                    from exchange1c.export_batch eb
                    join exchange1c.export_type et
                      on et.export_type_id = eb.export_type_id
                    where (@p_company_id is null or eb.source_company_id = @p_company_id)
                    order by eb.export_batch_id desc;";
                //cast(f.""FRM_ID"" as varchar) || ' - ' || coalesce(f.""FRM_Naimen"", '') as CompanyName
                //var command = new CommandDefinition(query
                //    , new { p_export_type_codeTab = exportTypeCode
                //            , p_company_id = companyId
                //            , p_date_from = dateFrom
                //            , p_date_to = dateTo 
                //        });

                //var result = (await connection
                //    .QueryAsync<ExchangeDocumentItem>(command))
                //    .AsList();
                var result = await connection.QueryAsync<ExportBatchItem>(query, new
                {
                    p_company_id = companyId
                });
                return result.ToList();
            }

            //const string sql = @"
            //    select
            //        eb.export_batch_id,
            //        eb.batch_no,
            //        et.code as export_type_code,
            //        eb.reason,
            //        eb.status,
            //        eb.document_count,
            //        eb.row_count,
            //        eb.created_at
            //    from exchange.export_batch eb
            //    join exchange.export_type et
            //      on et.export_type_id = eb.export_type_id
            //    where (@p_company_id is null or eb.source_company_id = @p_company_id)
            //    order by eb.export_batch_id desc;";

            //var result = new List<ExportBatchItem>();

            //await using var cn = CreateConnection();
            //await cn.OpenAsync();

            //await using var cmd = new NpgsqlCommand(sql, cn);
            //cmd.Parameters.AddWithValue("p_company_id", companyId);

            //await using var rd = await cmd.ExecuteReaderAsync();

            //while (await rd.ReadAsync())
            //{
            //    result.Add(new ExportBatchItem
            //    {
            //        ExportBatchId = Convert.ToInt64(rd["export_batch_id"]),
            //        BatchNo = rd["batch_no"]?.ToString(),
            //        ExportTypeCode = rd["export_type_code"]?.ToString(),
            //        Reason = rd["reason"]?.ToString(),
            //        Status = rd["status"]?.ToString(),
            //        DocumentCount = Convert.ToInt32(rd["document_count"]),
            //        RowCount = Convert.ToInt32(rd["row_count"]),
            //        CreatedAt = Convert.ToDateTime(rd["created_at"])
            //    });
            //}

            //return result;
        }

        public async Task<long> RunAktFurnPrimaryAsync(int companyId, DateTime dateFrom, DateTime dateTo, string userName)
        {
            await using var cn = CreateConnection();
            await cn.OpenAsync();

            await using var tx = await cn.BeginTransactionAsync();

            try
            {
                // Из-за особенностей CALL + INOUT в Npgsql надежнее сделать DO-блок и вернуть значение через SELECT.
                const string sql = @"
                    do $$
                    declare
                        v_batch_id bigint;
                    begin
                        call exchange1c.usp_export_akt_furn_data(
                            p_date_from => @p_date_from,
                            p_date_to => @p_date_to,
                            p_firm_id => @p_company_id,
                            p_created_by => @p_user_name,
                            p_reason => 'primary',
                            p_base_export_batch_id => null,
                            p_export_batch_id => v_batch_id
                        );

                        create temporary table if not exists tmp_export_result(batch_id bigint) on commit drop;
                        truncate table tmp_export_result;
                        insert into tmp_export_result(batch_id) values (v_batch_id);
                    end $$;

                    select batch_id from tmp_export_result limit 1;";

                await using var cmd = new NpgsqlCommand(sql, cn, tx);
                cmd.Parameters.AddWithValue("p_date_from", dateFrom.Date);
                cmd.Parameters.AddWithValue("p_date_to", dateTo.Date);
                cmd.Parameters.AddWithValue("p_company_id", companyId);
                cmd.Parameters.AddWithValue("p_user_name", userName ?? Environment.UserName);

                var result = await cmd.ExecuteScalarAsync();
                await tx.CommitAsync();

                return Convert.ToInt64(result);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<long> RunAktFurnByRequestAsync(int companyId, ExportRunMode mode, IReadOnlyCollection<long> documentIds, string userName)
        {
            if (documentIds == null || documentIds.Count == 0)
                throw new ArgumentException("Не переданы документы для догрузки/перевыгрузки.", nameof(documentIds));

            string requestType = mode switch
            {
                ExportRunMode.Delta => "delta",
                ExportRunMode.Reexport => "reexport",
                _ => throw new InvalidOperationException("Для первичной выгрузки используйте RunAktFurnPrimaryAsync.")
            };

            await using var cn = CreateConnection();
            await cn.OpenAsync();

            await using var tx = await cn.BeginTransactionAsync();

            try
            {
                long requestId;

                // 1. Создаем request
                const string createRequestSql = @"
                    do $$
                    declare
                        v_request_id bigint;
                    begin
                        call exchange1c.usp_create_export_request_for_documents(
                            p_export_type_code => 'akt_furn',
                            p_request_type => @p_request_type,
                            p_source_company_id => @p_company_id,
                            p_created_by => @p_user_name,
                            p_document_ids => @p_document_ids,
                            p_base_export_batch_id => null,
                            p_comment => @p_comment,
                            p_export_request_id => v_request_id
                        );

                        create temporary table if not exists tmp_request_result(request_id bigint) on commit drop;
                        truncate table tmp_request_result;
                        insert into tmp_request_result(request_id) values (v_request_id);
                    end $$;

                    select request_id from tmp_request_result limit 1;";

                await using (var cmd = new NpgsqlCommand(createRequestSql, cn, tx))
                {
                    cmd.Parameters.AddWithValue("p_request_type", requestType);
                    cmd.Parameters.AddWithValue("p_company_id", companyId);
                    cmd.Parameters.AddWithValue("p_user_name", userName ?? Environment.UserName);
                    cmd.Parameters.AddWithValue("p_document_ids", documentIds.ToArray());
                    cmd.Parameters.AddWithValue("p_comment", mode == ExportRunMode.Delta
                        ? "Догрузка документов из интерфейса"
                        : "Перевыгрузка документов из интерфейса");

                    var result = await cmd.ExecuteScalarAsync();
                    requestId = Convert.ToInt64(result);
                }

                // 2. Формируем пакет по request
                const string runRequestSql = @"
                    do $$
                    declare
                        v_batch_id bigint;
                    begin
                        call exchange1c.usp_export_akt_furn_by_request(
                            p_export_request_id => @p_request_id,
                            p_created_by => @p_user_name,
                            p_export_batch_id => v_batch_id
                        );

                        create temporary table if not exists tmp_batch_result(batch_id bigint) on commit drop;
                        truncate table tmp_batch_result;
                        insert into tmp_batch_result(batch_id) values (v_batch_id);
                    end $$;

                    select batch_id from tmp_batch_result limit 1;";

                long batchId;
                await using (var cmd = new NpgsqlCommand(runRequestSql, cn, tx))
                {
                    cmd.Parameters.AddWithValue("p_request_id", requestId);
                    cmd.Parameters.AddWithValue("p_user_name", userName ?? Environment.UserName);

                    var result = await cmd.ExecuteScalarAsync();
                    batchId = Convert.ToInt64(result);
                }

                await tx.CommitAsync();
                return batchId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}