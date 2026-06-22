# view_seb_vyaz_econom_assort

Представление dbo.view_seb_vyaz_econom_assort используется как источник данных для [[VyazKnitEconomAssortForm]].

Репозиторный скрипт расположен в:

~~~
Features/Sprav/Sql/ACE.dbo.view_seb_vyaz_econom_assort.sql
~~~

Форма загружается через dbo.VyazKnitEconomAssort_LoadRows; view является частью SQL-модели и не заменяет процедуру загрузки.

При изменении view:

1. сверить типы полей модели C#;
2. сохранить RTRIM для CHAR-значений;
3. не добавлять ORDER BY во view;
4. обновить [[VyazKnitEconomAssortForm]] и связанные пользовательские инструкции.
