# справочники

#ui #sql #legacy #process

Hub по модулю **Справочники** (`sewingproduction/Features/Sprav`).

## Реализованные формы

- [[VyazKnitEconomAssortForm]] — калькуляция вязального ассортимента (read-only грид, фильтры подразделений, `Показать всё`)
- [[ThreadNormsForm]] — нормы ниток (перенесена из `TeamWork`)

## прикладной слой

- [[Sprav Application Layer]] — use-cases для печати калькуляции
- [[Vyaz Econom Print Flow]] — поток «Печать калькуляция»

## SQL-слой

- [[VIEW - view_seb_vyaz_econom_assort]]
- [[Vyaz Econom Print Queries]]

## Меню

Формы открываются из `SpMainForm` → раздел **Справочники**:

| Пункт меню | Форма |
|------------|--------|
| Калькуляция вяз. ассорт. | `VyazKnitEconomAssortForm` |
| Нормы ниток | `ThreadNormsForm` |

## Статус печати электронной таблицы

| Этап | Статус |
|------|--------|
| Загрузка данных (DTO) | Готово |
| Excel-отчёт (макет как в VFP) | Готово: `DevExpressVyazEconomExcelExporter`, сохранение `.xlsx` через диалог |
| `UPDATE date_econom` после печати | Готово: выполняется оркестратором после успешного экспорта, не перезаписывает уже заполненную дату |

## Связанные заметки

- [[Forms]]
- [[KnittingProduction]]
