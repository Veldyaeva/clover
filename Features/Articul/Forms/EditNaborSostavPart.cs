using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class EditNaborSostavPart : CustomForm
    {
        ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
        public EditNaborSostavPart(UserClass user) : base(user)
        {
            InitializeComponent();
        }
        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {

            customLookUpEditClass.Properties.DataSource = await _ANSDataService.GetTovarClass();
            customLookUpEditClass.Properties.DisplayMember = "TC_ClassName";
            customLookUpEditClass.Properties.ValueMember = "TC_ID";
            customLookUpEditClass.Properties.NullText = "";

            customLookUpEditGroup.Properties.DataSource = await _ANSDataService.GetTovarGroup();
            customLookUpEditGroup.Properties.DisplayMember = "TG_GroupName";
            customLookUpEditGroup.Properties.ValueMember = "TG_ID";
            customLookUpEditGroup.Properties.NullText = "";

            customLookUpEditCategory.Properties.DataSource = await _ANSDataService.GetTovarCategory();
            customLookUpEditCategory.Properties.DisplayMember = "TCAT_CategoryName";
            customLookUpEditCategory.Properties.ValueMember = "TCAT_ID";
            customLookUpEditCategory.Properties.NullText = "";

            customLookUpEditCatDynsign.Properties.DataSource = await _ANSDataService.GetTovarCatDynsign();
            customLookUpEditCatDynsign.Properties.DisplayMember = "TCDS_Name";
            customLookUpEditCatDynsign.Properties.ValueMember = "TCDS_ID";
            customLookUpEditCatDynsign.Properties.NullText = "";

            customLookUpEditSpravNoskiDetal.Properties.DataSource = await _ANSDataService.GetSpravNoskiDetal();
            customLookUpEditSpravNoskiDetal.Properties.DisplayMember = "name";
            customLookUpEditSpravNoskiDetal.Properties.ValueMember = "id_spr";
            customLookUpEditSpravNoskiDetal.Properties.NullText = "";
        }
        #endregion
    }
}
