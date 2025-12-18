using DevExpress.XtraEditors;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
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
        //private ArticulDataService _articulDataService;
        private ArticulEditAdvanceService _articulEdAdvDataService;

        private readonly ILogger _logger = new FileLogger();

        private string _kodd;

        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;

        
        private List<GostModel> _gosts;
        //private BindingSource _bindingSourceGosts;

        public ArticulEditAdvance(UserClass user) : this(user, "", "")
        { }

        public ArticulEditAdvance(UserClass user, string kodd, string articul) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            _dbService = new DbService(_dbHelperAce);
            _articulEdAdvDataService = new ArticulEditAdvanceService();

            InitializeComponent();
            _user = user;
            _kodd = kodd;
            this.Text = articul + " " + kodd;
            ThemeManager.UpdateTheme(this);

            //_artKodRazm = new BindingList<ArticulModel>();

            _bindingSourceArtKod = new BindingSource { };
            if (gridEditAdRazm != null) gridEditAdRazm.DataSource = _bindingSourceArtKod;

            _bindingSourceArtCommon = new BindingSource { };
            //_bindingSourceGosts = new BindingSource {  };

        }

        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 

            _bindingSourceArtKod.DataSource = await _articulEdAdvDataService.GetArtByKoddAsync(this._kodd);

            _gosts = await _articulEdAdvDataService.GetGostNaborAsync();
            
            lookUpGost.EditValueChanged += (s, e) => UpdateOpi();

            //_bindingSourceGosts.DataSource = await _articulEdAdvDataService.GetBLGostNaborAsync();

            InitializeBindingsAsync();

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                //загрузка перечня кодов из справочника общая информация
                
                _bindingSourceArtCommon.DataSource = await _articulEdAdvDataService.GetCommonArtByKoddAsync(this._kodd);
                dataLayoutCommonArticul.DataSource = _bindingSourceArtCommon;


                #region заполнение блока основных данных артикула

                txbArticul.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Articul), true);
                txbMod.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Mod), true);

                lookUpGost.Properties.DataSource = _gosts;
                lookUpGost.Properties.DisplayMember = nameof(GostModel.Name_gost);
                lookUpGost.Properties.ValueMember = nameof(GostModel.Id_gost);
                lookUpGost.Properties.NullText = "Не выбрано";
                lookUpGost.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Id_gost), true, DataSourceUpdateMode.OnPropertyChanged);
                //инициализация описания госта
                UpdateOpi();

                chbArh.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Arh), true, DataSourceUpdateMode.OnPropertyChanged);
                //отделка
                chbIsUpak.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Is_upak), true);
                chbIsFurnit.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Is_furnit), true);

                chkP.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.P), true);
                chkV.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.V), true);
                chkBus.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Bus), true);
                chkStra.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.Stra), true);
                chkPres.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(SpArticulPreviewModel.P_pres), true);



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
        

        private void UpdateOpi()
        {
            if (lookUpGost.EditValue is int id)
                txbOpiGost.Text = _gosts.FirstOrDefault(x => x.Id_gost == id)?.Opi_gost ?? "";
            else
                txbOpiGost.Text = "";
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// Обновляет одно поле в таблице по заданному условию.
            /// </summary>
            /// <param name="tableName">Имя таблицы</param>
            /// <param name="fieldName">Имя обновляемого поля</param>
            /// <param name="newValue">Новое значение</param>
            /// <param name="whereField">Поле условия (например, "AnnId")</param>
            /// <param name="whereValue">Значение условия</param>
            // public async Task UpdateFieldAsync(string tableName, string fieldName, object newValue, string whereField, object whereValue)


        }

        private void chbArh_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
