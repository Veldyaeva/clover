using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class CreateArticulMatr : CustomForm

    {
        private DatabaseHelper _dbHelper;
        private DbService _dbService;
        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();
        private readonly ILogger _logger = new FileLogger();


        private BindingSource _bindingSourceArtMatr;

        public CreateArticulMatr(UserClass user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);

            InitializeComponent();

            _bindingSourceArtMatr = new BindingSource { };
            //if (gridArtMatr != null) gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatr.DataSource = _bindingSourceArtMatr;
            
            InitializeBindings();
        }

        private async void CreateArticulMatr_Load(object sender, EventArgs e)
        {
            //загрузка данных для отображения в гриде
            gridViewArtMatr.ShowLoadingPanel();
            var getArtTask = await _createArticulMatrService.GetMatrForArticulAsync();
            _bindingSourceArtMatr.DataSource = getArtTask;

            gridViewArtMatr.HideLoadingPanel();

        }
        private void InitializeBindings()
        {
            gcGrupmen_name.FieldName = nameof(CreateArticulMatrModel.Grupmen_name);
            gcTsn_name.FieldName = nameof(CreateArticulMatrModel.Tsn_name);
            gcTb_id.FieldName = nameof(CreateArticulMatrModel.Tb_id);
            gcMod.FieldName = nameof(CreateArticulMatrModel.Mod);
            gcArticul.FieldName = nameof(CreateArticulMatrModel.Articul);
            gcTm_name.FieldName = nameof(CreateArticulMatrModel.Tm_name);
            gcGrup.FieldName = nameof(CreateArticulMatrModel.Grup);
            gcText_mo.FieldName = nameof(CreateArticulMatrModel.Text_mo);
            gcP.FieldName = nameof(CreateArticulMatrModel.P);
            gcPrinter.FieldName = nameof(CreateArticulMatrModel.Printer);
            gcBus.FieldName = nameof(CreateArticulMatrModel.Bus);
            gcStra.FieldName = nameof(CreateArticulMatrModel.Stra);
            gcV.FieldName = nameof(CreateArticulMatrModel.V);
            gcKruj.FieldName = nameof(CreateArticulMatrModel.Kruj);
            gcTkan.FieldName = nameof(CreateArticulMatrModel.Tkan);
            gcSost.FieldName = nameof(CreateArticulMatrModel.Sost);
            gcSost2.FieldName = nameof(CreateArticulMatrModel.Sost2);
            gcSost3.FieldName = nameof(CreateArticulMatrModel.Sost3);
            gcRazmNames.FieldName = nameof(CreateArticulMatrModel.RazmNames);
            gcDatePublic.FieldName = nameof(CreateArticulMatrModel.DatePublic);


            //txbGostId.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true);
        }
    }
}
