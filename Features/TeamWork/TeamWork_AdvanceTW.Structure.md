## TeamWork_AdvanceTW: текущая структура

Форма `TeamWork_AdvanceTW` по-прежнему работает как единый `View`, но теперь разложена на функциональные partial-блоки.

### Основной файл

- `Forms/TeamWork_AdvanceTW.cs`
  - Инициализация формы и зависимостей.
  - Подписки на события и загрузка данных.
  - Логика вкладок `Rasz/Rask/Kont`.
  - Сохранение и оркестрация операций.

### Вынесенные функциональные блоки

- `Forms/TeamWork_AdvanceTW.CloneUtils.cs`
  - Утилиты клонирования коллекций (`CloneUtils`).
  - Снимки исходных коллекций (`_originalNormRaszList`, `_originalNormRaskList`, `_originalNormKontList`).

- `Forms/TeamWork_AdvanceTW.UiState.cs`
  - UI-state и инфраструктурные методы (`InvokeAsync`).
  - Валидация формы, вывод статуса, флаг несохраненных изменений.
  - Отображение/обновление буфера и стилизация строк грида.

- `Forms/TeamWork_AdvanceTW.Contracts.cs`
  - Контракт `ITeamWorkView` (явная реализация).
  - Локальные адаптеры для `ITeamWorkDataService`, `ITeamWorkUIService`, `ITeamWorkValidationService`.
  - Поле презентера `_presenter`.

### Что это дает

- Нет изменений бизнес-логики или SQL-поведения.
- Основной файл стал компактнее: в нем оставлены сценарии, а не инфраструктурные детали.
- Внутренние не-event методы постепенно переводятся на `Task`-контракты для безопасного async-пайплайна (минимизация `async void` вне UI-обработчиков).
- Дальнейшая декомпозиция (например, вынесение `Rasz/Rask/Kont` в отдельные partial-файлы) теперь делается поэтапно и безопасно.
