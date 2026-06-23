# Работа с документацией

SewingProduction_doc — исходник базы знаний. Каталог Help содержит публикуемый HTML и не редактируется вручную для статей нового формата.

## Когда обновлять

Документация обновляется в той же задаче, если изменились:

- бизнес-поток, форма или пункт меню;
- контракт DataService, view или stored procedure;
- запуск, настройка, сборка или развёртывание;
- пользовательское действие, которое требует инструкции.

## Пользовательская статья

Создайте Markdown в 05_UserGuide/{Module}. В front matter обязательны:

~~~markdown
---
title: "Название формы"
audience: user
module: Sprav
formType: SewingProduction.Features.Sprav.Forms.FormName
helpPath: Features/Sprav/Forms/FormName.html
reviewedOn: 2026-06-22
sourcePath: Features/Sprav/Forms/FormName.cs
---
~~~

helpPath должен совпадать с маршрутом, который вычисляет HelpForm.

## Публикация

Из корня sewingproduction:

~~~powershell
dotnet run --project tools/SewingProduction.Documentation -- build
dotnet run --project tools/SewingProduction.Documentation -- check
~~~

check выявляет битые ссылки Obsidian, отсутствующие исходные файлы, дубли маршрутов и неактуальный HTML-output.

Старые HTML/DOCX-инструкции сохраняются до миграции. При изменении такой инструкции сначала перенесите актуальное содержание в Markdown, затем разрешите генератору обновлять HTML.
