# KnitterWorkSpace: загрузка данных и привязка грида

## Обзор
- Форма `KnitterWorkSpace` отображает план работ вязальщика.
- Источник данных для грида `advBandedGridView1`: результат метода `GetPlanByTabAsync` (модель `KnitterPZVModel`).
- Выбор сотрудника осуществляется через `FioGridLookUpEdit`; по умолчанию выбирается табельный `999`.

## Поток данных
1. Формирование зависимостей:
   - `KnitterRepository` — минимальный доступ к БД через `DbService`.
   - `KnitterOrchestrator` — координация вызовов без UI-логики.
2. Инициализация формы (`InitializeAsync`):
   - Загружается список сотрудников (`GetFioListAsync`).
   - Гарантируется наличие сотрудника с табелем `999` (при необходимости подмешивается через `GetFioByTabAsync`).
   - Устанавливается `EditValue = 999`, что триггерит загрузку плана.
3. Загрузка данных в грид:
   - Обработчик `FioGridLookUpEdit_EditValueChanged` вызывает `GetPlanByTabAsync(tab)`.
   - Результат загружается в `GridControl` через `GridHelper.LoadListDataAsync`.

## Ключевые методы
- `KnitterRepository.GetPlanByTabAsync(int tab)` — выборка записей плана по табельному.
- `KnitterOrchestrator.GetPlanByTabAsync(int tab)` — прокси-метод к репозиторию.
- `KnitterWorkSpace.ConfigureAdvBandedGridColumns()` — маппинг колонок на свойства `KnitterPZVModel`.

## Формат параметров
- `tab`: табельный номер выбранного сотрудника.

## Расширение функциональности
- Для выборки по конкретным партиям сформируйте `nomListJson` на основании UI (например, выбранных строк) и передайте в оркестратор.
- При необходимости измените `DefaultVyazPodrKod` в форме.

## Привязка колонок грида
- Колонки привязаны к свойствам модели `KnitterPZVModel`, базовые поля:
  - `pzvID` (ID)
  - `pzvDivision` (Подразделение)
  - `pzvMod` (Модель)
  - `pzvArticul` (Артикул)
  - `pzvKol` (Кол-во)
  - `pzvDateStart` / `pzvDateEnd` (даты)

## Обработка ошибок
- Ошибки загрузки списка сотрудников и плана показываются через `XtraMessageBox`.
