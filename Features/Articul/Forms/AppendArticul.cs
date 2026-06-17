using DevExpress.ChartRangeControlClient.Core;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraMap.Drawing;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BindingSource = System.Windows.Forms.BindingSource;
using ToolTip = System.Windows.Forms.ToolTip;
using System.Reflection;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AppendArticul : CustomForm
    {
        private int _typeCreate;
        private string _nn;
        private string _kod;
        private ComparisonResult _comparisonResult;


        private BindingSource _bindingSourceArticul = new BindingSource();
        private BindingSource _bindingSourceRazms = new BindingSource();
        private BindingSource _bindingSourceMatr = new BindingSource();
        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();
        private ArticulEditAdvanceService _articulEdAdvDataService = new ArticulEditAdvanceService();
        private readonly ToolTip _toolTip = new();

        private readonly ILogger _logger = new FileLogger();

        public AppendArticul(UserClass user) : base(user)
        {
            InitializeComponent();
            gridRazm.DataSource = _bindingSourceRazms;
        }

        public AppendArticul(UserClass user, string nn) : this(user)
        {
            // 0 - создание
            _typeCreate = 0;
            _nn = nn;
        }
        public AppendArticul(UserClass user, string nn, string kod, ComparisonResult comparisonResult) : this(user)
        {
            // 1 - стыковка
            _typeCreate = 1;
            _nn = nn;
            _kod = kod;
            _comparisonResult = comparisonResult;
        }

        private async void AppendArticul_Load(object sender, EventArgs e)
        {
            var curMatrTask = _createArticulMatrService.GetMatrForNNAsync(_nn);
            // пока без пометки, не знаю понадобится ли в этом варианте
            var curArticulTask = _createArticulMatrService.GetPreviewArticulAsync(_nn, _kod);
            //var curArticulTask = _articulDataService.GetByKodAsync(_kod);

            await Task.WhenAll(curMatrTask, curArticulTask);

            _bindingSourceMatr.DataSource = new BindingList<CreateArticulMatrModel>(curMatrTask.Result);
            _bindingSourceArticul.DataSource = curArticulTask.Result;

            InitializeBindings();
            BindGostRazm();

            await ConfigureControlsByMode();

            HighlightMismatches(this, _comparisonResult.Mismatches);

        }
        private void InitializeBindings()
        {
            txtKod.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Kod), true);
            txtPo.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Po), true);
            txtArticul.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Articul), true);
            txtMod.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Mod), true);
            txtSeason.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.SeasonName), true);
            txtGrupMenName.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.GrupMenName), true);
            txtTM.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.GrupMenName), true);
            txtSost.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost), true);
            txtSost2.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost2), true);
            txtSost3.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost3), true);
            chkKruj.DataBindings.Add("Checked", _bindingSourceArticul, nameof(SpArticulPreviewModel.Kruj), true);
            txtIdGost.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Id_gost), true);
            txtGostName.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Gost), true);
            txtGrup.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Grup), true);
            txtTkb.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Tkb), true);
            txtAssort.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.AssortName), true);
            txtRazmNames.DataBindings.Add("Text", _bindingSourceMatr, nameof(CreateArticulMatrModel.RazmNames), true);

            //всегда не активно
            chkKruj.Enabled = false;


        }
        private async Task ConfigureControlsByMode()
        {
            switch (_typeCreate)
            {
                case 0: // Создание
                        // Настройка для режима создания
                    break;
                case 1: // Настройка для режима стыковки

                    txtKod.Enabled = false;
                    txtPo.Enabled = false;
                    btnAccept.Text = "Применить";
                    //неактивный грид с размерами 
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridViewRazm.Columns)
                    {
                        column.OptionsColumn.ReadOnly = true;
                        column.OptionsColumn.AllowEdit = false;
                    }

                    _bindingSourceRazms.DataSource = null;


                    var curArt = _bindingSourceArticul.Current as SpArticulPreviewModel;
                    _bindingSourceRazms.DataSource = await _createArticulMatrService.GetArticulRazmAsync(curArt.Kodd);


                    break;
                default:
                    throw new InvalidOperationException("Недопустимый режим создания артикула.");
            }
        }
        private void AppendArticul_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bindingSourceArticul.Dispose();
            _bindingSourceRazms.Dispose();
            _bindingSourceMatr.Dispose();
        }
        private async Task CreateRazm()
        {
            _bindingSourceArticul.EndEdit();
            var kod = (_bindingSourceArticul.Current as SpArticulPreviewModel).Kod;
            var po = (_bindingSourceArticul.Current as SpArticulPreviewModel).Po;

            _bindingSourceRazms.DataSource = null;
            _bindingSourceRazms.DataSource = await _createArticulMatrService.GetMatrPlanRazm(_nn, kod, po);

        }
        /// <summary>
        /// обработчик нажатия на кнопки заголовка группы 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup group && e.Button is GroupBoxButton button)
            {
                string tag = button.Tag?.ToString() ?? string.Empty;

                switch (tag)
                {
                    case "lcg1AddSize":
                        await CreateRazm();
                        break;

                    default:

                        break;
                }
            }
        }
        private async void BindGostRazm()
        {
            try
            {
                var idgost = (_bindingSourceArticul.Current as SpArticulPreviewModel).Id_gost;
                var ri = repositoryItemLookUpEdit1;
                ri.DataSource = await _articulEdAdvDataService.GetGostRazmByIDAsync(idgost);
                ri.DisplayMember = nameof(GostRazmerNabViewModel.Razm);
                ri.ValueMember = nameof(GostRazmerNabViewModel.Razm);
                // Колонки выпадающего списка (по желанию)
                ri.Columns.Clear();
                ri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Razm", "Название"));

                ri.NullText = ""; // что показывать, если значение null
                ri.ShowHeader = false;
                ri.ShowFooter = false;
                ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; // запрет ввода, только выбор
                ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок размера ГОСТ");
                throw;
            }
        }
        /// <summary>
        /// проверка на минимальную длину кода артикула при попытке покинуть поле ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtKod_Validating(object sender, CancelEventArgs e)
        {
            if (!txtKod.Enabled || !txtKod.Visible)
            {
                e.Cancel = false;
                return;
            }

            string input = txtKod.Text;
            if (input.Length < 8)
            {
                e.Cancel = true;  // Останавливаем выход из поля
                MessageBox.Show("Значение должно содержать не менее 8 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKod.Focus();
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            var curRowSpArt = _bindingSourceArticul.Current as SpArticulPreviewModel;


            var validator = new AppendArticulValidator(curRowSpArt);
            try
            {
            var canLink = await validator.checkBeforPublish();

            if (canLink.IsSuccess)
            {
                switch (_typeCreate)
                {
                    case 0: // Создание
                        var listRazm = (BindingList<PlanRazmSetkaModel>)_bindingSourceRazms.DataSource;

                            // проверки для кодов размеров в режиме создания
                            canLink = await validator.CheckRazmKod(listRazm);

                        break;
                    case 1: // Настройка для режима стыковки



                        break;

                }
            }
            
               
                 ApplyAcceptableMismatches(curRowSpArt, _comparisonResult);
               
                _bindingSourceArticul.ResetBindings(false);

                if (canLink.IsSuccess)
                {
                    MessageBox.Show("Проверка прошла успешно. Артикул можно создать/стыковать.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@$"Невозможно выбрать эту модель для стыковки: {canLink.ErrorMessage}", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при проверке артикула перед публикацией");
                MessageBox.Show("Произошла ошибка при проверке артикула. Пожалуйста, попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                validator = null;
            }
            
        }
        //считывает расхождения из comparisonResult и записывает значения в модель
        private static void ApplyAcceptableMismatches<TModel>(
                TModel model,
                ComparisonResult comparisonResult)
                where TModel : class
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (comparisonResult == null)
                throw new ArgumentNullException(nameof(comparisonResult));

            foreach (FieldMismatch mismatch in comparisonResult.AcceptableMismatches)
            {
                PropertyInfo? property = typeof(TModel).GetProperty(
                    mismatch.DatabasePropertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

                if (property == null)
                    continue;

                if (!property.CanWrite)
                    continue;

                object? value = mismatch.DatabaseValue;
                property.SetValue(model, value);

            }
        }
       
        public void HighlightMismatches(Control parent,IEnumerable<FieldMismatch> mismatches)
        {
            var mismatchMap = mismatches
                .ToDictionary(x => x.PropertyName, StringComparer.OrdinalIgnoreCase);

            foreach (Control control in FieldComparisonService.GetAllControls(parent))
            {
                // сброс подсветки не нужно
                //control.BackColor = SystemColors.Window;

                foreach (Binding binding in control.DataBindings)
                {
                    string propertyName = binding.BindingMemberInfo.BindingField;

                    if (mismatchMap.TryGetValue(propertyName, out var mismatch))
                    {
                        control.BackColor = Color.MistyRose;

                        var tooltipText =
                            $"Ожидалось: {mismatch.ExpectedDisplayValue ?? mismatch.ExpectedValue ?? ""}\n" +
                            $"Фактически: {mismatch.ActualValue ?? ""}";

                        _toolTip.SetToolTip(control, tooltipText);
                        break;
                    }
                }
            }
        }


    }

}

