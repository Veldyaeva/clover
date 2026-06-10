#nullable enable

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using SewingProduction.Core.Class;
using SewingProduction.Features.Articul.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public enum ArticulControlMode
    {
        View,
        Edit
    }

    public partial class ArticulControl
    {
        private ArticulControlMode _mode = ArticulControlMode.View;
        private ArticulControlEditBinder? _editBinder;
        private readonly BindingSource _bindingSourceGostGrup = new();
        private bool _editControlsInitialized;

        private CustomComboBox? _cbTm;
        private CustomComboBox? _cbSeason;
        private CustomComboBox? _cbGrupMen;
        private CustomComboBox? _cbCountry;
        private CustomComboBox? _cbAssort;
        private CustomSearchLookUpEdit? _cbTkan;
        private CustomSearchLookUpEdit? _lookUpGost;
        private SearchLookUpEdit? _lookUpGostGrup;

        private readonly List<(LayoutControlItem View, LayoutControlItem Edit)> _viewEditLayoutPairs = new();
        private readonly HashSet<Control> _viewOnlyBindingControls = new();
        private readonly HashSet<Control> _alwaysReadOnlyControls = new();
        private readonly Dictionary<string, Control> _editPropertyToControl =
            new(StringComparer.OrdinalIgnoreCase);

        public ArticulControlMode Mode => _mode;

        public async Task EnableEditModeAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            if (_bs == null)
                throw new InvalidOperationException("Перед включением режима редактирования вызовите BindTo.");

            EnsureEditControlsCreated();

            await CommonSpravArticulEditAdvance.EnsureLoadedAsync(_dbService).ConfigureAwait(true);
            ct.ThrowIfCancellationRequested();

            _editBinder ??= CreateEditBinder();
            _editBinder.ConfigureLookups();

            AttachEditPropertyMappings();
            _editBinder.WireCascadeEvents(_bs!);

            _mode = ArticulControlMode.Edit;
            _comparisonMapBuilt = false;

            _editPropertyToControl.Clear();
            foreach (var kv in _editBinder.BuildEditPropertyToControlMap())
                _editPropertyToControl[kv.Key] = kv.Value;

            ApplyModeVisibility();
            IsReadOnly = false;

            RecreateBindingsUpdateMode();
        }

        public void SetViewMode()
        {
            if (_mode == ArticulControlMode.View)
                return;

            _editBinder?.UnwireCascadeEvents();
            _mode = ArticulControlMode.View;
            RemoveEditPropertyMappings();
            _comparisonMapBuilt = false;
            _editPropertyToControl.Clear();
            ApplyModeVisibility();
            IsReadOnly = true;
            RecreateBindingsUpdateMode();
        }

        /// <summary>
        /// Точка расширения для будущих ограничений редактирования по данным и правам.
        /// </summary>
        public void SetFieldReadOnly(string propertyName, bool readOnly)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;

            if (!_editPropertyToControl.TryGetValue(propertyName, out var control) || control == null)
                return;

            ApplyControlReadOnly(control, readOnly);
        }

        private void EnsureEditControlsCreated()
        {
            if (_editControlsInitialized)
                return;

            _cbTm = CreateComboBox(nameof(_cbTm));
            _cbSeason = CreateComboBox(nameof(_cbSeason));
            _cbGrupMen = CreateComboBox(nameof(_cbGrupMen));
            _cbCountry = CreateComboBox(nameof(_cbCountry));
            _cbAssort = CreateComboBox(nameof(_cbAssort));
            _cbTkan = CreateSearchLookUp(nameof(_cbTkan));
            _lookUpGost = CreateSearchLookUp(nameof(_lookUpGost));
            _lookUpGostGrup = CreateSearchLookUpGrup(nameof(_lookUpGostGrup));

            AddEditPair(_cbTm, layoutControlItem9);
            AddEditPair(_cbSeason, layoutControlItem14);
            AddEditPair(_cbAssort, layoutControlItem16);
            AddEditPair(_cbCountry, layoutControlItem18);
            AddEditPair(_cbGrupMen, layoutControlItem21);
            AddEditPair(_lookUpGostGrup, layoutControlItem7);
            AddEditPair(_lookUpGost, layoutControlItem12);
            AddEditPair(_cbTkan, layoutControlItem27);

            RegisterViewOnlyBindingControl(txbTM);
            RegisterViewOnlyBindingControl(txbSeason);
            RegisterViewOnlyBindingControl(txbAssort);
            RegisterViewOnlyBindingControl(txbCountry);
            RegisterViewOnlyBindingControl(txbGrupMenName);
            RegisterViewOnlyBindingControl(txbGrup);
            RegisterViewOnlyBindingControl(txbNameGost);
            RegisterViewOnlyBindingControl(txbTkb);
            RegisterViewOnlyBindingControl(txbOpiGost);

            RegisterAlwaysReadOnlyControl(txbKod);
            RegisterAlwaysReadOnlyControl(txbPo);
            RegisterAlwaysReadOnlyControl(txbRazm);
            RegisterAlwaysReadOnlyControl(txbRazmPrint);

            _editControlsInitialized = true;
        }

        private ArticulControlEditBinder CreateEditBinder()
        {
            return new ArticulControlEditBinder(
                _cbTm!,
                _cbSeason!,
                _cbGrupMen!,
                _cbCountry!,
                _cbAssort!,
                _cbTkan!,
                _lookUpGost!,
                _lookUpGostGrup!,
                txbIdGost,
                txbOpiGost,
                _bindingSourceGostGrup);
        }

        private CustomComboBox CreateComboBox(string name)
        {
            var combo = new CustomComboBox
            {
                Name = name,
                Font = customLayoutControl1.Font,
                Visible = false
            };
            customLayoutControl1.Controls.Add(combo);
            return combo;
        }

        private CustomSearchLookUpEdit CreateSearchLookUp(string name)
        {
            var sle = new CustomSearchLookUpEdit
            {
                Name = name,
                Font = customLayoutControl1.Font,
                Visible = false
            };
            sle.Properties.PopupView = new GridView();
            customLayoutControl1.Controls.Add(sle);
            return sle;
        }

        private SearchLookUpEdit CreateSearchLookUpGrup(string name)
        {
            var sle = new SearchLookUpEdit
            {
                Name = name,
                Font = customLayoutControl1.Font,
                Visible = false
            };
            sle.Properties.PopupView = new GridView();
            customLayoutControl1.Controls.Add(sle);
            return sle;
        }

        private void AddEditPair(Control editControl, LayoutControlItem viewItem)
        {
            var editItem = CloneLayoutItemForEdit(editControl, viewItem);
            var parent = viewItem.Parent;
            parent?.Add(editItem);
            _viewEditLayoutPairs.Add((viewItem, editItem));
        }

        private static LayoutControlItem CloneLayoutItemForEdit(Control editControl, LayoutControlItem template)
        {
            var item = new LayoutControlItem
            {
                Control = editControl,
                Location = template.Location,
                Size = template.Size,
                MinSize = template.MinSize,
                MaxSize = template.MaxSize,
                SizeConstraintsType = template.SizeConstraintsType,
                Text = template.Text,
                TextSize = template.TextSize,
                TextAlignMode = template.TextAlignMode,
                TextLocation = template.TextLocation,
                TextToControlDistance = template.TextToControlDistance,
                Visibility = LayoutVisibility.Never,
                Name = $"lciEdit_{editControl.Name}"
            };
            return item;
        }

        private void ApplyModeVisibility()
        {
            var showEdit = _mode == ArticulControlMode.Edit;
            foreach (var (view, edit) in _viewEditLayoutPairs)
            {
                view.Visibility = showEdit ? LayoutVisibility.Never : LayoutVisibility.Always;
                edit.Visibility = showEdit ? LayoutVisibility.Always : LayoutVisibility.Never;
                if (edit.Control != null)
                    edit.Control.Visible = showEdit;
            }
        }

        private void AttachEditPropertyMappings()
        {
            if (_editBinder == null)
                return;

            _editBinder.RegisterEditPropertyMappings(_controlToArtNormProperty, typeof(Models.SpArticulPreviewModel));
        }

        private void RemoveEditPropertyMappings()
        {
            if (_cbTm != null) _controlToArtNormProperty.Remove(_cbTm);
            if (_cbSeason != null) _controlToArtNormProperty.Remove(_cbSeason);
            if (_cbGrupMen != null) _controlToArtNormProperty.Remove(_cbGrupMen);
            if (_cbCountry != null) _controlToArtNormProperty.Remove(_cbCountry);
            if (_cbAssort != null) _controlToArtNormProperty.Remove(_cbAssort);
            if (_cbTkan != null) _controlToArtNormProperty.Remove(_cbTkan);
            if (_lookUpGost != null) _controlToArtNormProperty.Remove(_lookUpGost);
            if (_lookUpGostGrup != null) _controlToArtNormProperty.Remove(_lookUpGostGrup);
        }

        private void RegisterViewOnlyBindingControl(Control control)
            => _viewOnlyBindingControls.Add(control);

        private void RegisterAlwaysReadOnlyControl(Control control)
            => _alwaysReadOnlyControls.Add(control);

        private void ApplyAlwaysReadOnlyControls()
        {
            foreach (var control in _alwaysReadOnlyControls)
                ApplyControlReadOnly(control, true);
        }

        private static void ApplyControlReadOnly(Control control, bool readOnly)
        {
            switch (control)
            {
                case TextBoxBase tb:
                    tb.ReadOnly = readOnly;
                    tb.TabStop = !readOnly;
                    break;
                case CheckBox cb:
                    cb.Enabled = !readOnly;
                    cb.TabStop = !readOnly;
                    break;
                case BaseEdit be:
                    be.Properties.ReadOnly = readOnly;
                    be.TabStop = !readOnly;
                    break;
                case System.Windows.Forms.ComboBox combo:
                    combo.Enabled = !readOnly;
                    combo.TabStop = !readOnly;
                    break;
            }
        }

        private bool ShouldBindControl(Control control)
        {
            if (_mode == ArticulControlMode.View)
                return true;

            return !_viewOnlyBindingControls.Contains(control);
        }

        private bool IsEditBindingControl(Control control)
        {
            if (!_editControlsInitialized)
                return false;

            return ReferenceEquals(control, _cbTm)
                   || ReferenceEquals(control, _cbSeason)
                   || ReferenceEquals(control, _cbGrupMen)
                   || ReferenceEquals(control, _cbCountry)
                   || ReferenceEquals(control, _cbAssort)
                   || ReferenceEquals(control, _cbTkan)
                   || ReferenceEquals(control, _lookUpGost)
                   || ReferenceEquals(control, _lookUpGostGrup);
        }
    }
}
