#nullable enable

using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
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
        private bool _editMetadataInitialized;

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

            InitEditModeMetadata();

            await CommonSpravArticulEditAdvance.EnsureLoadedAsync(_dbService).ConfigureAwait(true);
            ct.ThrowIfCancellationRequested();

            _editBinder ??= CreateEditBinder();
            _editBinder.ConfigureLookups();

            AttachEditPropertyMappings();
            _editBinder.WireCascadeEvents(_bs);

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

        public void ApplyEditableFields(IReadOnlySet<string> editableProperties)
        {
            if (editableProperties == null)
                return;

            foreach (Control control in FieldComparisonService.GetAllControls(this))
            {
                var editable = TryResolveEditPropertyName(control, out var propertyName)
                    && editableProperties.Contains(propertyName);

                ApplyControlReadOnly(control, !editable);
            }

            ApplyAlwaysReadOnlyControls();
        }

        private void InitEditModeMetadata()
        {
            if (_editMetadataInitialized)
                return;

            _viewEditLayoutPairs.Add((layoutControlItem9, layoutItemEditTM));
            _viewEditLayoutPairs.Add((layoutControlItem14, layoutItemEditSeason));
            _viewEditLayoutPairs.Add((layoutControlItem16, layoutItemEditAssort));
            _viewEditLayoutPairs.Add((layoutControlItem18, layoutItemEditCountry));
            _viewEditLayoutPairs.Add((layoutControlItem21, layoutItemEditGrupMen));
            _viewEditLayoutPairs.Add((layoutControlItem7, layoutItemEditGostGrup));
            _viewEditLayoutPairs.Add((layoutControlItem12, layoutItemEditGost));
            _viewEditLayoutPairs.Add((layoutControlItem27, layoutItemEditTkan));

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

            _editMetadataInitialized = true;
        }

        private ArticulControlEditBinder CreateEditBinder()
        {
            return new ArticulControlEditBinder(
                cbTM,
                cbSeason,
                cbGrupMen,
                cbCountry,
                cbAssort,
                cbTkan,
                lookUpGost,
                lookUpGostGrup,
                txbIdGost,
                txbOpiGost,
                _bindingSourceGostGrup);
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
            _editBinder?.RegisterEditPropertyMappings(_controlToArtNormProperty, typeof(Models.SpArticulPreviewModel));
        }

        private void RemoveEditPropertyMappings()
        {
            _controlToArtNormProperty.Remove(cbTM);
            _controlToArtNormProperty.Remove(cbSeason);
            _controlToArtNormProperty.Remove(cbGrupMen);
            _controlToArtNormProperty.Remove(cbCountry);
            _controlToArtNormProperty.Remove(cbAssort);
            _controlToArtNormProperty.Remove(cbTkan);
            _controlToArtNormProperty.Remove(lookUpGost);
            _controlToArtNormProperty.Remove(lookUpGostGrup);
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

        private bool TryResolveEditPropertyName(Control control, out string propertyName)
        {
            foreach (var pair in _editPropertyToControl)
            {
                if (ReferenceEquals(pair.Value, control))
                {
                    propertyName = pair.Key;
                    return true;
                }
            }

            if (_controlToArtNormProperty.TryGetValue(control, out var propertyInfo)
                && propertyInfo != null)
            {
                propertyName = propertyInfo.Name;
                return true;
            }

            propertyName = string.Empty;
            return false;
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
                case System.Windows.Forms.ComboBox combo:
                    combo.Enabled = !readOnly;
                    combo.TabStop = !readOnly;
                    break;
                case BaseEdit be:
                    be.Properties.ReadOnly = readOnly;
                    be.TabStop = !readOnly;
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
            if (!_editMetadataInitialized)
                return false;

            return ReferenceEquals(control, cbTM)
                   || ReferenceEquals(control, cbSeason)
                   || ReferenceEquals(control, cbGrupMen)
                   || ReferenceEquals(control, cbCountry)
                   || ReferenceEquals(control, cbAssort)
                   || ReferenceEquals(control, cbTkan)
                   || ReferenceEquals(control, lookUpGost)
                   || ReferenceEquals(control, lookUpGostGrup);
        }
    }
}
