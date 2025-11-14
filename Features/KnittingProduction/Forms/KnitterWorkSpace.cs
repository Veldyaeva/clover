using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        /// <summary>
        /// Оркестратор доменной логики: загрузка данных, сохранение дат и прочие операции.
        /// </summary>
        private readonly IKnitterOrchestrator _orchestrator;
        /// <summary>
        /// Источник данных, к которому привязан GridControl.
        /// </summary>
        private readonly BindingSource _planBindingSource = new BindingSource();
        /// <summary>
        /// Презентер, который собирает иерархию мастер-деталь и настраивает события.
        /// </summary>
        private readonly KnitterPlanPresenter _planPresenter = new KnitterPlanPresenter();

        // Вью для третьего уровня (деталь детальной таблицы)
        private RepositoryItemButtonEdit _pzvDateStartButtonEdit;
        private RepositoryItemTextEdit _pzvDateStartTextEdit;
        private RepositoryItemButtonEdit _pzvDateEndButtonEdit;
        private RepositoryItemTextEdit _pzvDateEndTextEdit;

        /// <summary>
        /// Таймер для отслеживания бездействия пользователя (1 минута).
        /// </summary>
        private readonly System.Windows.Forms.Timer _idleTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Список ФИО для повторного показа сплеша при бездействии.
        /// </summary>
        private List<FioModel> _cachedFioList;

        /// <summary>
        /// Флаг, указывающий, что сплеш выбора сотрудника уже открыт.
        /// </summary>
        private bool _isSplashShowing = false;

        /// <summary>
        /// Инициализирует форму рабочего места вязальщика.
        /// Настраивает источники данных, колонки гридов, оркестратор и подписки.
        /// </summary>
        public KnitterWorkSpace()
        {
            try
        {
            InitializeComponent();
                dataLayoutControl1.DataSource = _planBindingSource;

                ConfigureAdvBandedGridColumns();

                var dbHelper = new DatabaseHelper();
                IKnitterRepository repo = new KnitterRepository(dbHelper);
                _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

            PlanZagrVyazGridControl.DataSource = _planBindingSource;

                // Детализация на втором уровне настраивается в Designer: advBandedGridView1 является шаблоном уровня "ArtNom"
            this.Load += async (s, e) => await InitializeAsync();

                SetupPzvDateStartColumn();
                SetupIdleTimer();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вариант конструктора с внедрением зависимостей (DI).
        /// </summary>
        /// <param name="orchestrator">Оркестратор доменной логики.</param>
        public KnitterWorkSpace(IKnitterOrchestrator orchestrator)
        {
            try
            {
                InitializeComponent();
                _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
                dataLayoutControl1.DataSource = _planBindingSource;
                ConfigureAdvBandedGridColumns();
                PlanZagrVyazGridControl.DataSource = _planBindingSource;
                // Детализация на втором уровне настраивается в Designer: advBandedGridView1 является шаблоном уровня "ArtNom"
                this.Load += async (s, e) => await InitializeAsync();
                SetupPzvDateStartColumn();
                SetupIdleTimer();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Первичная инициализация: загрузка справочника ФИО, установка дефолтного табеля (1438) и автозагрузка плана.
        /// </summary>
        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _orchestrator.GetFioListAsync();
                fioList ??= new List<FioModel>();

                // Жестко выбираем табельный при загрузке формы
                const int defaultTab = 1438;
                bool hasDefault = fioList.Any(f => f.Tab == defaultTab);
                if (!hasDefault)
                {
                    string defaultFio = await _orchestrator.GetFioByTabAsync(defaultTab);
                    if (!string.IsNullOrWhiteSpace(defaultFio))
                    {
                        fioList.Insert(0, new FioModel { Tab = defaultTab, Fio = defaultFio });
                    }
                }

                FioGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Fio);
                FioGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                FioGridLookUpEdit.Properties.DataSource = fioList;
                TabGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Tab);
                TabGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                TabGridLookUpEdit.Properties.DataSource = fioList;
                dateEdit1.EditValue = DateTime.Now;
                
                // Сохраняем список для повторного показа сплеша при бездействии
                _cachedFioList = fioList;
                
                PresentFioSelectionSplash(fioList, defaultTab);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PresentFioSelectionSplash(IReadOnlyCollection<FioModel> fioList, int defaultTab)
        {
            if (fioList == null || fioList.Count == 0)
            {
                XtraMessageBox.Show(this, "Список сотрудников пуст. Обратитесь к администратору.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Останавливаем таймер бездействия, пока показывается сплеш
            _idleTimer.Stop();

            int? currentTab = int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int parsedTab)
                ? parsedTab
                : (int?)null;

            int? initialTab = currentTab;
            if (initialTab is null && fioList.Any(f => f.Tab == defaultTab))
            {
                initialTab = defaultTab;
            }

            _isSplashShowing = true;
            try
            {
                using (var splash = new FioSelectionSplash(fioList, initialTab))
                {
                    var result = splash.ShowDialog(this);
                    if (result == DialogResult.OK && splash.SelectedTab.HasValue)
                    {
                        FioGridLookUpEdit.EditValue = splash.SelectedTab.Value;
                        TabGridLookUpEdit.EditValue = splash.SelectedTab.Value;
                        // Перезапускаем таймер после успешного выбора
                        ResetIdleTimer();
                    }
                    else
                    {
                        BeginInvoke(new Action(Close));
                    }
                }
            }
            finally
            {
                _isSplashShowing = false;
            }
        }

        /// <summary>
        /// Дополнительная настройка второго уровня (advBandedGridView1):
        /// - создаёт скрытую unbound-колонку с готовой строкой заголовка группы
        /// - группирует по этой колонке и авторазворачивает единственную группу
        /// В результате под номером В/М сразу отображается шапка "Пачка | Расчёт | Размер | Кол-во" и таблица операций.
        /// </summary>
        private void ConfigureAdvBandedGridColumns()
        {
            // Конфигурация колонок задана в Designer.cs
            // Дополнительная настройка: группировка второго уровня (advBandedGridView1)
            if (advBandedGridView1 == null)
                return;

            // 1) Единая скрытая колонка с готовым заголовком группы
            var headerCol = advBandedGridView1.Columns.ColumnByFieldName("__Header");
            if (headerCol == null)
            {
                headerCol = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                {
                    FieldName = "__Header",
                    Caption = "Header",
                    UnboundType = DevExpress.Data.UnboundColumnType.String,
                    // Строка заголовка: Пачка | Расчёт | Размер | Кол-во
                    UnboundExpression = "Concat('Пачка: ', [n_pach], ' | Расчёт: ', [pzvNom], ' | Размер: ', [razm], ' | Кол-во: ', [pzvKol])",
                    Visible = false,
                    OptionsColumn = { ShowInCustomizationForm = false }
                };
                advBandedGridView1.Columns.Add(headerCol);
            }

            // 2) Сбрасываем прошлую группировку и группируем только по __Header
            advBandedGridView1.BeginUpdate();
            try
            {
                advBandedGridView1.ClearGrouping();

                headerCol.GroupIndex = 0;

                // 3) Внешний вид группы — показываем только текст, без имён полей
                advBandedGridView1.GroupFormat = "{1}";
                advBandedGridView1.OptionsView.ShowGroupedColumns = false;
                advBandedGridView1.OptionsView.ShowGroupPanel = false;
                advBandedGridView1.OptionsBehavior.AutoExpandAllGroups = true;

                // На случай прежней подписки — отключаем переотрисовку групп (если была)
                // (метод больше не используется)
            }
            finally
            {
                advBandedGridView1.EndUpdate();
            }
        }

        // Пользовательская отрисовка группы больше не требуется — заголовок формируется колонкой __Header

        // Сборка иерархии — вынесено в KnitterPlanPresenter

        // master-detail логика перенесена в KnitterPlanPresenter

        /// <summary>
        /// Загружает данные из модели высокого уровня (не используется в текущей версии, оставлено для совместимости).
        /// </summary>
        public void LoadData(PlanZagrVyaz data)
        {
            _planBindingSource.DataSource = data;
        }

        /// <summary>
        /// Обработчик выбора сотрудника: получает план по табелю, собирает иерархию и привязывает к гриду.
        /// </summary>
        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Сбрасываем таймер бездействия при активности пользователя
                ResetIdleTimer();

                if (FioGridLookUpEdit.EditValue == null || !int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    TabGridLookUpEdit.EditValue = null;
                    return;
                }

                // Синхронизируем TabGridLookUpEdit с выбранным табельным номером
                TabGridLookUpEdit.EditValue = tab;

                var plan = await _orchestrator.GetPlanByTabAsync(tab);
                // Уровень 1 (детали) строится сразу в презентере; второй уровень — advBandedGridView1 с групповой шапкой.
                _planPresenter.BindGroupDetails(bandedGridView3, /*bandedG*/gridView1, advBandedGridView1, _planBindingSource, plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Кнопка "Начать смену": массово назначает табель выбранным строкам и обновляет отображение.
        /// </summary>
        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var focusView = PlanZagrVyazGridControl.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView;
                var rowsForUpdate = _planPresenter.GetRowsForViewSelection(focusView).ToList();
                if (!rowsForUpdate.Any())
                {
                    XtraMessageBox.Show(this, "Выберите строки плана для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pzvIds = rowsForUpdate
                    .Select(r => r.pzvID)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (pzvIds.Count == 0)
                {
                    XtraMessageBox.Show(this, "Не удалось определить записи плана для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);

                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(selectedTab);
                _planPresenter.BindGroupDetails(bandedGridView3, /*bandedG*/gridView1, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Получение выбранных строк теперь через _planPresenter.GetRowsForViewSelection(...)

        /// <summary>
        /// Настройка редакторов ячеек для колонок "Начато" и "Закончено":
        /// - в "Начато" показывает кнопку, если дата пустая, и текст — если дата заполнена
        /// - в "Закончено" показывает кнопку "Завершить" только когда дата начала уже заполнена и дата окончания пуста
        /// - регистрирует репозитории редакторов в GridControl
        /// </summary>
        private void SetupPzvDateStartColumn()
        {
            bandedGridColumn18.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn18.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateStartButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateStartButtonEdit.Buttons.Clear();
            _pzvDateStartButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Начать", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateStartButtonEdit.DoubleClick += PzvDateStartButtonEdit_DoubleClick;
      //      _pzvDateStartButtonEdit.ButtonClick += PzvDateStartButtonEdit_ButtonClick;

            _pzvDateStartTextEdit = new RepositoryItemTextEdit { ReadOnly = true };

            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartTextEdit);
            BandedGridColumn dateStart = bandedGridColumn18;
             //  advBandedGridView1.CustomRowCellEdit += AdvBandedGridView1_CustomRowCellEdit;
            advBandedGridView1.CustomRowCellEdit += (s, e) =>
            {
                if (e.Column != null && e.Column.FieldName == dateStart.FieldName)
                {
                    var cellValue = e.CellValue;
                    bool isEmpty = cellValue == null ||
                                   cellValue == DBNull.Value ||
                                   (cellValue is DateTime dt && dt == DateTime.MinValue);
                    e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
                }
                // Закончено
                if (e.Column != null && e.Column.FieldName == bandedGridColumn19.FieldName)
                {
                    // Кнопка "Завершить" показывается ТОЛЬКО если дата начала заполнена и дата окончания пуста
                    GridView view = (GridView)PlanZagrVyazGridControl.FocusedView;
                    var startValue = view.GetRowCellValue(e.RowHandle, bandedGridColumn18);//advBandedGridView1.GetRowCellValue(e.RowHandle, bandedGridColumn18);
                    bool hasStart = !(startValue == null ||
                                      startValue == DBNull.Value ||
                                      (startValue is DateTime sdt && sdt == DateTime.MinValue));

                    var endValue = e.CellValue;
                    bool isEndEmpty = endValue == null ||
                                      endValue == DBNull.Value ||
                                      (endValue is DateTime edt && edt == DateTime.MinValue);

                    e.RepositoryItem = (hasStart && isEndEmpty) ? _pzvDateEndButtonEdit : _pzvDateEndTextEdit;
                }
            };

            // Настройка для "Закончено" (pzvDateEnd)
            bandedGridColumn19.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn19.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateEndButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateEndButtonEdit.Buttons.Clear();
            _pzvDateEndButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Завершить", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateEndButtonEdit.DoubleClick += PzvDateEndButtonEdit_DoubleClick;
       //     _pzvDateEndButtonEdit.ButtonClick += PzvDateEndButtonEdit_ButtonClick;

            _pzvDateEndTextEdit = new RepositoryItemTextEdit { ReadOnly = true };
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndTextEdit);
            // Колонка фактического количества — unbound для отображения введённого значения
            bandedGridColumn22.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
        }

        /// <summary>
        /// Подменяет редактор ячейки "Начато" (кнопка/текст) в зависимости от значения.
        /// </summary>
        private void AdvBandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column != bandedGridColumn18)
                return;

            bool isEmpty = e.CellValue == null || 
                          e.CellValue == DBNull.Value || 
                          (e.CellValue is DateTime dt && dt == DateTime.MinValue);

            e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
        }

        /// <summary>
        /// Клик по кнопке в "Начато" — установить дату начала для текущей строки.
        /// </summary>
        private async void PzvDateStartButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView; 
            await ApplyPzvDateStartAsync(view);
        }

        /// <summary>
        /// Двойной клик по ячейке "Начато" — установить дату начала для текущей строки.
        /// </summary>
        private async void PzvDateStartButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateStartAsync(view);
        }

        /// <summary>
        /// Устанавливает дату начала: сохраняет на сервере (с использованием серверного времени) и моментально отражает в ячейке.
        /// </summary>
        private async Task ApplyPzvDateStartAsync(GridView view)//int rowHandle)
        {
            await ApplyPzvDateAsync(
                view,
                bandedGridColumn18,
                _orchestrator.UpdatePzvDateStartAsync,
                m => m.pzvDateStart,
                (m, v) => m.pzvDateStart = v,
                "начала");
        }

        /// <summary>
        /// Клик по кнопке "Завершить" — запросить количество и завершить операцию (установить дату окончания).
        /// </summary>
        private async void PzvDateEndButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        /// <summary>
        /// Двойной клик по ячейке "Закончено" — запросить количество и завершить операцию.
        /// </summary>
        private async void PzvDateEndButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        /// <summary>
        /// Запрашивает у пользователя фактическое количество, отражает его в колонке "Кол-во факт (шт)" и устанавливает дату окончания.
        /// </summary>
        private async Task ApplyPzvDateEndAsync(GridView view)
        {
            GridView _view = view;
            int rowHandle = _view?.FocusedRowHandle ?? -1;
            if (_view == null || rowHandle < 0)
                return;

            // Получим текущую строку для плейсхолдера (кол-во к выполнению)
            var currentRow = _view.GetRow(rowHandle) as KnitterPZVModel;
            int defaultQty = currentRow?.pzvKol ?? 0;

            // Диалог ввода количества отвязанных изделий
            var qtyObj = DevExpress.XtraEditors.XtraInputBox.Show(
                "Количество отвязанных изделий",
                "Завершение операции",
                defaultQty);
            if (qtyObj == null)
                return; // отмена
            if (!int.TryParse(qtyObj.ToString(), out int qty) || qty < 0 || qty > defaultQty)
            {
                XtraMessageBox.Show(this, "Значение должно быть меньше запланированного.", "Неверное значение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Отобразим введённое значение в столбце факта (unbound)
            _view.SetRowCellValue(rowHandle, bandedGridColumn22, qty);
            _view.PostEditor();
            _view.CloseEditor();
            _view.UpdateCurrentRow();
            _view.RefreshRowCell(rowHandle, bandedGridColumn22);

            // Если отвязано меньше запланированного — разделяем запись на “факт” и “остаток”
            var plannedQty = currentRow?.pzvKolNazn ?? currentRow?.pzvKol ?? 0;
            if (qty < defaultQty && currentRow?.pzvID > 0)
            {
                await _orchestrator.SplitPzvByFactAsync(currentRow.pzvID, qty);
            }

            await ApplyPzvDateAsync(
                view,
                bandedGridColumn19,
                _orchestrator.UpdatePzvDateEndAsync,
                m => m.pzvDateEnd,
                (m, v) => m.pzvDateEnd = v,
                "окончания");
            
            // Обновим план, чтобы показать новую запись остатка (если была создана)
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab))
            {
                // Сохраняем текущую машину, чтобы вернуть фокус после обновления
                var currentMachineKey = NormalizeMachineKey(currentRow?.kmlNumber);
                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(tab);
                // перестраиваем иерархию без очистки табеля
                _planPresenter.BindGroupDetails(bandedGridView3, /*bandedG*/gridView1, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);
                // Вернём фокус и раскроем нужную машину
                if (!string.IsNullOrEmpty(currentMachineKey))
                {
                    for (int i = 0; i < bandedGridView3.DataRowCount; i++)
                    {
                        if (bandedGridView3.GetRow(i) is KnitterPZVModel machineRow &&
                            NormalizeMachineKey(machineRow.kmlNumber) == currentMachineKey)
                        {
                            bandedGridView3.FocusedRowHandle = i;
                            bandedGridView3.SetMasterRowExpanded(i, true);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Унифицированный метод для установки даты в колонках "Начато"/"Закончено":
        /// - сохраняет дату на сервере (серверное время)
        /// - применяет дельту к модели и немедленно отражает значение в ячейке без смены фокуса
        /// </summary>
        private async Task ApplyPzvDateAsync(
            GridView view,
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn column,
            Func<int, Task<KnitterPZVModel>> updateFunc,
            Func<KnitterPZVModel, DateTime?> getDate,
            Action<KnitterPZVModel, DateTime?> setDate,
            string errorContext)
        {
            GridView _view = view;
            int rowHandle = _view.FocusedRowHandle;
            KnitterPZVModel row = _view.GetRow(rowHandle) as KnitterPZVModel;
            if (rowHandle < 0 || row?.pzvID <= 0)
                return;

            try
            {
                var updated = await updateFunc(row.pzvID);
                var newValue = getDate(updated) ?? getDate(row);
                setDate(row, newValue);
                _view.PostEditor();
                _view.SetRowCellValue(rowHandle, column, newValue);
                _view.PostEditor();
                _view.CloseEditor();
                _view.UpdateCurrentRow();
                _view.RefreshRowCell(rowHandle, column);
                _view.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при обновлении даты {errorContext}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
        }
        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        private static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          //  var a = Block14Composer.Format(textEdit2.Text);

          //  textEdit3.Text = a.ToString();
        }

        /// <summary>
        /// Настраивает таймер бездействия и подписывается на события активности пользователя.
        /// </summary>
        private void SetupIdleTimer()
        {
            _idleTimer.Interval = 60000; // 1 минута = 60000 миллисекунд
            _idleTimer.Tick += IdleTimer_Tick;

            // Подписываемся на события активности для сброса таймера
            this.MouseMove += (s, e) => ResetIdleTimer();
            this.KeyDown += (s, e) => ResetIdleTimer();
            this.MouseClick += (s, e) => ResetIdleTimer();
            this.MouseDown += (s, e) => ResetIdleTimer();
            this.KeyPress += (s, e) => ResetIdleTimer();

            // Подписываемся на события активности после полной загрузки формы
            this.Shown += (s, e) =>
            {
                // Также отслеживаем активность в дочерних контролах
                AttachActivityHandlers(this);
            };

            // Останавливаем таймер при закрытии формы
            this.FormClosing += (s, e) =>
            {
                _idleTimer.Stop();
                _idleTimer.Dispose();
            };
        }

        /// <summary>
        /// Рекурсивно подписывается на события активности для всех дочерних контролов.
        /// </summary>
        private void AttachActivityHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.MouseMove += (s, e) => ResetIdleTimer();
                control.MouseClick += (s, e) => ResetIdleTimer();
                control.MouseDown += (s, e) => ResetIdleTimer();
                control.KeyDown += (s, e) => ResetIdleTimer();
                control.KeyPress += (s, e) => ResetIdleTimer();

                // Рекурсивно обрабатываем вложенные контролы
                if (control.HasChildren)
                {
                    AttachActivityHandlers(control);
                }
            }
        }

        /// <summary>
        /// Обработчик таймера бездействия: показывает сплеш выбора сотрудника.
        /// </summary>
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            // Не показываем сплеш, если он уже открыт
            if (_isSplashShowing)
                return;

            // Останавливаем таймер перед показом сплеша
            _idleTimer.Stop();

            // Показываем сплеш с сохраненным списком ФИО
            if (_cachedFioList != null && _cachedFioList.Count > 0)
            {
                const int defaultTab = 1438;
                PresentFioSelectionSplash(_cachedFioList, defaultTab);
            }
        }

        /// <summary>
        /// Сбрасывает таймер бездействия, перезапуская отсчет с начала.
        /// </summary>
        private void ResetIdleTimer()
        {
            // Не сбрасываем таймер, если сплеш уже открыт
            if (_isSplashShowing)
                return;

            _idleTimer.Stop();
            _idleTimer.Start();
        }
    }
}


