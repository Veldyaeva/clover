using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : CustomForm
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
        private CheckBox _adminToggle;
        private CheckBox _expandNrToggle;
        private RepositoryItemProgressBar _statusProgressBar;
        private DevExpress.XtraGrid.GridGroupSummaryItem _pzvChasNaznGroupSumItem;

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
        /// Таймер смены (идёт с момента нажатия 'Начать смену' до 'Закончить смену').
        /// </summary>
        private readonly System.Windows.Forms.Timer _shiftTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _blinkCheckTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _blinkTimer = new System.Windows.Forms.Timer();
        private bool _isBlinking;
        private string _lastBlinkWindowKey;
        private DateTime _blinkEndTime;
        private Color _buttonDefaultBackColor;
        private TimeSpan _blinkTimeMorning = new TimeSpan(8, 0, 0);
        private TimeSpan _blinkTimeEvening = new TimeSpan(20, 0, 0);
        private int _blinkDurationMinutes = 1;
        private decimal _maxHoursClosedShift = 14m;
        private bool _showAllAssignedWhenClosed = false;
        private Button _adminSettingsButton;
        /// <summary>
        /// Флаг активной смены.
        /// </summary>
        private bool _isShiftRunning = false;
        /// <summary>
        /// Текущая запись смены (kwsID) для закрытия.
        /// </summary>
        private int? _currentShiftId = null;
        /// <summary>
        /// Время старта смены.
        /// </summary>
        private DateTime? _shiftStartTime = null;
        /// <summary>
        /// Текущая зона сотрудника (id и номер).
        /// </summary>
        private int? _currentKmaId = null;
        private string _currentKmaNum = null;
        /// <summary>
        /// Последний загруженный табельный номер, чтобы не перезагружать план без смены таба.
        /// </summary>
        private int? _currentLoadedTab = null;

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
        public KnitterWorkSpace(UserClass user) : base(user)
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
                SetupShiftTimer();
                InitAdminToggle();
                InitExpandNrToggle();
                //InitAdminSettingsButton();
                SetupStatusColumn();
                SetupBlinkTimers();
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
                bandedGridView3.CustomDrawFooterCell += BandedGridView3_CustomDrawFooterCell;
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
                SetupShiftTimer();
                InitAdminToggle();
                InitExpandNrToggle();
                //InitAdminSettingsButton();
                SetupStatusColumn();
                SetupBlinkTimers();
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
                bandedGridView3.CustomDrawFooterCell += BandedGridView3_CustomDrawFooterCell;
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
                fioList = await FilterFioByOpenShiftAsync(fioList);
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

                // Отображаем ФИО, зона — отдельным столбцом в всплывающем списке
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
			double oldOpacity = this.Opacity;
			Form overlay = null;
			try
			{
				// Перекрываем только текущую вкладку/форму KnitterWorkSpace, не блокируя остальные вкладки/кнопки
				overlay = new Form();
				overlay.FormBorderStyle = FormBorderStyle.None;
				overlay.StartPosition = FormStartPosition.Manual;
				overlay.ShowInTaskbar = false;
				overlay.BackColor = System.Drawing.Color.AliceBlue;
				overlay.TopMost = false; // достаточно быть над текущей формой
				overlay.Owner = this;

				// Берём границы основного layout текущей вкладки; если что-то пойдёт не так — используем всю клиентскую область формы
				var bounds = dataLayoutControl1?.RectangleToScreen(dataLayoutControl1.ClientRectangle)
					?? this.RectangleToScreen(this.ClientRectangle);
				overlay.Bounds = bounds;
				overlay.Show();

				using (var splash = new FioSelectionSplash(fioList, initialTab))
				{
					splash.StartPosition = FormStartPosition.CenterScreen;
				//	splash.TopMost = true;
					var result = splash.ShowDialog(overlay);
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
				// Убираем оверлей и возвращаем видимость формы
				if (overlay != null)
				{
					try { overlay.Close(); } catch { }
					overlay.Dispose();
				}
				this.Opacity = oldOpacity;
				_isSplashShowing = false;
			}
        }

        /// <summary>
        /// Дополнительная настройка второго уровня (advBandedGridView1):
        /// - создаёт скрытую unbound-колонку с готовой строкой заголовка группы
        /// - группирует по этой колонке и авторазворачивает единственную группу
        /// В результате под номером В/М сразу отображается шапка "Пачка | Задание | Размер | Кол-во" и таблица операций.
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
                    UnboundExpression = "Concat('Пачка: ', [n_pach], ' | Задание: ', [pzvNomZad], ' | Размер: ', [razm], ' | Кол-во: ', [pzvRKol])",
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

                // Групповой итог по "назначено в м/ч" (pzvChasNazn) в футере группы
                //var assignedCol = gridColumn6 ?? advBandedGridView1.Columns.ColumnByFieldName("pzvChasNazn");
                //if (assignedCol != null)
                //{
                //    _pzvChasNaznGroupSumItem = advBandedGridView1.GroupSummary
                //        .OfType<DevExpress.XtraGrid.GridGroupSummaryItem>()
                //        .FirstOrDefault(gs => gs.FieldName == "pzvChasNazn" && gs.SummaryType == DevExpress.Data.SummaryItemType.Sum);

                //    if (_pzvChasNaznGroupSumItem == null)
                //    {
                //        _pzvChasNaznGroupSumItem = new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "pzvChasNazn", assignedCol, "{0:0.00}");
                //        advBandedGridView1.GroupSummary.Add(_pzvChasNaznGroupSumItem);
                //    }
                //}

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
                    _currentLoadedTab = null;
                    return;
                }

                await LoadPlanForTabAsync(tab);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Кнопка "Начать смену": массово назначает табель выбранным строкам, присваивает kolNazn, ChasiNazn, sekNazn и обновляет отображение
        /// </summary>
        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Если смена уже запущена — завершаем смену: запись в БД, остановка таймера и смена текста
                if (_isShiftRunning)
                {
                    // Перед завершением смены: обработать все операции; если есть незавершённые — не закрываем.
                    var canClose = await ProcessOperationsOnShiftEndAsync();
                    if (!canClose)
                        return;
                    //// снимаем назначение у всех НЕ начатых в текущей смене
                    //await _orchestrator.UnassignNotStartedByShiftAsync(_currentShiftId);


                    //WarnIfMachineFactHoursLessThan12(_currentShiftId);
                    var stat = (await _orchestrator.AdjustNotStartedBeforeShiftEndAsync(_currentShiftId, 12m)).ToList();

                    //var bad = stat.Where(x => x.StillLessThanMin == 1).ToList();
                    //if (bad.Count > 0)
                    //{
                    //    var msg =
                    //        "По некоторым станкам даже с добором неначатых не набирается 12 часов:\n\n" +
                    //        string.Join("\n", bad.Select(x => $"• kmlID={x.pzvKmlID}: факт {x.FactHours:0.##} + добор {x.KeptAssignedHours:0.##} = {x.TotalForCheck:0.##}"));
                    //    XtraMessageBox.Show(this, msg, "Проверка 12 часов", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}


                    if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tabEnd) || tabEnd <= 0)
                    {
                        XtraMessageBox.Show(this, "Не удалось определить табель при завершении смены.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
                    {
                        await _orchestrator.EndWorkingShiftAsync(_currentShiftId.Value, tabEnd);
                        // Перезагрузим план, чтобы обновить статусы/проценты
                        await LoadPlanForTabAsync(tabEnd, forceReload: true);
                    }

                    await RefreshFioListAsync();
                    ApplyShiftUi(false, null, null);
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Проверка: нельзя открыть вторую смену в зоне
                if (_currentKmaId.HasValue)
                {
                    var openByZone = await _orchestrator.GetOpenShiftByZoneAsync(_currentKmaId.Value);
                    if (openByZone.shiftId.HasValue)
                    {
                        XtraMessageBox.Show(this, $"В зоне {_currentKmaNum} уже открыта смена (таб. {openByZone.tabStart}), сначала завершите её.", "Смена уже открыта", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Назначаем таб ВСЕМ загруженным строкам 
                var rowsForUpdate = _planPresenter.AllRows?.ToList() ?? new List<KnitterPZVModel>();
                if (!rowsForUpdate.Any())
                {
                    XtraMessageBox.Show(this, "Нет строк для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pzvIds = rowsForUpdate
                    .Select(r => r.pzvID)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (pzvIds.Count == 0)
                {
                    XtraMessageBox.Show(this, "Не удалось определить записи для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Успешный старт смены: фиксируем в БД, проставляем pzvKwsID для всех! операций, меняем текст кнопки и запускаем таймер
                try
                {
                    await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);
                    _currentShiftId = await _orchestrator.StartWorkingShiftAsync(selectedTab, _currentKmaId, _currentKmaNum);
                    if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
                    {
                        await _orchestrator.UpdatePzvKwsIdAsync(pzvIds, _currentShiftId.Value);
                    }

                    // Обновим план после проставления pzvKwsID
                    await LoadPlanForTabAsync(selectedTab, forceReload: true);
                    await RefreshFioListAsync();

                    ApplyShiftUi(true, _currentShiftId, DateTime.Now);
                }
                catch (Exception exStart)
                {
                    XtraMessageBox.Show(this, $"Не удалось записать начало смены: {exStart.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshStatusColumns()
        {
            // Форсируем перерасчёт unbound-колонок (процент/статус)
            bandedGridView3?.RefreshData();
            advBandedGridView1?.RefreshData();
            RefreshFooterSummaries();
        }

        /// <summary>
        /// Обновляет футеры после изменений данных.
        /// </summary>
        private void RefreshFooterSummaries()
        {
            bandedGridView3?.UpdateSummary();
            advBandedGridView1?.UpdateSummary();
        }

        /// <summary>
        /// Перезагружает список ФИО с учётом фильтра по открытым сменам, сохраняет текущий выбор, если он есть.
        /// </summary>
        private async Task RefreshFioListAsync()
        {
            var currentSelection = FioGridLookUpEdit.EditValue?.ToString();

            List<FioModel> fioList = await _orchestrator.GetFioListAsync();
            fioList = await FilterFioByOpenShiftAsync(fioList);
            fioList ??= new List<FioModel>();

            FioGridLookUpEdit.Properties.DataSource = fioList;
            TabGridLookUpEdit.Properties.DataSource = fioList;
            _cachedFioList = fioList;

            if (int.TryParse(currentSelection, out int tab) && fioList.Any(f => f.Tab == tab))
            {
                FioGridLookUpEdit.EditValue = tab;
                TabGridLookUpEdit.EditValue = tab;
            }
            else
            {
                FioGridLookUpEdit.EditValue = null;
                TabGridLookUpEdit.EditValue = null;
            }
        }

        private void ShowAdminSettingsDialog()
        {
            using var form = new Form
            {
                Text = "Настройки админки",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(320, 220)
            };

            var lblBlink1 = new Label { Text = "Время мигания 1:", Location = new Point(10, 20), AutoSize = true };
            var timeBlink1 = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(150, 16),
                Width = 120,
                Value = DateTime.Today.Add(_blinkTimeMorning)
            };

            var lblBlink2 = new Label { Text = "Время мигания 2:", Location = new Point(10, 55), AutoSize = true };
            var timeBlink2 = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(150, 51),
                Width = 120,
                Value = DateTime.Today.Add(_blinkTimeEvening)
            };

            var lblMaxHours = new Label { Text = "MaxHours (закрытая):", Location = new Point(10, 90), AutoSize = true };
            var numMaxHours = new NumericUpDown
            {
                Location = new Point(150, 86),
                Width = 120,
                DecimalPlaces = 1,
                Minimum = 0,
                Maximum = 500,
                Value = _maxHoursClosedShift
            };

            var chkShowAllAssigned = new CheckBox
            {
                Text = "Показывать все назначенные (закрытая)",
                Location = new Point(10, 125),
                AutoSize = true,
                Checked = _showAllAssignedWhenClosed
            };

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(70, 170), Width = 80 };
            var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Location = new Point(170, 170), Width = 80 };

            form.Controls.AddRange(new Control[] { lblBlink1, timeBlink1, lblBlink2, timeBlink2, lblMaxHours, numMaxHours, chkShowAllAssigned, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _blinkTimeMorning = timeBlink1.Value.TimeOfDay;
                _blinkTimeEvening = timeBlink2.Value.TimeOfDay;
                _maxHoursClosedShift = numMaxHours.Value;
                _showAllAssignedWhenClosed = chkShowAllAssigned.Checked;

                // Сбросим ключ окна, чтобы мигание могло сработать с новыми настройками
                _lastBlinkWindowKey = null;
            }
        }

        private void SetupBlinkTimers()
        {
            _buttonDefaultBackColor = simpleButton2.BackColor;
            _blinkCheckTimer.Interval = 15_000; // раз в 15 секунд проверяем окно 8:00/20:00
            _blinkCheckTimer.Tick += BlinkCheckTimer_Tick;
            _blinkCheckTimer.Start();

            _blinkTimer.Interval = 500; // мигаем раз в полсекунды
            _blinkTimer.Tick += BlinkTimer_Tick;
        }

        private void BlinkCheckTimer_Tick(object sender, EventArgs e)
        {
            if (_isBlinking)
                return;

            var now = DateTime.Now;
            var windowKey = GetBlinkWindowKey(now);
            if (windowKey == null)
                return;
            if (windowKey == _lastBlinkWindowKey)
                return; // уже мигали в этом окне

            var windowStart = GetWindowStart(now);
            if (now >= windowStart && now <= windowStart.AddMinutes(1))
            {
                StartBlink(windowKey, windowStart.AddMinutes(1));
            }
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (!_isBlinking)
                return;

            if (DateTime.Now >= _blinkEndTime)
            {
                StopBlink();
                return;
            }

            // Тоггл цвета между фиолетовым и дефолтным
            simpleButton2.BackColor = simpleButton2.BackColor == Color.MediumPurple
                ? _buttonDefaultBackColor
                : Color.MediumPurple;
        }

        private void StartBlink(string windowKey, DateTime endTime)
        {
            _isBlinking = true;
            _blinkEndTime = endTime;
            _lastBlinkWindowKey = windowKey;
            _blinkTimer.Start();
        }

        private void StopBlink()
        {
            _blinkTimer.Stop();
            _isBlinking = false;
            simpleButton2.BackColor = _buttonDefaultBackColor;
        }

        private static string GetBlinkWindowKey(DateTime now)
        {
            if (IsInBlinkWindow(now))
            {
                return $"{now:yyyyMMdd}_{now.Hour}";
            }
            return null;
        }

        private static DateTime GetWindowStart(DateTime now)
        {
            if (now.Hour >= 15 && now.Hour < 17)
                return new DateTime(now.Year, now.Month, now.Day, 16, 13, 0);
            if (now.Hour >= 17)
                return new DateTime(now.Year, now.Month, now.Day, 20, 0, 0);
            // до 8 утра: окно предыдущего дня в 20:00 уже прошло, следующее — 8:00 сегодняшнего
            return new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
        }

        private static bool IsInBlinkWindow(DateTime now)
        {
            var start8 = new DateTime(now.Year, now.Month, now.Day, 16, 8, 0);
            var start20 = new DateTime(now.Year, now.Month, now.Day, 16, 7, 0);

            return (now >= start8 && now <= start8.AddMinutes(1)) ||
                   (now >= start20 && now <= start20.AddMinutes(1));
        }

        /// <summary>
        /// При завершении смены: для неначатых — split mode=2 с отриц. количеством; для начатых без конца — спросить факт и закрыть.
        /// </summary>
        private async Task<bool> ProcessOperationsOnShiftEndAsync()
        {
            var rows = _planPresenter.AllRows?.Where(r => r != null && r.pzvID > 0).ToList() ?? new List<KnitterPZVModel>();
            if (!rows.Any())
                return true;


            // Начатые, но не завершённые → спросить факт, закрыть, при необходимости split по факту
            var inProgress = rows.Where(r => r.pzvDateStart != null && r.pzvDateEnd == null).ToList();
            if (inProgress.Any())
            {
                MessageBox.Show("В смене есть начатые, но не завершённые операции. Завершите операции, прежде чем закончить смену.", "Завершение операций", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
                //foreach (var row in inProgress)
                //{
                //    int plannedQty = row.pzvKolNazn > 0 ? row.pzvKolNazn : (row.pzvKol ?? 0);
                //    var qtyObj = DevExpress.XtraEditors.XtraInputBox.Show(
                //        $"Введите фактическое количество для операции {row.pzvNomZad}/{row.pzvArticul}",
                //        "Завершение операции",
                //        plannedQty);
                //    if (qtyObj == null)
                //        continue; // пропускаем, если отмена
                //    if (!int.TryParse(qtyObj.ToString(), out int qty) || qty < 0 || qty > plannedQty)
                //    {
                //        XtraMessageBox.Show(this, "Значение должно быть в диапазоне 0..план.", "Неверное значение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        continue;
                //    }

                //    try
                //    {
                //        await _orchestrator.UpdatePzvDateEndAsync(row.pzvID);

                //        if (qty == 0)
                //        {
                //            await _orchestrator.SplitPzvAsync(row.pzvID, 2, 0);
                //        }
                //        else if (qty < plannedQty)
                //        {
                //            await _orchestrator.SplitPzvByFactAsync(row.pzvID, qty);
                //        }
                //        // qty == plannedQty: только дата окончания уже поставлена
                //    }
                //    catch
                //    {
                //        // Игнорируем сбой одной операции, продолжаем остальные
                //    }
                //}
            }
            // Неначатые (нет даты старта и окончания) → split mode=2
            var notStarted = rows.Where(r => r.pzvDateStart == null && r.pzvDateEnd == null).ToList();
            foreach (var row in notStarted)
            {
                try
                { //если завершается в конце смены с фактом 0 - это случай 2 с отрицательной строкой
                    await _orchestrator.SplitPzvAsync(row.pzvID, 2, 0);
                }
                catch
                {
                    // Игнорируем сбой split одной операции, продолжаем остальные
                }
            }

            return true;
        }



        private void WarnIfMachineFactHoursLessThan12(int? currentKwsId)
        {
            var rows = _planPresenter.AllRows?
                .Where(r => (r.pzvKwsID ?? 0) == currentKwsId)
                .Where(r => r.pzvKmlID > 0)
                .ToList();

            if (rows == null || rows.Count == 0)
                return;

            const decimal minHours = 12m;

            var bad = rows
                .GroupBy(r => new { KmlId = r.pzvKmlID!, r.kmlNumber })
                .Select(g => new
                {
                    g.Key.KmlId,
                    Machine = string.IsNullOrWhiteSpace(g.Key.kmlNumber) ? g.Key.KmlId.ToString() : g.Key.kmlNumber,
                    Hours = g.Sum(x => x.FactChas_UI) 
                })
                .Where(x => x.Hours < minHours)
                .OrderBy(x => x.Machine)
                .ToList();

            if (!bad.Any())
                return;

            var msg =
                "Недобор фактических часов по машинам (< 12 ч):\n\n" +
                string.Join("\n", bad.Select(x => $"Машина {x.Machine}: {x.Hours:0.##} ч"));

            XtraMessageBox.Show(this, msg, "Проверка часов перед закрытием смены",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            advBandedGridView1.CustomColumnDisplayText -= AdvBandedGridView1_CustomColumnDisplayText;
            advBandedGridView1.CustomColumnDisplayText += AdvBandedGridView1_CustomColumnDisplayText;

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
        /// Показываем сумму по группе в ячейке "назначено в м/ч" (pzvChasNazn) в строке группы.
        /// </summary>
        private void AdvBandedGridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != "pzvChasNazn")
                return;

            if (_pzvChasNaznGroupSumItem == null)
                return;

            if (sender is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
            {
                int rowHandle = view.GetRowHandle(e.ListSourceRowIndex);
                if (!view.IsGroupRow(rowHandle))
                    return;

                var val = view.GetGroupSummaryValue(rowHandle, _pzvChasNaznGroupSumItem);
                if (val != null && val != DBNull.Value)
                {
                    e.DisplayText = string.Format("{0:0.00}", val);
                }
            }
        }

        /// <summary>
        /// Считает общие суммы часов по всем строкам детального уровня.
        /// </summary>
        private (decimal planTotal, decimal factTotal) GetGlobalHourTotals()
        {
            if (_planPresenter?.AllRows == null)
                return (0m, 0m);

            decimal plan = _planPresenter.AllRows
                .Where(r => r != null)
                .Sum(r => r.PlanChas_UI ?? 0m);

            decimal fact = _planPresenter.AllRows
                .Where(r => r != null)
                .Sum(r => r.FactChas_UI ?? 0m);

            return (Math.Round(plan, 2), Math.Round(fact, 2));
        }

        /// <summary>
        /// В футере bandedGridView3 показываем локальную сумму и общую сумму по всем строкам.
        /// </summary>
        private void BandedGridView3_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;

            bool isPlan = string.Equals(e.Column.FieldName, "pzvChasNazn", StringComparison.OrdinalIgnoreCase);
            bool isFact = string.Equals(e.Column.FieldName, "pzvNChasi", StringComparison.OrdinalIgnoreCase);
            if (!isPlan && !isFact)
                return;

            decimal localSum = 0m;
            if (e.Info?.Value != null && e.Info.Value != DBNull.Value && decimal.TryParse(e.Info.Value.ToString(), out var parsedLocal))
            {
                localSum = parsedLocal;
            }

            var totals = GetGlobalHourTotals();
            decimal globalSum = isPlan ? totals.planTotal : totals.factTotal;

            e.Info.DisplayText = $"все: {globalSum:0.##}";
        }

        /// <summary>
        /// Для верхнего уровня (bandedGridView3): в колонке pzvChasNazn отображаем сумму назначенных часов по всем операциям этой машины/задания.
        /// </summary>
        private void BandedGridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != "pzvChasNazn")
                return;

            if (sender is BandedGridView view)
            {
                int rowHandle = view.GetRowHandle(e.ListSourceRowIndex);
                var row = view.GetRow(rowHandle) as KnitterPZVModel;
                if (row == null)
                    return;

                var taskNum = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
                var machineKey = KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber);

                var sum = _planPresenter.AllRows
                    .Where(r =>
                        KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad) == taskNum &&
                        KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber) == machineKey)
                    .Sum(r => r.pzvChasNazn);

                e.DisplayText = string.Format("{0:0.00}", sum);
            }
        }

        /// <summary>
        /// Устанавливает часы факт (pzvNChasi), если они пусты, по формуле pzvSek * факт.кол-во / 3600.
        /// </summary>
        private void EnsureFactHours(KnitterPZVModel row)
        {
            if (row == null)
                return;

            // Считаем факт-часы только для завершённых операций (есть дата окончания)
            if ((row.pzvNChasi == null || row.pzvNChasi == 0m) && row.pzvDateEnd != null)
            {
                var factQty = row.pzvKol ?? 0;
                if (row.pzvSek > 0 && factQty > 0)
                {
                    row.pzvNChasi = Math.Round((row.pzvSek * factQty) / 3600m, 2);
                }
            }
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
            // Если плановое количество уже перенесено в назначенное (pzvKol обнулён), используем pzvKolNazn как "к выполнению"
            int defaultQty = currentRow?.pzvKolNazn ?? 0;
            if (defaultQty == 0 && currentRow != null && currentRow.pzvKolNazn > 0)
                defaultQty = currentRow.pzvKolNazn;

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
            // Обновляем факт в текущей строке и в мастер-коллекции, чтобы статус пересчитался без полной перезагрузки
            if (currentRow != null)
            {
                currentRow.pzvKol = qty;
                //currentRow.FactKol_UI = defaultQty; схерали дефалт квантити??? 
                currentRow.FactKol_UI = qty; // вроде так
                // Мгновенно пересчитываем часы факт для прогресса (секунды на изделие * факт / 3600)
                decimal factHours = 0m;
                if (currentRow.pzvSek > 0)
                {
                    factHours = Math.Round((currentRow.pzvSek * qty) / 3600m, 2);
                    currentRow.pzvNChasi = factHours; 
                }
                var masterRow = _planPresenter.AllRows?.FirstOrDefault(r => r != null && r.pzvID == currentRow.pzvID);
                if (masterRow != null)
                {
                    masterRow.pzvKol = qty;
                    if (factHours > 0)
                        masterRow.pzvNChasi = factHours;
                }
            }
            // Отобразим введённое значение в столбце факта (unbound)
            _view.SetRowCellValue(rowHandle, bandedGridColumn22, qty);
            _view.PostEditor();
            _view.CloseEditor();
            _view.UpdateCurrentRow();
            _view.RefreshRowCell(rowHandle, bandedGridColumn22);

            // Сначала сохраняем факт в БД (кол-во и часы факт)
            if (currentRow?.pzvID > 0)
            {
                await _orchestrator.UpdatePzvFactAsync(currentRow.pzvID, qty);
            }

            // Затем устанавливаем дату окончания (SP также ставит её, но нам важно обойти проверки до вызова SP)
            await ApplyPzvDateAsync(
                view,
                bandedGridColumn19,
                _orchestrator.UpdatePzvDateEndAsync,
                m => m.pzvDateEnd,
                (m, v) => m.pzvDateEnd = v,
                "окончания");

			// Затем — разделение записи в зависимости от введённого количества
			IReadOnlyList<PzvSplitResult> newIds = Array.Empty<PzvSplitResult>();
			if (currentRow?.pzvID > 0)
			{ if (defaultQty > 0)
                {
                    //if (qty == 0)
                    //{
                    //    // создаём отрицательную строку mode = 2
                    //    newIds = await _orchestrator.SplitPzvAsync(currentRow.pzvID, 2, 0);
                    //}
                    //else Если сама завершает с фактом 0 - это тот же случай 1 с введённым количеством 
                    if (qty < defaultQty) 
                    {
                        // Факт меньше запланированного — mode = 1 c qtyFact
                        newIds = await _orchestrator.SplitPzvByFactAsync(currentRow.pzvID, qty);
                    }
                }
			}
            
            // Обновим план, чтобы показать новую запись остатка (если была создана)
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab))
            {
                // Сохраняем текущую машину, чтобы вернуть фокус после обновления
                var currentMachineKey = NormalizeMachineKey(currentRow?.kmlNumber);
                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(tab, _currentShiftId, _currentKmaId, false, false, 14);//(tab);
                // перестраиваем иерархию без очистки табеля
                _planPresenter.BindGroupDetails(bandedGridView3, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);
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
                            // Если знаем новый pzvID — найдём строку во втором уровне и выделим её
                            if (newIds != null && newIds.Count > 0)
                            {
                                var detailView = bandedGridView3.GetDetailView(i, 0) as DevExpress.XtraGrid.Views.Base.ColumnView;
                                if (detailView != null)
                                {
                                    var targetId = newIds[0].NewPzvId;
                                    for (int r = 0; r < detailView.DataRowCount; r++)
                                    {
                                        if (detailView.GetRow(r) is KnitterPZVModel opRow && opRow.pzvID == targetId)
                                        {
                                            detailView.FocusedRowHandle = r;
                                            if (detailView is GridView gvDetail)
                                                gvDetail.MakeRowVisible(r, true);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                // Нет табеля — просто обновим расчётные колонки
                RefreshStatusColumns();
            }

            // Обновляем статус/процент после завершения операции
            RefreshStatusColumns();
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
                // Также обновляем мастер-коллекцию, чтобы проверки при закрытии смены видели актуальные даты
                var masterRow = _planPresenter?.AllRows?.FirstOrDefault(r => r != null && r.pzvID == row.pzvID);
                if (masterRow != null)
                {
                    setDate(masterRow, newValue);
                }
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
            _idleTimer.Interval = 180000000; // 1 минута = 60000 миллисекунд
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
                _shiftTimer.Stop();
                _shiftTimer.Dispose();
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

        /// <summary>
        /// Настройка таймера смены.
        /// </summary>
        private void SetupShiftTimer()
        {
            _shiftTimer.Interval = 1000; // 1 секунда
            _shiftTimer.Tick += (s, e) =>
            {
                if (_isShiftRunning && _shiftStartTime.HasValue)
                {
                    var elapsed = DateTime.Now - _shiftStartTime.Value;
                    if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;
                    simpleLabelItem1.Text = $"Смена: {elapsed:hh\\:mm\\:ss}";
                }
            };
        }

        #region adminToggle
        private void InitAdminToggle()
        {
            _adminToggle = new CheckBox
            {
                Text = "Админ режим",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 130, 5)
            };
            Controls.Add(_adminToggle);
            _adminToggle.BringToFront();
            _adminToggle.CheckedChanged += async (s, e) => await ReloadCurrentTabAsync();
        }

        private void InitExpandNrToggle()
        {
            _expandNrToggle = new CheckBox
            {
                Text = "Все операции",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 130, 20)
            };
            Controls.Add(_expandNrToggle);
            _expandNrToggle.BringToFront();
            _expandNrToggle.CheckedChanged += async (s, e) => await ReloadCurrentTabAsync();
        }

        private void InitAdminSettingsButton()
        {
            _adminSettingsButton = new Button
            {
                Text = "Админка",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 220, 5)
            };
            Controls.Add(_adminSettingsButton);
            _adminSettingsButton.BringToFront();
            _adminSettingsButton.Click += (s, e) => ShowAdminSettingsDialog();
        }
        #endregion

        private void SetupStatusColumn()
        {
            // Индикатор в колонке статуса: часы факт / часы назначено
            _statusProgressBar = new RepositoryItemProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                ShowTitle = true,
                PercentView = true
            };

            gridColumn8.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            gridColumn8.UnboundExpression = string.Empty;
            gridColumn8.ColumnEdit = _statusProgressBar;

            bandedGridView3.CustomUnboundColumnData -= BandedGridView3_CustomUnboundColumnData;
            bandedGridView3.CustomUnboundColumnData += BandedGridView3_CustomUnboundColumnData;
        }

        private void BandedGridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column != gridColumn8 || !e.IsGetData)
                return;

            // По умолчанию показываем 0%
            e.Value = 0m;

            if (e.Row is KnitterPZVModel row)
            {
                // Считаем статус по суммам часов из БД:
                // bandedGridColumn26 (pzvChasNazn) и bandedGridColumn27 (pzvNChasi)
                var machineKey = NormalizeMachineKey(row.kmlNumber);
                var taskKey = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
                var rows = _planPresenter.AllRows?
                    .Where(r =>
                        r != null &&
                        string.Equals(NormalizeMachineKey(r.kmlNumber), machineKey, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad), taskKey, StringComparison.OrdinalIgnoreCase))
                    .ToList() ?? new List<KnitterPZVModel>();

                //decimal? assignedHours = rows.Sum(r => r.PlanChas_UI);//pzvChasNazn);//pzvSekNazn);//
                //decimal doneHours = rows.Sum(r => r.FactChas_UI ?? 0m);//pzvNChasi ?? 0m);//pzvSek);//

                //decimal? percent = 0m;
                //if (assignedHours > 0)
                //{
                //    percent = doneHours / assignedHours * 100m;
                //    if (percent > 100m) percent = 100m;
                //    if (percent < 0m) percent = 0m;
                //}
                decimal assignedHours = rows.Sum(r => r.PlanChas_UI ?? 0m);
                decimal doneHours = rows.Sum(r => r.FactChas_UI ?? 0m);

                decimal percent =
                    assignedHours > 0m
                        ? Math.Round(doneHours * 100m / assignedHours, 1)
                        : 0m;
                e.Value = percent;
            }
        }

        private async Task ReloadCurrentTabAsync()
        {
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab) && tab > 0)
            {
                await LoadPlanForTabAsync(tab, forceReload: true);
            }
        }

        /// <summary>
        /// Запрещает редактирование строк, назначенных на другой табельный номер.
        /// </summary>
        private void GridView_PreventForeignEdit(object sender, CancelEventArgs e)
        {
            if (sender is not ColumnView view)
                return;

            if (view is DevExpress.XtraGrid.Views.Base.ColumnView columnView)
            {
                // Групповые строки не редактируются
                if (view is GridView grid && grid.IsGroupRow(grid.FocusedRowHandle))
                {
                    e.Cancel = true;
                    return;
                }

                if (columnView.GetFocusedRow() is not KnitterPZVModel row)
                {
                    e.Cancel = true;
                    return;
                }

                if (_currentLoadedTab.HasValue && row.pzvTab.HasValue && row.pzvTab.Value != _currentLoadedTab.Value)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Фильтрует список ФИО: если в зоне есть открытая смена, показывает только сотрудника(ов) с этой сменой; иначе — всех в зоне.
        /// </summary>
        private async Task<List<FioModel>> FilterFioByOpenShiftAsync(List<FioModel> fioList)
        {
            if (fioList == null || fioList.Count == 0)
                return fioList ?? new List<FioModel>();

            var result = new List<FioModel>();

            // Группируем по зоне; пустая зона считается отдельной группой
            foreach (var group in fioList.GroupBy(f => f.Zone ?? string.Empty))
            {
                var openTabs = new List<FioModel>();
                foreach (var fio in group)
                {
                    var open = await _orchestrator.GetOpenShiftByTabAsync(fio.Tab);
                    if (open.shiftId.HasValue)
                    {
                        openTabs.Add(fio);
                    }
                }

                if (openTabs.Any())
                {
                    // Если найдены открытые смены, показываем только их в данной зоне
                    result.AddRange(openTabs);
                }
                else
                {
                    // Иначе показываем всех сотрудников зоны
                    result.AddRange(group);
                }
            }

            return result;
        }

        /// <summary>
        /// Централизованно применяет состояние смены к UI и поведению гридов.
        /// </summary>
        private void ApplyShiftUi(bool isRunning, int? shiftId, DateTime? shiftStart)
        {
            _isShiftRunning = isRunning;
            _currentShiftId = shiftId;
            _shiftStartTime = shiftStart;

            if (isRunning && shiftStart.HasValue)
            {
                simpleButton2.Text = "Закончить смену";
                var elapsed = DateTime.Now - shiftStart.Value;
                if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;
                simpleLabelItem1.Text = $"Смена: {elapsed:hh\\:mm\\:ss}";
                _shiftTimer.Start();
            }
            else
            {
                _shiftTimer.Stop();
                simpleButton2.Text = "Начать смену";
                simpleLabelItem1.Text = " ";
            }

            ApplyShiftEditMode(isRunning);
        }

        /// <summary>
        /// Переключает режим редактирования гридов: только просмотр, если смена не начата.
        /// </summary>
        private void ApplyShiftEditMode(bool isRunning)
        {
            bool editable = isRunning;

            void SetViewState(ColumnView view)
            {
                if (view == null)
                    return;

                view.OptionsBehavior.Editable = editable;
                view.OptionsBehavior.ReadOnly = !editable;

                foreach (GridColumn col in view.Columns)
                {
                    col.OptionsColumn.AllowEdit = editable;
                    col.OptionsColumn.AllowFocus = editable;
                }
            }

            SetViewState(bandedGridView3);
            SetViewState(advBandedGridView1);
        }

        private async Task LoadPlanForTabAsync(int tab, bool forceReload = false)
        {
            if (!forceReload && _currentLoadedTab.HasValue && _currentLoadedTab.Value == tab)
                return;

            TabGridLookUpEdit.EditValue = tab;

            await UpdateZoneAsync(tab);
            await UpdateShiftStateAsync(tab);

            bool isAdmin = _adminToggle?.Checked == true;
            bool isShiftOpen = _isShiftRunning && _currentShiftId.HasValue;

            // Режимы выборки:
            // - закрытая смена: только неназначенные, ограничение 14ч
            // - открытая смена: назначенные на текущую смену, без лимита по часам
            // - админ: includeFinished=true (видит завершённые)
            int? kwsId = isShiftOpen ? _currentShiftId : 0;
            //kwsId = isAdmin
            bool onlyUnassigned = !isShiftOpen && !_showAllAssignedWhenClosed;
         //   bool includeFinished = true;//isAdmin;
            decimal maxHours = isShiftOpen ? 240m : _maxHoursClosedShift;
            bool expandByNr = _expandNrToggle?.Checked == true;

            //var plan = await _orchestrator.GetPlanByTabAsync(tab, kwsId, onlyUnassigned, includeFinished, maxHours);
            var plan = await _orchestrator.GetPlanByTabAsync(tab, kwsId, _currentKmaId, onlyUnassigned, expandByNr, maxHours, includeFinished: isAdmin);
            // Если из БД факт часов пуст, считаем его по формуле pzvSek * фактическое количество / 3600
            if (plan != null)
            {
                foreach (var row in plan)
                {
                    EnsureFactHours(row);
                }
            }
                _planPresenter.BindGroupDetails(bandedGridView3, advBandedGridView1, _planBindingSource, plan ?? new List<KnitterPZVModel>(), clearTabs: false);
            RefreshFooterSummaries();
            _currentLoadedTab = tab;
        }

        private async Task UpdateZoneAsync(int tab)
        {
            try
            {
                var zone = await _orchestrator.GetZoneByTabAsync(tab);
                _currentKmaId = zone.kmaId;
                _currentKmaNum = zone.kmaNum;
                textEdit1.Text = _currentKmaNum?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                textEdit1.Text = string.Empty;
            }
        }

        private async Task UpdateShiftStateAsync(int tab)
        {
            try
            {
                var open = await _orchestrator.GetOpenShiftByTabAsync(tab);
                if (open.shiftId.HasValue && open.dateStart.HasValue)
                {
                    ApplyShiftUi(true, open.shiftId.Value, open.dateStart);
                }
                else
                {
                    ApplyShiftUi(false, null, null);
                }
            }
            catch (Exception)
            {
                ApplyShiftUi(false, null, null);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


