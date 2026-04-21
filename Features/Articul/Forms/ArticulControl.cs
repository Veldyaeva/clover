using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class ArticulControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DatabaseHelper _dbHelperAce;
        private bool _isInitialized;
        private readonly ILogger _logger = new FileLogger();
        private const string LoggerContext = "ArticulControl";
        private readonly DbService _dbService;
        private readonly ArticulDataService _articulDataService = new ArticulDataService();
        private readonly Dictionary<Control, System.Reflection.PropertyInfo> _controlToArtNormProperty = new Dictionary<Control, System.Reflection.PropertyInfo>();
        //private readonly BindingSource _bs = new BindingSource();
        private readonly DXErrorProvider _dx = new DXErrorProvider();

        private bool _isReadOnly = true;
        private readonly Dictionary<string, Control> _propertyToControl =
    new(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<Control, Color> _originalBackColors = new();
        private readonly Dictionary<Control, Color> _originalForeColors = new();
        private readonly Dictionary<Control, Color> _originalEditorBackColors = new();
        private readonly Dictionary<Control, Color> _originalEditorBorderColors = new();
        private readonly Dictionary<Control, bool> _originalUseForeColors = new();
        private readonly Dictionary<Control, bool> _originalUseBackColors = new();
        private readonly Dictionary<Control, bool> _originalUseBorderColors = new();
        private readonly Dictionary<Control, (CheckBoxStyle Style, Color Checked, Color Unchecked, Color Grayed)> _originalCheckBoxStyles = new();
        private readonly FieldComparisonService _comparisonService = new();
        private bool _comparisonMapBuilt;
        /// <summary>
        /// Построить маппинг между именами свойств модели и контролами для сравнения.
        /// </summary>
        private void BuildComparisonMap()
        {
            _propertyToControl.Clear();

            foreach (var kv in _controlToArtNormProperty)
            {
                if (kv.Key != null && kv.Value != null)
                    _propertyToControl[kv.Value.Name] = kv.Key;
            }
            /*
            RegisterSeries("txbNorm_t", "Norm_t");
            RegisterSeries("txbTkanSeb_t", "Seb_t");
            RegisterSeries("txbBrak", "Brak_t");
            RegisterSeries("txtBrakPercent", "Brak_percent");
            RegisterSeries("txbKfKach", "Kf_tkan_kach");
            RegisterSeries("txbOpis_t", "Opis_t");
            RegisterSeries("tkb", "Tkb");
            */
            _propertyToControl[nameof(SpArticulPreviewModel.Ag_id)] = txbGrup;
        }

        private void RegisterSeries(string controlPrefix, string propertyPrefix)
        {
            foreach (var c in GetAllControls(this))
            {
                if (string.IsNullOrWhiteSpace(c.Name)) continue;
                if (!c.Name.StartsWith(controlPrefix, StringComparison.Ordinal)) continue;

                var suffix = GetNumericSuffix(c.Name);
                if (suffix == null) continue;

                _propertyToControl[propertyPrefix + suffix] = c;
            }
        }

        private static string? GetNumericSuffix(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            var i = name.Length - 1;
            while (i >= 0 && char.IsDigit(name[i])) i--;
            var start = i + 1;
            return start < name.Length ? name.Substring(start) : null;
        }

        public ComparisonResult CompareAndHighlight(IEnumerable<FieldComparisonItem> items)
        {
            var result = new ComparisonResult();
            try
            {
                ClearComparisonHighlight();
                //BuildComparisonMap();
                EnsureComparisonMap();
                result = _comparisonService.Compare(Model, items);

                foreach (var mismatch in result.Mismatches)
                {
                    if (_propertyToControl.TryGetValue(mismatch.PropertyName, out var control))
                        MarkMismatch(control, mismatch);
                }
            }
            catch (Exception ex)
            {
                _=SafeLogAsync(() => _logger.LogErrorAsync(ex, $"{LoggerContext}.CompareAndHighlight"));
                return result;
            }
            return result;
        }
        private void EnsureComparisonMap()
        {
            if (_comparisonMapBuilt) return;
            BuildComparisonMap();
            _comparisonMapBuilt = true;
        }
        public void ClearComparisonHighlight()
        {
            foreach (var kv in _originalBackColors.ToList())
                ClearMark(kv.Key);

            _dx.ClearErrors();
        }

        private void MarkMismatch(Control c, FieldMismatch mismatch)
        {
            var tooltipText = BuildMismatchTooltipText(mismatch);

            if (!_originalBackColors.ContainsKey(c))
                _originalBackColors[c] = c.BackColor;

            if (c is DevExpress.XtraEditors.CheckEdit ce)
            {
                if (!_originalForeColors.ContainsKey(c))
                {
                    _originalForeColors[c] = ce.Properties.Appearance.ForeColor;
                    _originalUseForeColors[c] = ce.Properties.Appearance.Options.UseForeColor;
                }

                if (!_originalEditorBackColors.ContainsKey(c))
                {
                    _originalEditorBackColors[c] = ce.Properties.Appearance.BackColor;
                    _originalUseBackColors[c] = ce.Properties.Appearance.Options.UseBackColor;
                }

                if (!_originalEditorBorderColors.ContainsKey(c))
                {
                    _originalEditorBorderColors[c] = ce.Properties.Appearance.BorderColor;
                    _originalUseBorderColors[c] = ce.Properties.Appearance.Options.UseBorderColor;
                }

                if (!_originalCheckBoxStyles.ContainsKey(c))
                {
                    _originalCheckBoxStyles[c] = (
                        ce.Properties.CheckBoxOptions.Style,
                        ce.Properties.CheckBoxOptions.SvgColorChecked,
                        ce.Properties.CheckBoxOptions.SvgColorUnchecked,
                        ce.Properties.CheckBoxOptions.SvgColorGrayed);
                }

                ce.Properties.Appearance.ForeColor = Color.Red;
                ce.Properties.Appearance.BackColor = Color.MistyRose;
                ce.Properties.Appearance.BorderColor = Color.Red;
                ce.ForeColor = Color.Red;
                ce.Properties.Appearance.Options.UseForeColor = true;
                ce.Properties.Appearance.Options.UseBackColor = true;
                ce.Properties.Appearance.Options.UseBorderColor = true;
                ce.Properties.CheckBoxOptions.Style = CheckBoxStyle.SvgCheckBox1;
                ce.Properties.CheckBoxOptions.SvgColorChecked = Color.Red;
                ce.Properties.CheckBoxOptions.SvgColorUnchecked = Color.Red;
                ce.Properties.CheckBoxOptions.SvgColorGrayed = Color.Red;
                _dx.SetError(ce, tooltipText);
            }
            else if (c is BaseEdit be)
            {
                be.Properties.Appearance.BackColor = Color.MistyRose;
                _dx.SetError(be, tooltipText);
            }
            else
            {
                c.BackColor = Color.MistyRose;
                _dx.SetError(c, tooltipText);
            }
        }

        private static string BuildMismatchTooltipText(FieldMismatch mismatch)
        {
            return $"Значение отличается.{Environment.NewLine}Ожидаемое значение: {FormatComparisonValue(mismatch.ExpectedDisplayValue ?? mismatch.ExpectedValue)}";
        }

        private static string FormatComparisonValue(object? value)
        {
            if (value == null)
                return "(пусто)";

            return value switch
            {
                string s => string.IsNullOrWhiteSpace(s) ? "(пусто)" : s.Trim(),
                bool b => b ? "Да" : "Нет",
                DateTime dt => dt.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture),
                DateTimeOffset dto => dto.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture),
                IFormattable formattable => formattable.ToString(null, CultureInfo.CurrentCulture) ?? "(пусто)",
                _ => value.ToString() ?? "(пусто)"
            };
        }

        private void ClearMark(Control c)
        {
            if (c is DevExpress.XtraEditors.CheckEdit ce)
            {
                if (_originalForeColors.TryGetValue(c, out var fore))
                {
                    ce.Properties.Appearance.ForeColor = fore;
                    _originalForeColors.Remove(c);
                }

                if (_originalUseForeColors.TryGetValue(c, out var use))
                {
                    ce.Properties.Appearance.Options.UseForeColor = use;
                    _originalUseForeColors.Remove(c);
                }

                if (_originalEditorBackColors.TryGetValue(c, out var back))
                {
                    ce.Properties.Appearance.BackColor = back;
                    _originalEditorBackColors.Remove(c);
                }

                if (_originalUseBackColors.TryGetValue(c, out var useBack))
                {
                    ce.Properties.Appearance.Options.UseBackColor = useBack;
                    _originalUseBackColors.Remove(c);
                }

                if (_originalEditorBorderColors.TryGetValue(c, out var border))
                {
                    ce.Properties.Appearance.BorderColor = border;
                    _originalEditorBorderColors.Remove(c);
                }

                if (_originalUseBorderColors.TryGetValue(c, out var useBorder))
                {
                    ce.Properties.Appearance.Options.UseBorderColor = useBorder;
                    _originalUseBorderColors.Remove(c);
                }

                if (_originalCheckBoxStyles.TryGetValue(c, out var originalStyle))
                {
                    ce.Properties.CheckBoxOptions.Style = originalStyle.Style;
                    ce.Properties.CheckBoxOptions.SvgColorChecked = originalStyle.Checked;
                    ce.Properties.CheckBoxOptions.SvgColorUnchecked = originalStyle.Unchecked;
                    ce.Properties.CheckBoxOptions.SvgColorGrayed = originalStyle.Grayed;
                    _originalCheckBoxStyles.Remove(c);
                }
            }
            else
            {
                if (!_originalBackColors.TryGetValue(c, out var color))
                    return;

                if (c is BaseEdit be)
                    be.Properties.Appearance.BackColor = color;
                else
                    c.BackColor = color;
            }

            _originalBackColors.Remove(c);
            _dx.SetError(c, "");
        }

        private void LogSuccess(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogEventAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogWarning(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogWarningAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogError(Exception ex, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogErrorAsync(ex, $"{LoggerContext}.{scope}"));
        }

        private static async Task SafeLogAsync(Func<Task> writeLog)
        {
            try
            {
                await writeLog().ConfigureAwait(false);
            }
            catch
            {
                // Логирование не должно ломать UI.
            }
        }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                ApplyReadOnlyState();
                RecreateBindingsUpdateMode();
            }
        }

        public SpArticulPreviewModel Model => _bs?.Current as SpArticulPreviewModel;

        public ArticulControl()
        {
            _dbHelperAce = new DatabaseHelper();
            _dbService = new DbService(_dbHelperAce);
            InitializeComponent();

            _dx.ContainerControl = this;
        }
        private BindingSource _bs;

        public void BindTo(BindingSource source)
        {
            if (ReferenceEquals(_bs, source)) return;
            _bs = source;
            _dx.DataSource = _bs;

            // Гарантируем, что маппинг контролов подготовлен даже если Load уже прошел
            // или еще не наступил.
            if (_controlToArtNormProperty.Count == 0)
                AttachChangeHandlers();

            InitializeBindings();
            ApplyReadOnlyState();
            LogSuccess("BindingSource успешно привязан к карточке артикула.", nameof(BindTo));
        }

        public async Task LoadImageAsync(string kodd)
        {
            if (string.IsNullOrWhiteSpace(kodd))
            {
                ClearImage();
                LogWarning("Пустой Kodd при загрузке эскиза, изображение очищено.", nameof(LoadImageAsync));
                return;
            }

            var imagePath = await _articulDataService.GetFileEskizForKod(kodd);
            pictureBoxArticul.ImageLocation = string.IsNullOrWhiteSpace(imagePath) ? null : imagePath;
        }

        public void ClearImage()
        {
            pictureBoxArticul.ImageLocation = null;
            pictureBoxArticul.Image = null;
        }

        private async void ArticulControl_Load(object sender, EventArgs e)
        {
            try
            {
                if (!_isInitialized)
                    AttachChangeHandlers();

                // На момент Load внешний BindingSource может быть еще не передан через BindTo().
                if (_bs != null)
                    InitializeBindings();

                ApplyReadOnlyState();      // учитываем начальный режим
                _isInitialized = true;
                LogSuccess("Контрол артикула успешно инициализирован.", nameof(ArticulControl_Load));
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(ArticulControl_Load));
            }
        }

        ///// <summary> Привязать карточку к модели (загрузка/переключение артикула) </summary>
        //public void BindModel(SpArticulPreviewModel? model)
        //{
        //    // чтобы не было дёрганий и лишних событий при массовой установке:
        //    _bs.RaiseListChangedEvents = false;
        //    try
        //    {
        //        _bs.DataSource = model ?? new SpArticulPreviewModel();
        //    }
        //    finally
        //    {
        //        _bs.RaiseListChangedEvents = true;
        //        _bs.ResetBindings(false);
        //    }
        //}

        private void InitializeBindings()
        {
            if (_bs == null) return;

            // тип для дизайнерской поддержки, но по сути — держим один объект
            _bs.DataSource ??= new SpArticulPreviewModel();
            _bs.ResetBindings(false);

            foreach (var kv in _controlToArtNormProperty)
            {
                var control = kv.Key;
                var prop = kv.Value;
                if (control == null || prop == null) continue;

                control.DataBindings.Clear();

                var (controlProp, updateMode) = GetBindingTarget(control);
                if (string.IsNullOrEmpty(controlProp)) continue;

                var binding = new Binding(
                    controlProp,
                    _bs,
                    prop.Name,
                    formattingEnabled: true,
                    dataSourceUpdateMode: updateMode);

                // DateTime? <-> MaskedTextBox.Text
                if (control is MaskedTextBox)
                {
                    binding.Format += (_, e) =>
                    {
                        if (e.Value == null) { e.Value = ""; return; }
                        if (e.Value is DateTime dt) e.Value = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    };
                    binding.Parse += (_, e) =>
                    {
                        var s = StringNormalizer.TrimOrNull(Convert.ToString(e.Value));
                        if (string.IsNullOrWhiteSpace(s)) { e.Value = null; return; }
                        if (DateTime.TryParse(s, out var dt)) e.Value = dt;
                    };
                }

                control.DataBindings.Add(binding);
            }
        }
        private void RecreateBindingsUpdateMode()
        {
            // пересоздаём бинды с новым UpdateMode
            InitializeBindings();
            _bs?.ResetBindings(false);
        }

        private (string ControlPropertyName, DataSourceUpdateMode UpdateMode) GetBindingTarget(Control control)
        {
            // В read-only режиме не пишем обратно в модель
            var mode = _isReadOnly ? DataSourceUpdateMode.Never : DataSourceUpdateMode.OnPropertyChanged;

            // CustomTextBox => TextBoxBase
            if (control is TextBoxBase)
                return ("Text", mode);

            // чекбоксы — Checked
            if (control is CheckBox || control.GetType().Name.Contains("CheckBox"))
                return ("Checked", mode);

            // если где-то появятся DevExpress editors
            if (control is BaseEdit)
                return ("EditValue", mode);

            return (string.Empty, DataSourceUpdateMode.Never);
        }

        private void ApplyReadOnlyState()
        {
            foreach (Control c in GetAllControls(this))
            {
                switch (c)
                {
                    case TextBoxBase tb:
                        tb.ReadOnly = _isReadOnly;
                        tb.TabStop = !_isReadOnly;
                        break;

                    case CheckBox cb:
                        cb.Enabled = !_isReadOnly;
                        cb.TabStop = !_isReadOnly;
                        break;

                    case BaseEdit be:
                        be.Properties.ReadOnly = _isReadOnly;
                        be.TabStop = !_isReadOnly;
                        break;
                }
            }
        }

        private static IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var cc in GetAllControls(c)) yield return cc;
            }
        }
        private void AttachChangeHandlers()
        {
            // Инициализируем маппинг Control -> PropertyInfo один раз
            #region заполнение блока основных данных артикула
            _controlToArtNormProperty.Clear();
            var artType = typeof(SpArticulPreviewModel);
            _controlToArtNormProperty[txbKod] = artType.GetProperty(nameof(SpArticulPreviewModel.Kod));
            _controlToArtNormProperty[txbTkb] = artType.GetProperty(nameof(SpArticulPreviewModel.Tkb));
            _controlToArtNormProperty[txbArticul] = artType.GetProperty(nameof(SpArticulPreviewModel.Articul));
            _controlToArtNormProperty[txbPo] = artType.GetProperty(nameof(SpArticulPreviewModel.Po));
            _controlToArtNormProperty[txbMod] = artType.GetProperty(nameof(SpArticulPreviewModel.Mod));
            _controlToArtNormProperty[txbTM] = artType.GetProperty(nameof(SpArticulPreviewModel.TmName));
            _controlToArtNormProperty[txbSeason] = artType.GetProperty(nameof(SpArticulPreviewModel.SeasonName));
            _controlToArtNormProperty[txbAssort] = artType.GetProperty(nameof(SpArticulPreviewModel.AssortName));
            _controlToArtNormProperty[txbCountry] = artType.GetProperty(nameof(SpArticulPreviewModel.CountryName));
            _controlToArtNormProperty[txbGrupMenName] = artType.GetProperty(nameof(SpArticulPreviewModel.GrupMenName));
            _controlToArtNormProperty[mtbDateOpis] = artType.GetProperty(nameof(SpArticulPreviewModel.DateOpis));
            _controlToArtNormProperty[txbGrup] = artType.GetProperty(nameof(SpArticulPreviewModel.Grup));
            _controlToArtNormProperty[txbIdGost] = artType.GetProperty(nameof(SpArticulPreviewModel.Id_gost));
            _controlToArtNormProperty[txbNameGost] = artType.GetProperty(nameof(SpArticulPreviewModel.GostName));
            _controlToArtNormProperty[txbOpiGost] = artType.GetProperty(nameof(SpArticulPreviewModel.GostOpi));
            _controlToArtNormProperty[txbSost] = artType.GetProperty(nameof(SpArticulPreviewModel.Sost));
            _controlToArtNormProperty[txbSost2] = artType.GetProperty(nameof(SpArticulPreviewModel.Sost2));
            _controlToArtNormProperty[txbSost3] = artType.GetProperty(nameof(SpArticulPreviewModel.Sost3));
            _controlToArtNormProperty[txbRazm] = artType.GetProperty(nameof(SpArticulPreviewModel.Razm));
            _controlToArtNormProperty[txbScNomer] = artType.GetProperty(nameof(SpArticulPreviewModel.ScNomer));
            _controlToArtNormProperty[txbKodTnved] = artType.GetProperty(nameof(SpArticulPreviewModel.Kod_tnved));
            _controlToArtNormProperty[txbNDS] = artType.GetProperty(nameof(SpArticulPreviewModel.Nds));
            #endregion



            #region галки с отделками
            //галки вяз отделки
            _controlToArtNormProperty[chbKombIzd] = artType.GetProperty(nameof(SpArticulPreviewModel.KombIzdFlag));
            _controlToArtNormProperty[chbKombDet] = artType.GetProperty(nameof(SpArticulPreviewModel.KombDetFlag));
            _controlToArtNormProperty[chbKruj] = artType.GetProperty(nameof(SpArticulPreviewModel.KrujFlag));
            //архив
            //  _controlToArtNormProperty[chbArh] = artType.GetProperty(nameof(SpArticulPreviewModel.ArhFlag));

            #endregion


        }
    }
}
