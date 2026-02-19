using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class EditNaborSostavPart : CustomForm
    {
        ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
        private List<TovarClassModel> _classes = new();
        private List<TovarGroupModel> _groups = new();
        private List<TovarCategoryModel> _categories = new();
        private List<TovarCatDynsignModel> _dynsigns = new();
        private List<SpravNoskiDetalModel> _spravNoskiDetal = new();
        private object _sourceRow;
        private bool _isUpdating;
        public EditNaborSostavPart(UserClass user, object sourceRow) : base(user)
        {
            InitializeComponent();
            _sourceRow = sourceRow;
        }
        public EditNaborSostavPart(UserClass user) : base(user)
        {
            InitializeComponent();
        }
        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
            switch (_sourceRow)
            {
                case PlanSezonAllModel p:
                    if (p.Nn == null) { Close(); return; }
                    ApplyChainByDynsign(p.Tgm_id_n);
                    LoadArticulModel(p.Articul,p.Mod);
                    ApplyVisibilityRules();
                    break;

                case ArtKomplektModel a:
                    if (a.Parent_nn == null) { Close(); return; }
                    ApplyChainByDynsign(a.Tgm_id_n);
                    LoadArticulModel("", "");
                    ApplyVisibilityRules();
                    break;

                default:
                    XtraMessageBox.Show($"Неподдерживаемый тип строки: {_sourceRow?.GetType().FullName}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    break;
            }
        }
        private void LoadArticulModel(string articul, string mod)
        {
            customHeaderLabelArt.Text = articul;
            customHeaderLabelMod.Text = mod;
        }
        private async Task InitializeFormAsync()
        {
            _isUpdating = true;
            try
            {
                await LoadClass();
                await LoadGroup();
                await LoadCategory();
                await LoadDynsign();
                await LoadNoski();

                //ResetGroup();
                //ResetCategory();
                //ResetDynsign();
            }
            finally
            {
                _isUpdating = false;
            }
        }
        #endregion

        #region Класс
        private async Task LoadClass()
        {
            _classes = await _ANSDataService.GetTovarClass();
            customLookUpEditClass.Properties.DataSource = _classes;
            customLookUpEditClass.Properties.DisplayMember = "TC_ClassName";
            customLookUpEditClass.Properties.ValueMember = "TC_ID";
            customLookUpEditClass.Properties.NullText = "";
        }
        private void ResetClass()
        {
            customLookUpEditClass.Properties.DataSource = new List<TovarClassModel>();
            customLookUpEditClass.EditValue = null;
        }
        private void customLookUpEditClass_EditValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;

            if (customLookUpEditClass.EditValue == null || 
                customLookUpEditClass.EditValue == DBNull.Value ||
               !int.TryParse(customLookUpEditClass.EditValue.ToString(), out int classId))
            {
                ResetGroup();
                ResetCategory();
                ResetDynsign();
                return;
            }

            _isUpdating = true;
            try
            {
                var filteredGroups = _groups
                    .Where(g => g.TG_TC_ID == classId)
                    .OrderBy(g => g.TG_GroupName)
                    .ToList();

                customLookUpEditGroup.Properties.DataSource = filteredGroups;
                customLookUpEditGroup.EditValue = null;

                ResetCategory();
                ResetDynsign();
            }
            finally
            {
                _isUpdating = false;
            }
        }
        #endregion

        #region Группа
        private async Task LoadGroup()
        {
            _groups = await _ANSDataService.GetTovarGroup();
            customLookUpEditGroup.Properties.DataSource = _groups;
            customLookUpEditGroup.Properties.DisplayMember = "TG_GroupName";
            customLookUpEditGroup.Properties.ValueMember = "TG_ID";
            customLookUpEditGroup.Properties.NullText = "";
        }
        private void ResetGroup()
        {
            customLookUpEditGroup.Properties.DataSource = new List<TovarGroupModel>();
            customLookUpEditGroup.EditValue = null;
        }
        private void customLookUpEditGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;

            if (customLookUpEditGroup.EditValue == null || customLookUpEditGroup.EditValue == DBNull.Value)
            {
                ResetCategory();
                ResetDynsign();
                return;
            }

            if (!int.TryParse(customLookUpEditGroup.EditValue.ToString(), out int groupId))
            {
                ResetCategory();
                ResetDynsign();
                return;
            }

            _isUpdating = true;
            try
            {
                // фильтруем категории по группе
                var filteredCategories = _categories
                    .Where(c => c.TCAT_TG_ID == groupId)
                    .OrderBy(c => c.TCAT_CategoryName)
                    .ToList();

                customLookUpEditCategory.Properties.DataSource = filteredCategories;
                customLookUpEditCategory.EditValue = null;

                ResetDynsign();
            }
            finally
            {
                _isUpdating = false;
            }
        }

        #endregion

        #region Категория
        private async Task LoadCategory()
        {
            _categories = await _ANSDataService.GetTovarCategory();
            customLookUpEditCategory.Properties.DataSource = _categories;
            customLookUpEditCategory.Properties.DisplayMember = "TCAT_CategoryName";
            customLookUpEditCategory.Properties.ValueMember = "TCAT_ID";
            customLookUpEditCategory.Properties.NullText = "";
        }
        private void ResetCategory()
        {
            customLookUpEditCategory.Properties.DataSource = new List<TovarCategoryModel>();
            customLookUpEditCategory.EditValue = null;
        }
        private void customLookUpEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;

            if (customLookUpEditCategory.EditValue == null || customLookUpEditCategory.EditValue == DBNull.Value)
            {
                ResetDynsign();
                return;
            }

            if (!int.TryParse(customLookUpEditCategory.EditValue.ToString(), out int categoryId))
            {
                ResetDynsign();
                return;
            }

            _isUpdating = true;
            try
            {
                // фильтруем признаки по категории
                var filteredDynsigns = _dynsigns
                    .Where(d => d.TCDS_TCAT_ID == categoryId)
                    .OrderBy(d => d.TCDS_Name)
                    .ToList();

                customLookUpEditCatDynsign.Properties.DataSource = filteredDynsigns;
                customLookUpEditCatDynsign.EditValue = null;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        #endregion

        #region Признаки
        private async Task LoadDynsign()
        {
            _dynsigns = await _ANSDataService.GetTovarCatDynsign();
            customLookUpEditCatDynsign.Properties.DataSource = _dynsigns;
            customLookUpEditCatDynsign.Properties.DisplayMember = "TCDS_Name";
            customLookUpEditCatDynsign.Properties.ValueMember = "TCDS_ID";
            customLookUpEditCatDynsign.Properties.NullText = "";
        }
        private void ResetDynsign()
        {
            customLookUpEditCatDynsign.Properties.DataSource = new List<TovarCatDynsignModel>();
            customLookUpEditCatDynsign.EditValue = null;
        }
        private void customLookUpEditCatDynsign_EditValueChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Носки
        private void customLookUpEditSpravNoskiDetal_EditValueChanged(object sender, EventArgs e)
        {

        }
        private async Task LoadNoski()
        {
            _spravNoskiDetal = await _ANSDataService.GetSpravNoskiDetal();
            customLookUpEditSpravNoskiDetal.Properties.DataSource = _spravNoskiDetal;
            customLookUpEditSpravNoskiDetal.Properties.DisplayMember = "name";
            customLookUpEditSpravNoskiDetal.Properties.ValueMember = "id_spr";
            customLookUpEditSpravNoskiDetal.Properties.NullText = "";
        }
        private void ApplyVisibilityRules()
        {
            bool showNoskiDetal = false;
            if (_sourceRow is PlanSezonAllModel p)
                showNoskiDetal = p.Men?.ToString() == "10";
            customLookUpEditSpravNoskiDetal.Visible = showNoskiDetal;
            customLabelSpravNoskiDetal.Visible = showNoskiDetal;
        }
        #endregion

        #region Загрузка данных
        private void ApplyChainByDynsign(int tgm_id_n)
        {
            var dyn = _dynsigns.FirstOrDefault(d => d.TCDS_ID == tgm_id_n);
            if (dyn == null) return;

            var cat = _categories.FirstOrDefault(c => c.TCAT_ID == dyn.TCDS_TCAT_ID);
            if (cat == null) return;

            var grp = _groups.FirstOrDefault(g => g.TG_ID == cat.TCAT_TG_ID);
            if (grp == null) return;

            _isUpdating = true;
            try
            {
                // 1) класс
                customLookUpEditClass.EditValue = grp.TG_TC_ID;

                // 2) группы под класс
                var groups = _groups.Where(g => g.TG_TC_ID == grp.TG_TC_ID).OrderBy(g => g.TG_GroupName).ToList();
                customLookUpEditGroup.Properties.DataSource = groups;
                customLookUpEditGroup.EditValue = grp.TG_ID;

                // 3) категории под группу
                var cats = _categories.Where(c => c.TCAT_TG_ID == grp.TG_ID).OrderBy(c => c.TCAT_CategoryName).ToList();
                customLookUpEditCategory.Properties.DataSource = cats;
                customLookUpEditCategory.EditValue = cat.TCAT_ID;

                // 4) признаки под категорию
                var dyns = _dynsigns.Where(d => d.TCDS_TCAT_ID == cat.TCAT_ID).OrderBy(d => d.TCDS_Name).ToList();
                customLookUpEditCatDynsign.Properties.DataSource = dyns;
                customLookUpEditCatDynsign.EditValue = dyn.TCDS_ID;
            }
            finally
            {
                _isUpdating = false;
            }
        }
        #endregion

        #region Кнопки
        private async void customActionButtonSave_Click(object sender, EventArgs e)
        {
            if (customLookUpEditCategory.EditValue == null || customLookUpEditCatDynsign.EditValue == null)
                return;

            int tgId = (int)customLookUpEditCategory.EditValue;
            int tgmId = (int)customLookUpEditCatDynsign.EditValue;

            switch (_sourceRow)
            {
                case PlanSezonAllModel p:
                    p.Tg_id_n = tgId;
                    p.Tgm_id_n = tgmId;
                    await _ANSDataService.UpdatePlanSezonAll(p);

                    if (p.Men.ToString() == "10")
                    {
                        var id_spr = customLookUpEditSpravNoskiDetal.EditValue as int?;
                        var TCDS_id = customLookUpEditCatDynsign.EditValue as int?;
                        await _ANSDataService.UpdateSpravNoskiDetal(id_spr, TCDS_id); 
                    }
                    break;

                case ArtKomplektModel a:
                    a.Tg_id_n = tgId;
                    a.Tgm_id_n = tgmId;
                    await _ANSDataService.UpdateArtKomplekt(a);
                    break;
            }

            DialogResult = DialogResult.OK;
            Close();

        }
        private void customButtonOtm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

    }
}
