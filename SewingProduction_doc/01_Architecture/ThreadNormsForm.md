# Нормы ниток

Форма ThreadNormsForm находится в Features/Sprav/Forms/ThreadNormsForm.cs.

## Назначение

Форма показывает и редактирует нормы ниток. По умолчанию включён фильтр строк с нулевой нормой; пользователь может переключиться на полный список и обновить данные.

## Поток данных

~~~
ThreadNormsForm
  → ThreadNormsDataService
  → DbService
  → dbo.ThreadNorms_LoadRows / cfn.ThreadNorms_Save
~~~

Для конечного пользователя публикуется инструкция в 05_UserGuide. Её HTML-версия формируется по маршруту Features/Sprav/Forms/ThreadNormsForm.html.
