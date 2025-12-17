using Dapper;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Репозиторий данных для KnitterWorkSpace (минимальный слой доступа к БД).
    /// </summary>
    public class KnitterRepository : IKnitterRepository
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public KnitterRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(dbHelper);
        }

        /// <summary>
        /// Возвращает список записей плана загрузки вязальщика по табельному номеру.
        /// </summary>
        /// <param name="tab">Табельный номер сотрудника.</param>
        /// <returns>Список укороченной модели <see cref="KnitterPZVModel"/> для отображения.</returns>
        public async Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab)
        {
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                var map = new ConcurrentDictionary<int, KnitterPZVModel>();

                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.QueryAsync<KnitterPZVModel, nrModel, rzvModel, KnitterPZVModel>(
                        "EXEC dbo.GetPlanZagrVyazNorm_ByTab @tab, @OnlyActive;",
                        (pzv, nr, rzv) =>
                        {
                            var parent = map.GetOrAdd(pzv.pzvID, _ =>
                            {
                                pzv.nrModels ??= new BindingList<nrModel>();
                                pzv.rzvModels ??= new BindingList<rzvModel>();
                                return pzv;
                            });

                            if (nr != null)
                            {
                                if (string.IsNullOrWhiteSpace(parent.kmlNumber) && !string.IsNullOrWhiteSpace(nr.kmlNumber))
                                {
                                    parent.kmlNumber = nr.kmlNumber;
                                }
                                //if (string.IsNullOrWhiteSpace(parent.pzvNomZad) && !string.IsNullOrWhiteSpace(pzv.pzvNomZad))
                                //{
                                //    parent.pzvNomZad = pzv.pzvNomZad;
                                //}
                                if (parent.nrN == null)
                                    parent.nrN = nr.nrN;
                                if (parent.nrN1 == null)
                                    parent.nrN1 = nr.nrN1;
                                if (string.IsNullOrWhiteSpace(parent.nrText))
                                    parent.nrText = nr.nrText;
                                if (parent.nrRazryd == null)
                                    parent.nrRazryd = nr.nrRazryd;
                                if (string.IsNullOrWhiteSpace(parent.nrObor))
                                    parent.nrObor = nr.nrObor;
                                if (parent.nr_kod_ob == null)
                                    parent.nr_kod_ob = nr.nr_kod_ob;
                                if (parent.nr_kod_proizv == null)
                                    parent.nr_kod_proizv = nr.nr_kod_proizv;

                                parent.nrModels ??= new BindingList<nrModel>();
                                if (!KnitterPlanUtils.ContainsNr(parent.nrModels, nr))
                                {
                                    parent.nrModels.Add(nr);
                                }
                            }

                            if (rzv != null && KnitterPlanUtils.HasRzv(rzv))
                            {
                                if (rzv.n_pach != 0)
                                {
                                    parent.n_pach ??= rzv.n_pach;
                                }

                                if (string.IsNullOrWhiteSpace(parent.razm) && !string.IsNullOrWhiteSpace(rzv.razm))
                                {
                                    parent.razm = rzv.razm;
                                }

                                parent.rzvModels ??= new BindingList<rzvModel>();
                                if (!KnitterPlanUtils.ContainsRzv(parent.rzvModels, rzv))
                                {
                                    parent.rzvModels.Add(rzv);
                                }
                            }

                            return parent;
                        },
                        new { tab, OnlyActive = 1 },
                        splitOn: "nrID,n_pach",
                        commandType: CommandType.Text,
                        buffered: true);

                    var parents = map.Values.ToList();

                    var missingKmlIds = parents
                        .Where(p => p.pzvKmlID > 0 && string.IsNullOrWhiteSpace(p.kmlNumber))
                        .Select(p => p.pzvKmlID)
                        .Distinct()
                        .ToArray();

                    if (missingKmlIds.Length > 0)
                    {
                        const string kmlQuery = "SELECT kmlID, kmlNumber FROM dbo.view_kml_vyaz WHERE kmlID IN @ids";
                        var lookup = (await connection.QueryAsync<(int kmlID, string kmlNumber)>(kmlQuery, new { ids = missingKmlIds }))
                            .ToDictionary(x => x.kmlID, x => x.kmlNumber);

                        foreach (var parent in parents)
                        {
                            if (string.IsNullOrWhiteSpace(parent.kmlNumber) && lookup.TryGetValue(parent.pzvKmlID, out var number))
                            {
                                parent.kmlNumber = number;
                            }
                        }
                    }

                    return parents;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"GetPlanByTabAsync failed (tab={tab})", ex);
            }
        }

        /// <summary>
        /// Возвращает ФИО сотрудника по табельному номеру.
        /// </summary>
        /// <param name="tab">Табельный номер.</param>
        /// <returns>Строка ФИО или пустая строка, если не найдено.</returns>
        public async Task<string> GetFioByTabAsync(int tab)
        {
            try
            {
                const string query = "SELECT fio FROM dbo.fio WHERE tab = @tab";
                var fio = await _dbService.GetFirstOrDefaultAsync<string>(query, new { tab });
                return fio ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetFioByTabAsync failed (tab={tab})", ex);
            }
        }

        /// <summary>
        /// Возвращает справочник сотрудников (таб и ФИО) для выпадающего списка.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        public async Task<List<FioModel>> GetFioListAsync()
        {
            try
            {
				// Берём данные из knitMachineAreaEmp_view, как в сплеше
				const string query = @"
SELECT DISTINCT kmaeTab AS Tab, fio AS Fio
FROM ACE.dbo.knitMachineAreaEmp_view
WHERE ((kmaeDel = 0 OR kmaeDel IS NULL) and kmaIDNazn = 6)
ORDER BY fio";
				return await _dbService.GetListAsync<FioModel>(query, new { });
            }
            catch (Exception ex)
            {
                throw new Exception("GetFioListAsync failed", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public async Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab)
        {
            try
            {
                const string query = "SELECT * FROM dbo.planZagrVyaz WHERE pzvTab = @tab";
                return await _dbService.GetListAsync<PlanZagrVyaz>(query, new { tab });
            }
            catch (Exception ex)
            {
                throw new Exception($"GetPlanTreeByTabAsync failed (tab={tab})", ex);
            }
        }

        public async Task UpdatePzvTabAsync(IEnumerable<int> pzvIds, int tab)
        {
            try
            {
                if (pzvIds == null)
                    return;

                var ids = pzvIds.Distinct().ToArray();
                if (ids.Length == 0)
                    return;
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"UPDATE dbo.planZagrVyaz
SET pzvTab = @tab,
    pzvDateNaznTab = GETDATE(),
    pzvKolNazn = ISNULL(pzvKol, 0),
    pzvSekNazn = ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0),
    pzvChasNazn = CAST(ROUND((ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0)) / 3600.0, 2) AS decimal(18,2)) 
WHERE pzvID IN @ids";

                    await connection.ExecuteAsync(sql, new { tab, ids });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"UpdatePzvTabAsync failed (tab={tab})", ex);
            }
        }

        public async Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"
UPDATE dbo.planZagrVyaz
SET pzvDateStart = GETDATE()
WHERE pzvID = @pzvId;
SELECT pzvID, pzvDateStart FROM dbo.planZagrVyaz WHERE pzvID = @pzvId;";
                    var result = await connection.QuerySingleAsync<(int pzvID, DateTime? pzvDateStart)>(sql, new { pzvId });
                    return new KnitterPZVModel { pzvID = result.pzvID, pzvDateStart = result.pzvDateStart };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"UpdatePzvDateStartAsync failed (pzvId={pzvId})", ex);
            }
        }

        public async Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"
UPDATE dbo.planZagrVyaz
SET pzvDateEnd = GETDATE()
WHERE pzvID = @pzvId;
SELECT pzvID, pzvDateEnd FROM dbo.planZagrVyaz WHERE pzvID = @pzvId;";
                    var result = await connection.QuerySingleAsync<(int pzvID, DateTime? pzvDateEnd)>(sql, new { pzvId });
                    return new KnitterPZVModel { pzvID = result.pzvID, pzvDateEnd = result.pzvDateEnd };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"UpdatePzvDateEndAsync failed (pzvId={pzvId})", ex);
            }
        }

        public async Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var parameters = new
                    {
                        pzvId,
                        mode = 1,
                        qtyFact = factQty,
                        userName = (string)null
                    };
                    var ids = new List<PzvSplitResult>();
                    using (var grid = await connection.QueryMultipleAsync(
                        "dbo.PZV_Split",
                        param: parameters,
                        commandTimeout: 60,
                        commandType: CommandType.StoredProcedure))
                    {
                        if (!grid.IsConsumed)
                        {
                            try
                            {
                                var newIds = await grid.ReadAsync<PzvSplitResult>();
                                ids.AddRange(newIds);
                            }
                            catch
                            {
                                // ignore if no set
                            }
                        }
                        if (!grid.IsConsumed)
                        {
                            var newIds = await grid.ReadAsync<PzvSplitResult>();
                            ids.AddRange(newIds);
                        }
                    }
                    return ids;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"SplitPzvByFactAsync failed (pzvId={pzvId}, factQty={factQty})", ex);
            }
        }

		public async Task<IReadOnlyList<PzvSplitResult>> SplitPzvByModeAsync(int pzvId, int mode, int qtyFact)
		{
			try
			{
				using (var connection = _dbHelper.GetConnection())
				{
					var parameters = new
					{
						pzvId,
						mode,
						qtyFact,
						userName = (string)null
					};
					var ids = new List<PzvSplitResult>();
					using (var grid = await connection.QueryMultipleAsync(
						"dbo.PZV_Split",
						param: parameters,
						commandTimeout: 60,
						commandType: CommandType.StoredProcedure))
					{
						if (!grid.IsConsumed)
						{
							try
							{
								var head = await grid.ReadAsync();
								// ignore head set if present
							}
							catch { }
						}
						if (!grid.IsConsumed)
						{
							var newIds = await grid.ReadAsync<PzvSplitResult>();
							ids.AddRange(newIds);
						}
					}
					return ids;
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"SplitPzvByModeAsync failed (pzvId={pzvId}, mode={mode}, qtyFact={qtyFact})", ex);
			}
		}

        public async Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum, int? kmsId = 0)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                   // await connection.OpenAsync();
                    using (var tx = connection.BeginTransaction())
                    {
                        const string sqlMain = @"
INSERT INTO ACE.dbo.knitWorkingShiftNew (kwsTabStart, kwsKmaID, kwsKmsID, kwsDateStart)
VALUES (@tabStart, @kmaId, @kmsID, GETDATE());
SELECT CAST(SCOPE_IDENTITY() AS int);";
                        var kwsId = await connection.ExecuteScalarAsync<int>(sqlMain, new { tabStart, kmaId, kmsId, kmaNum }, transaction: tx);

                        // запись машин зоны в таблицу knitWorkingShiftMachineListNew
                        const string sqlList = @"
INSERT INTO ACE.dbo.knitWorkingShiftMachineListNew (kwsmlKwsID, kwsmlKmlID, kwsmlKodOb, kiwsmlLongRep)
SELECT @kwsId, mlv.kmlID, mlv.kmlKodOb, mlv.kmlLongRep
FROM ACE.dbo.knitMachineList_view mlv
WHERE mlv.kmlKmaID = @kmaId;";
                        await connection.ExecuteAsync(sqlList, new { kwsId, kmaId }, transaction: tx);

                        tx.Commit();
                        return kwsId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"StartWorkingShiftAsync failed (tabStart={tabStart})", ex);
            }
        }

        public async Task EndWorkingShiftAsync(int shiftId, int tabEnd)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"
UPDATE ACE.dbo.knitWorkingShiftNew
SET kwsTabEnd = @tabEnd,
    kwsDateEnd = GETDATE()
WHERE kwsID = @shiftId AND (kwsDel = 0 OR kwsDel IS NULL) AND kwsDateEnd IS NULL;";
                    await connection.ExecuteAsync(sql, new { shiftId, tabEnd });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"EndWorkingShiftAsync failed (shiftId={shiftId}, tabEnd={tabEnd})", ex);
            }
        }

        public async Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"
SELECT TOP 1 
    kmae.kmaeKmaID AS kmaId, 
    kmae.kmaNumber AS kmaNum

FROM ACE.dbo.knitMachineAreaEmp_view kmae
WHERE kmaeTab = @tab
ORDER BY kmaID";
                    var result = await connection.QueryFirstOrDefaultAsync<(int? kmaId, string kmaNum)>(sql, new { tab });
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"GetZoneByTabAsync failed (tab={tab})", ex);
            }
        }

		public async Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab)
		{
			try
			{
				using (var connection = _dbHelper.GetConnection())
				{
					const string sql = @"
SELECT TOP 1 
	kwsID AS shiftId,
	kwsDateStart AS dateStart
FROM ACE.dbo.knitWorkingShiftNew
WHERE kwsTabStart = @tab
  AND (kwsDel = 0 OR kwsDel IS NULL)
  AND kwsDateEnd IS NULL
ORDER BY kwsDateStart DESC";
					var row = await connection.QueryFirstOrDefaultAsync<(int shiftId, DateTime? dateStart)>(sql, new { tab });
					if (row.shiftId == 0)
						return (null, null);
					return (row.shiftId, row.dateStart);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"GetOpenShiftByTabAsync failed (tab={tab})", ex);
			}
		}

		public async Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId)
		{
			try
			{
				if (pzvIds == null)
					return;
				var ids = pzvIds.Distinct().ToArray();
				if (ids.Length == 0)
					return;
				using (var connection = _dbHelper.GetConnection())
				{
					const string sql = @"UPDATE dbo.planZagrVyaz
SET pzvKwsID = @kwsId
WHERE pzvID IN @ids";
					await connection.ExecuteAsync(sql, new { kwsId, ids });
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"UpdatePzvKwsIdAsync failed (kwsId={kwsId})", ex);
			}
		}

    }
}


