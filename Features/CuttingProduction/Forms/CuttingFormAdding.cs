
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using SewingProduction.Core.Class;
using SewingProduction.Extensions;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.CuttingProduction.Services;
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

namespace SewingProduction.Features.CuttingProduction.Forms
{
    public partial class CuttingFormAdding : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private readonly CuttingService _cuttingService;
        private List<appeZakrNewView> _currentAppeZakr = new List<appeZakrNewView>();
        private BindingList<appeZakrNewView> _appeZakrViewList;
        private BindingSource _appeZakrViewBindingSource;
        private LoadingScreen _loadingScreen;
        public CuttingFormAdding()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _cuttingService = new CuttingService(_dbHelper);
            _loadingScreen = new LoadingScreen();

        }

        private async void CuttingFormAdding_Load(object sender, EventArgs e)
        {
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            //Form mainForm = Application.OpenForms["CuttingFormAdding"];
            _loadingScreen.CreateOverlaySpinner(this);
            _loadingScreen.ShowOverlay();
            try
            {
                await LoadAppeZakrAsync();
            }
            finally { _loadingScreen.HideOverlay(); }
            //InitializeComboBox();
        }
        //private void InitializeComboBox()
        //{
        //    SelectNomZadComboBox.DataSource = _appeZakrViewBindingSource;
        //    SelectNomZadComboBox.ValueMember = "nom";
        //    // Не устанавливаем DisplayMember, потому что рисуем сами
        //    SelectNomZadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        //    // Owner-draw
        //    SelectNomZadComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        //    SelectNomZadComboBox.ItemHeight = 20; // фиксированная высота строки (подберите под шрифт)
        //    SelectNomZadComboBox.DrawItem += SelectNomZadComboBox_DrawItem;

        //    // Увеличим ширину выпадающего списка, чтобы вместить обе колонки
        //    SelectNomZadComboBox.DropDownWidth = 400;

        //    // Опционально — обработчик изменения выбранного элемента (если нужно обновлять другие контролы)
        //    //SelectNomZadComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        //}
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var appeZakrViewTask = Task.Run(() =>
                {
                    _appeZakrViewList = new BindingList<appeZakrNewView>();
                    _appeZakrViewBindingSource = new BindingSource { DataSource = _appeZakrViewList };
                });
                await Task.WhenAll(appeZakrViewTask);
                customLookUpEdit1.Properties.DataSource = _appeZakrViewBindingSource;
                customLookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Задание"));
                customLookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nom", "Номер"));
                customLookUpEdit1.Properties.DisplayMember = "Nom";
                customLookUpEdit1.Properties.ValueMember = "Nom";
                //SelectNomZadComboBox.DataSource = _appeZakrViewBindingSource;
                //SelectNomZadComboBox.SelectedIndex = -1;
                //SelectNomZadComboBox.ValueMember = "Nom";
                //SelectNomZadComboBox.DisplayMember = "DisplayText";
                ArticulCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Articul), true, DataSourceUpdateMode.Never);
                ModCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Mod), true, DataSourceUpdateMode.Never);
                BazaCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Baza), true, DataSourceUpdateMode.Never);
                nZvetCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.nZvet), true, DataSourceUpdateMode.Never);
                trCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Ta_id), true, DataSourceUpdateMode.Never);
                ModBlokCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Mod_blok), true, DataSourceUpdateMode.Never);
                RostCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Rost), true, DataSourceUpdateMode.Never);
                PrintCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.P), true, DataSourceUpdateMode.Never);
                PrintKolCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Kol_p), true, DataSourceUpdateMode.Never);
                PrintKolZvCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Kol_zv), true, DataSourceUpdateMode.Never);
                PrinterCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Printer), true, DataSourceUpdateMode.Never);
                NabivAllCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Nabiv_all), true, DataSourceUpdateMode.Never);
                PTampCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.P_tamp), true, DataSourceUpdateMode.Never);
                GofpCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Gofp), true, DataSourceUpdateMode.Never);
                VishCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.V), true, DataSourceUpdateMode.Never);
                KolVCustomLabel.DataBindings.Add("Text", _appeZakrViewBindingSource, nameof(appeZakrNewView.Kol_v), true, DataSourceUpdateMode.Never);
                LazerCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Laz), true, DataSourceUpdateMode.Never);
                ZvetAllCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Zvet_all), true, DataSourceUpdateMode.Never);
                PPresCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.P_pres), true, DataSourceUpdateMode.Never);
                StraCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Stra), true, DataSourceUpdateMode.Never);
                PoetCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Poet), true, DataSourceUpdateMode.Never);
                StirCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Stir), true, DataSourceUpdateMode.Never);
                BusCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Bus), true, DataSourceUpdateMode.Never);
                BdCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Bd), true, DataSourceUpdateMode.Never);
                DtfCustomCheckBox.DataBindings.Add("Checked", _appeZakrViewBindingSource, nameof(appeZakrNewView.Dtf_print), true, DataSourceUpdateMode.Never);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private async Task LoadAppeZakrAsync()
        {
            try
            {
                _appeZakrViewBindingSource.Clear();
                _appeZakrViewBindingSource.ResetBindings(false);
                var Data = await _cuttingService.GetAppeZakrViewAsync();
                if (Data != null)
                {
                    await _logger.LogEventAsync($"Получены данные AppeZakrNewView", "LoadCuttingFormAdding");
                    await this.InvokeAsync(() =>
                    {
                        _currentAppeZakr = Data;                // Обновляем текущую модель
                        _appeZakrViewBindingSource.DataSource = _currentAppeZakr; // Привязываем данные к форме
                    });
                    _appeZakrViewList.Add(Data[0]);
                    _appeZakrViewBindingSource.ResetBindings(false);

                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные appeZakrView", "LoadAppeZakrView");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных appeZakrView");
            }


        }

        private void customLookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            LookUpEdit lookUpEdit = (LookUpEdit)sender;
            if (lookUpEdit.EditValue != null)
            {
                var item = _appeZakrViewBindingSource.List.OfType<appeZakrNewView>()
                .FirstOrDefault(x => x.Nom.Equals(lookUpEdit.EditValue));
                if (item != null)
                {
                    _appeZakrViewBindingSource.Position = _appeZakrViewBindingSource.IndexOf(item);
                }
            }
        }
    }
}
