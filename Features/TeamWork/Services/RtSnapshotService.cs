using Dapper;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    public sealed class RtSnapshotService
    {
        private readonly DbService _db;
        private readonly DatabaseHelper _helper;
        private readonly ILogger _logger;

        public RtSnapshotService(DbService db, DatabaseHelper helper, ILogger logger)
        {
            _db = db;
            _helper = helper;
            _logger = logger;
        }

        // Снять снимок текущего состояния ANN+Rasz+Rask+Kont
        public async Task EnsurePendingSnapshotAsync(int annId, string user = null)
        {
            using var conn = _helper.GetConnection();

            // Есть уже активный снимок? — выходим
            var has = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM dbo.rt_snapshot WHERE AnnId=@ann AND Consumed=0",
                new { ann = annId });
            if (has > 0) return;

            var dto = await ReadCurrentStateAsync(annId);

            var json = System.Text.Json.JsonSerializer.Serialize(dto,
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });

            await conn.ExecuteAsync(
                "INSERT INTO dbo.rt_snapshot(AnnId, CreatedBy, Payload) VALUES(@ann, @user, @payload)",
                new { ann = annId, user, payload = json });

            await _logger.LogEventAsync($"Снят снимок РТ AnnId={annId}", "RtSnapshot");
        }

        // Сравнить активный снимок с текущим и отдать читаемый текст
        public async Task<string> CompareWithCurrentAsync(int annId, DateTime approvedAt)
        {
            using var conn = _helper.GetConnection();
            var row = await conn.QuerySingleOrDefaultAsync<(int id, string payload)?>(
                "SELECT TOP(1) id, Payload FROM dbo.rt_snapshot WHERE AnnId=@ann AND Consumed=0 ORDER BY id DESC",
                new { ann = annId });

            if (row == null || row.Value.id == default) return "Активного снимка не найдено.";

            var oldDto = System.Text.Json.JsonSerializer.Deserialize<RtSnapshotDto>(row.Value.payload);
            var curDto = await ReadCurrentStateAsync(annId);

            var diffText = BuildTextDiff(oldDto, curDto);

            // помечаем снимок использованным
            await conn.ExecuteAsync(
                "UPDATE dbo.rt_snapshot SET Consumed=1 WHERE id=@id",
                new { id = row.Value.id });

            await _logger.LogEventAsync($"Сравнение выполнено для AnnId={annId}", "RtSnapshot");
            return $"Утверждено: {approvedAt:yyyy-MM-dd HH:mm}\r\n\r\n{diffText}";
        }

        private async Task<RtSnapshotDto> ReadCurrentStateAsync(int annId)
        {
            // Загружаем ANN, sek, seb, sek_vyaz, data_obn, sek_shvб status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_vyaz18, sek_vyaz57, sek_kr, slogn, , annDateDel, annCompDel, annDateAdd, annCompAdd, arh, parentId, annRecommendation as Reco,
            var annQuery = @"
                SELECT 
                    annId, grup, RTRIM(LTRIM(articul)) articul, mod, size_label, status_ann.name AS statusText, komment,
                    data_sozd, diz, constr
                FROM ArtNormNView 
                JOIN status_ann ON status = status_id
                WHERE annId = @annId";
            var ann = await _db.GetEntityAsync<ArtNormN>(annQuery, new { annId });

            // Загружаем Rasz
            var raszQuery = @"
                SELECT 
                    nr.AnnId, nr.N, nr.N1, nr.razryd, nr.Text,
                    nr.Sek, nr.Seb, nr.Kod, 
                    nr.kod_o AS Kod_o,       
                    nr.kod_ob AS KodOb,   
                    nr.kod_podr AS KodPodr,  
                    nr.kod_proizv AS KodProizv,
                    nr.Spec, nr.Obor, nr.nrID,
                    nr.nrDateAdd, nr.nrCompAdd, nr.nrDateDel, nr.nrCompDel,
                    kp.text_proizv as TextProizv,
                    pv.text_vyaz as TextVyaz,
                    ob.text_ob as TextOb
                FROM dbo.normraszview nr
                LEFT JOIN kod_proizv kp ON nr.kod_proizv = kp.kod_proizv
                LEFT JOIN podr_vyaz pv ON nr.kod_podr = pv.kod_vyaz
                LEFT JOIN oborud_shv ob ON nr.kod_ob = ob.kod_ob
                WHERE nr.annId = @annId";
            var rasz = await _db.GetListAsync<NormRasz>(raszQuery, new { annId });

            // Загружаем Rask
            var raskQuery = @"
                SELECT 
                    id, AnnId, kod_o, Text AS TextRask, razryd, Sek, Kod, Seb, N, n_ch AS N_ch, N1, seb_s AS Seb_s, Obor, spec
                FROM norm_rask
                WHERE AnnId = @annId";
            var rask = await _db.GetListAsync<NormRask>(raskQuery, new { annId });

            // Загружаем Kont
            var kontQuery = @"
                SELECT AnnId, kod_o, text, razryd, sek, nkId, spec, obor, kod, n, n_ch, n1, seb_s
                FROM norm_kont
                WHERE AnnId = @annId";
            var kont = await _db.GetListAsync<NormKont>(kontQuery, new { annId });

            return new RtSnapshotDto
            {
                Ann = ann,
                Rasz = rasz ?? new List<NormRasz>(),
                Rask = rask ?? new List<NormRask>(),
                Kont = kont ?? new List<NormKont>()
            };
        }

        // Построение текстового diff-а
        private string BuildTextDiff(RtSnapshotDto oldS, RtSnapshotDto curS)
        {
            if (oldS == null && curS == null) return "Изменений не обнаружено.";
            if (oldS == null || curS == null) return "Один из снимков отсутствует.";

            var sb = new StringBuilder();

            // 1) Заголовок ANN — сравним поле-значение
            if (oldS.Ann != null || curS.Ann != null)
            {
                AppendObjectDiff(sb, "ЗАГОЛОВОК", oldS.Ann, curS.Ann,
                    new[] {
                    nameof(ArtNormN.Kod), nameof(ArtNormN.Articul), nameof(ArtNormN.grup),
                    nameof(ArtNormN.Mod), nameof(ArtNormN.Sek), nameof(ArtNormN.Komment),
                    nameof(ArtNormN.Reco), nameof(ArtNormN.Diz), nameof(ArtNormN.Constr)
                    });
            }

            // 2) Операции Rasz — добавленные/удалённые/изменённые
            AppendListDiff(sb, "ОПЕРАЦИИ (Rasz)",
                oldS.Rasz ?? Enumerable.Empty<NormRasz>(),
                curS.Rasz ?? Enumerable.Empty<NormRasz>(),
                 key: r => (r.nrID > 0
        ? $"id:{r.nrID}"                                   // группировка по id
        : $"N:{r.DisplayNumber}|код:{r.Kod}|текст:{r.Text?.TrimEnd()}"), // fallback для новых/без id
                  headerOld: r => $"№ {r.DisplayNumber}",     // ← СТАРЫЙ номер из снимка
    headerTitle: r => $"№ {r.DisplayNumber}",              // ← показываем DisplayNumber
    important: r => new (string name, object value)[] {
        ("N", r.DisplayNumber), ("текст", r.Text?.TrimEnd()), ("сек", r.Sek),
        ("разряд ", r.razryd), ("произв. ", r.KodProizv),
        ("подр. ", r.KodPodr), ("обор. ", r.KodOb), ("спец. ", r.Spec)
                });

            // Раскрой Rask — добавленные/удалённые/изменённые
            AppendListDiff(sb, "РАСКРОЙ (Rask)",
                oldS.Rask ?? Enumerable.Empty<NormRask>(),
                curS.Rask ?? Enumerable.Empty<NormRask>(),
                key: x => (x.id > 0 ? $"id:{x.id}" : $"n:{x.N}|n1:{x.N1}|код:{x.Kod}|текст:{x.TextRask}"),
                headerOld: r => $"№ {r.DisplayNumber}",     // ← СТАРЫЙ номер из снимка
                headerTitle: x => $"N={x.N}, N1={x.N1}",
                important: x => new (string name, object value)[] {
                    ("N", x.N), ("N1", x.N1), ("TextRask", x.TextRask), ("сек ", x.Sek),
                    ("разряд ", x.razryd), ("код ", x.Kod), ("спец ", x.Spec), ("обор ", x.Obor)
                });

            // Контроль Kont — добавленные/удалённые/изменённые
            AppendListDiff(sb, "КОМПЛЕКТОВКА (Kont)",
                oldS.Kont ?? Enumerable.Empty<NormKont>(),
                curS.Kont ?? Enumerable.Empty<NormKont>(),
                key: x => (x.nkId > 0 ? $"id:{x.nkId}" : $"n:{x.n}|n1:{x.n1}|kod:{x.kod}|text:{x.text}"),
                headerOld: r => $"№ {r.n}",     // ← СТАРЫЙ номер из снимка
                headerTitle: x => $"n={x.n}, n1={x.n1}",
                important: x => new (string name, object value)[] {
                    ("n", x.n), ("n1", x.n1), ("text", x.text), ("sek", x.sek),
                    ("razryd", x.razryd), ("kod", x.kod), ("spec", x.spec), ("obor", x.obor)
                });

            return sb.ToString();
        }

        private static void AppendObjectDiff<T>(StringBuilder sb, string title, T oldObj, T newObj, IEnumerable<string> fields)
        {
            if (oldObj == null && newObj == null) return;

            var diffs = new List<string>();
            foreach (var name in fields)
            {
                var p = typeof(T).GetProperty(name);
                if (p == null) continue;
                var ov = oldObj != null ? p.GetValue(oldObj) : null;
                var nv = newObj != null ? p.GetValue(newObj) : null;
                if (!Equals(ov, nv))
                    diffs.Add($"• {name}: «{ov ?? "—"}» → «{nv ?? "—"}»");
            }
            if (diffs.Count > 0)
            {
                sb.AppendLine(title);
                foreach (var d in diffs)
                {
                    sb.AppendLine(d);
                }
                sb.AppendLine();
            }
        }

        private static void AppendListDiff<T>(
            StringBuilder sb, string title,
            IEnumerable<T> oldList, IEnumerable<T> newList,
            Func<T, string> key,
            Func<T, string> headerOld,                   // заголовок из СТАРОГО состояния (для ~)
            Func<T, string> headerTitle,                   // что показываем в ~ заголовке
            Func<T, (string name, object value)[]> important)
        {
            // Используем GroupBy для обработки дубликатов ключей (берем первый элемент)
            var oldMap = oldList?.GroupBy(key).ToDictionary(g => g.Key, g => g.First()) ?? new Dictionary<string, T>();
            var newMap = newList?.GroupBy(key).ToDictionary(g => g.Key, g => g.First()) ?? new Dictionary<string, T>();

            var added = newMap.Keys.Except(oldMap.Keys).ToList();
            var removed = oldMap.Keys.Except(newMap.Keys).ToList();
            var common = newMap.Keys.Intersect(oldMap.Keys).ToList();

            var has = false;
            var sec = new StringBuilder();
            foreach (var k in added)
            {
                has = true;
                var pairs = string.Join(", ", important(newMap[k]).Select(p => $"{p.name}={p.value}"));
                sec.AppendLine($"ДОБАВЛЕНО: {pairs}");
            }
            foreach (var k in removed)
            {
                has = true;
                var pairs = string.Join(", ", important(oldMap[k]).Select(p => $"{p.name}={p.value}"));
                sec.AppendLine($"УДАЛЕНО: {pairs}");
            }
            foreach (var k in common)
            {
                var o = important(oldMap[k]);
                var n = important(newMap[k]);

                var changes = o.Zip(n, (op, np) =>
                    op.value?.ToString() == np.value?.ToString() ? null : $"• {op.name}: «{op.value}» → «{np.value}»")
                    .Where(x => x != null).ToList();

                if (changes.Count > 0)
                {
                    has = true;
                    var header = headerOld(oldMap[k]);
                    sec.AppendLine($"ИЗМЕНЕНЫ: {header}");
                    foreach (var c in changes)
                    {
                        sec.AppendLine($"  {c}");
                    }
                }
            }

            if (has)
            {
                sb.AppendLine(title);
                sb.Append(sec.ToString());
                sb.AppendLine();
            }
        }

        public async Task<bool> HasPendingAsync(int annId, CancellationToken ct = default)
        {
            if (annId <= 0) return false; // защита от «новой РТ без ID»

            using var conn = _helper.GetConnection();
            var exists = await conn.ExecuteScalarAsync<int>(
                new CommandDefinition(@"
            SELECT CASE WHEN EXISTS (
                SELECT 1
                FROM dbo.rt_snapshot
                WHERE AnnId = @ann AND Consumed = 0
            ) THEN 1 ELSE 0 END",
                    new { ann = annId }, cancellationToken: ct));

            return exists == 1;
        }
    }

    public sealed class RtSnapshotDto
    {
        public ArtNormN Ann { get; set; }
        public List<NormRasz> Rasz { get; set; }
        public List<NormRask> Rask { get; set; }
        public List<NormKont> Kont { get; set; }
    }
}

