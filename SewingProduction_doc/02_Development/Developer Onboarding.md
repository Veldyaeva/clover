# Онбординг разработчика

## Перед первым изменением

1. Откройте SewingProduction.sln.
2. Проверьте git status из папки sewingproduction.
3. Не записывайте connection strings, пароли и токены в исходники, заметки или коммиты.
4. Прочитайте [[Solution Architecture]] и страницу нужного модуля.

## Базовые команды

~~~powershell
dotnet build SewingProduction.sln -c Debug
dotnet run --project tools/SewingProduction.Documentation -- build
dotnet run --project tools/SewingProduction.Documentation -- check
~~~

Для SQL-изменений подготовьте скрипт в Features/{Module}/Sql. Скрипт выполняется вручную в ACE после отдельной проверки; сборка приложения его не запускает.

## Перед передачей задачи

- опишите затронутый модуль и цепочку вызовов;
- обновите связанные заметки и пользовательские инструкции;
- выполните сборку и релевантные тесты;
- укажите известные риски, включая непроверенные SQL-изменения.

Подробный регламент документации: [[Documentation Workflow]].
