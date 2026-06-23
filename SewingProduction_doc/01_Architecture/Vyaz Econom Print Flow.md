# Печать вязальной калькуляции

Поток запускается из формы [[VyazKnitEconomAssortForm]] и отделяет загрузку данных, экспорт и отметку печати.

~~~
Выбранная строка
  → VyazEconomPrintContext
  → LoadVyazEconomPrintDataUseCase
  → DevExpressVyazEconomExcelExporter
  → MarkVyazEconomPrintedUseCase
~~~

## Инварианты

- Экспорт создаёт временный .xlsx и открывает его пользователю.
- date_econom обновляется только после успешного создания и открытия файла.
- Уже заполненная дата не перезаписывается.
- Отмена или ошибка экспорта не должна менять данные в ACE.

SQL-контракты описаны в [[Vyaz Econom Print Queries]].
