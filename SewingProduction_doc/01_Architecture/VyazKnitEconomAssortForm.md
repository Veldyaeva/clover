# форма ассортимента вязальной калькуляции

#ui #sql #legacy

Форма **«Калькуляция вязального ассортимента»** — read-only просмотр данных калькуляции по вязальному ассортименту.

## Расположение в коде

- `sewingproduction/Features/Sprav/Forms/VyazKnitEconomAssortForm.cs`
- `VyazKnitEconomAssortForm.Designer.cs`
- `VyazKnitEconomAssortDataService.cs`
- `VyazKnitEconomAssortRow.cs`

## Источник данных

Представление [[VIEW - view_seb_vyaz_econom_assort]] на ACE (`dbo.view_seb_vyaz_econom_assort`).

Скрипт развёртывания: `Features/Sprav/Sql/ACE.dbo.view_seb_vyaz_econom_assort.sql`.

## SQL

Загрузка грида выполняется через серверную процедуру `dbo.VyazKnitEconomAssort_LoadRows`.

Скрипт: `sewingproduction/Features/Sprav/Sql/VyazEconom/ACE.dbo.VyazKnitEconomAssort_LoadRows.sql`.

Параметры:

| Параметр | Назначение |
|----------|------------|
| `@PodrMode varchar(16)` | Фильтр подразделения: `Sock`, `Knit`, `Cord` |
| `@ShowAll bit = 0` | Управляет фильтром по `date_econom` |

Логика SQL:

- `Sock` выбирает `id_podr IN (10, 16)`.
- `Knit` выбирает `id_podr = 6`.
- `Cord` выбирает `id_podr = 19`.
- Если `@ShowAll = 0`, возвращаются только строки, где `date_econom IS NULL` или `date_econom` равна сегодняшней дате сервера.
- Если `@ShowAll = 1`, фильтр по `date_econom` отключается и возвращаются все записи выбранного подразделения.

SQL для печати вынесен в процедуры, см. [[Vyaz Econom Print Queries]].

## пользовательский интерфейс

- `CustomGridControl` + `GridView`, только чтение
- Сохранение раскладки колонок: `VyazKnitEconomAssortGrid.xml`
- Заголовочные кнопки (radio-фильтры + действия):

| Кнопка | Tag | Действие |
|--------|-----|----------|
| Носочный | `vyaz-econom:sock` | `id_podr IN (10, 16)` |
| Вязальный | `vyaz-econom:knit` | `id_podr = 6` |
| Шнуры | `vyaz-econom:cord` | `id_podr = 19` |
| Показать всё | `vyaz-econom:show-all` | checkButton: включает/выключает фильтр по `date_econom` |
| Обновить | `vyaz-econom:refresh` | перезагрузка грида |
| Печать калькуляция | `vyaz-econom:print` | [[Vyaz Econom Print Flow]] |

По умолчанию выбран фильтр **Вязальный**, а **Показать всё** загружается в состоянии `checked = false`.

## Модель строки ассортимента вязальной калькуляции

| Поле | Примечание |
|------|------------|
| `nn` | Ключ записи `seb_vyaz_econom` |
| `nom_zadany` | Номер задания (= `zad_pl` в раскрое) |
| `razm_ryad` | **string** (в БД диапазоны вида `16-18 - 18-20`) |
| `pach_min`, `pach_max` | Пачки |
| `articul`, `mod`, `grup` | Справочные поля |
| `date_econom` | Дата калькуляции |
| `seb_all` | Себестоимость всего |
| `id_podr` | Подразделение |

## программная логика

Загрузка данных:

1. `VyazKnitEconomAssortForm_Load` восстанавливает раскладку грида и вызывает `LoadDataAsync()`.
2. `LoadDataAsync()` закрывает редактор грида, показывает loading panel и вызывает `VyazKnitEconomAssortDataService.LoadRowsAsync(_podrMode, showAll)`.
3. `showAll` берётся из состояния header check-button `Показать всё`.
4. `VyazKnitEconomAssortDataService` вызывает `dbo.VyazKnitEconomAssort_LoadRows` через `DbService.GetListFromProcedureAsync`.
5. Результат перегружается в `BindingList<VyazKnitEconomAssortRow>`, затем выполняется `bindingSource.ResetBindings(false)` и `BestFitColumns()`.

Переключение фильтров:

- `Носочный`, `Вязальный`, `Шнуры` ведут себя как взаимоисключающие фильтры подразделения.
- `Показать всё` независим от фильтра подразделения и при checked/unchecked сразу перезагружает грид.
- `Обновить` перезагружает данные с текущими значениями `_podrMode` и `Показать всё`.

Печать:

По кнопке **Печать калькуляция**:

1. Берётся focused row → `VyazEconomPrintContext.FromRow`
2. Вызывается `PrintVyazEconomCalculationUseCase`
3. Use-case загружает DTO через `LoadVyazEconomPrintDataUseCase`
4. `DevExpressVyazEconomExcelExporter` сохраняет `.xlsx` и открывает файл
5. `MarkVyazEconomPrintedUseCase` проставляет `date_econom`, только если дата ещё пустая
6. Форма показывает сообщение об успехе и перезагружает грид через `LoadDataAsync()`

Если пользователь отменяет сохранение Excel, операция завершается без ошибки и без обновления `date_econom`.

## связанные заметки

- [[Sprav Application Layer]]
- [[Vyaz Econom Print Flow]]
- [[VIEW - view_seb_vyaz_econom_assort]]
- [[Sprav]]
