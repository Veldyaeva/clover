using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingMachinesLoading : CustomForm
    {
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        private List<KnitMachineLoadAllInfo> _currentKnitMachineLoadInfoData = new List<KnitMachineLoadAllInfo>();
        private BindingList<KnitMachineLoadAllInfo> _knitMachineLoadInfoBindingList;
        private BindingSource _knitMachineLoadInfoBindingSource;

        private List<KnitMachineList> _currentKnitMachineListData = new List<KnitMachineList>();
        private BindingList<KnitMachineList> _knitMachineListBindingList;
        private BindingSource _knitMachineListBindingSource;

        private List<KnitMachineClassList> _currentKnitMachineClassListData = new List<KnitMachineClassList>();
        private BindingList<KnitMachineClassList> _knitMachineClassListBindingList;
        private BindingSource _knitMachineClassListBindingSource;
        private List<KnitMachineClassList> knitMachineClassListData = new List<KnitMachineClassList>();

        private int xClassID;

        // --- Поля формы ---
        private PopupContainerControl _popup;
        private WebBrowser _browser; // можно заменить на WebView2
        private PopupContainerEdit _popupHost;         // скрытый хост для показа попапа
        private RepositoryItemPopupContainerEdit _repoPopup;
        private RepositoryItemMemoEdit _repoPreview;   // МНОГОСТРОЧНОЕ превью
        //private RepositoryItemHyperTextEdit _repoPreviewHyper;
        //private RepositoryItemLabelControl _repoPreviewLabel;
        private bool _configureOnce;
        private readonly string _htmlColumnName = "combinedPszNom"; // <--- ИМЯ КОЛОНКИ С HTML
        private bool _previewWired;
        private const int PREVIEW_HEIGHT = 120;   // высота зоны превью внутри карточки
        private int MIN_CARD_HEIGHT = 180;  // минимальная высота карточки (страховка)

        //private List<KnitMachineAreaListView> _currentKnitMachineAreaListViewData = new List<KnitMachineAreaListView>();
        //private BindingList<KnitMachineAreaListView> _knitMachineAreaListViewBindingList;
        //private BindingSource _knitMachineAreaListViewBindingSource;
        //private List<KnitMachineAreaListView> knitMachineAreaListViewData = new List<KnitMachineAreaListView>();

        public KnittingMachinesLoading(int classID)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL("ace");
            _dbService = new DbService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);
            _bulkHelper = new BulkHelper();
            xClassID = classID;
            //if (classID == 0)
            //{
            //    сomboBoxKnitMachineClassList.SelectedIndex = -1;
            //}
            //else
            //{
            //MessageBox.Show($"{xClassID}");
            сomboBoxKnitMachineClassList.SelectedValue = classID;
            //MessageBox.Show($"{сomboBoxKnitMachineClassList.SelectedValue}");
            //}
            сomboBoxKnitMachineClassList.Refresh();
        }
        // Функция конвертации HTML в RTF (используем RichTextBox)
        private string ConvertHtmlToRtf(string html)
        {
            using (RichTextBox rtBox = new RichTextBox())
            {
                rtBox.Text = html;
                return rtBox.Rtf;
            }
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var knitMachineListInfoTask = Task.Run(() =>
                {
                    _knitMachineLoadInfoBindingList = new BindingList<KnitMachineLoadAllInfo>();
                    _knitMachineLoadInfoBindingSource = new BindingSource { DataSource = _knitMachineLoadInfoBindingList };
                });
                //var knitMachineAreaListViewTask = Task.Run(() =>
                //{
                //    _knitMachineAreaListViewBindingList = new BindingList<KnitMachineAreaListView>();
                //    _knitMachineAreaListViewBindingSource = new BindingSource { DataSource = _knitMachineAreaListViewBindingList };
                //});
                var knitMachineClassListTask = Task.Run(() =>
                {
                    _knitMachineClassListBindingList = new BindingList<KnitMachineClassList>();
                    _knitMachineClassListBindingSource = new BindingSource { DataSource = _knitMachineClassListBindingList };
                });
                var knitMachineListTask = Task.Run(() =>
                {
                    _knitMachineListBindingList = new BindingList<KnitMachineList>();
                    _knitMachineListBindingSource = new BindingSource { DataSource = _knitMachineListBindingList };
                });

                //await Task.WhenAll(knitMachineListInfoTask, knitMachineAreaListViewTask, knitMachineListTask);
                await Task.WhenAll(knitMachineListInfoTask, knitMachineClassListTask, knitMachineListTask);

                #region описание gridControlKnitMachineList "текущий загруз В/М"
                gridControlKnitMachineLoadInfo.DataSource = _knitMachineLoadInfoBindingSource;
                gridKnitMachineLoadInfoColumnYearMonth.FieldName = "yearMonth";
                gridKnitMachineLoadInfoColumnYearMonthCard.FieldName = "yearMonth";
                gridKnitMachineLoadInfoColumnKmlNumber.FieldName = "kmlNumber";
                gridKnitMachineLoadInfoColumnKmlNumberCard.FieldName = "kmlNumber";
                gridKnitMachineLoadInfoColumnCombinedPszNom.FieldName = "combinedPszNom";
                gridKnitMachineLoadInfoColumnCombinedPszNomCard.FieldName = "combinedPszNom";
                //gridViewKnitMachineLoadLayoutView.CustomDrawCardFieldValue += View_CustomDrawCardFieldValue;
                ConfigureLayoutView();
                //SetupHtmlPopupForLayoutView("combinedPszNom", previewHeight: 140, cardHeight: 220);
                //var lcol = gridViewKnitMachineLoadLayoutView.Columns["gridKnitMachineLoadInfoColumnCombinedPszNomCard"] as LayoutViewColumn;
                //if (lcol != null)
                //{
                //    var field = lcol.LayoutViewField;

                //    field.SizeConstraintsType = SizeConstraintsType.Custom;
                //    field.MinSize = new Size(field.MinSize.Width, 150);  // фиксируем высоту секции
                //    field.MaxSize = new Size(int.MaxValue, 150);

                //    //// внутри этого поля должен быть редактор со скроллом
                //    //// пример для RichEdit:
                //    //var repoRich = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditControl { AutoHeight = false };
                //    //gridControlKnitMachineLoadInfo.RepositoryItems.Add(repoRich);
                //    //lcol.ColumnEdit = repoRich;
                //    //// (HTML заполняйте в самом редакторе: rich.Document.HtmlText = <ваш HTML>)
                //    //// либо для MemoEdit с прокруткой:
                //    //// var memo = new RepositoryItemMemoEdit { AutoHeight=false, WordWrap=true, ScrollBars=ScrollBars.Vertical };
                //    //// lcol.ColumnEdit = memo; // (без HTML-разметки)


                //}
                //// popup-контейнер
                //var popup = new PopupContainerControl { Size = new Size(600, 400) };
                //var browser = new WebBrowser { Dock = DockStyle.Fill }; // или WebView2
                //popup.Controls.Add(browser);

                //// редактор для колонки "Подробнее"
                //var repoPopup = new RepositoryItemPopupContainerEdit
                //{
                //    PopupControl = popup,
                //    AutoHeight = false
                //};
                //gridControlKnitMachineLoadInfo.RepositoryItems.Add(repoPopup);

                //// колонка "Подробнее"
                //var colMore = gridViewKnitMachineLoadLayoutView.Columns["More"]; // создаёшь служебную колонку
                //colMore.ColumnEdit = repoPopup;

                //// подгружаем HTML при открытии попапа
                //repoPopup.QueryPopUp += (s, e) =>
                //{
                //    var view = gridViewKnitMachineLoadLayoutView;
                //    var html = Convert.ToString(view.GetFocusedRowCellValue("combinedPszNom")); // поле с HTML
                //    browser.DocumentText = html ?? string.Empty;
                //};

                //// фиксируем высоту превью-поля (MemoEdit с прокруткой по желанию)
                //var lcolPreview = gridViewKnitMachineLoadLayoutView.Columns["combinedPszNom"] as LayoutViewColumn;
                //var memo = new RepositoryItemMemoEdit { AutoHeight = false, WordWrap = true, ScrollBars = ScrollBars.Vertical };
                //gridControlKnitMachineLoadInfo.RepositoryItems.Add(memo);
                //lcolPreview.ColumnEdit = memo;

                //var fieldPreview = lcolPreview.LayoutViewField;
                //fieldPreview.SizeConstraintsType = SizeConstraintsType.Custom;
                //fieldPreview.MinSize = new Size(fieldPreview.MinSize.Width, 120);
                //fieldPreview.MaxSize = new Size(int.MaxValue, 120);



                //gridViewKnitMachineLoadInfo.Columns["combinedPszNom"].DisplayFormat.CustomFormat += (value) =>
                //{
                //    return value?.ToString(); // RTF-текст передается напрямую
                //};

                //// Подключаем обработчик события
                //gridViewKnitMachineLoadInfo.CustomColumnDisplayText += (sender, e) =>
                //{
                //    // Проверяем, что это нужная колонка
                //    if (e.Column.FieldName == "combinedPszNom")
                //    {
                //        // Если значение не пустое, конвертируем его в RTF
                //        if (e.Value != null)
                //        {
                //            string htmlText = e.Value.ToString();
                //            string rtfText = ConvertHtmlToRtf(htmlText);
                //            e.DisplayText = rtfText; // Устанавливаем преобразованный текст
                //        }
                //    }
                //};
                #endregion
                #region описание comboBox "Список зон обслуживания"
                //сomboBoxKnitMachineAreaList.DataSource = _knitMachineAreaListViewBindingSource;
                //сomboBoxKnitMachineAreaList.SelectedIndex = -1;
                //сomboBoxKnitMachineAreaList.ValueMember = "kmaID";
                //сomboBoxKnitMachineAreaList.DisplayMember = "kmaNumber";
                //int xSelectedIndex = сomboBoxKnitMachineClassList.SelectedIndex;
                сomboBoxKnitMachineClassList.DataSource = _knitMachineClassListBindingSource;
                //сomboBoxKnitMachineClassList.SelectedIndex = xSelectedIndex;
                сomboBoxKnitMachineClassList.ValueMember = "id_class";
                сomboBoxKnitMachineClassList.DisplayMember = "caption";
                сomboBoxKnitMachineClassList.SelectedValue = xClassID;
                #endregion

                //// Подписываемся на событие отрисовки ячейки
                //gridViewKnitMachineLoadInfo.CustomDrawCell += (s, e) =>
                //{
                //    // Проверяем, что это нужная колонка (например, "Tags")
                //    if (e.Column.FieldName == "Tags")
                //    {
                //        // Получаем данные строки (предположим, что привязан объект с полями DatePlanFrom и DatePlanTo)
                //        var rowData = gridViewKnitMachineLoadInfo.GetRow(e.RowHandle) as KnitMachineLoadAllInfo;
                //        if (rowData == null) return;

                //        // Задаем целевой год и месяц (например, текущие)
                //        int targetYear = DateTime.Now.Year;
                //        int targetMonth = DateTime.Now.Month;

                //        // Проверяем условие: если год и месяц DatePlanFrom или DatePlanTo не совпадают с целевыми
                //        bool shouldHighlight =
                //            (rowData.DatePlanFrom.Year != targetYear || rowData.DatePlanFrom.Month != targetMonth) ||
                //            (rowData.DatePlanTo.Year != targetYear || rowData.DatePlanTo.Month != targetMonth);

                //        // Если условие выполнено — рисуем текст красным, иначе стандартным цветом
                //        e.Appearance.ForeColor = shouldHighlight ? Color.Red : e.Appearance.ForeColor;

                //        // Стандартная отрисовка текста
                //        e.DefaultDraw();
                //    }
                //};
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        // === КАСТОМНАЯ ОТРИСОВКА ПРЕВЬЮ ===
        private void View_CustomDrawCardFieldValue(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != _htmlColumnName) return;

            // стандартный фон/рамку нарисовать
            e.DefaultDraw();

            string html = Convert.ToString(e.CellValue) ?? string.Empty;
            var segs = ParseHtmlSegments(html);

            var baseFont = e.Appearance.Font ?? Control.DefaultFont;
            using var bold = new Font(baseFont, FontStyle.Bold);

            var bounds = e.Bounds;
            bounds.Inflate(-4, -4); // внутренние отступы

            DrawSegmentsWrapped(e.Graphics, segs, baseFont, bold, e.Appearance.ForeColor, bounds);

            e.Handled = true;
        }


        //private void SetupHtmlPopupForLayoutView()
        //{
        //    var view = gridViewKnitMachineLoadLayoutView; // это LayoutView, несмотря на имя :)

        //    // 1) Попап + браузер
        //    _popup = new PopupContainerControl
        //    {
        //        Parent = this,                      // родитель — форма (или любой живой контейнер)
        //        Size = new Size(700, 500)
        //    };
        //    _browser = new WebBrowser { Dock = DockStyle.Fill };
        //    _popup.Controls.Add(_browser);

        //    // 2) Репозиторий PopupContainerEdit
        //    _repoPopup = new RepositoryItemPopupContainerEdit
        //    {
        //        AutoHeight = false,
        //        PopupControl = _popup,
        //        ShowPopupCloseButton = true,
        //        TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor,
        //        NullText = "Открыть…"
        //    };
        //    gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPopup);

        //    // Подгружаем HTML из поля данных при открытии
        //    _repoPopup.QueryPopUp += (s, e) => {
        //        // ЗДЕСЬ ПОДСТАВЬ НАЗВАНИЕ СВОЕГО ПОЛЯ С HTML:
        //        var html = Convert.ToString(view.GetFocusedRowCellValue("combinedPszNom"));
        //        _browser.DocumentText = html ?? string.Empty;
        //    };

        //    // 3) Колонка "Подробнее" (создаём, если нет)
        //    var colMore = view.Columns["More"] as LayoutViewColumn;
        //    if (colMore == null)
        //    {
        //        colMore = view.Columns.AddField("More") as LayoutViewColumn; // AddField вернёт LayoutViewColumn
        //        colMore.Caption = "Подробнее";
        //        colMore.Visible = true;
        //        colMore.OptionsColumn.AllowEdit = true;
        //        colMore.OptionsColumn.ReadOnly = false;

        //        // Размещаем элемент в карточке и фиксируем высоту "строки" под кнопку/поле
        //        var field = colMore.LayoutViewField;
        //        field.TextVisible = false;
        //        field.SizeConstraintsType = SizeConstraintsType.Custom;
        //        field.MinSize = new Size(0, 28);
        //        field.MaxSize = new Size(int.MaxValue, 28);
        //    }

        //    // 4) Назначаем редактор на колонку
        //    colMore.ColumnEdit = _repoPopup;

        //    // (опционально) показывать редактор по клику
        //    view.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
        //}
        private async void KnittingMachinesLoading_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);
                //MessageBox.Show("1");
                //await LoadKnitMachineAreaListDataAsync();
                await LoadKnitMachineClassListDataAsync();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingMachinesLoading");
            }
        }

        //private async Task LoadKnitMachineAreaListDataAsync()
        //{
        //    try
        //    {
        //        _knitMachineAreaListViewBindingSource.Clear();
        //        _knitMachineAreaListViewBindingSource.ResetBindings(false);
        //        knitMachineAreaListViewData = await _vyazService.GetKnitMachineAreaList();
        //        //_vyazPlanViewBindingList = vyazPlanViewData;
        //        //_vyazPlanViewBindingList = await _vyazService.GetVyazPlanView();
        //        if (knitMachineAreaListViewData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные knitMachineArea_view", "LoadKnitMachineAreaListDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                _currentKnitMachineAreaListViewData = knitMachineAreaListViewData;                // Обновляем текущую модель
        //                _knitMachineAreaListViewBindingSource.DataSource = _currentKnitMachineAreaListViewData; // Привязываем данные к форме
        //            });

        //            await _logger.LogEventAsync($"Данные knitMachineArea_view успешно загружены", "LoadKnitMachineAreaListDataAsync");
        //            //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
        //            //_knitMachineAreaListViewBindingList.Add(knitMachineAreaListViewData[0]);
        //            _knitMachineAreaListViewBindingSource.ResetBindings(false);
        //            сomboBoxKnitMachineClassList.SelectedIndex = -1;
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные knitMachineArea_view", "LoadKnitMachineAreaListDataAsync");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных knitMachineArea_view");
        //    }
        //}
        private async Task LoadKnitMachineClassListDataAsync()
        {
            try
            {
                _knitMachineClassListBindingSource.Clear();
                _knitMachineClassListBindingSource.ResetBindings(false);
                knitMachineClassListData = await _vyazService.GetKnitMachineClassList();
                //_vyazPlanViewBindingList = vyazPlanViewData;
                //_vyazPlanViewBindingList = await _vyazService.GetVyazPlanView();
                if (knitMachineClassListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные matrix_class", "LoadKnitMachineClassListDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentKnitMachineClassListData = knitMachineClassListData;                // Обновляем текущую модель
                        _knitMachineClassListBindingSource.DataSource = _currentKnitMachineClassListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные matrix_class успешно загружены", "LoadKnitMachineClassListDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    //_knitMachineAreaListViewBindingList.Add(knitMachineAreaListViewData[0]);
                    _knitMachineClassListBindingSource.ResetBindings(false);
                    //сomboBoxKnitMachineClassList.SelectedIndex = -1;
                    сomboBoxKnitMachineClassList.DataSource = _knitMachineClassListBindingSource;
                    сomboBoxKnitMachineClassList.ValueMember = "id_class";
                    сomboBoxKnitMachineClassList.DisplayMember = "caption";
                    сomboBoxKnitMachineClassList.SelectedValue = xClassID;
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные matrix_class", "LoadKnitMachineClassListDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных matrix_class");
            }
        }
        private async Task LoadKnitMachineLoadInfoByClassIDDataAsync(int classID)
        {
            try
            {
                _knitMachineLoadInfoBindingSource.Clear();
                _knitMachineLoadInfoBindingSource.ResetBindings(false);
                var knitMachineLoadInfoData = await _vyazService.GetKnitMachineLoadInfoByClassID(classID);
                if (knitMachineLoadInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные KnitMachineLoadInfo", "LoadKnitMachineLoadInfoByClassIDDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentKnitMachineLoadInfoData = knitMachineLoadInfoData;                // Обновляем текущую модель
                        _knitMachineLoadInfoBindingSource.DataSource = _currentKnitMachineLoadInfoData; // Привязываем данные к форме
                        _knitMachineLoadInfoBindingSource.Sort = "kmlNumber, yearMonth";
                    });



                    await _logger.LogEventAsync($"Данные KnitMachineLoadInfo успешно загружены", "LoadKnitMachineLoadInfoByClassIDDataAsync");
                    _knitMachineLoadInfoBindingSource.ResetBindings(false);

                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные KnitMachineLoadInfo", "LoadKnitMachineLoadInfoByClassIDDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных LoadKnitMachineLoadInfoByClassIDDataAsync");
            }
        }
        private async Task LoadKnitMachineListByClassIDDataAsync(int classID)
        {
            try
            {
                _knitMachineListBindingSource.Clear();
                _knitMachineListBindingSource.ResetBindings(false);
                var knitMachineListData = await _vyazService.GetKnitMachineListByClassID(classID);
                if (knitMachineListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные KnitMachineList", "LoadKnitMachineListByClassIDDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentKnitMachineListData = knitMachineListData;                // Обновляем текущую модель
                        _knitMachineListBindingSource.DataSource = _currentKnitMachineListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные KnitMachineList успешно загружены", "LoadKnitMachineListByClassIDDataAsync");
                    _knitMachineListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные KnitMachineList", "LoadKnitMachineListByClassIDDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных LoadKnitMachineListByClassIDDataAsync");
            }
        }
        private async void сomboBoxKnitMachineClassList_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private async void сomboBoxKnitMachineClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var selectedRow = _knitMachineAreaListViewBindingSource.Current as KnitMachineAreaListView;
            int selectedClassID = Convert.ToInt32(сomboBoxKnitMachineClassList.SelectedValue);
            if (selectedClassID != 0)
            {
                Task knitMachineLoadInfoTask = LoadKnitMachineLoadInfoByClassIDDataAsync(selectedClassID);
                Task knitMachineListTask = LoadKnitMachineListByClassIDDataAsync(selectedClassID);
                await Task.WhenAll(knitMachineLoadInfoTask, knitMachineListTask);

                //ConfigureCardView();
                ConfigureLayoutView();
                //gridViewKnitMachineLoadInfoCards.CardWidth = 185;
                //gridViewKnitMachineLoadInfoCards.CardCaptionFormat = " ";

                ////// Скрываем ненужные колонки
                ////foreach (GridColumn column in gridViewKnitMachineLoadInfoCards.Columns)
                ////{
                ////    column.Visible = column.FieldName == "yearMonth" ||
                ////                   column.FieldName == "combinedPszNom" /*||
                ////                   column.FieldName == "kmlNumber"*/;
                ////}

                //gridViewKnitMachineLoadInfoCards.CustomDrawCardCaption += (sender, e) =>
                //{
                //    // Получаем значение kmlNumber для текущей карточки
                //    object kmlNumber = gridViewKnitMachineLoadInfoCards.GetRowCellValue(e.RowHandle, "kmlNumber");

                //    // Рисуем стандартный заголовок
                //    e.DefaultDraw();

                //    // Добавляем текст поверх
                //    e.Graphics.DrawString(kmlNumber?.ToString(), e.Appearance.Font,
                //                        Brushes.Black, e.Bounds,
                //                        new StringFormat
                //                        {
                //                            Alignment = StringAlignment.Center,
                //                            LineAlignment = StringAlignment.Center
                //                        });
                //};


            }
            else
            {
                //comboBoxKnitMachineList.SelectedValue = -1;
            }
        }
        // Настройка CardView
        //private void ConfigureCardView()
        //{
        //    //// 1. Установка CardView как основного вида
        //    //gridControlKnitMachineLoadInfo.MainView = gridViewKnitMachineLoadInfoCards;
        //    //gridControlKnitMachineLoadInfo.ForceInitialize();

        //    // 2. Настройка базовых параметров
        //    gridViewKnitMachineLoadInfoCards.BeginUpdate();
        //    try
        //    {
        //        // Настройка ширины карточки
        //        gridViewKnitMachineLoadInfoCards.CardWidth = 185;
        //        gridViewKnitMachineLoadInfoCards.CardCaptionFormat = " ";

        //        // Настройка отображаемых полей
        //        //gridViewKnitMachineLoadInfoCards.PopulateColumns();

        //        //// Скрываем ненужные колонки
        //        //foreach (GridColumn column in gridViewKnitMachineLoadInfoCards.Columns)
        //        //{
        //        //    column.Visible = column.FieldName == "yearMonth" ||
        //        //                   column.FieldName == "combinedPszNom" /*||
        //        //                   column.FieldName == "kmlNumber"*/;
        //        //}

        //        // 3. Настройка заголовка через CustomDraw
        //        gridViewKnitMachineLoadInfoCards.CustomDrawCardCaption += (sender, e) =>
        //        {
        //            // Получаем значение kmlNumber для текущей карточки
        //            object kmlNumber = gridViewKnitMachineLoadInfoCards.GetRowCellValue(e.RowHandle, "kmlNumber");

        //            // Рисуем стандартный заголовок
        //            e.DefaultDraw();

        //            // Добавляем текст поверх
        //            e.Graphics.DrawString(kmlNumber?.ToString(), e.Appearance.Font,
        //                                Brushes.Black, e.Bounds,
        //                                new StringFormat
        //                                {
        //                                    Alignment = StringAlignment.Center,
        //                                    LineAlignment = StringAlignment.Center
        //                                });
        //        };

        //        //// 4. Настройка внешнего вида
        //        if (_knitMachineListBindingSource != null)
        //        {
        //            gridViewKnitMachineLoadInfoCards.MaximumCardColumns = _knitMachineListBindingSource.Count;
        //            gridViewKnitMachineLoadInfoCards.MaximumCardRows = _knitMachineLoadInfoBindingSource.Count / _knitMachineListBindingSource.Count;
        //        }
        //        else
        //        {
        //            gridViewKnitMachineLoadInfoCards.MaximumCardColumns = -1;
        //            gridViewKnitMachineLoadInfoCards.MaximumCardRows = -1;
        //        }

        //        //------------------------------
        //        //gridViewKnitMachineLoadInfoCards.CustomDrawCell += (s, e) =>
        //        //{
        //        //    if (e.Column == gcCertGrupmen_name1)
        //        //    {
        //        //        string rtf = e.CellValue as string;

        //        //        if (!string.IsNullOrEmpty(rtf))
        //        //        {
        //        //            using (var rtfControl = new DevExpress.XtraRichEdit.RichEditControl())
        //        //            {
        //        //                rtfControl.RtfText = rtf;

        //        //                rtfControl.DocumentLayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;

        //        //                rtfControl.Appearance.Text.Options.UseTextOptions = true;

        //        //                Bitmap bmp = new Bitmap(e.Bounds.Width, e.Bounds.Height);
        //        //                rtfControl.ExportToImage(bmp, new DevExpress.XtraRichEdit.API.Native.Range(rtfControl.Document.Range.Start, rtfControl.Document.Range.End));
        //        //                e.Cache.DrawImage(bmp, e.Bounds);
        //        //                e.Handled = true;
        //        //            }
        //        //        }
        //        //    }
        //        //};

        //        //gridViewKnitMachineLoadInfoCards.Appearance.Card.BackColor = Color.White;
        //        //gridViewKnitMachineLoadInfoCards.Appearance.Card.BorderColor = Color.LightGray;
        //        ////gridViewKnitMachineLoadInfoCards.OptionsView.ShowCardCaption = false;
        //        //gridViewKnitMachineLoadInfoCards.Appearance.CardCaption.BackColor = Color.SteelBlue;
        //        //gridViewKnitMachineLoadInfoCards.Appearance.CardCaption.ForeColor = Color.Black;
        //    }
        //    finally
        //    {
        //        gridViewKnitMachineLoadInfoCards.EndUpdate();
        //    }
        //    //richEditControl1.RtfText = @"{\rtf1\ansi\deff0 {\fonttbl{\f0 Arial;}} \f0\fs24 Привет мир!}";
        //    //richEditControl1.RtfText = gridViewKnitMachineLoadInfoCards.GetRowCellValue(gridViewKnitMachineLoadInfoCards.FocusedRowHandle, "combinedPszNom").ToString();
        //    // 5. Обработчик клика по карточке
        //    //gridViewKnitMachineLoadInfoCards.DoubleClick += (s, e) =>
        //    //{
        //    //    CardHitInfo hitInfo = gridViewKnitMachineLoadInfoCards.CalcHitInfo(e.Location);
        //    //    if (hitInfo.InCard)
        //    //    {
        //    //        int rowHandle = hitInfo.RowHandle;
        //    //        // Ваш код
        //    //    }
        //    //    //if (e.Button == MouseButtons.Left)
        //    //    //{
        //    //    //    CardHitInfo hitInfo = gridViewKnitMachineLoadInfoCards.CalcHitInfo(e.Location);
        //    //    //    if (hitInfo.InCardCaption)
        //    //    //    {
        //    //    //        object kmlNumber = gridViewKnitMachineLoadInfoCards.GetRowCellValue(hitInfo.RowHandle, "kmlNumber");
        //    //    //        MessageBox.Show($"Выбрана карточка: {kmlNumber}", "Информация");
        //    //    //    }
        //    //    //}
        //    //};
        //    // Двойной клик по карточке (правильная реализация)
        //    gridViewKnitMachineLoadInfoCards.MouseDown += (s, e) =>
        //    {
        //        if (e.Button == MouseButtons.Left && e.Clicks == 2) // Проверяем двойной клик
        //        {
        //            // Получаем информацию о позиции клика
        //            CardHitInfo hitInfo = gridViewKnitMachineLoadInfoCards.CalcHitInfo(e.Location);

        //            if (hitInfo.InCard)
        //            {
        //                //var selectedRow = _knitMachineLoadInfoBindingSource.Current as KnitMachineLoadAllInfo;
        //                ////string kmlNumber = gridViewKnitMachineLoadInfoCards.GetRowCellValue(hitInfo.RowHandle, "kmlNumber")?.ToString();
        //                ////string kmlID = gridViewKnitMachineLoadInfoCards.GetRowCellValue(hitInfo.RowHandle, "kmlID")?.ToString();
        //                //MessageBox.Show($"Выбрана В/М: {selectedRow.kmlNumber}, ID: {selectedRow.kmlID}");
        //                int xKmlID = 0;
        //                string xKmlNumber = "";
        //                var selectedRow = _knitMachineLoadInfoBindingSource.Current as KnitMachineLoadAllInfo;
        //                if (selectedRow != null && selectedRow.kmlID != 0)
        //                {
        //                    xKmlID = selectedRow.kmlID;
        //                    xKmlNumber = selectedRow.kmlNumber;
        //                }
        //                else
        //                {
        //                    xKmlID = 0;
        //                    xKmlNumber = "";
        //                }
        //                KnittingMachinesUnitLoading KML = new KnittingMachinesUnitLoading(xKmlID, xKmlNumber);

        //                DialogResult result = KML.ShowDialog();
        //                // Обработка результата, возвращенного модальной формой
        //                if (result == DialogResult.OK)
        //                {
        //                    // Действия при успешном завершении работы модальной формы
        //                    //MessageBox.Show("OK");
        //                }
        //                else
        //                {
        //                    // Действия при отмене или другом результате
        //                    //MessageBox.Show("Cancel");
        //                }
        //            }
        //        }
        //    };
        //}

        //private void ConfigureLayoutView()
        //{
        //    gridViewKnitMachineLoadLayoutView.BeginUpdate();
        //    try
        //    {
        //        if (_knitMachineLoadInfoBindingSource.Count != 0)
        //        {
        //            gridViewKnitMachineLoadLayoutView.CardMinSize = new System.Drawing.Size(gridViewKnitMachineLoadLayoutView.CardMinSize.Width, gridControlKnitMachineLoadInfo.Size.Height / (_knitMachineLoadInfoBindingSource.Count / _knitMachineListBindingSource.Count + 1));
        //        }
        //        //RecalcCardHeight();

        //        //---------------------------------------------
        //        gridViewKnitMachineLoadLayoutView.BeginSort();
        //        gridViewKnitMachineLoadLayoutView.ClearSorting();

        //        gridViewKnitMachineLoadLayoutView.SortInfo.AddRange(new[] {
        //                new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewKnitMachineLoadLayoutView.Columns["kmlNumber"], DevExpress.Data.ColumnSortOrder.Ascending),
        //                new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewKnitMachineLoadLayoutView.Columns["yearNumber"], DevExpress.Data.ColumnSortOrder.Ascending),
        //                new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewKnitMachineLoadLayoutView.Columns["monthNumber"], DevExpress.Data.ColumnSortOrder.Ascending)
        //            });
        //        gridViewKnitMachineLoadLayoutView.EndSort();
        //        //gridViewKnitMachineLoadLayoutView.Refresh();
        //        //gridViewKnitMachineLoadLayoutView.SelectRow(0);
        //        gridViewKnitMachineLoadLayoutView.FocusedRowHandle = 0;
        //        //---------------------------------------------
        //    }
        //    finally
        //    {
        //        gridViewKnitMachineLoadLayoutView.EndUpdate();
        //    }
        //}

        //private void ConfigureLayoutView()
        //{
        //    var view = gridViewKnitMachineLoadLayoutView; // это LayoutView
        //    view.BeginUpdate();
        //    try
        //    {
        //        // 0) Репозиторий попапа с браузером (один раз)
        //        EnsureHtmlPopup(view);

        //        // 1) Назначаем попап-редактор на целевую колонку и фиксируем высоту её layout-поля
        //        var lcol = view.Columns[_htmlColumnName] as LayoutViewColumn
        //                   ?? throw new InvalidOperationException($"Колонка '{_htmlColumnName}' не найдена или это не LayoutViewColumn.");
        //        lcol.OptionsColumn.AllowEdit = true;
        //        lcol.OptionsColumn.ReadOnly = false;
        //        lcol.ColumnEdit = _repoPopup;

        //        var field = lcol.LayoutViewField;
        //        field.SizeConstraintsType = SizeConstraintsType.Custom;
        //        field.MinSize = new Size(field.MinSize.Width, PREVIEW_HEIGHT);
        //        field.MaxSize = new Size(int.MaxValue, PREVIEW_HEIGHT);

        //        // 2) Отключаем авто-рост редакторов, чтобы карточки не «распирало»
        //        FixEditorsAutoHeight(view);

        //        // 3) Высота карточек: расчёт от ваших данных (или fallback)
        //        RecalcCardHeight();

        //        // 4) Сортировка и прочее — как у вас
        //        view.BeginSort();
        //        view.ClearSorting();
        //        view.SortInfo.AddRange(new[] {
        //                new GridColumnSortInfo(view.Columns["kmlNumber"],  DevExpress.Data.ColumnSortOrder.Ascending),
        //                new GridColumnSortInfo(view.Columns["yearNumber"], DevExpress.Data.ColumnSortOrder.Ascending),
        //                new GridColumnSortInfo(view.Columns["monthNumber"],DevExpress.Data.ColumnSortOrder.Ascending)
        //            });
        //        view.EndSort();

        //        view.FocusedRowHandle = 0;

        //        // 5) Единоразовая подписка на события
        //        if (!_configureOnce)
        //        {
        //            // обновлять высоту при ресайзе грида
        //            gridControlKnitMachineLoadInfo.SizeChanged += (s, e) => RecalcCardHeight();

        //            // красивое превью (чистим HTML для отображения в ячейке)
        //            view.CustomColumnDisplayText += (s, e) =>
        //            {
        //                if (e.Column == lcol && e.Value is string html)
        //                    e.DisplayText = StripHtml(html, 200);
        //            };

        //            // открывать редактор/попап по клику
        //            view.OptionsBehavior.EditorShowMode = EditorShowMode.Click;

        //            _configureOnce = true;
        //        }
        //    }
        //    finally
        //    {
        //        view.EndUpdate();
        //    }
        //}
        //private void View_CustomDrawCardFieldValue_RenderHtmlPreview(object sender, LayoutViewCustomDrawCardFieldValueEventArgs e)
        //{
        //    if (e.Column == null || e.Column.FieldName != _htmlColumnName) return;

        //    // фон/рамку — стандартом
        //    e.DefaultDraw();

        //    // текст HTML
        //    string html = Convert.ToString(e.CellValue) ?? string.Empty;
        //    var segments = ParseHtmlSegments(html); // разбиваем на стилизованные куски

        //    // базовый шрифт/кисть
        //    var baseFont = e.Appearance.Font ?? Control.DefaultFont;
        //    var normalFont = baseFont;
        //    var boldFont = new Font(baseFont, FontStyle.Bold);

        //    Rectangle bounds = e.Bounds;
        //    bounds.Inflate(-4, -4); // небольшие отступы

        //    // раскладка по строкам с переносами
        //    using (var g = e.Cache.GetGraphics())
        //    {
        //        DrawSegmentsWrapped(g, segments, normalFont, boldFont, e.Appearance.ForeColor, bounds);
        //    }

        //    e.Handled = true;
        //}

        //// --- HTML -> сегменты (текст + стиль) ---
        //private sealed class Seg
        //{
        //    public string Text;
        //    public bool Bold;
        //    public Color? Color;
        //    public bool NewLine;
        //}

        //private static List<Seg> ParseHtmlSegments(string html)
        //{
        //    var s = html ?? string.Empty;

        //    // переносы
        //    s = Regex.Replace(s, @"<\s*br\s*/?>", "\n", RegexOptions.IgnoreCase);
        //    s = Regex.Replace(s, @"</?(p|div)[^>]*>", "\n", RegexOptions.IgnoreCase);

        //    // жирный
        //    s = Regex.Replace(s, @"<\s*(strong|b)\s*>", "[b]", RegexOptions.IgnoreCase);
        //    s = Regex.Replace(s, @"<\s*/\s*(strong|b)\s*>", "[/b]", RegexOptions.IgnoreCase);

        //    // цвет через <font color=...>
        //    s = Regex.Replace(s, @"<\s*font[^>]*color\s*=\s*['""]?([#0-9a-zA-Z]+)['""]?[^>]*>", "[color=$1]", RegexOptions.IgnoreCase);
        //    s = Regex.Replace(s, @"<\s*/\s*font\s*>", "[/color]", RegexOptions.IgnoreCase);

        //    // цвет через <span style="color:...">
        //    s = Regex.Replace(s, @"<\s*span[^>]*style\s*=\s*['""][^'""]*color\s*:\s*([#0-9a-zA-Z]+)[^'""]*['""][^>]*>", "[color=$1]", RegexOptions.IgnoreCase);
        //    s = Regex.Replace(s, @"<\s*/\s*span\s*>", "[/color]", RegexOptions.IgnoreCase);

        //    // убрать прочие теги
        //    s = Regex.Replace(s, "<.*?>", string.Empty);

        //    // декодировать сущности и нормализовать переносы
        //    s = WebUtility.HtmlDecode(s).Replace("\r\n", "\n");

        //    var list = new List<Seg>();
        //    var sb = new StringBuilder();
        //    bool bold = false;
        //    Color? color = null;

        //    for (int i = 0; i < s.Length;)
        //    {
        //        if (s[i] == '[')
        //        {
        //            // попытка распознать маркер
        //            int end = s.IndexOf(']', i + 1);
        //            if (end > i)
        //            {
        //                string tag = s.Substring(i + 1, end - i - 1);
        //                Flush();
        //                if (tag.Equals("b", StringComparison.OrdinalIgnoreCase)) bold = true;
        //                else if (tag.Equals("/b", StringComparison.OrdinalIgnoreCase)) bold = false;
        //                else if (tag.StartsWith("color=", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    color = ParseColor(tag.Substring(6));
        //                }
        //                else if (tag.Equals("/color", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    color = null;
        //                }
        //                i = end + 1;
        //                continue;
        //            }
        //        }

        //        char ch = s[i++];
        //        if (ch == '\n')
        //        {
        //            Flush();
        //            list.Add(new Seg { NewLine = true });
        //        }
        //        else
        //        {
        //            sb.Append(ch);
        //        }

        //        void Flush()
        //        {
        //            if (sb.Length == 0) return;
        //            list.Add(new Seg { Text = sb.ToString(), Bold = bold, Color = color });
        //            sb.Clear();
        //        }
        //    }
        //    if (sb.Length > 0) list.Add(new Seg { Text = sb.ToString(), Bold = bold, Color = color });

        //    return list;
        //}

        //private static Color? ParseColor(string raw)
        //{
        //    raw = raw?.Trim().Trim('\'', '"');
        //    if (string.IsNullOrEmpty(raw)) return null;
        //    // имена цветов
        //    try
        //    {
        //        if (!raw.StartsWith("#"))
        //            return Color.FromName(raw);
        //        // #RRGGBB
        //        if (raw.Length == 7)
        //            return ColorTranslator.FromHtml(raw);
        //    }
        //    catch { }
        //    return null;
        //}

        //// --- рисуем с переносами ---
        //private static void DrawSegmentsWrapped(Graphics g, List<Seg> segs, Font normal, Font bold, Color defaultColor, Rectangle bounds)
        //{
        //    int x = bounds.X;
        //    int y = bounds.Y;
        //    int maxX = bounds.Right;
        //    int lineHeight = 0;

        //    void NewLine()
        //    {
        //        y += Math.Max(lineHeight, normal.Height);
        //        x = bounds.X;
        //        lineHeight = 0;
        //    }

        //    foreach (var seg in segs)
        //    {
        //        if (seg.NewLine)
        //        {
        //            NewLine();
        //            if (y >= bounds.Bottom) break;
        //            continue;
        //        }

        //        string text = seg.Text;
        //        if (string.IsNullOrEmpty(text)) continue;

        //        var font = seg.Bold ? bold : normal;
        //        var brush = new SolidBrush(seg.Color ?? defaultColor);

        //        // Разбиваем по словам, чтобы переносить
        //        foreach (var word in Regex.Split(text, @"(\s+)"))
        //        {
        //            if (string.IsNullOrEmpty(word)) continue;

        //            var sz = TextRenderer.MeasureText(g, word, font, new Size(int.MaxValue, int.MaxValue),
        //                TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

        //            bool overflow = x + sz.Width > maxX;

        //            if (overflow && x > bounds.X && !IsWhitespace(word))
        //            {
        //                // перенос на новую строку
        //                NewLine();
        //                if (y >= bounds.Bottom) { brush.Dispose(); return; }
        //            }

        //            // обрезка, если по высоте не влезаем
        //            if (y + Math.Max(lineHeight, sz.Height) > bounds.Bottom)
        //            {
        //                // поставим многоточие в конце строки
        //                TextRenderer.DrawText(g, "…", font, new Point(Math.Min(x, maxX - sz.Width), y),
        //                    seg.Color ?? defaultColor,
        //                    TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
        //                brush.Dispose();
        //                return;
        //            }

        //            TextRenderer.DrawText(g, word, font, new Point(x, y),
        //                seg.Color ?? defaultColor,
        //                TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

        //            x += sz.Width;
        //            lineHeight = Math.Max(lineHeight, sz.Height);
        //        }

        //        brush.Dispose();
        //    }
        //}

        // === HTML -> сегменты (жирный/цвет/переносы) ===
        private sealed class Seg { public string Text; public bool Bold; public Color? Color; public bool NewLine; }

        private static List<Seg> ParseHtmlSegments(string html)
        {
            var s = html ?? "";
            s = Regex.Replace(s, @"<\s*br\s*/?>", "\n", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"</?(p|div)[^>]*>", "\n", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*(strong|b)\s*>", "[b]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*/\s*(strong|b)\s*>", "[/b]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*font[^>]*color\s*=\s*['""]?([#0-9a-zA-Z]+)['""]?[^>]*>", "[color=$1]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*/\s*font\s*>", "[/color]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*span[^>]*style\s*=\s*['""][^'""]*color\s*:\s*([#0-9a-zA-Z]+)[^'""]*['""][^>]*>", "[color=$1]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<\s*/\s*span\s*>", "[/color]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, "<.*?>", "");
            s = WebUtility.HtmlDecode(s).Replace("\r\n", "\n");

            var list = new List<Seg>();
            var sb = new StringBuilder();
            bool bold = false; Color? col = null;

            for (int i = 0; i < s.Length;)
            {
                if (s[i] == '[')
                {
                    int j = s.IndexOf(']', i + 1);
                    if (j > i)
                    {
                        Flush();
                        var tag = s.Substring(i + 1, j - i - 1);
                        if (tag.Equals("b", StringComparison.OrdinalIgnoreCase)) bold = true;
                        else if (tag.Equals("/b", StringComparison.OrdinalIgnoreCase)) bold = false;
                        else if (tag.StartsWith("color=", StringComparison.OrdinalIgnoreCase)) col = ParseColor(tag.Substring(6));
                        else if (tag.Equals("/color", StringComparison.OrdinalIgnoreCase)) col = null;
                        i = j + 1; continue;
                    }
                }
                char ch = s[i++];
                if (ch == '\n') { Flush(); list.Add(new Seg { NewLine = true }); }
                else sb.Append(ch);
                void Flush() { if (sb.Length > 0) { list.Add(new Seg { Text = sb.ToString(), Bold = bold, Color = col }); sb.Clear(); } }
            }
            if (sb.Length > 0) list.Add(new Seg { Text = sb.ToString(), Bold = bold, Color = col });
            return list;
        }

        private static Color? ParseColor(string raw)
        {
            raw = raw?.Trim().Trim('"', '\''); if (string.IsNullOrEmpty(raw)) return null;
            try { return raw.StartsWith("#") ? ColorTranslator.FromHtml(raw) : Color.FromName(raw); } catch { return null; }
        }

        // === Разбор на строки с переносами + отрисовка ===
        private static void DrawSegmentsWrapped(Graphics g, List<Seg> segs, Font normal, Font bold, Color defColor, Rectangle bounds)
        {
            int x = bounds.X, y = bounds.Y, maxX = bounds.Right, lineH = 0;

            void NewLine() { y += Math.Max(lineH, normal.Height); x = bounds.X; lineH = 0; }

            foreach (var seg in segs)
            {
                if (seg.NewLine) { NewLine(); if (y >= bounds.Bottom) break; continue; }
                if (string.IsNullOrEmpty(seg.Text)) continue;

                var font = seg.Bold ? bold : normal;
                var color = seg.Color ?? defColor;

                foreach (var token in Regex.Split(seg.Text, @"(\s+)"))
                {
                    if (token.Length == 0) continue;

                    var sz = TextRenderer.MeasureText(g, token, font, new Size(int.MaxValue, int.MaxValue),
                        TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                    bool overflow = x + sz.Width > maxX;
                    if (overflow && x > bounds.X && !string.IsNullOrWhiteSpace(token))
                    {
                        NewLine(); if (y >= bounds.Bottom) return;
                    }
                    if (y + Math.Max(lineH, sz.Height) > bounds.Bottom)
                    {
                        TextRenderer.DrawText(g, "…", font, new Point(Math.Min(x, maxX - sz.Width), y), color,
                            TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                        return;
                    }

                    TextRenderer.DrawText(g, token, font, new Point(x, y), color,
                        TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                    x += sz.Width;
                    lineH = Math.Max(lineH, sz.Height);
                }
            }
        }
        private static bool IsWhitespace(string s) => string.IsNullOrWhiteSpace(s);

        private void View_CustomColumnDisplayText_NoLongerUsed(object s, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e) { }

        private void ConfigureLayoutView()
        {
            var view = gridViewKnitMachineLoadLayoutView;
            view.BeginUpdate();
            try
            {
                //// фиксируем «окошко» превью (высоту поля в карточке):
                var lcol = (LayoutViewColumn)view.Columns[_htmlColumnName];
                //var field = lcol.LayoutViewField;
                //field.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                //field.MinSize = new Size(field.MinSize.Width, 120);
                //field.MaxSize = new Size(int.MaxValue, 120);

                //// включим перенос (на всякий случай)
                //lcol.AppearanceCell.Options.UseTextOptions = true;
                //lcol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;

                //// убираем прошлые преобразования текста (если делали)
                //view.CustomColumnDisplayText -= View_CustomColumnDisplayText_NoLongerUsed;

                ////вешаем кастомный рендер превью
                //if (!_previewWired)
                //{
                //    view.CustomDrawCardFieldValue += View_CustomDrawCardFieldValue_RenderHtmlPreview;
                //    // попап уже есть у вас — только обёртку для цветов добавьте (ниже)
                //    _previewWired = true;
                //}
                EnsureHtmlPopup(); // создаём _popup/_browser/_popupHost/_repoPopup (см. ниже)

                //// целевая колонка
                //var lcol = view.Columns[_htmlColumnName] as LayoutViewColumn
                //           ?? throw new InvalidOperationException($"Колонка '{_htmlColumnName}' не найдена.");

                // --- МНОГОСТРОЧНОЕ ПРЕВЬЮ В ЯЧЕЙКЕ ---
                if (_repoPreview == null)
                {
                    _repoPreview = new RepositoryItemMemoEdit
                    {
                        AutoHeight = false,
                        WordWrap = true,
                        ScrollBars = ScrollBars.Vertical
                    };
                    gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPreview);
                }
                lcol.ColumnEdit = _repoPreview;                         // <— заменили PopupContainerEdit на MemoEdit
                lcol.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                //if (_repoPreviewHyper == null)
                //{
                //    _repoPreviewHyper = new RepositoryItemHyperTextEdit
                //    {
                //        ReadOnly = true,
                //        AutoHeight = false
                //        // Если есть свойство WordWrap/AllowHtmlDraw — оставьте по умолчанию, HyperText сам переносит строки.
                //    };
                //    gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPreviewHyper);
                //}

                //var lcol = (LayoutViewColumn)gridViewKnitMachineLoadLayoutView.Columns[_htmlColumnName];
                //lcol.ColumnEdit = _repoPreviewHyper;                         // <-- теперь рендерится форматированный текст
                //lcol.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                // фиксируем высоту layout-поля под превью
                var field = lcol.LayoutViewField;
                field.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                field.MinSize = new Size(field.MinSize.Width, 120);
                field.MaxSize = new Size(int.MaxValue, 120);

                // — остальной твой код сортировок/фокуса/пересчёта высоты карточек —
                RecalcCardHeight(); // твой расчёт; оставь как есть
                view.BeginSort();
                view.ClearSorting();
                view.SortInfo.AddRange(new[] {
                    new GridColumnSortInfo(view.Columns["kmlNumber"],  DevExpress.Data.ColumnSortOrder.Ascending),
                    new GridColumnSortInfo(view.Columns["yearNumber"], DevExpress.Data.ColumnSortOrder.Ascending),
                    new GridColumnSortInfo(view.Columns["monthNumber"],DevExpress.Data.ColumnSortOrder.Ascending)
                });
                view.EndSort();
                view.FocusedRowHandle = 0;

                if (!_configureOnce)
                {
                    gridControlKnitMachineLoadInfo.SizeChanged += (s, e) => RecalcCardHeight();

                    ////превью без HTML - тегов
                    //view.CustomColumnDisplayText += (s, e) =>
                    //{
                    //    if (e.Column == lcol && e.Value is string html)
                    //        e.DisplayText = StripHtml(html, 200);
                    //};
                    gridViewKnitMachineLoadLayoutView.CustomColumnDisplayText += (s, e) =>
                    {
                        if (e.Column.FieldName == _htmlColumnName && e.Value is string html)
                            e.DisplayText = HtmlToDxMarkup(html);
                    };

                    // открываем ПОПАП с HTML по клику на ячейку этой колонки
                    view.MouseDown += View_MouseDownOpenHtml;

                    _configureOnce = true;
                }
            }
            finally { view.EndUpdate(); }
        }


        // --- ВСПОМОГАТЕЛЬНОЕ ---

        //private void EnsureHtmlPopup(LayoutView view)
        //{
        //    if (_popup != null) return;

        //    _popup = new PopupContainerControl { Parent = this, Size = new Size(700, 500) };
        //    _browser = new WebBrowser { Dock = DockStyle.Fill };
        //    _popup.Controls.Add(_browser);

        //    _repoPopup = new RepositoryItemPopupContainerEdit
        //    {
        //        AutoHeight = false,
        //        PopupControl = _popup,
        //        ShowPopupCloseButton = true,
        //        TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor,
        //        NullText = "Открыть…"
        //    };
        //    gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPopup);

        //    _repoPopup.QueryPopUp += (s, e) =>
        //    {
        //        var html = Convert.ToString(gridViewKnitMachineLoadLayoutView.GetFocusedRowCellValue(_htmlColumnName));
        //        _browser.DocumentText = html ?? string.Empty;
        //    };
        //}

        private void EnsureHtmlPopup()
        {
            if (_popup != null) return;

            _popup = new PopupContainerControl { Parent = this, Size = new Size(700, 500) };
            _browser = new WebBrowser { Dock = DockStyle.Fill };
            _popup.Controls.Add(_browser);

            _repoPopup = new RepositoryItemPopupContainerEdit
            {
                AutoHeight = false,
                PopupControl = _popup, // у репозитория свойство есть напрямую
                ShowPopupCloseButton = true,
                TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            };
            gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPopup);

            // скрытый хост для программного показа попапа
            _popupHost = new PopupContainerEdit
            {
                Parent = this,
                Visible = false
            };
            _popupHost.Properties.PopupControl = _popup; // <-- ключевая строка
        }

        private void View_MouseDownOpenHtml(object sender, MouseEventArgs e)
        {
            var view = (LayoutView)sender;
            var hit = view.CalcHitInfo(e.Location);
            if (!hit.InField || hit.Column == null || hit.Column.FieldName != _htmlColumnName) return;

            // берём HTML и показываем попап рядом с курсором
            var html = Convert.ToString(view.GetRowCellValue(hit.RowHandle, hit.Column)) ?? string.Empty;
            _browser.DocumentText = html;

            // позиционируем хост в точку клика и открываем попап
            var screenPt = gridControlKnitMachineLoadInfo.PointToScreen(e.Location);
            _popupHost.Location = this.PointToClient(screenPt);
            _popupHost.ShowPopup();
        }


        private void FixEditorsAutoHeight(LayoutView view)
        {
            foreach (LayoutViewColumn c in view.Columns)
            {
                var ri = c.ColumnEdit;
                if (ri == null) continue;

                ri.AutoHeight = false;

                if (ri is RepositoryItemMemoEdit memo)
                {
                    memo.WordWrap = true;
                    memo.ScrollBars = ScrollBars.Vertical;
                }
            }
        }

        private void RecalcCardHeight()
        {
            var view = gridViewKnitMachineLoadLayoutView;

            int h;
            // ваш исходный расчёт (на базе counts)
            if (_knitMachineLoadInfoBindingSource != null &&
                _knitMachineListBindingSource != null &&
                _knitMachineLoadInfoBindingSource.Count != 0)
            {
                int perMachine = (_knitMachineLoadInfoBindingSource.Count / Math.Max(1, _knitMachineListBindingSource.Count)) + 1;
                h = gridControlKnitMachineLoadInfo.ClientSize.Height / Math.Max(1, perMachine);
            }
            else
            {
                // запасной вариант: делим видимую высоту на 3 «строки» карточек
                h = gridControlKnitMachineLoadInfo.ClientSize.Height / 3;
                //h = gridControlKnitMachineLoadInfo.ClientSize.Height / (_knitMachineLoadInfoBindingSource.Count != 0? _knitMachineLoadInfoBindingSource.Count:1);
            }

            //MIN_CARD_HEIGHT = gridControlKnitMachineLoadInfo.Size.Height / (_knitMachineLoadInfoBindingSource.Count / (_knitMachineLoadInfoBindingSource.Count != 0 ? _knitMachineLoadInfoBindingSource.Count : 1) + 1);
            h = Math.Max(MIN_CARD_HEIGHT, h);
            view.CardMinSize = new Size(view.CardMinSize.Width, h);
        }

        //private static string StripHtml(string html, int maxLen)
        //{
        //    if (string.IsNullOrEmpty(html)) return string.Empty;
        //    string text = Regex.Replace(html, "<.*?>", " ");
        //    text = WebUtility.HtmlDecode(text);
        //    text = Regex.Replace(text, "\\s+", " ").Trim();
        //    return text.Length <= maxLen ? text : text.Substring(0, maxLen) + "…";
        //}
        private static string StripHtml(string html, int maxLen)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            string text = System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", " ");
            text = System.Net.WebUtility.HtmlDecode(text);
            text = System.Text.RegularExpressions.Regex.Replace(text, "\\s+", " ").Trim();
            return text.Length <= maxLen ? text : text.Substring(0, maxLen) + "…";
        }
        private static string HtmlToDxMarkup(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            // грубый, но быстрый маппинг самых частых тегов
            string s = html;
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\/?(p|div|br)\s*\/?>", "\n", RegexOptions.IgnoreCase);  // абзацы -> переносы
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*strong\s*>", "<b>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*\/\s*strong\s*>", "</b>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*em\s*>", "<i>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*\/\s*em\s*>", "</i>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*b\s*>", "<b>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*\/\s*b\s*>", "</b>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*i\s*>", "<i>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*\/\s*i\s*>", "</i>", RegexOptions.IgnoreCase);
            // <font color="#RRGGBB">...</font>  -> <color=#RRGGBB>...</color>
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*font[^>]*color\s*=\s*['""]?(#[0-9a-fA-F]{6}|[a-zA-Z]+)['""]?[^>]*>", "<color=$1>", RegexOptions.IgnoreCase);
            s = System.Text.RegularExpressions.Regex.Replace(s, @"<\s*/\s*font\s*>", "</color>", RegexOptions.IgnoreCase);
            // убираем оставшиеся теги
            s = System.Text.RegularExpressions.Regex.Replace(s, "<.*?>", string.Empty);
            // нормализуем переносы
            s = System.Text.RegularExpressions.Regex.Replace(s, @"(\r?\n)\s*(\r?\n)+", "\n");
            return s.Trim();
        }
        //private string WrapHtml(string bodyHtml)
        //{
        //    return @"<!DOCTYPE html>
        //    <html>
        //    <head>
        //        <meta http-equiv='X-UA-Compatible' content='IE=edge' />
        //        <meta charset='utf-8' />
        //        <style>
        //            html,body{margin:0;padding:12px;font-family:Segoe UI,Arial,sans-serif;font-size:12px;line-height:1.4;}
        //            /* Пример: если в тексте есть классы/теги без инлайна */
        //            b,strong{font-weight:600;}
        //        </style>
        //    </head>
        //    <body>" + (bodyHtml ?? "") + @"</body></html>";
        //}
        private string WrapHtml(string bodyHtml)
        {
            return @"<!DOCTYPE html>
                <html>
                <head>
                  <meta http-equiv='X-UA-Compatible' content='IE=edge' />
                  <meta charset='utf-8' />
                  <style>
                    html,body{margin:0;padding:12px;font-family:Segoe UI,Arial,sans-serif;font-size:12px;line-height:1.4;}
                    b,strong{font-weight:600;}
                  </style>
                </head>
                <body>" + (bodyHtml ?? "") + @"</body></html>";
        }

        // там, где вы подаёте HTML в попап:
        //        _repoPopup.QueryPopUp += (s, e) =>
        //{
        //    var html = Convert.ToString(gridViewKnitMachineLoadLayoutView.GetFocusedRowCellValue(_htmlColumnName));
        //        _browser.DocumentText = WrapHtml(html);
        //    };

        private void customLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gridControlKnitMachineLoadInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cardDBClick();
        }

        private void repositoryItemTextEditKmlNumber_DoubleClick(object sender, EventArgs e)
        {
            cardDBClick();
        }

        private void cardDBClick()
        {
            //var selectedItem = _knitMachineLoadInfoBindingSource.Current as KnitMachineLoadAllInfo;
            //MessageBox.Show($"Выбрано: {selectedItem.kmlNumber}");

            int xKmlID = 0;
            string xKmlNumber = "";
            var selectedRow = _knitMachineLoadInfoBindingSource.Current as KnitMachineLoadAllInfo;
            if (selectedRow != null && selectedRow.kmlID != 0)
            {
                xKmlID = selectedRow.kmlID;
                xKmlNumber = selectedRow.kmlNumber;
            }
            else
            {
                xKmlID = 0;
                xKmlNumber = "";
            }
            KnittingMachinesUnitLoading KML = new KnittingMachinesUnitLoading(xKmlID, xKmlNumber);

            DialogResult result = KML.ShowDialog();
            // Обработка результата, возвращенного модальной формой
            if (result == DialogResult.OK)
            {
                // Действия при успешном завершении работы модальной формы
                //MessageBox.Show("OK");
            }
            else
            {
                // Действия при отмене или другом результате
                //MessageBox.Show("Cancel");
            }
        }

        private void repositoryItemTextEditYearMonth_DoubleClick(object sender, EventArgs e)
        {
            cardDBClick();
        }

        private void repositoryItemRichTextEditCombinedPszNomCard_DoubleClick(object sender, EventArgs e)
        {
            cardDBClick();
        }

        private void repositoryItemHypertextLabelCombinedPszNomCard_DoubleClick(object sender, EventArgs e)
        {
            cardDBClick();
        }

        private void repositoryItemTextEditKmlID_DoubleClick(object sender, EventArgs e)
        {
            cardDBClick();
        }

    }
}
