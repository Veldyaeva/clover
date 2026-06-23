#nullable enable

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Service
{
    /// <summary>
    /// Настройка редакторов справочников и каскадов ГОСТ для режима редактирования ArticulControl.
    /// </summary>
    public sealed class ArticulControlEditBinder
    {
        private readonly System.Windows.Forms.ComboBox _cbTm;
        private readonly System.Windows.Forms.ComboBox _cbSeason;
        private readonly System.Windows.Forms.ComboBox _cbGrupMen;
        private readonly System.Windows.Forms.ComboBox _cbCountry;
        private readonly System.Windows.Forms.ComboBox _cbAssort;
        private readonly SearchLookUpEdit _cbTkan;
        private readonly SearchLookUpEdit _lookUpGost;
        private readonly SearchLookUpEdit _lookUpGostGrup;
        private readonly TextEdit _txbIdGost;
        private readonly MemoEdit _txbOpiGost;
        private readonly TextEdit _txbPo;
        private readonly BindingSource _gostGrupSource;

        private BindingSource? _dataSource;
        private bool _eventsWired;

        public ArticulControlEditBinder(
            System.Windows.Forms.ComboBox cbTm,
            System.Windows.Forms.ComboBox cbSeason,
            System.Windows.Forms.ComboBox cbGrupMen,
            System.Windows.Forms.ComboBox cbCountry,
            System.Windows.Forms.ComboBox cbAssort,
            SearchLookUpEdit cbTkan,
            SearchLookUpEdit lookUpGost,
            SearchLookUpEdit lookUpGostGrup,
            TextEdit txbIdGost,
            MemoEdit txbOpiGost,
            TextEdit txbPo,
            BindingSource gostGrupSource)
        {
            _cbTm = cbTm ?? throw new ArgumentNullException(nameof(cbTm));
            _cbSeason = cbSeason ?? throw new ArgumentNullException(nameof(cbSeason));
            _cbGrupMen = cbGrupMen ?? throw new ArgumentNullException(nameof(cbGrupMen));
            _cbCountry = cbCountry ?? throw new ArgumentNullException(nameof(cbCountry));
            _cbAssort = cbAssort ?? throw new ArgumentNullException(nameof(cbAssort));
            _cbTkan = cbTkan ?? throw new ArgumentNullException(nameof(cbTkan));
            _lookUpGost = lookUpGost ?? throw new ArgumentNullException(nameof(lookUpGost));
            _lookUpGostGrup = lookUpGostGrup ?? throw new ArgumentNullException(nameof(lookUpGostGrup));
            _txbIdGost = txbIdGost ?? throw new ArgumentNullException(nameof(txbIdGost));
            _txbOpiGost = txbOpiGost ?? throw new ArgumentNullException(nameof(txbOpiGost));
            _txbPo = txbPo ?? throw new ArgumentNullException(nameof(txbPo));
            _gostGrupSource = gostGrupSource ?? throw new ArgumentNullException(nameof(gostGrupSource));
        }

        public void ConfigureLookups()
        {
            ConfigureCombo(_cbTm, CommonSpravArticulEditAdvance.Tms, nameof(TmModel.Kle_naimen), nameof(TmModel.M_id_gl));
            ConfigureCombo(_cbSeason, CommonSpravArticulEditAdvance.Seasons, nameof(Szon_newModel.Txt), nameof(Szon_newModel.N));
            ConfigureCombo(_cbGrupMen, CommonSpravArticulEditAdvance.GrupMen, nameof(GrupMenModel.Name), nameof(GrupMenModel.Men_int));
            ConfigureCombo(_cbCountry, CommonSpravArticulEditAdvance.Countries, nameof(CountryModel.frm_country), nameof(CountryModel.frm_cu_id));
            ConfigureCombo(_cbAssort, CommonSpravArticulEditAdvance.Assorts, nameof(AssortModel.txt_v), nameof(AssortModel.kod_v));

            ConfigureTkanLookup(_cbTkan);
            ConfigureGostLookup(_lookUpGost);
            ConfigureGostGrupLookup(_lookUpGostGrup);
        }

        public void RegisterEditPropertyMappings(
            IDictionary<Control, PropertyInfo> map,
            Type modelType)
        {
            map[_cbTm] = modelType.GetProperty(nameof(ArticulModel.Id_country))!;
            map[_cbSeason] = modelType.GetProperty(nameof(ArticulModel.Baza))!;
            map[_cbGrupMen] = modelType.GetProperty(nameof(ArticulModel.Grupp))!;
            map[_cbCountry] = modelType.GetProperty(nameof(ArticulModel.Id_country))!;
            map[_cbAssort] = modelType.GetProperty(nameof(ArticulModel.Kod_v))!;
            map[_cbTkan] = modelType.GetProperty(nameof(ArticulModel.Tkb))!;
            map[_lookUpGost] = modelType.GetProperty(nameof(ArticulModel.Id_gost))!;
            map[_lookUpGostGrup] = modelType.GetProperty(nameof(ArticulModel.Ag_id))!;
            map[_txbPo] = modelType.GetProperty(nameof(ArticulModel.Po))!;
        }

        public void WireCascadeEvents(BindingSource dataSource)
        {
            if (_eventsWired && ReferenceEquals(_dataSource, dataSource))
                return;

            UnwireCascadeEvents();

            _dataSource = dataSource;
            _lookUpGost.EditValueChanged += LookUpGost_EditValueChanged;
            _lookUpGostGrup.EditValueChanged += LookUpGostGrup_EditValueChanged;
            _txbIdGost.EditValueChanged += TxbIdGost_EditValueChanged;
            _eventsWired = true;

            RefreshGostGrupFilter();
            UpdateOpiGost();
        }

        public void UnwireCascadeEvents()
        {
            if (!_eventsWired)
                return;

            _lookUpGost.EditValueChanged -= LookUpGost_EditValueChanged;
            _lookUpGostGrup.EditValueChanged -= LookUpGostGrup_EditValueChanged;
            _txbIdGost.EditValueChanged -= TxbIdGost_EditValueChanged;
            _eventsWired = false;
            _dataSource = null;
        }

        public IReadOnlyDictionary<string, Control> BuildEditPropertyToControlMap()
        {
            return new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(ArticulModel.Id_country)] = _cbCountry,
                [nameof(ArticulModel.Baza)] = _cbSeason,
                [nameof(ArticulModel.Grupp)] = _cbGrupMen,
                [nameof(ArticulModel.Kod_v)] = _cbAssort,
                [nameof(ArticulModel.Tkb)] = _cbTkan,
                [nameof(ArticulModel.Id_gost)] = _lookUpGost,
                [nameof(ArticulModel.Ag_id)] = _lookUpGostGrup,
                [nameof(ArticulModel.Po)] = _txbPo,
            };
        }

        private void TxbIdGost_EditValueChanged(object? sender, EventArgs e)
            => LookUpGost_EditValueChanged(sender, e);

        private void LookUpGost_EditValueChanged(object? sender, EventArgs e)
        {
            UpdateOpiGost();
            RefreshGostGrupFilter();
        }

        private void LookUpGostGrup_EditValueChanged(object? sender, EventArgs e)
        {
            if (_dataSource?.Current is not ArticulModel model)
                return;

            if (_lookUpGostGrup.EditValue is int agId)
                model.Grup = CommonSpravArticulEditAdvance.GostGroupNames
                    .FirstOrDefault(x => x.Ag_id == agId)?.Ag_name_sokr ?? string.Empty;
            else
                model.Grup = string.Empty;
        }

        private void UpdateOpiGost()
        {
            if (_lookUpGost.EditValue is int idGost)
                _txbOpiGost.Text = CommonSpravArticulEditAdvance.Gosts
                    .FirstOrDefault(x => x.Id_gost == idGost)?.Opi_gost ?? string.Empty;
            else if (int.TryParse(Convert.ToString(_txbIdGost.EditValue), out var parsed))
                _txbOpiGost.Text = CommonSpravArticulEditAdvance.Gosts
                    .FirstOrDefault(x => x.Id_gost == parsed)?.Opi_gost ?? string.Empty;
            else
                _txbOpiGost.Text = string.Empty;
        }

        private void RefreshGostGrupFilter()
        {
            int? idGost = _lookUpGost.EditValue as int?;
            if (idGost == null && int.TryParse(Convert.ToString(_txbIdGost.EditValue), out var parsed))
                idGost = parsed;

            if (idGost == null)
            {
                _gostGrupSource.DataSource = Array.Empty<GostGrupIzdViewModel>();
                return;
            }

            _gostGrupSource.DataSource = CommonSpravArticulEditAdvance.GostGroupNames
                .Where(x => x.Id_gost == idGost.Value)
                .ToList();
        }

        private static void ConfigureCombo<T>(System.Windows.Forms.ComboBox combo, IReadOnlyList<T> data, string displayMember, string valueMember)
        {
            combo.DataSource = data;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private static void ConfigureGostLookup(SearchLookUpEdit sle)
        {
            sle.Properties.DataSource = CommonSpravArticulEditAdvance.Gosts;
            sle.Properties.DisplayMember = nameof(GostModel.Name_gost);
            sle.Properties.ValueMember = nameof(GostModel.Id_gost);
            sle.Properties.NullText = "Не выбрано";

            var view = EnsureGridView(sle);
            view.OptionsView.ShowColumnHeaders = true;
            view.OptionsView.ShowIndicator = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            view.Columns.Clear();
            view.Columns.AddVisible(nameof(GostModel.Name_gost), "ГОСТ");
            view.BestFitColumns();
        }

        private void ConfigureGostGrupLookup(SearchLookUpEdit sle)
        {
            sle.Properties.DataSource = _gostGrupSource;
            sle.Properties.DisplayMember = nameof(GostGrupIzdViewModel.Ag_name_sokr);
            sle.Properties.ValueMember = nameof(GostGrupIzdViewModel.Ag_id);
            sle.Properties.NullText = "Не выбрано";

            var view = EnsureGridView(sle);
            view.OptionsView.ShowColumnHeaders = true;
            view.OptionsView.ShowIndicator = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            view.Columns.Clear();
            view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Id_gost), "ГостId");
            view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_name_sokr), "Сокращенное название");
            view.Columns.AddVisible(nameof(GostGrupIzdViewModel.N_i), "Наименование");
            view.BestFitColumns();
        }

        private static void ConfigureTkanLookup(SearchLookUpEdit sle)
        {
            sle.Properties.DataSource = CommonSpravArticulEditAdvance.Tkans;
            sle.Properties.DisplayMember = nameof(SpArticulTkanSokr.Tkan);
            sle.Properties.ValueMember = nameof(SpArticulTkanSokr.Tkb);
            sle.Properties.NullText = "Не выбрано";

            var view = EnsureGridView(sle);
            view.OptionsView.ShowColumnHeaders = true;
            view.OptionsView.ShowIndicator = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            view.Columns.Clear();
            view.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkb), "Сокращенное наим");
            view.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkan), "Наименование");
            view.BestFitColumns();
        }

        private static GridView EnsureGridView(SearchLookUpEdit sle)
        {
            if (sle.Properties.PopupView is not GridView view)
                throw new InvalidOperationException("PopupView не GridView");

            return view;
        }
    }
}
