using DevExpress.Charts.Native;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Card.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.Events;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Design; // тут лежит RepositoryItemRichEditControl
using SewingProduction.Core.Services;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingMachinesLoading : CustomForm
    {
        private static DatabaseHelper _dbHelper;
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
        private RepositoryItemPopupContainerEdit _repoPopup;
        private bool _configureOnce;
        private readonly string _htmlColumnName = "combinedPszNom"; // <--- ИМЯ КОЛОНКИ С HTML
        private const int PREVIEW_HEIGHT = 120;   // высота зоны превью внутри карточки
        private int MIN_CARD_HEIGHT = 180;  // минимальная высота карточки (страховка)

        //private List<KnitMachineAreaListView> _currentKnitMachineAreaListViewData = new List<KnitMachineAreaListView>();
        //private BindingList<KnitMachineAreaListView> _knitMachineAreaListViewBindingList;
        //private BindingSource _knitMachineAreaListViewBindingSource;
        //private List<KnitMachineAreaListView> knitMachineAreaListViewData = new List<KnitMachineAreaListView>();

        public KnittingMachinesLoading(int classID)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
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
        //        //    if (e.Column == gridColumn1)
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
        private void ConfigureLayoutView()
        {
            var view = gridViewKnitMachineLoadLayoutView; // это LayoutView
            view.BeginUpdate();
            try
            {
                // 0) Репозиторий попапа с браузером (один раз)
                EnsureHtmlPopup(view);

                // 1) Назначаем попап-редактор на целевую колонку и фиксируем высоту её layout-поля
                var lcol = view.Columns[_htmlColumnName] as LayoutViewColumn
                           ?? throw new InvalidOperationException($"Колонка '{_htmlColumnName}' не найдена или это не LayoutViewColumn.");
                lcol.OptionsColumn.AllowEdit = true;
                lcol.OptionsColumn.ReadOnly = false;
                lcol.ColumnEdit = _repoPopup;

                var field = lcol.LayoutViewField;
                field.SizeConstraintsType = SizeConstraintsType.Custom;
                field.MinSize = new Size(field.MinSize.Width, PREVIEW_HEIGHT);
                field.MaxSize = new Size(int.MaxValue, PREVIEW_HEIGHT);

                // 2) Отключаем авто-рост редакторов, чтобы карточки не «распирало»
                FixEditorsAutoHeight(view);

                // 3) Высота карточек: расчёт от ваших данных (или fallback)
                RecalcCardHeight();

                // 4) Сортировка и прочее — как у вас
                view.BeginSort();
                view.ClearSorting();
                view.SortInfo.AddRange(new[] {
            new GridColumnSortInfo(view.Columns["kmlNumber"],  DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["yearNumber"], DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["monthNumber"],DevExpress.Data.ColumnSortOrder.Ascending)
        });
                view.EndSort();

                view.FocusedRowHandle = 0;

                // 5) Единоразовая подписка на события
                if (!_configureOnce)
                {
                    // обновлять высоту при ресайзе грида
                    gridControlKnitMachineLoadInfo.SizeChanged += (s, e) => RecalcCardHeight();

                    // красивое превью (чистим HTML для отображения в ячейке)
                    view.CustomColumnDisplayText += (s, e) =>
                    {
                        if (e.Column == lcol && e.Value is string html)
                            e.DisplayText = StripHtml(html, 200);
                    };

                    // открывать редактор/попап по клику
                    view.OptionsBehavior.EditorShowMode = EditorShowMode.Click;

                    _configureOnce = true;
                }
            }
            finally
            {
                view.EndUpdate();
            }
        }

        // --- ВСПОМОГАТЕЛЬНОЕ ---

        private void EnsureHtmlPopup(LayoutView view)
        {
            if (_popup != null) return;

            _popup = new PopupContainerControl { Parent = this, Size = new Size(700, 500) };
            _browser = new WebBrowser { Dock = DockStyle.Fill };
            _popup.Controls.Add(_browser);

            _repoPopup = new RepositoryItemPopupContainerEdit
            {
                AutoHeight = false,
                PopupControl = _popup,
                ShowPopupCloseButton = true,
                TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor,
                NullText = "Открыть…"
            };
            gridControlKnitMachineLoadInfo.RepositoryItems.Add(_repoPopup);

            _repoPopup.QueryPopUp += (s, e) =>
            {
                var html = Convert.ToString(gridViewKnitMachineLoadLayoutView.GetFocusedRowCellValue(_htmlColumnName));
                _browser.DocumentText = html ?? string.Empty;
            };
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
                h = gridControlKnitMachineLoadInfo.ClientSize.Height / _knitMachineLoadInfoBindingSource.Count;
            }
            
            MIN_CARD_HEIGHT = gridControlKnitMachineLoadInfo.Size.Height / (_knitMachineLoadInfoBindingSource.Count / _knitMachineListBindingSource.Count + 1);
            h = Math.Max(MIN_CARD_HEIGHT, h);
            view.CardMinSize = new Size(view.CardMinSize.Width, h);
        }

        private static string StripHtml(string html, int maxLen)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            string text = Regex.Replace(html, "<.*?>", " ");
            text = WebUtility.HtmlDecode(text);
            text = Regex.Replace(text, "\\s+", " ").Trim();
            return text.Length <= maxLen ? text : text.Substring(0, maxLen) + "…";
        }
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
