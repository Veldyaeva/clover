# SQL-процедуры печати вязальной калькуляции

#sql #legacy

SQL-источники для [[Vyaz Econom Print Flow]]. Реализация: `VyazEconomPrintDataService`.

Все запросы вынесены из C# в серверные процедуры. Скрипты лежат в `sewingproduction/Features/Sprav/Sql/VyazEconom/`.

## 1. Список пряжи

Процедура: `dbo.VyazEconomPrint_GetWoolLines`

Скрипт: `ACE.dbo.VyazEconomPrint_GetWoolLines.sql`

```sql
SELECT nakl, kol, type_pryz, zvet
FROM dbo.v_spis_pryz
WHERE id_podr = @idPodr AND nom_zadany = @nomZadany
```

## 2. Шапка раскроя

Процедура: `dbo.VyazEconomPrint_GetRaskrHeader`

Скрипт: `ACE.dbo.VyazEconomPrint_GetRaskrHeader.sql`

```sql
SELECT TOP 1 ... 
FROM dbo.raskr_zeh_vyaz
WHERE zad_pl = @nomZadany
```

Поля: `kod_k`, `articul`, `mod`, `articul_k`, `mod_k`, `zad_pl`, флаги отделки (`v`, `p`, `stir`, `pr_printer`, …), `v_seb`, `p_seb`.

## 3. Диапазон пачек и размеров

Процедура: `dbo.VyazEconomPrint_GetDiap`

Скрипт: `ACE.dbo.VyazEconomPrint_GetDiap.sql`

```sql
SELECT
  SUM(kol) AS kol,
  TRIM(STR(MIN(n_pach))) + ' - ' + TRIM(STR(MAX(n_pach))) AS diapPach,
  TRIM(MIN(razm)) + ' - ' + TRIM(MAX(razm)) AS diapSize
FROM dbo.raskr_zeh_vyaz
WHERE zad_pl = @nomZadany
```

## 4. Приход пряжи партиями по накладной

Процедура: `dbo.VyazEconomPrint_GetPrihodPryzByNakls`

Скрипт: `ACE.dbo.VyazEconomPrint_GetPrihodPryzByNakls.sql`

```sql
SELECT nakl, t_articul, zvet, seb_t_m
FROM dbo.prihod_pryz
WHERE nakl IN @nakls
```

В C# список накладных передаётся JSON-массивом `@NaklsJson`; процедура разбирает его через `OPENJSON`. Аналог VFP `SEEK(nakl)` — строки без записи в отчёт не попадают.

## 5. Макс. цена по кварталам партиями по цвету

Процедура: `dbo.VyazEconomPrint_GetQuarterMaxPricesByZvet`

Скрипт: `ACE.dbo.VyazEconomPrint_GetQuarterMaxPricesByZvet.sql`

Один запрос вместо 4×N в VFP. Периоды года `@year`:

| Квартал | Начало | Конец |
|---------|--------|-------|
| 1 | 01.01 | 31.03 |
| 2 | 01.04 | 30.06 |
| 3 | 01.07 | 30.09 |
| 4 | 01.10 | 31.12 |

```sql
SELECT pp.zvet,
  MAX(CASE WHEN pv.data_sozd BETWEEN @q1Start AND @q1End THEN pp.seb_t_m END) AS Q1,
  ...
FROM prih_v v
INNER JOIN prihod_v pv ON v.np_id = pv.np_id
INNER JOIN prihod_pryz pp ON pv.kod_pr = pp.kod_pr
WHERE pp.zvet IN @zvets
GROUP BY pp.zvet
```

В C# список цветов передаётся JSON-массивом `@ZvetsJson`; процедура разбирает его через `OPENJSON`. Фильтр по `zvet` из строки пряжи (`listwool.zvet` в VFP).

## 6. Отметка печати

Процедура: `dbo.VyazEconomPrint_MarkDateEconom`

Скрипт: `ACE.dbo.VyazEconomPrint_MarkDateEconom.sql`

```sql
UPDATE dbo.seb_vyaz_econom
SET date_econom = @today
WHERE nn = @nn
  AND date_econom IS NULL
```

Use-case: `MarkVyazEconomPrintedUseCase`.

Дата печати не перезаписывается, если `date_econom` уже была заполнена.

## 7. Загрузка грида ассортимента

Процедура: `dbo.VyazKnitEconomAssort_LoadRows`

Скрипт: `ACE.dbo.VyazKnitEconomAssort_LoadRows.sql`

Используется формой [[VyazKnitEconomAssortForm]], а не самим потоком печати.

```sql
@PodrMode varchar(16),
@ShowAll bit = 0
```

`@ShowAll = 0` ограничивает список строками с пустой `date_econom` или сегодняшней датой. `@ShowAll = 1` возвращает все записи выбранного подразделения.

## связанные заметки

- [[Sprav Application Layer]]
- [[VIEW - view_seb_vyaz_econom_assort]]
- [[Vyaz Econom Print Flow]]
- [[VyazKnitEconomAssortForm]]
