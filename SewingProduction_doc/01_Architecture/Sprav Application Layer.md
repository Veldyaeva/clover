# Прикладной слой Sprav

Features/Sprav содержит справочники и read-only экраны. Для простых форм допустима цепочка:

~~~
Form → DataService → DbService → view/procedure
~~~

Для печати, экспорта и сложных операций используется прикладной слой:

~~~
Form → UseCase → validation/service/exporter → DataService → SQL
~~~

## Контракты

- DataService не скрывает ошибки за null или 0, если это не является явным контрактом.
- Значения SQL передаются параметрами Dapper.
- Для CHAR-полей ACE строки нормализуются через RTRIM в SQL.
- Пользовательский UI показывает typed result или понятную ошибку, а не разбирает SQL-исключение.

Связанные сценарии:

- [[VyazKnitEconomAssortForm]]
- [[ThreadNormsForm]]
- [[Vyaz Econom Print Flow]]
