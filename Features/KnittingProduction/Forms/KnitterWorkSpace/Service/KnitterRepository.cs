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

        /// <summary>
        /// Вызывает хранимую процедуру <c>GetPlanZagrVyazByPachList</c> для получения плана по списку партий.
        /// </summary>
        /// <param name="nomListJson">JSON массив с элементами номеров задания/номенклатуры (например: [{"nomZad":"123","nom":456}]).</param>
        /// <param name="vyazPodrKod">Код вязального подразделения.</param>
        /// <returns>Список операций плана <see cref="PlanZagrVyazOper"/>.</returns>
        public async Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod)
        {
            const string query = "EXEC GetPlanZagrVyazByPachList @xNomZadNomListJson = @nomListJson, @xVyazPodrKod = @vyazPodrKod";
            return await _dbService.GetListAsync<PlanZagrVyazOper>(query, new { nomListJson, vyazPodrKod });
        }

        /// <summary>
        /// Возвращает ФИО сотрудника по табельному номеру.
        /// </summary>
        /// <param name="tab">Табельный номер.</param>
        /// <returns>Строка ФИО или пустая строка, если не найдено.</returns>
        public async Task<string> GetFioByTabAsync(int tab)
        {
            const string query = "SELECT fio FROM dbo.fio WHERE tab = @tab";
            var fio = await _dbService.GetFirstOrDefaultAsync<string>(query, new { tab });
            return fio ?? string.Empty;
        }

        /// <summary>
        /// Возвращает справочник сотрудников (таб и ФИО) для выпадающего списка.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        public async Task<List<FioModel>> GetFioListAsync()
        {
            const string query = @"SELECT tab AS Tab, fio AS Fio FROM dbo.fio ORDER BY fio";
            return await _dbService.GetListAsync<FioModel>(query, new { });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public async Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab)
        {
            const string query = "SELECT * FROM dbo.planZagrVyaz WHERE pzvTab = @tab";
            return await _dbService.GetListAsync<PlanZagrVyaz>(query, new { tab });
        }

        public async Task UpdatePzvTabAsync(IEnumerable<int> pzvIds, int tab)
        {
            if (pzvIds == null)
                return;

            var ids = pzvIds.Distinct().ToArray();
            if (ids.Length == 0)
                return;
//SET pzvTab = @tab,
//    pzvDateNaznTab = CASE WHEN @tab = 0 THEN NULL ELSE GETDATE() END,
//    pzvKolNazn = CASE WHEN @tab = 0 THEN pzvKolNazn ELSE ISNULL(pzvKol, 0) END,
//    pzvSekNazn = CASE WHEN @tab = 0 THEN pzvSekNazn ELSE ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0) END,
//    pzvChasNazn = CASE 
//                      WHEN @tab = 0 THEN pzvChasNazn 
//                      ELSE CAST(ROUND((ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0)) / 3600.0, 2) AS decimal(18,2)) 
            using (var connection = _dbHelper.GetConnection())
            {
                const string sql = @"UPDATE dbo.planZagrVyaz
SET pzvTab = @tab,
    pzvDateNaznTab = GETDATE(),
    pzvKolNazn = ISNULL(pzvKol, 0),
    pzvSekNazn = ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0),
    pzvChasNazn = CAST(ROUND((ISNULL(pzvKol, 0) * ISNULL(pzvSek, 0)) / 3600.0, 2) AS decimal(18,2)) 
                  END
WHERE pzvID IN @ids";

                await connection.ExecuteAsync(sql, new { tab, ids });
            }
        }

        public async Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId)
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

        public async Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId)
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

        public async Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty)
        {
            // Для режима уточнения факта (mode = 1) вызываем универсальную SP PZV_Split.
            // @qtyFact – фактическое количество, @userName можно не передавать (по умолчанию NULL).
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
                    // Первая выборка может содержать сводную строку ('FinishedWithFact' и т.п.) — читаем и отбрасываем
                    if (!grid.IsConsumed)
                    {
                        try { await grid.ReadAsync(); } catch { /* ignore if no set */ }
                    }
                    // Вторая (или единственная) выборка — список новых Id (если были вставки)
                    if (!grid.IsConsumed)
                    {
                        var newIds = await grid.ReadAsync<PzvSplitResult>();
                        ids.AddRange(newIds);
                    }
                }
                return ids;
            }
        }
 

    }
}


