# SQL-процедуры печати вязальной калькуляции

#sql #legacy

SQL-источники для [[Vyaz Econom Print Flow]]. Реализация: `VyazEconomPrintDataService`.

Основная read-часть печати объединена в одну серверную процедуру с несколькими result set.

## 1. Загрузка данных печати

Процедура: `dbo.VyazEconomPrint_LoadData`

Скрипт: `sewingproduction/Features/Sprav/Sql/VyazEconom/ACE.dbo.VyazEconomPrint_LoadData.sql`

Параметры:

```sql
@IdPodr int,
@NomZadany nvarchar(50),
@Year int
```

Процедура возвращает result set в строгом порядке:

| # | Данные | Модель C# |
|---|--------|-----------|
| 1 | Список пряжи из `dbo.v_spis_pryz` | `VyazEconomWoolRow` |
| 2 | Шапка раскроя из `dbo.raskr_zeh_vyaz` | `VyazEconomRaskrRow` |
| 3 | Диапазон пачек/размеров и количество | `VyazEconomDiapRow` |
| 4 | Приход пряжи по накладным из набора пряжи | `VyazEconomPrihodPryzRow` |
| 5 | Максимальные цены по кварталам по цветам из набора пряжи | `VyazEconomQuarterPriceRow` |

В C# result set читаются через `DbService.QueryMultipleFromProcedureAsync`, затем упаковываются в `VyazEconomPrintRows`.

## 2. Состав result set

Список пряжи:

```sql
SELECT nakl, kol, type_pryz, zvet
FROM dbo.v_spis_pryz
WHERE id_podr = @IdPodr
  AND nom_zadany = @NomZadany
```

Шапка раскроя:

```sql
SELECT TOP 1 ...
FROM dbo.raskr_zeh_vyaz
WHERE zad_pl = @NomZadany
```

Поля: `kod_k`, `articul`, `mod`, `articul_k`, `mod_k`, `zad_pl`, флаги отделки (`v`, `p`, `stir`, `pr_printer`, ...), `v_seb`, `p_seb`.

Диапазон пачек и размеров:

```sql
SELECT
  SUM(kol) AS kol,
  TRIM(STR(MIN(n_pach))) + ' - ' + TRIM(STR(MAX(n_pach))) AS diapPach,
  TRIM(MIN(razm)) + ' - ' + TRIM(MAX(razm)) AS diapSize
FROM dbo.raskr_zeh_vyaz
WHERE zad_pl = @NomZadany
```

Приход пряжи:

```sql
WITH NaklFilter AS (...)
SELECT nakl, t_articul, zvet, seb_t_m
FROM dbo.prihod_pryz
INNER JOIN NaklFilter ON ...
```

`NaklFilter` строится внутри процедуры из строк `dbo.v_spis_pryz` для текущих `@IdPodr` и `@NomZadany`. Отдельный JSON-параметр больше не нужен.

Максимальная цена по кварталам:

```sql
WITH ZvetFilter AS (...)
SELECT pp.zvet,
  MAX(CASE WHEN pv.data_sozd >= @q1Start AND pv.data_sozd <= @q1End THEN pp.seb_t_m END) AS Q1,
  ...
FROM dbo.prih_v pv
INNER JOIN dbo.prihod_v prv ON pv.np_id = prv.np_id
INNER JOIN dbo.prihod_pryz pp ON prv.kod_pr = pp.kod_pr
INNER JOIN ZvetFilter ON ...
GROUP BY pp.zvet
```

`ZvetFilter` строится внутри процедуры из строк `dbo.v_spis_pryz`. Периоды кварталов рассчитываются от `@Year`.

## 3. Отметка печати

Процедура: `dbo.VyazEconomPrint_MarkDateEconom`

Скрипт: `sewingproduction/Features/Sprav/Sql/VyazEconom/ACE.dbo.VyazEconomPrint_MarkDateEconom.sql`

```sql
UPDATE dbo.seb_vyaz_econom
SET date_econom = @DateEconom
WHERE nn = @Nn
  AND date_econom IS NULL
```

Use-case: `MarkVyazEconomPrintedUseCase`.

Отметка остаётся отдельной процедурой, потому что её можно выполнять только после успешного сохранения временного `.xlsx`. Дата печати не перезаписывается, если `date_econom` уже была заполнена.

## 4. Загрузка грида ассортимента

Процедура: `dbo.VyazKnitEconomAssort_LoadRows`

Скрипт: `sewingproduction/Features/Sprav/Sql/VyazEconom/ACE.dbo.VyazKnitEconomAssort_LoadRows.sql`

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
