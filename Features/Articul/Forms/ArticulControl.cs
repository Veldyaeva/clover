using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SewingProduction.Features.Articul;
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
using DevExpress.XtraEditors.DXErrorProvider;
using System.Globalization;

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

        public void BindModel(SpArticulPreviewModel model)
        {
            if (_bs == null) throw new InvalidOperationException("Сначала вызови BindTo(bindingSource)");
            _bs.DataSource = model;
            _bs.ResetBindings(false);
        }

        public void SetKod(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod)) return;
            txbKod.Text = kod;
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
                        var s = Convert.ToString(e.Value)?.Trim();
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
            // В read-only режиме не пишем обратно в модель.
            var mode = _isReadOnly ? DataSourceUpdateMode.Never : DataSourceUpdateMode.OnPropertyChanged;

            // твои поля — CustomTextBox => TextBoxBase
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
            //архив
          //  _controlToArtNormProperty[chbArh] = artType.GetProperty(nameof(SpArticulPreviewModel.ArhFlag));

            #endregion


        }
    }
}