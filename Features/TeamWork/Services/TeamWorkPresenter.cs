using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Features.TeamWork.Operations;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Services
{
    public sealed class TeamWorkPresenter : ITeamWorkPresenter
    {
        private readonly ITeamWorkView _view;
        private readonly ILogger _logger;
        private readonly ArtNormRepository _artNormService;
        private readonly DatabaseHelper _dbHelper;
        private readonly TWGridHelper _gridHelper;

        private readonly BindingList<NormRasz> _rasz;
        private readonly BindingList<NormRask> _rask;
        private readonly BindingList<NormKont> _kont;
        private readonly BindingSource _raszSource;
        private readonly BindingSource _annSource;
        private readonly GridControl _raszGrid;
        private readonly GridView _raszView;
        private GridView _kontView;

        // Popup handlers moved from form
        private DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler _raszPopupHandler;
        private DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler _kontPopupHandler;

        private RaszOperationsController _ops;
        private BufferImportService _bufferService;
        private SavePipeline _savePipeline;
        private SecondsAggregator _secondsAggregator;

        //Делегаты для повторного использования логики формы без дублирования бизнес-кода здесь (тонкий presenter)
        private readonly Func<bool, Task<bool>> _processSaveFunc;
        private readonly Func<Task> _importFromBufferFunc;
        private readonly Func<Task> _addOperationFunc;

        public TeamWorkPresenter(
            ITeamWorkView view,
            ILogger logger,
            ArtNormRepository artNormService,
            DatabaseHelper dbHelper,
            TWGridHelper gridHelper,
            GridControl raszGrid,
            GridView raszView,
            BindingList<NormRasz> rasz,
            BindingList<NormRask> rask,
            BindingList<NormKont> kont,
            BindingSource raszSource,
            BindingSource annSource,
            Func<bool, Task<bool>> processSave,
            Func<Task> importFromBuffer,
            Func<Task> addOperation)
        {
            _view = view;
            _logger = logger;
            _artNormService = artNormService;
            _dbHelper = dbHelper;
            _gridHelper = gridHelper;
            _raszGrid = raszGrid;
            _raszView = raszView;
            _rasz = rasz;
            _rask = rask;
            _kont = kont;
            _raszSource = raszSource;
            _annSource = annSource;
            _processSaveFunc = processSave;
            _importFromBufferFunc = importFromBuffer;
            _addOperationFunc = addOperation;
        }

        public async Task InitializeAsync()
        {
            _savePipeline = _savePipeline ?? new SavePipeline(_dbHelper, _logger, () => { });
            _bufferService = _bufferService ?? new BufferImportService(
                _artNormService,
                _dbHelper,
                _logger,
                _rasz,
                _raszSource,
                _raszView,
                () => TWGridHelper.sortGridView(_raszView),
                (op, clear) => { try { _raszView.GridControl.BeginInvoke(new Action(() => { if (clear) _raszView.ClearSelection(); })); } catch { } }
            );
            _secondsAggregator = _secondsAggregator ?? new SecondsAggregator(
                _rasz,
                _annSource,
                _logger,
                (act) => { try { _view.AsControl.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => act())); } catch { } }
            );
            await Task.CompletedTask;
        }

        public void Attach()
        {
            if (_ops == null)
            {
                _ops = new RaszOperationsController(
                    _rasz,
                    _raszGrid,
                    _raszView,
                    _raszSource,
                    _logger,
                    () => TWGridHelper.sortGridView(_raszView),
                    (op, clear) => { try { if (clear) _raszView.ClearSelection(); } catch { } }
                );
            }
            _ops.Attach();
        }

        public void Detach()
        {
            try { _ops?.Detach(); } catch { }
            DetachPopupMenus();
        }

        // Popup menus wiring
        public void AttachPopupMenus(DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler raszHandler,
                                     DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler kontHandler,
                                     GridView kontView)
        {
            DetachPopupMenus();
            _raszPopupHandler = raszHandler;
            _kontPopupHandler = kontHandler;
            _kontView = kontView;
            try
            {
                if (_raszPopupHandler != null)
                    _raszView.PopupMenuShowing += _raszPopupHandler;
            }
            catch { }
            try
            {
                if (_kontView != null && _kontPopupHandler != null)
                    _kontView.PopupMenuShowing += _kontPopupHandler;
            }
            catch { }
        }

        public void DetachPopupMenus()
        {
            try
            {
                if (_raszPopupHandler != null)
                    _raszView.PopupMenuShowing -= _raszPopupHandler;
            }
            catch { }
            try
            {
                if (_kontView != null && _kontPopupHandler != null)
                    _kontView.PopupMenuShowing -= _kontPopupHandler;
            }
            catch { }
            _raszPopupHandler = null;
            _kontPopupHandler = null;
            _kontView = null;
        }

        public async Task SaveAsync(bool closeAfterSave)
        {
            if (_processSaveFunc != null)
            {
                await _processSaveFunc(closeAfterSave);
                return;
            }
            // Fallback: Убедиться, что данные в таблицах согласованы
            // TODO: добавить логику сохранения данных остальных таблиц
            try { TWGridHelper.sortGridView(_raszView); } catch { }
            await Task.CompletedTask;
        }

        public async Task ImportFromBufferAsync()
        {
            if (_importFromBufferFunc != null)
            {
                await _importFromBufferFunc();
                return;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteSelectedAsync()
        {
            try
            {
                var selected = _raszView?.GetSelectedRows()?.Where(h => h >= 0).ToArray() ?? Array.Empty<int>();
                if (selected.Length == 0) return;
                _raszView.BeginDataUpdate();
                try
                {
                    foreach (var h in selected)
                    {
                        var r = _raszView.GetRow(h) as NormRasz;
                        if (r != null) _rasz.Remove(r);
                    }
                }
                finally
                {
                    _raszView.EndDataUpdate();
                }
                TWGridHelper.sortGridView(_raszView);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, nameof(DeleteSelectedAsync));
            }
        }

        public async Task AddOperationAsync()
        {
            if (_addOperationFunc != null)
            {
                await _addOperationFunc();
                return;
            }
            await Task.CompletedTask;
        }
    }
}
