## TeamWork – рабочее место по разделениям труда

Форма `TeamWork` – это центральное рабочее место технолога для работы с:

- **Разделениями труда (РТ)** – сущность `ArtNormN` (таблица `art_norm_n`, ключ `AnnId`).
- **Нормами** – `NormRasz` (раскрой), `NormRask` (операции), `NormKont` (контроль).
- **Артикулами** – справочник `sp_articul` / представления `View_sp_articul`, `articulListGroupBySizeLabel`.
- **НЗП и бригадами** – через хранимые процедуры и Jabber‑рассылку.

Основной код формы разбит на partial‑файлы:

- `TeamWork.cs` – конструктор, инициализация сервисов и биндингов, общие обработчики.
- `TeamWork.WorkDivisions.cs` – логика вкладки «Разделения труда».
- `TeamWork.Articles.cs` – логика вкладки «Работа с артикулами».
- `TeamWork.Helpers.cs` – настройки гридов, фильтрация/поиск, управление режимами и формой расширенного редактирования.

---

## Используемые сервисы и репозитории

Форма создаёт и использует следующие сервисы:

- **`DatabaseHelper`** (`Core\helpers\DbHelper.cs`)
  - Низкоуровневый доступ к SQL Server (`SqlConnection`, `SqlCommand`).
  - Общие методы: `ExecuteQueryAsync`, `ExecuteNonQueryAsync`, `ExecuteScalarAsync<T>`, транзакции.

- **`DbService`** (`Core\services\DbService.cs`)
  - Универсальный слой поверх `DatabaseHelper` и Dapper.
  - CRUD/утилиты:
    - `GetEntityAsync<T>`, `GetListAsync<T>`, `GetFirstOrDefaultAsync<T>`.
    - `InsertEntityAsync`, `UpdateEntityAsync`, `UpdateFieldAsync`, `DeleteEntityAsync`.
    - `ExecuteSpWithStatusAsync`.

- **`ArtNormRepository`** (`Features\TeamWork\Services\ArtNormService.cs`)
  - Профильный репозиторий для РТ и норм:
    - загрузка РТ: `GetArtNormData`, `GetArtNormDataById`, `GetArtNormDataCurrent`, `GetArtNormDataByArticul`;
    - загрузка норм: `GetRelatedNormRasz`, `GetRelatedNormRask`, `GetRelatedNormKont`, `GetCalculatedSekFromViewAsync`;
    - работа со справочниками: `GetNormOper`, `GetKod_proizv`, `GetPodr_vyaz`, `GetOborud_shv`, `GetRelDesigner`, `GetEmployeeFullName`;
    - работа с артикулами и НЗП: `UpdateAnnIdinArticul`, `ResetAnnIdinArticul`, `GetRelatedSpArt`, `GetNzpWithPztCounts`, `GetNZPByKoddRtAsync`, `GetWorkingBrigs`;
    - пересчёт себестоимости: `getArtNormnSeb`;
    - выборка эскизов: `GetImage`;
    - очистка связанных норм: `DeleteRelatedNormTables`.

- **`TeamWorkOrchestrator`** (`Features\TeamWork\Services\TeamWorkService.cs`)
  - Координация загрузки данных:
    - `LoadWorkDivisionsWithFocusAsync` – загрузка списка РТ с восстановлением фокуса.
    - `RefreshRelatedDataAsync` – пакетная загрузка `NormRasz/NormRask/NormKont` по `AnnId`.

- **`JabberSender` / `IJabberSender`** (в `ArtNormService.cs`)
  - Отправка Jabber‑сообщений бригадам при утверждении/обновлении РТ:
    - `SendToBrigsAsync` – вставка сообщений в `[WMSWRITE].planeta.dbo.Jabber_Messager` по данным `view_sprav_men`.

Дополнительно используются вспомогательные классы:

- `SecondsUpdateManager` – отложенный пересчёт секунд по РТ.
- `UIHelper` – применение загруженных данных к UI‑контролам.
- `TWGridHelper` – общие операции с грид‑контролами.
- `TeamWorkBuffer` – буфер для операций «комплект».
- `RtSnapshotService` – управление снапшотами РТ и построение diff при утверждении.
- `GridOverlayLoader` – типовой паттерн асинхронной загрузки в грид с оверлеем.

---

## Объекты базы данных

### Таблицы (основные)

- **`art_norm_n`**  
  - Хранит разделения труда (`ArtNormN`).
  - Ключ: `AnnId`.
  - Поля: артикул, модель, группа, суммы секунд, сложность, статус (`status`), флаги архивации и др.

- **`norm_rasz`, `norm_rask`, `norm_kont`, `norm_dop_obr`**  
  - Детализирующие нормы:
    - `NormRasz` – раскрой (`norm_rasz` / `normraszview`).
    - `NormRask` – операции (`norm_rask`).
    - `NormKont` – контроль (`norm_kont`).

- **`sp_articul`**  
  - Справочник артикулов (размерный ряд).
  - Используется для привязки/отвязки артикулов к РТ через поле `annId`.

- **`kod_proizv`, `podr_vyaz`, `oborud_shv`**  
  - Справочники производств/подразделений/оборудования, подтягиваются в модели норм для отображения текстов.

- **`fio`**  
  - Сотрудники; используется для дизайнеров/конструкторов и получения ФИО по табельному.

- **`[WMSWRITE].planeta.dbo.Jabber_Messager`**  
  - Таблица сообщений Jabber. Заполняется `JabberSender`.

### Представления

- **`ArtNormNView` / `artNormNView`**
  - Основное представление РТ, обычно с join на `status_ann`.
  - Используется:
    - при загрузке списка РТ (`GetArtNormData`),
    - при загрузке архива/предварительного архива,
    - при выборке одной записи (`GetArtNormDataById`).

- **`NormRaszSek_view`**
  - Представление с рассчитанными секундам по нормам (`NormRaszSekView`), используется при дополнительном расчёте сек.

- **`normraszview`**
  - Представление по `norm_rasz` с уже присоединёнными текстами (`kod_proizv`, `podr_vyaz`, `oborud_shv`), используется во второй версии `GetRelatedNormRasz`.

- **`articulListGroupBySizeLabel`**
  - Список артикулов, сгруппированный по `size_label`.
  - Во вкладке «Работа с артикулами» из него берутся неувязанные артикула:
    - `SELECT * FROM articulListGroupBySizeLabel where annId is null or annId = 0`.

- **`View_sp_articul` / `view_sp_articul`**
  - Представление по справочнику артикулов, используется:
    - для обновления поля `annId` (привязка артикула к РТ),
    - для поиска эскизов по `AnnId` (через `getFileEskizForKodd_rt`).

- **`view_sprav_men`**
  - Справочник исполнителей/бригад, используется `JabberSender` для определения получателей сообщений.

### Хранимые процедуры

- **`dbo.updateSebZArticulPsz`**
  - Массовый пересчёт себестоимости и синхронизация справочников по РТ.
  - Вызывается:
    - при утверждении РТ (кнопка «Проставить утверждение» / колонка `dateUpdate`),
    - при привязке артикула к РТ (кнопка «Увязать»).

- **`dbo.pztOperUpdateFast`**
  - Обновление операций ПЗТ; вызывается `ArtNormRepository.ExecutePztOperUpdateAsync`.

- **`dbo.GetNZPAndOperByKoddRT`**
  - Получение НЗП и операций по `AnnId` (старый вариант).

- **`dbo.GetNZPByKoddRT`**
  - Основной источник данных по НЗП / бригадам:
    - `GetNzpWithPztCounts` – список НЗП по РТ.
    - `GetWorkingBrigs` – список бригад по РТ (с параметром `@xRezType = 1`).

- **`dbo.GetPztCountsByKoddRT`**
  - Возвращает количество ПЗТ по РТ; данные объединяются с НЗП в `GetNzpWithPztCounts`.

### SQL‑функции

- **`dbo.getFileEskizForKodd_rt(@annId)`**, **`dbo.getFileEskizForKodd(@kod)`**
  - Возвращают путь к файлу эскиза изделия, используются в `ArtNormRepository.GetImage`.

- **`dbo.getArtNormnSeb(@xAnnID)`**
  - Возвращает рассчитанную себестоимость РТ.
  - Используется:
    - в `ArtNormRepository.getArtNormnSeb`,
    - из формы – при отметке флага `Upd` в `ANNgridView_CellValueChanged`.

---

## Логика вкладки «Разделения труда»

Файл: `TeamWork.WorkDivisions.cs`.

Основные сценарии:

- **Загрузка списка РТ**
  - Метод `LoadWorkDivisions(CancellationToken ct)`:
    - грузит список FIO дизайнеров/конструкторов (`GetRelDesigner`) и записывает в `ArtNormN.FioSource`;
    - грузит все РТ из `ArtNormNView` (`GetArtNormData`);
    - настраивает внешний вид `ANNgridView` и фильтры статусов (`filterTable`);
    - для текущего `AnnId` вызывает `LoadRelatedData(annId)`:
      - через `TeamWorkOrchestrator.RefreshRelatedDataAsync` загружаются `NormRasz/NormRask/NormKont`;
      - через `UIHelper.UpdateRelatedDataUIAsync` данные попадают в гриды раскроя/операций/контроля.

- **Создание предварительного РТ**
  - Обработчик `ButtonPreliminaryWd_Click_Internal`:
    - в обычном режиме создаёт пустой `ArtNormN` со статусом `Preliminary`;
    - в режиме «комплект» (`toggleSwitchKit.IsOn`) – копирует две выбранные записи в `TeamWorkBuffer`;
    - вставка в `art_norm_n` (`InsertEntityAsync`), получение нового `AnnId`;
    - открывается немодальная форма `TeamWork_AdvanceTW` в режиме:
      - `Mode.NewWorkDivision` или `Mode.Kit` (если в буфере 2 записи);
    - по закрытии формы:
      - при `DialogResult.OK` – обновление полей РТ, установка фокуса на новую запись, старт `SecondsUpdateManager`;
      - при отмене – удаление созданной записи из `art_norm_n` и всех связанных норм (`DeleteRelatedNormTables`).

- **Дублирование РТ**
  - Обработчик `DuplicateWorkDivision_Click_Internal`:
    - берёт текущую запись `ArtNormN` или загружает её по `MyDataANN.AnnId`;
    - создаёт клон через `CloneOperationalData` со статусом `Preliminary`, `AnnId = 0`;
    - вставляет новую запись (`InsertEntityAsync`), открывает `TeamWork_AdvanceTW` в режиме `Mode.Clone`;
    - при `OK` – добавляет запись в биндинг и переводит фокус на неё;
    - при отмене – удаляет запись и связанные нормы.

- **Архив + копия**
  - Методы `ArchAndCopy` / `ArchAndCopy(GridView, ...)`:
    - проверяют наличие НЗП (`checkNzp`);
    - создают новую запись через `CloneForArchiveCopy` (с нужным исходным статусом);
    - открывают `TeamWork_AdvanceTW` в режиме `Mode.ArchAndCopy`;
    - при подтверждении:
      - старому РТ выставляется статус `Archive` или `PreliminaryArchive` (если есть НЗП);
      - новые артикулы переназначаются на новый `AnnId` (если НЗП нет);
      - архивный список обновляется (`RefreshArchData`), для новой записи запускается `SecondsUpdateManager`.

- **Утверждение и пересчёт себестоимости**
  - Колонка `dateUpdate` настраивается в `SetupDateUpdateColumn`.
  - При двойном клике по пустой ячейке (`CommandsEditDateNull_DoubleClick`):
    - проверяется сложность и наличие вязальных операций;
    - выводится подтверждение пользователю;
    - вызывается use-case оркестратора `TeamWorkOrchestrator.ApproveWorkDivisionAsync(annId, art)`:
      - `dbo.updateSebZArticulPsz(@xAnnID)` – пересчёт справочников;
      - обновление полей `data_obn` и `status` в `art_norm_n` через `UpdateFieldAsync`;
      - поиск действующих бригад по РТ (`GetWorkingBrigs` → `GetNZPByKoddRT`);
      - построение diff по изменениям РТ (`RtSnapshotService.CompareWithCurrentAsync`);
      - формирование текстового сообщения и отправка через `JabberSender.SendToBrigsAsync`.

- **Логи изменений**
  - Кнопки `customSimpleButtonAnnLog` и `customSimpleButtonRaszLog` открывают форму `Log`:
    - источник `LogSourceType.Ann` – журнал `art_norm_n_updLog`;
    - источник `LogSourceType.Rasz` – журнал `norm_rasz_updLog`.

---

## Логика вкладки «Работа с артикулами»

Файл: `TeamWork.Articles.cs`.

Основные сценарии:

- **Инициализация вкладки**
  - Метод `CurrentWorks_Load(CancellationToken)`:
    - загружает:
      - предварительный архив (`PreArchLoad`: `status = 4` из `artNormNView`),
      - архив (`ArchLoad`: `status = 3` из `artNormNView`);
    - заполняет:
      - список неувязанных артикулов (`MyDataArtLoad` из `articulListGroupBySizeLabel` с `annId is null or 0`),
      - список РТ для увязки (`MyDataAnnLoad` через `GetArtNormDataCurrent(loadAll)`).
    - по текущему `AnnId` во вкладке артикулов подгружает `NormRasz/NormRask/NormKont` в отдельные биндинги (`_normRaszListArticles`, и т.п.).

- **Выбор артикула**
  - Обработчик `gridView_unboundArts_FocusedRowChanged_Internal`:
    - сбрасывает фильтры и чек «показать все РТ» для гридов РТ;
    - по выбранному артикулу (код `kodd_rt`, `Articul`):
      - вызывает `LoadWorksbyArt(articul)` → ищет подходящие РТ по:
        - полному артикулу,
        - артикулу без дефиса,
        - префиксу до дефиса;
      - обновляет список РТ для увязки (`_myDataAnnList`);
      - подгружает нормы (`NormRasz/NormRask/NormKont`) по выбранному РТ;
      - загружает/очищает изображение артикула через `LoadGridImage(pictureBox3, kod)`.

- **Выбор РТ для увязки**
  - Обработчик `gridViewWdToBind_FocusedRowChanged`:
    - пишет в текстовые поля модуль, артикул и группу выбранного РТ;
    - загружает изображение по `AnnId` (`GetImage(annId: ...)`);
    - через `RefreshNormRaszForArticlesTab / RefreshNormRaskForArticlesTab / RefreshNormKontForArticlesTab` подгружает нормы в гриды;
    - через `LoadNZPForArticlesTab` загружает НЗП (`GetNzpWithPztCounts`) и обновляет доступность кнопки «Отвязать».

- **Привязка артикула к РТ**
  - Обработчик `BindButton_Click_Internal`:
    - ожидает, что пользователь отметил в гриде:
      - один артикул (`MyDataART.IsChecked`),
      - одно РТ (`MyDataANN.IsChecked`);
    - просит подтверждение в отдельном диалоге:
      - опционально заполнить `grup` и `mod` РТ из справочника артикулов;
    - устанавливает `size_label` РТ из выбранного артикула;
    - вызывает `ArtNormRepository.UpdateAnnIdinArticul(annId, kodd, kodd_rt, articul)` – установка `annId` в `View_sp_articul`/`sp_articul`;
    - сохраняет изменённую запись РТ через `_dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, selectedAnnRow)`;
    - переносит артикул:
      - из списка неувязанных в список «привязанных» (грид привязанных артикулов);
    - вызывает `dbo.updateSebZArticulPsz(@xAnnID)` для пересчёта себестоимости и связанных справочников.

- **Отвязка артикулов от РТ**
  - Методы `UnbindWD` и универсальный `UnbindArticulesFromWorkDivision_Internal`:
    - определяют выбранные НЗП (по флагу `IsChecked` или текущей строке);
    - определяют соответствующее РТ (`ArtNormN` или `MyDataANN` → `GetArtNormDataById`);
    - подтверждают операцию с пользователем (показывают AnnId, группу, модель, артикул);
    - для каждого выбранного элемента:
      - выполняют `UPDATE sp_articul SET annId = NULL` с дополнительными условиями по коду/артикулу/AnnId;
      - обнуляют `size_label` в `art_norm_n` для РТ;
      - при необходимости обновляют статусы потомков по `parentId`;
    - после отвязки:
      - перезагружают связанные данные по РТ (`LoadRelatedData(AnnId)`),
      - обновляют НЗП (`RefreshNzpData`),
      - обновляют доступность кнопки «Отвязать».

- **Архив на вкладке артикулов**
  - `PreArchLoad` / `ArchLoad` – заполняют Grids предварительного архива и архива из `artNormNView` по статусам 4 и 3.
  - `RefreshArchData` – переиспользует `ArchLoad`, затем обновляет источник данных.
  - `RestoreFromArchive_Internal` – восстанавливает выбранные РТ из архива:
    - меняет статус с `Archive` (3) на `Preliminary` (1) в `art_norm_n`;
    - удаляет восстановленные записи из списка архива;
    - добавляет их обратно в основной список РТ.

---

## Режим «комплект» и поиск

Режим управляется переключателем `toggleSwitchKit` и методами `SetNormalMode` / `SetKitMode` (см. `TeamWork.Helpers.cs`):

- **Обычный режим**
  - одиночный выбор строк в `ANNgridView`;
  - доступны кнопки:
    - «добавить предварительное РТ»,
    - обычное/расширенное редактирование,
    - «дубль», «архив + копия», печать;
  - режим работы с одним РТ.

- **Режим комплекта**
  - включается `toggleSwitchKit.IsOn = true`;
  - `ANNgridView` переходит в режим множественного выбора (CheckBoxRowSelect);
  - скрываются обычные кнопки редактирования/архивации, показываются кнопки «создать комплект» и «копировать комплект в буфер»;
  - логика:
    - пользователь выбирает ровно 2 РТ;
    - они попадают в `TeamWorkBuffer`;
    - создаётся новый предварительный РТ‑комплект (режим `Mode.Kit`) и открывается в `TeamWork_AdvanceTW`.

Дополнительно реализован специальный поиск по комплектным артикулам:

- Метод `ParseKitArticle` разбирает строки вида `1Т1773С1990`, `1dТ1773С1990`, `1Ф7738Ш1395` на две компонентные части (основной артикул + добавка).
- В обработчике `ANNgridView_ActiveFilterChanged`, если включён режим комплекта и есть текст поиска:
  - строка разбирается на две компоненты;
  - формируется фильтр `Articul LIKE '%component1%' OR Articul LIKE '%component2%'`;
  - применяется к гриду вместо стандартного QuickSearch.

---

## Краткое резюме логики формы

- **TeamWork** – центральная форма для:
  - ведения РТ (`ArtNormN`),
  - работы с нормами (`NormRasz`, `NormRask`, `NormKont`),
  - привязки/отвязки артикулов,
  - управления архивами РТ,
  - запуска пересчёта себестоимости и уведомления бригад.
- Вся работа с БД разбита на уровни:
  - `DatabaseHelper` → `DbService` (универсальный слой) → `ArtNormRepository` (предметный слой TeamWork) → `TeamWorkOrchestrator` и форма.
- Основные связные объекты БД:
  - представления: `ArtNormNView`, `normraszview`, `NormRaszSek_view`, `articulListGroupBySizeLabel`, `View_sp_articul`, `view_sprav_men`;
  - процедуры: `dbo.updateSebZArticulPsz`, `dbo.pztOperUpdateFast`, `dbo.GetNZPByKoddRT`, `dbo.GetPztCountsByKoddRT`, `dbo.GetNZPAndOperByKoddRT`;
  - функции: `dbo.getFileEskizForKodd_rt`, `dbo.getFileEskizForKodd`, `dbo.getArtNormnSeb`.

