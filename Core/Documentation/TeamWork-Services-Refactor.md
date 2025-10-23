# TeamWork Services Refactor — что изменилось и как пользоваться

## Кратко
- Переименованы ядровые сервисы для понятности ролей:
  - ArtNormService → ArtNormRepository (репозиторий/доступ к данным)
  - TeamWorkService → TeamWorkOrchestrator (оркестратор/бизнес-координация)
- Разнесены смешанные классы по ответственности:
  - TeamWorkDataService: загрузка/сохранение одной записи ANN
  - TeamWorkUIService: адаптер вызовов в форму (UI)
  - TeamWorkValidationService: бизнес-валидации (например, номера операций)
- Обратная совместимость сохранена: старые имена доступны как thin-обёртки.

## Где лежит
- Репозиторий данных:
  - `Features/TeamWork/Services/ArtNormService.cs` → Класс: `ArtNormRepository`
- Оркестратор:
  - `Features/TeamWork/Services/TeamWorkService.cs` → Класс: `TeamWorkOrchestrator`
- Совместимость (старые имена):
  - `Features/TeamWork/Services/ArtNormRepository.cs` → класс-обёртка `ArtNormService : ArtNormRepository`
  - `Features/TeamWork/Services/TeamWorkOrchestrator.cs` → класс-обёртка `TeamWorkService : TeamWorkOrchestrator`
- Разнесённые сервисы:
  - `Features/TeamWork/Services/TeamWorkDataService.cs`
  - `Features/TeamWork/Services/TeamWorkUIService.cs`
  - `Features/TeamWork/Services/TeamWorkValidationService.cs`

## Как теперь использовать
### 1) Создание репозитория и оркестратора (рекомендуемый путь)
```csharp
var dbHelper = new DatabaseHelper();
var repo = new ArtNormRepository(dbHelper); // доступ к данным и SQL
var dbService = new DbService(dbHelper);
var logger = new FileLogger();
var orchestrator = new TeamWorkOrchestrator(repo, dbService, logger); // бизнес-координация
```

### 2) Получение списка РТ
```csharp
var all = await repo.GetArtNormData();
```

### 3) Загрузка связанных таблиц по AnnID
```csharp
int annId = 123;
var rasz = await repo.GetRelatedNormRasz(annId);
var rask = await repo.GetRelatedNormRask(annId);
var kont = await repo.GetRelatedNormKont(annId);
```

### 4) Обновление связанных данных «одной кнопкой» (оркестратор)
```csharp
var related = await orchestrator.RefreshRelatedDataAsync(annId);
if (related.Success) {
    var rasz = related.NormRasz;
    var rask = related.NormRask;
    var kont = related.NormKont;
}
```

### 5) NZP и количество назначенных операций
```csharp
var nzp = await repo.GetNzpWithPztCounts(annId);
```

### 6) UI-адаптер (обновить гриды/статусы из сервисов)
```csharp
var ui = new TeamWorkUIService(teamWorkAdvanceTwForm);
ui.RefreshAllGrids();
ui.ShowStatus("Сохранено", delayMs: 3000);
```

### 7) Валидации
```csharp
var validator = new TeamWorkValidationService();
var result = validator.ValidateOperationNumbers(raszList);
if (result != ValidationResult.Success) {
    // показать сообщение об ошибке
}
```

## Миграция старого кода (если хочется полностью перейти на новые имена)
Можно постепенно:
- Создание: заменяйте `new ArtNormService(...)` → `new ArtNormRepository(...)`
- Создание: заменяйте `new TeamWorkService(...)` → `new TeamWorkOrchestrator(...)`
- Остальной код (вызовы методов) менять не требуется: API репозитория и оркестратора сохранён.
- Для быстрого поиска:
  - Ищите по проекту `ArtNormService` и `TeamWorkService`
  - Меняйте конструкторы на новые имена классов

Старые имена оставлены как thin-обёртки для совместимости. Можно удалить их позднее, когда весь код будет переведён на новые имена.

## Почему так понятнее
- ArtNormRepository — явно указывает на слой данных (SQL/Dapper) и отсутствие UI/бизнес-оркестрации.
- TeamWorkOrchestrator — подчёркивает координацию сценариев и отсутствие прямого SQL/UI.
- Разделение Data/UI/Validation — снижает связность и упрощает сопровождение.

## Примеры в коде
- См. актуальные места инициализации в `Features/TeamWork/Forms/TeamWork.cs`.
- Для форм `TeamWork_AdvanceTW`, `NormOperNew`, `NormRaskrNew`, `PlanZagrVyaz` уже используется `ArtNormRepository`.

## Вопросы/ответы
- Можно ли и далее использовать старые имена классов?
  - Да, через класс-обёртку совместимости. Но рекомендуется переходить на новые.
- Менялся ли интерфейс методов?
  - Нет, только имена типов и разнесение по файлам.
