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
    public partial class ArticulEditAdvance : CustomForm
    {
        private DatabaseHelper _dbHelperAce;
        private DbService _dbService;
        private ArticulDataService _articulDataService;
        private readonly ILogger _logger = new FileLogger();

        private string _kodd;

        private List<ArticulModel> _artKodRazm;
        private BindingList<ArticulModel> _articulCommon;

        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;

        public  ArticulEditAdvance(UserClass user) : this(user, "")
        { }
        
         public ArticulEditAdvance(UserClass user, string kodd) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            _dbService = new DbService(_dbHelperAce);
            _articulDataService = new ArticulDataService();

            InitializeComponent();
            _user = user;
            _kodd = kodd;
            this.Text = kodd;
            ThemeManager.UpdateTheme(this);

            //_artKodRazm = new BindingList<ArticulModel>();

            _bindingSourceArtKod = new BindingSource { DataSource = _artKodRazm };
            if (gridEditAdRazm != null) gridEditAdRazm.DataSource = _bindingSourceArtKod;

            
        }
        

        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 
            _artKodRazm = await _articulDataService.GetArtByKoddAsync(this._kodd);
            _bindingSourceArtKod.DataSource = _artKodRazm;
            InitializeBindingsAsync();

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                
                //загрузка перечня кодов из справочника общая информация
                _articulCommon = new BindingList<ArticulModel>();
                _bindingSourceArtCommon = new BindingSource { DataSource = _articulCommon };

                #region заполнение блока основных данных артикула
                txbArticul.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Articul), true);
                txbMod.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Mod), true);
                txbIdGost.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Id_gost), true);
                txbSost.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Sost), true);
                txbSost2.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Sost2), true);
                txbSost3.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Sost3), true);

                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            } 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
