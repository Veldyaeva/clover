using Dapper;
using DevExpress.XtraDiagram.Base;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
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
            // Базовый путь всегда через SP4: закрытая смена, только неназначенные, без завершённых, лимит 25 часов
            return await GetPlanByTabAsync(tab, kwsId: 0, kmaId: null, onlyUnassigned: true, expandAssignedByNrId: false, maxHours: 25m);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="kwsId"></param>
        /// <param name="kmaId"></param>
        /// <param name="onlyUnassigned"></param>
        /// <param name="expandAssignedByNrId"></param>
        /// <param name="maxHours"></param>
        /// <param name="includeFinished"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, int? kmaId, bool onlyUnassigned, bool expandAssignedByNrId, decimal maxHours, bool includeFinished = false)
        {
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                using var connection = _dbHelper.GetConnection();

                var map = new ConcurrentDictionary<int, KnitterPZVModel>();

                // Multi-mapping: агрегируем строки по pzvID и наполняем коллекции операций/раскроя для детального уровня
                // SP возвращает два набора: 1) назначенные/родственные; 2) кандидаты
                // !!!! при закрытой смене (kwsId = 0/null) использовать второй набор (кандидаты) -- и переставлять местави часы и кол назн и факт --не нужно переставлять
                using (var grid = await connection.QueryMultipleAsync(
                  "dbo.GetPlanZagrVyazNorm_ByTab3",
                    new
                    {
                        tab,
                        MaxHours = maxHours,
                        OnlyActive = 1,
                        KwsId = kwsId,
                        KmaId = kmaId,
                        OnlyUnassigned = onlyUnassigned ? 1 : 0,
                        ExpandAssignedByNrId = expandAssignedByNrId ? 1 : 0,
                        IncludeFinished = includeFinished ? 1 : 0
                    },
                    commandType: CommandType.StoredProcedure))
                {
                    // Локальный helper для чтения одного result set с multi-mapping
                    void ReadAndMapSet()
                    {
                        var rows = grid.Read<KnitterPZVModel, nrModel, rzvModel, KnitterPZVModel>(
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
                            splitOn: "nrID,n_pach");

                        // Форсируем выполнение выборки, чтобы маппер отработал и наполнил словарь
                        foreach (var _ in rows) { }
                    }

                    bool hasShift = kwsId.HasValue && kwsId.Value != 0;

                    if (hasShift)
                    {
                        // Открытая смена: используем первый набор (назначенные) + второй (кандидаты) если он есть
                        ReadAndMapSet(); // первый result set
                        if (!grid.IsConsumed)
                        {
                            ReadAndMapSet(); // второй result set
                        }
                    }
                    else
                    {
                        // Закрытая смена: пропускаем первый набор, используем второй (кандидаты)
                        if (!grid.IsConsumed)
                        {
                            grid.Read(); // просто съедаем первый набор, чтобы перейти ко второму
                        }
                        if (!grid.IsConsumed)
                        {
                            ReadAndMapSet();
                        }
                    }
                }

                var parents = map.Values.ToList();

                var missingKmlIds = parents
                    .Where(p =>
                        p.pzvKmlID > 0 &&
                        (string.IsNullOrWhiteSpace(p.kmlNumber) || p.koefObServ == null || string.IsNullOrWhiteSpace(p.name_class)))
                    .Select(p => p.pzvKmlID)
                    .Distinct()
                    .ToArray();

                if (missingKmlIds.Length > 0)
                {
                    const string kmlQuery = @"
Select
    kwsmlKmlID as kmlID,
    kmlNumber,
    koefObServ,
    nameVyazClass as name_class
from ace.dbo.knitWorkingShiftStatement
where kwsmlKmlID in @ids
  and (@kmaId is null or kwsKmaId = @kmaId)";
                    // knitWorkingShiftStatement может вернуть дубли по одной машине на разные интервалы — группируем по kmlID, чтобы не падать на ToDictionary
                    var lookup = (await connection.QueryAsync<(int kmlID, string kmlNumber, decimal? koefObServ, string name_class)>(kmlQuery, new { ids = missingKmlIds, kmaId }))
                        .GroupBy(x => x.kmlID)
                        .ToDictionary(g => g.Key, g => g.First());

                    foreach (var parent in parents)
                    {
                        if (parent.pzvKmlID > 0 && lookup.TryGetValue(parent.pzvKmlID, out var kml))
                        {
                            parent.kmlNumber ??= kml.kmlNumber;
                            parent.koefObServ ??= kml.koefObServ;
                            parent.name_class ??= kml.name_class;
                        }
                    }
                }

                static void FillUi(KnitterPZVModel r)
                {
                    // - При открытии смены "назначено" в БД может не быть заполнено.
                    //   При этом pzvKol/pzvNChasi содержат ПЛАН (до начала работы), а факт в UI должен быть 0.
                    // - После начала/завершения работы pzvKol/pzvNChasi становятся ФАКТОМ, а план лежит в pzvKolNazn/pzvChasNazn.
                    // - Обнулять нужно ТОЛЬКО UI-поля, базовые колонки в модели не трогаем.

                    var planKolFromFact = (r.pzvKol ?? 0);
                    var planChasFromFact = (r.pzvNChasi ?? 0m);

                    var planKolFromNazn = (r.pzvKolNazn != 0) ? r.pzvKolNazn : planKolFromFact;
                    var planChasFromNazn = (r.pzvChasNazn != 0m) ? r.pzvChasNazn : planChasFromFact;

                    // "Начата" = есть дата начала.
                    // "Завершена" = есть дата завершения.
                    // Факт показываем ТОЛЬКО после завершения; до завершения факт = 0.
                    // Если НЕ начато — факт в UI = 0, а план в UI берём из pzvKol/pzvNChasi 
                    bool started = r.pzvDateStart != null;
                    bool finished = r.pzvDateEnd != null;

                    if (!started)
                    {
                        // План до старта берём из факт полей
                        r.PlanKol_UI = planKolFromFact;
                        r.PlanChas_UI = planChasFromFact;
                        r.FactKol_UI = 0;
                        r.FactChas_UI = 0m;
                        return;
                    }

                    // После старта: план — из Nazn, иначе fallback на факт (на случай остатка/новых строк)
                    r.PlanKol_UI = planKolFromNazn;
                    r.PlanChas_UI = planChasFromNazn;
                    if (finished)
                    {
                        r.FactKol_UI = (r.pzvKol ?? 0);
                        r.FactChas_UI = (r.pzvNChasi ?? 0m);
                    }
                    else
                    {
                        r.FactKol_UI = 0;
                        r.FactChas_UI = 0m;
                    }
                }

                foreach (var p in parents)
                    FillUi(p);

                parents = parents
                    .OrderBy(p => p.kmlNumber, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                return parents;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetPlanByTabAsync failed (tab={tab}, kwsId={kwsId}, onlyUnassigned={onlyUnassigned}, expandAssignedByNrId={expandAssignedByNrId}, maxHours={maxHours})", ex);
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
SELECT DISTINCT
    kmaeTab AS Tab,
    fio    AS Fio,
    kmaNumber AS Zone
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
                using (var connection = _dbHelper.GetConnection())
                {
                    await UpdatePzvTabAsync(connection, transaction: null, pzvIds, tab);
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

        public async Task UpdatePzvFactAsync(int pzvId, int factQty)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    const string sql = @"
UPDATE dbo.planZagrVyaz
SET pzvKol = @factQty,
    pzvNChasi = CAST(ROUND(ISNULL(pzvSek,0) * @factQty / 3600.0, 2) AS decimal(16,2))
WHERE pzvID = @pzvId;
";
                    await connection.ExecuteAsync(sql, new { pzvId, factQty });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"UpdatePzvFactAsync failed (pzvId={pzvId}, factQty={factQty})", ex);
            }
        }

        public async Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty)
        {
            try
            {
                var ids = await SplitPzvByModeAsync(pzvId, mode: 1, qtyFact: factQty);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            return ids;
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
                        if (mode == 2)
                            if (!grid.IsConsumed) //не нужно выкидывать первый набор, там данные
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


        public async Task<IEnumerable<MachineHoursStat>> AdjustNotStartedBeforeShiftEndAsync(
            int? kwsId,
            decimal minHours)
        {
            if (kwsId is null || kwsId <= 0)
                throw new ArgumentException("kwsId must be > 0 for shift end adjustment.", nameof(kwsId));

            if (minHours <= 0)
                minHours = 12m;

            try
            {
                using var connection = _dbHelper.GetConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@KwsId", kwsId.Value, DbType.Int32);
                parameters.Add("@MinHours", minHours, DbType.Decimal);

                using var multi = await connection.QueryMultipleAsync(
                    sql: "dbo.PZV_AdjustNotStartedBeforeShiftEnd",
                    param: parameters,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 60);

                var stats = (await multi.ReadAsync<MachineHoursStat>()).ToList();
                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception($"AdjustNotStartedBeforeShiftEnd failed (kwsId={kwsId}, minHours={minHours})", ex);
            }
        }
        public async Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum, int? kmsId = 0)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    using (var tx = connection.BeginTransaction())
                    {
                        try
                        {
                            var kwsId = await StartWorkingShiftAsync(connection, tx, tabStart, kmaId, kmaNum, kmsId);
                            tx.Commit();
                            return kwsId;
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
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

		public async Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId)
		{
			try
			{
				using (var connection = _dbHelper.GetConnection())
				{
					const string sql = @"
SELECT TOP 1 
	kwsID AS shiftId,
	kwsTabStart AS tabStart,
	kwsDateStart AS dateStart
FROM ACE.dbo.knitWorkingShiftNew
WHERE kwsKmaID = @kmaId
  AND (kwsDel = 0 OR kwsDel IS NULL)
  AND kwsDateEnd IS NULL
ORDER BY kwsDateStart DESC";

					var row = await connection.QueryFirstOrDefaultAsync<(int shiftId, int tabStart, DateTime? dateStart)>(sql, new { kmaId });
					if (row.shiftId == 0)
						return (null, null, null);
					return (row.shiftId, row.tabStart, row.dateStart);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"GetOpenShiftByZoneAsync failed (kmaId={kmaId})", ex);
			}
		}

		public async Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId)
		{
			try
			{
				using (var connection = _dbHelper.GetConnection())
				{
					await UpdatePzvKwsIdAsync(connection, transaction: null, pzvIds, kwsId);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"UpdatePzvKwsIdAsync failed (kwsId={kwsId})", ex);
			}
		}

        public async Task<int> StartShiftWorkflowAsync(int tabStart, int? kmaId, string kmaNum, IEnumerable<int> pzvIds)
        {
            try
            {
                var ids = NormalizeIds(pzvIds);
                using (var connection = _dbHelper.GetConnection())
                using (var tx = connection.BeginTransaction())
                {
                    try
                    {
                        await UpdatePzvTabAsync(connection, tx, ids, tabStart);
                        var shiftId = await StartWorkingShiftAsync(connection, tx, tabStart, kmaId, kmaNum, kmsId: 0);
                        await UpdatePzvKwsIdAsync(connection, tx, ids, shiftId);
                        tx.Commit();
                        return shiftId;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"StartShiftWorkflowAsync failed (tabStart={tabStart})", ex);
            }
        }

        public async Task SplitNotStartedOnShiftCloseAsync(int pzvId)
        {
            try
            {
                await SplitPzvByModeAsync(pzvId, mode: 2, qtyFact: 0);
            }
            catch (Exception ex)
            {
                throw new Exception($"SplitNotStartedOnShiftCloseAsync failed (pzvId={pzvId})", ex);
            }
        }

        public async Task CloseShiftWorkflowAsync(int shiftId, int tabEnd, decimal minHours)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                using (var tx = connection.BeginTransaction())
                {
                    try
                    {
                        await AdjustNotStartedBeforeShiftEndAsync(connection, tx, shiftId, minHours);
                        await EndWorkingShiftAsync(connection, tx, shiftId, tabEnd);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"CloseShiftWorkflowAsync failed (shiftId={shiftId}, tabEnd={tabEnd})", ex);
            }
        }

        public sealed class MachineHoursStat
        {
            public int pzvKmlID { get; set; }
            public decimal FactHours { get; set; }
            public decimal KeptAssignedHours { get; set; }
            public decimal TotalForCheck { get; set; }
            public int StillLessThanMin { get; set; }
        }

        private static int[] NormalizeIds(IEnumerable<int> pzvIds)
        {
            if (pzvIds == null)
                return Array.Empty<int>();

            return pzvIds
                .Where(id => id > 0)
                .Distinct()
                .ToArray();
        }

        private static async Task UpdatePzvTabAsync(System.Data.SqlClient.SqlConnection connection, IDbTransaction transaction, IEnumerable<int> pzvIds, int tab)
        {
            var ids = NormalizeIds(pzvIds);
            if (ids.Length == 0)
                return;

            const string sql = @"UPDATE dbo.planZagrVyaz
SET pzvTab = @tab,
    pzvDateNaznTab = GETDATE(),
    -- переносим плановое количество в назначенное
    pzvKolNazn = ISNULL(pzvKol, 0),
    pzvSekNazn = ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0),
    pzvChasNazn = CAST(ROUND((ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0)) / 3600.0, 2) AS decimal(16,2))
WHERE pzvID IN @ids";
            await connection.ExecuteAsync(sql, new { tab, ids }, transaction: transaction);
        }

        private static async Task<int> StartWorkingShiftAsync(System.Data.SqlClient.SqlConnection connection, IDbTransaction transaction, int tabStart, int? kmaId, string kmaNum, int? kmsId)
        {
            const string sqlMain = @"
INSERT INTO ACE.dbo.knitWorkingShiftNew (kwsTabStart, kwsKmaID, kwsKmsID, kwsDateStart)
VALUES (@tabStart, @kmaId, @kmsID, GETDATE());
SELECT CAST(SCOPE_IDENTITY() AS int);";
            var kwsId = await connection.ExecuteScalarAsync<int>(sqlMain, new { tabStart, kmaId, kmsId, kmaNum }, transaction: transaction);

            const string sqlList = @"
INSERT INTO ACE.dbo.knitWorkingShiftMachineListNew (kwsmlKwsID, kwsmlKmlID, kwsmlKodOb, kiwsmlLongRep)
SELECT @kwsId, mlv.kmlID, mlv.kmlKodOb, mlv.kmlLongRep
FROM ACE.dbo.knitMachineList_view mlv
WHERE mlv.kmlKmaID = @kmaId;";
            await connection.ExecuteAsync(sqlList, new { kwsId, kmaId }, transaction: transaction);

            return kwsId;
        }

        private static async Task<IEnumerable<MachineHoursStat>> AdjustNotStartedBeforeShiftEndAsync(System.Data.SqlClient.SqlConnection connection, IDbTransaction transaction, int kwsId, decimal minHours)
        {
            if (minHours <= 0)
                minHours = 12m;

            var parameters = new DynamicParameters();
            parameters.Add("@KwsId", kwsId, DbType.Int32);
            parameters.Add("@MinHours", minHours, DbType.Decimal);

            using (var multi = await connection.QueryMultipleAsync(
                sql: "dbo.PZV_AdjustNotStartedBeforeShiftEnd",
                param: parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 60))
            {
                return (await multi.ReadAsync<MachineHoursStat>()).ToList();
            }
        }

        private static async Task EndWorkingShiftAsync(System.Data.SqlClient.SqlConnection connection, IDbTransaction transaction, int shiftId, int tabEnd)
        {
            const string sql = @"
UPDATE ACE.dbo.knitWorkingShiftNew
SET kwsTabEnd = @tabEnd,
    kwsDateEnd = GETDATE()
WHERE kwsID = @shiftId AND (kwsDel = 0 OR kwsDel IS NULL) AND kwsDateEnd IS NULL;";
            await connection.ExecuteAsync(sql, new { shiftId, tabEnd }, transaction: transaction);
        }

        private static async Task UpdatePzvKwsIdAsync(System.Data.SqlClient.SqlConnection connection, IDbTransaction transaction, IEnumerable<int> pzvIds, int kwsId)
        {
            var ids = NormalizeIds(pzvIds);
            if (ids.Length == 0)
                return;

            const string sql = @"UPDATE dbo.planZagrVyaz
SET pzvKwsID = @kwsId
WHERE pzvID IN @ids";
            await connection.ExecuteAsync(sql, new { kwsId, ids }, transaction: transaction);
        }

    }
}


