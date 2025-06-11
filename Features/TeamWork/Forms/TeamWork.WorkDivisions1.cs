
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Forms
{
    public partial class TeamWork_WorkDivisions : Form
    {
        private readonly ArtNormService _artNormService;
        private BindingSource _workDivisionsSource = new();
        private BindingSource _normRaszSource = new();
        private BindingSource _normRaskSource = new();
        private BindingSource _normKontSource = new();

        private List<AnnModel> _workDivisions = new();
        private List<NormRasz> _normRaszList = new();
        private List<NormRask> _normRaskList = new();
        private List<NormKont> _normKontList = new();

        public TeamWork_WorkDivisions(ArtNormService artNormService)
        {
            InitializeComponent();
            _artNormService = artNormService;
            Load += TeamWork_WorkDivisions_Load;
        }

        private async void TeamWork_WorkDivisions_Load(object sender, EventArgs e)
        {
            await LoadWorkDivisionsAsync();
            BindMainGrid();
            SetupEvents();

            if (_workDivisions.Count > 0)
                await LoadAndBindNormTablesAsync(_workDivisions[0].AnnId);
        }

        private async Task LoadWorkDivisionsAsync()
        {
            _workDivisions = await _artNormService.GetAnnListAsync();
            _workDivisionsSource.DataSource = _workDivisions;
        }

        private async Task LoadAndBindNormTablesAsync(int annId)
        {
            _normRaszList = await _artNormService.GetRelatedNormRasz(annId);
            _normRaskList = await _artNormService.GetRelatedNormRask(annId);
            _normKontList = await _artNormService.GetRelatedNormKont(annId);

            _normRaszSource.DataSource = _normRaszList;
            _normRaskSource.DataSource = _normRaskList;
            _normKontSource.DataSource = _normKontList;

            gridControlNormRasz.DataSource = _normRaszSource;
            gridControlNormRask.DataSource = _normRaskSource;
            gridControlNormKont.DataSource = _normKontSource;
        }

        private void BindMainGrid()
        {
            gridControlAnn.DataSource = _workDivisionsSource;
        }

        private void SetupEvents()
        {
            if (gridViewAnn is GridView gv)
            {
                gv.FocusedRowChanged += async (s, e) =>
                {
                    if (gv.GetRow(e.FocusedRowHandle) is AnnModel selected)
                    {
                        await LoadAndBindNormTablesAsync(selected.AnnId);
                    }
                };
            }
        }
    }
}
