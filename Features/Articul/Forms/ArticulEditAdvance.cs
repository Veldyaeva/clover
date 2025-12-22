using DevExpress.XtraEditors;
using SewingProduction.Core.Class;
using SewingProduction.Core.helpers;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class ArticulEditAdvance : CustomForm
    {
        private DatabaseHelper _dbHelper;
        private DbService _dbService;
        private static BulkHelper _bulkHelper;

        //private ArticulDataService _articulDataService;
        private ArticulEditAdvanceService _articulEdAdvDataService;

        private readonly ILogger _logger = new FileLogger();

        private string _kodd;

        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;
        private BindingSource _bindingSourceArtCommonSave;



        private List<GostModel> _gosts;
        //private BindingSource _bindingSourceGosts;

        public ArticulEditAdvance(UserClass user) : this(user, "", "")
        { }

        public ArticulEditAdvance(UserClass user, string kodd, string articul) : base(user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();

            _articulEdAdvDataService = new ArticulEditAdvanceService();

            InitializeComponent();
            _user = user;
            _kodd = kodd;
            this.Text = articul + " " + kodd;
            ThemeManager.UpdateTheme(this);

            _bindingSourceArtKod = new BindingSource { };
            if (gridEditAdRazm != null) gridEditAdRazm.DataSource = _bindingSourceArtKod;

            _bindingSourceArtCommon = new BindingSource { };
            _bindingSourceArtCommonSave = new BindingSource { };

        }

        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 

            _bindingSourceArtKod.DataSource = await _articulEdAdvDataService.GetArtByKoddAsync(this._kodd);
            _gosts = await _articulEdAdvDataService.GetGostNaborAsync();

            InitializeBindingsAsync();

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                //загрузка перечня кодов из справочника общая информация
                
                _bindingSourceArtCommon.DataSource = await _articulEdAdvDataService.GetCommonArtByKoddAsync(this._kodd);

                #region заполнение блока основных данных артикула

                txbArticul.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Articul), true);
                //txbArticul.DataBindings.Add("Text", _bindingSourceArtCommonSokr, nameof(SpArticulSaveAdvance.Articul), true);

                txbMod.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Mod), true);

                lookUpGost.Properties.DataSource = _gosts;
                lookUpGost.Properties.DisplayMember = nameof(GostModel.Name_gost);
                lookUpGost.Properties.ValueMember = nameof(GostModel.Id_gost);
                lookUpGost.Properties.NullText = "Не выбрано";
                lookUpGost.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true, DataSourceUpdateMode.OnPropertyChanged);
                //инициализация описания госта
                UpdateOpi();

                chbArh.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Arh), true, DataSourceUpdateMode.OnPropertyChanged);
                //отделка
                chbIsUpak.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Is_upak), true);
                chbIsFurnit.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Is_furnit), true);

                chkP.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.P), true);
                chkV.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.V), true);
                chkBus.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Bus), true);
                chkStra.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Stra), true);
                chkPres.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.P_pres), true);



                txbSost.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost), true);
                txbSost2.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost2), true);
                txbSost3.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost3), true);


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

        private void SaveChanges(object sender, EventArgs e)
        {
            try
            {
                _bindingSourceArtCommon.EndEdit();
                //копирование всех кодов с измененными общими данными в список для сохранения
                var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
                if (currentItem.IsModified == false)
                {
                    return;
                }

                for (int i = 0; i < _bindingSourceArtKod.Count; i++)
                {
                    var item = (ArticulModel)_bindingSourceArtKod[i];
                    //    
                    var newItem = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                    {
                        clone.Kod = item.Kod;
                        clone.IsModified = true;

                    }, "Kod", "Razm");
                    _bindingSourceArtCommonSave.Add(newItem);
                }
                //формирование списка для сохранения с уникальными кодами
                List<ArticulModel> filteredList = _bindingSourceArtCommonSave.List
                    .OfType<ArticulModel>()
                    .Where(x => x?.IsModified == true)
                    .ToList();

                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<ArticulModel>(connection, filteredList, "sp_articul", new[] { "kod" });
                    }
                }
                _bindingSourceArtCommonSave.Clear();
                XtraMessageBox.Show("Изменения успешно сохранены.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении изменений артикула");
                XtraMessageBox.Show("Ошибка при сохранении изменений артикула: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            }


        private void lookUpGost_EditValueChanged(object sender, EventArgs e)
        {
            UpdateOpi();
        }

        private void ArticulEditAdvance_FormClosing(object sender, FormClosingEventArgs e)
        {
            _gosts = null;

        }
    }
}
