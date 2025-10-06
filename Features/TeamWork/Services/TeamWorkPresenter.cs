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
        private readonly ArtNormService _artNormService;
        private readonly DatabaseHelper _dbHelper;
        private readonly TWGridHelper _gridHelper;

        private readonly BindingList<NormRasz> _rasz;
        private readonly BindingList<NormRask> _rask;
        private readonly BindingList<NormKont> _kont;
        private readonly BindingSource _raszSource;
        private readonly BindingSource _annSource;
        private readonly GridControl _raszGrid;
        private readonly GridView _raszView;

        private RaszOperationsController _ops;
        private BufferImportService _bufferService;
        private SavePipeline _savePipeline;
        private SecondsAggregator _secondsAggregator;

        public TeamWorkPresenter(
            ITeamWorkView view,
            ILogger logger,
            ArtNormService artNormService,
            DatabaseHelper dbHelper,
            TWGridHelper gridHelper,
            GridControl raszGrid,
            GridView raszView,
            BindingList<NormRasz> rasz,
            BindingList<NormRask> rask,
            BindingList<NormKont> kont,
            BindingSource raszSource,
            BindingSource annSource)
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
        }

        public async Task SaveAsync(bool closeAfterSave)
        {
            // Numbering should be recalculated before save to ensure consistency
            try
            {
                _view.AsControl.BeginInvoke((System.Windows.Forms.MethodInvoker)(() =>
                {
                    // The view (form) should expose a public method to recalc; if not, sorting refresh is a minimal safe op
                    TWGridHelper.sortGridView(_raszView);
                }));
            }
            catch { }

            // Actual save pipeline should be invoked from the form context where lists and deleted IDs are managed.
            await Task.CompletedTask;
        }

        public async Task ImportFromBufferAsync()
        {
            // Delegate to BufferImportService; actual parameters (ids) are controlled by the view/state
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
            // Placeholder: actual add flow requires selection dialog from the view
            await Task.CompletedTask;
        }
    }
}
