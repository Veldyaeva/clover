# Архитектура решения

Основное приложение находится в корне репозитория и собирается как .NET 8 WinForms-проект SewingProduction.csproj.

## Слои

~~~
WinForms / DevExpress
        ↓
Application / orchestrator / service
        ↓
DataService / repository
        ↓
DbService / DatabaseHelperSQL / SQL Server
~~~

- Core — запуск приложения, общая инфраструктура, БД, настройки, Service Broker и UI-база.
- Features — вертикальные модули бизнес-функциональности.
- Report — общие отчёты DevExpress.
- Help — опубликованная HTML-справка, копируемая в output.
- SewingProduction_doc — исходники базы знаний Obsidian.

## Правила изменений

- Новый SQL размещается в DataService, repository или SQL-скрипте модуля, но не в форме.
- Сложная логика формы выносится в use-case, service, orchestrator или отдельный helper.
- Формы остаются тонким UI-слоем; не создаются новые partial-файлы.
- Изменение SQL-скрипта не означает автоматическое применение к ACE.
- Связанные с модулем заметки и user-help обновляются в той же задаче.

## Документация

[[Documentation Workflow]] описывает превращение Markdown в HTML-help. Пользовательские статьи не редактируются напрямую в Help: этот каталог является опубликованным результатом.
