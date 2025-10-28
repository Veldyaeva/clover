## KnitterWorkSpace — форма, данные и мастер–деталь представление

### Назначение
- Форма `KnitterWorkSpace` показывает план загрузки вязальщика и связанные операции.
- Источник данных берётся из представления БД `vwPlanZagrVyazNorm_ByTab`.

### Ключевые UI-элементы
- `FioGridLookUpEdit` — выбор сотрудника (табельный номер).
- `PlanZagrVyazGridControl` — основной `GridControl`.
  - `gridView1` — мастер-вью (шапки групп).
  - `bandedGridView1` — дочерняя вью (детали группы).

### Модели данных
- `KnitterPZVModel` (упрощённая строка плана и операции):
  - Основные: `pzvID`, `pzvDivision`, `pzvMod`, `pzvArticul`, `pzvNomZad`, `pzvAnnID`, `pzvNom`, `pzvKol`, `pzvDateStart`, `pzvDateEnd`.
  - Для деталей (из той же вьюхи): `N`, `N1`, `nrText`.

### Поток данных и загрузка
1) При создании формы инициализируются зависимости: `KnitterRepository` → `KnitterOrchestrator`.
2) В `InitializeAsync` загружается список сотрудников (`GetFioListAsync`), при необходимости подмешивается дефолтный таб, выставляется `EditValue`.
3) Изменение `FioGridLookUpEdit` вызывает `GetPlanByTabAsync(tab)` и передаёт результат в `BindGroupDetails`.

### Группировка «мастер–деталь» (групповая деталь)
- Ключ группы: `(pzvNomZad, pzvAnnID)`.
- `_allRows` — полный список строк `KnitterPZVModel` из `vwPlanZagrVyazNorm_ByTab`.
- `_byGroup` — словарь групп: ключ → список строк той же группы.
- Мастер (`gridView1`) показывает по одной «шапке» на группу: первый элемент каждой группы.
- Деталь для раскрытой строки мастера — все строки этой группы (включая поля `N`, `N1`, `nrText`).

Связка реализована обработчиками:
- `MasterRowGetRelationCount` → `1`.
- `MasterRowGetRelationName` → `"Items"`.
- `MasterRowGetChildList` → отдаёт список `_byGroup[key]` для текущей шапки.

В `Designer` настроено:
- `PlanZagrVyazGridControl.LevelTree` содержит узел с `LevelTemplate = bandedGridView1` и `RelationName = "Items"`.
- В `bandedGridView1` колонки привязаны к `N`, `N1`, `nrText`.

### Репозиторий и доступ к данным
- `KnitterRepository.GetPlanByTabAsync(int tab)`:
  - Выполняет `SELECT * FROM dbo.vwPlanZagrVyazNorm_ByTab WHERE pzvTab = @tab`.
  - Возвращает список `KnitterPZVModel`, содержащий и шапочные, и детальные поля.

### Сценарии работы
- Выбор сотрудника → загрузка строк из вью → группировка в памяти → показ шапок.
- Раскрытие мастера → в детальной вью показываются элементы той же группы, где присутствуют `N`, `N1`, `nrText`.

### Расширение/кастомизация
- Изменить ключ группировки: заменить функцию группировки в `BindGroupDetails` (например, только `pzvNomZad` или только `pzvAnnID`).
- Ограничить/расширить детальные поля: настроить колонки `bandedGridView1`.
- Форматирование дат/чисел: сконфигурировать формат `GridColumn.DisplayFormat` в `ConfigureAdvBandedGridColumns` или в Designer.

### Обработка ошибок
- Ошибки загрузки ФИО и плана обрабатываются через `XtraMessageBox`.

### Быстрый чек-лист интеграции
- Колонки `bandedGridView1` привязаны к `N`, `N1`, `nrText`.
- `RelationName` уровня — `"Items"` (совпадает с хендлерами мастера).
- `GetPlanByTabAsync` возвращает строки из `vwPlanZagrVyazNorm_ByTab` (содержит детальные поля).
- `BindGroupDetails` формирует `_byGroup` и подключает обработчики мастера.

