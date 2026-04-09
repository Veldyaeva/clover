using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormRepository _artNormService; // репозиторий данных
        private readonly JabberSender _jabberSender;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private readonly FormSettingsHelper _formSettingsHelper = new FormSettingsHelper();
        private readonly SecondsUpdateManager _secondsUpdateManager;
        private readonly ITeamWorkOrchestrator _teamWorkService;
        private readonly TeamWorkArticlesQueryService _articlesQueryService;
        private UIHelper _uiHelper;
        private int bufferId = 0;
        private BindingList<ArtNormN> _bindingList;
        private BindingSource _bindingSource;
        private bool _hasUnsavedChanges = false;
        private BindingSource _nzpByKoddRtSourceArt;
        private BindingSource _nzpByKoddRtSourceWd;
        private BindingList<NZPByKoddRt> _nzpListArt;
        private BindingList<NZPByKoddRt> _nzpListWd;
        private BindingList<NormRasz> _normRaszListTW;
        private BindingSource _normRaszBindingSourceTW;
        private BindingList<NormRask> _normRaskListTW;
        private BindingSource _normRaskBindingSourceTW;
        private BindingList<NormKont> _normKontListTW;
        private BindingSource _normKontBindingSourceTW;
        private static List<FioModel> _cachedFioData;
        private List<FioModel> fioList;
        private BindingList<MyDataANN> _preArchList;
        private BindingSource _preArchBindingSource;
        private BindingList<ArtNormN> _archList;
        private BindingSource _archBindingSource;

        // Глобальная переменная для состояния кнопки "показать все"
        private bool showAllWD = false;

        //// Глобальная переменная для управления видимостью кнопки customSimpleButtonUnbind
        //private bool customSimpleButtonUnbindVisible = true;
        // Объект для управления доступностью кнопки "bind:unlink"
        private ButtonUnbindWd ButtonUnbindWd;
        private BindingList<MyDataART> _myDataArtList;
        private BindingSource _myDataArtBindingSource;
        private BindingList<MyDataANN> _myDataAnnList;
        private BindingSource _myDataAnnBindingSource;

        private BindingList<MyDataART> _boundArtList;
        private BindingSource _boundArtBindingSource;

        // BindingList и BindingSource схемы разденеий на вкладке Articles 
        private BindingList<NormRasz> _normRaszListArticles;
        private BindingSource _normRaszBindingSourceArticles;

        private BindingList<NormRask> _normRaskListArticles;
        private BindingSource _normRaskBindingSourceArticles;

        private BindingList<NormKont> _normKontListArticles;
        private BindingSource _normKontBindingSourceArticles;
        private bool _articlesTabInitialized = false;
        private bool _isUpdatingAnnGridKitFilter = false;
        private bool _annGridKitFilterActive = false;
        private readonly HashSet<int> _recommendedAnnIds = new HashSet<int>();

        private List<KodProizvModel> kodProizvList;
        private List<PodrVyazModel> podrVyazList;
        private List<OborudShvModel> oborudShvList;

        private CancellationTokenSource _loadCts = new CancellationTokenSource();


        public TeamWork(UserClass user) : base(user)
        {
            InitializeComponent();


            ANNgridView.OptionsView.ShowPreview = false;
            ANNgridView.PreviewLineCount = 0;
            DapperMappings.Configure();
            var coreServices = TeamWorkDependencyFactory.CreateCoreServices(_logger);
            _dbHelper = coreServices.DbHelper;
            _dbService = coreServices.DbService;
            _artNormService = coreServices.ArtNormRepository;
            _jabberSender = (JabberSender)coreServices.JabberSender;
            _secondsUpdateManager = new SecondsUpdateManager(_artNormService, _logger);
            _teamWorkService = coreServices.Orchestrator;
            _articlesQueryService = new TeamWorkArticlesQueryService(_artNormService, _dbService, _logger);
            _uiHelper = new UIHelper(_logger);

            // Инициализация основных BindingList и BindingSource
            _bindingList = new BindingList<ArtNormN>();
            _bindingSource = new BindingSource { DataSource = _bindingList };
            if (ANNgridControl != null) ANNgridControl.DataSource = _bindingSource;

            _preArchList = new BindingList<MyDataANN>();
            _preArchBindingSource = new BindingSource { DataSource = _preArchList };
            if (gridControlPreArch != null) gridControlPreArch.DataSource = _preArchBindingSource;

            _archList = new BindingList<ArtNormN>();
            _archBindingSource = new BindingSource { DataSource = _archList };
            if (gridControlArch != null) gridControlArch.DataSource = _archBindingSource;

            _nzpListArt = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceArt = new BindingSource { DataSource = _nzpListArt };
            if (gridControlNZP != null) gridControlNZP.DataSource = _nzpByKoddRtSourceArt;

            _nzpListWd = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceWd = new BindingSource { DataSource = _nzpListWd };
            if (GridControlBindedArts != null) GridControlBindedArts.DataSource = _nzpByKoddRtSourceWd;

            // Инициализация для вкладки "Работа с артикулами"
            _myDataArtList = new BindingList<MyDataART>();
            _myDataArtBindingSource = new BindingSource { DataSource = _myDataArtList };
            if (gridControl_unboundArts != null) gridControl_unboundArts.DataSource = _myDataArtBindingSource;

            _myDataAnnList = new BindingList<MyDataANN>();
            _myDataAnnBindingSource = new BindingSource { DataSource = _myDataAnnList };
            if (gridControl_wdToBind != null) gridControl_wdToBind.DataSource = _myDataAnnBindingSource;

            _boundArtList = new BindingList<MyDataART>();
            _boundArtBindingSource = new BindingSource { DataSource = _boundArtList };
            if (gridControl_binded != null) gridControl_binded.DataSource = _boundArtBindingSource;

            _normRaszListArticles = new BindingList<NormRasz>();
            _normRaszBindingSourceArticles = new BindingSource { DataSource = _normRaszListArticles };
            if (customGridControl3 != null) customGridControl3.DataSource = _normRaszBindingSourceArticles;

            //// Инициализация для NormRask на вкладке Articles
            _normRaskListArticles = new BindingList<NormRask>();
            _normRaskBindingSourceArticles = new BindingSource { DataSource = _normRaskListArticles };
            if (customGridControl2 != null)
            //{
                customGridControl2.DataSource = _normRaskBindingSourceArticles;
            //}
                //_logger?.LogEventAsync($"Constructor: customGridControl2.DataSource set to _normRaskBindingSourceArticles", "TeamWork.Constructor");

            //    // Verify the grid view configuration
            //    if (customGridControl2.MainView is GridView gridView)
            //    {
            //        _logger?.LogEventAsync($"Constructor: customGridControl2.MainView is GridView with {gridView.Columns.Count} columns", "TeamWork.Constructor");
            //        foreach (var col in gridView.Columns)
            //        {
            //            _logger?.LogEventAsync($"Constructor: Column '{col.Name}' - FieldName: '{col.FieldName}', Visible: {col.Visible}, Width: {col.Width}", "TeamWork.Constructor");
            //        }
            //    }
            //}
            //else
            //{
            //    _logger?.LogWarningAsync("Constructor: customGridControl2 is null, cannot set DataSource", "TeamWork.Constructor");
            //}

            // Инициализация для NormKont на вкладке Articles
            _normKontListArticles = new BindingList<NormKont>();
            _normKontBindingSourceArticles = new BindingSource { DataSource = _normKontListArticles };
            if (customGridControl1 != null) customGridControl1.DataSource = _normKontBindingSourceArticles;

            InitializeGridSettings();
            SetupDateUpdateColumn();
            VerifyGridConfigurations();

            // Взаимоисключаем видимость кнопок редактирования по событию изменения видимости
            if (ButtonEditOnlyAdv != null)
                ButtonEditOnlyAdv.VisibleChanged += ButtonEditOnlyAdv_VisibleChanged;
        }

        ///// <summary>
        ///// Устанавливает видимость кнопки customSimpleButtonUnbind
        ///// </summary>
        ///// <param name="visible">Видимость кнопки</param>
        //private void SetCustomSimpleButtonUnbindVisible(bool visible)
        //{
        //    customSimpleButtonUnbindVisible = visible;
        //    if (customSimpleButtonUnbind != null)
        //    {
        //        customSimpleButtonUnbind.Visible = visible;
        //    }
        //}

        ///// <summary>
        ///// Получает состояние видимости кнопки customSimpleButtonUnbind
        ///// </summary>
        ///// <returns>true если кнопка видима</returns>
        //private bool GetCustomSimpleButtonUnbindVisible()
        //{
        //    return customSimpleButtonUnbindVisible;
        //}

        ///// <summary>
        ///// Показать кнопку отвязки артикулов
        ///// </summary>
        //private void ShowUnbindButton()
        //{
        //    SetCustomSimpleButtonUnbindVisible(true);
        //}

        ///// <summary>
        ///// Скрыть кнопку отвязки артикулов
        ///// </summary>
        //private void HideUnbindButton()
        //{
        //    SetCustomSimpleButtonUnbindVisible(false);
        //}

    }
}
