using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Extensions;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;

namespace SewingProduction
{
    [DefaultEvent(nameof(TextChanged))]
    public partial class FurnitZayavView : UserControl
    {
        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add => tbKodF.TextChanged += value;
            remove => tbKodF.TextChanged -= value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get => tbKodF.Text;
            set => tbKodF.Text = value;
        }

        [Browsable(true)]
        public event EventHandler TextChangedViewType
        {
            add => tbViewType.TextChanged += value;
            remove => tbViewType.TextChanged -= value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ViewType
        {
            get => tbViewType.Text;
            set => tbViewType.Text = value;
        }
        private readonly DatabaseHelper _dbHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly FurnitService _furnitService;

        private FurnitNView _currentFurnitNViewData = new FurnitNView();
        private BindingList<FurnitNView> _furnitNViewBindingList;
        private BindingSource _furnitNViewBindingSource;

        private List<FurnitArtView> _currentFurnitArtViewData = new List<FurnitArtView>();
        private BindingList<FurnitArtView> _furnitArtViewBindingList;
        private BindingSource _furnitArtViewBindingSource;

        private List<FurnitPach> _currentFurnitPachData = new List<FurnitPach>();
        private BindingList<FurnitPach> _furnitPachBindingList;
        private BindingSource _furnitPachBindingSource;

        private List<FurnitF> _currentFurnitFData = new List<FurnitF>();
        private BindingList<FurnitF> _furnitFbindingList;
        private BindingSource _furnitFBindingSource;

        private List<FurnitFIt> _currentFurnitFItData = new List<FurnitFIt>();
        private BindingList<FurnitFIt> _furnitFItBindingList;
        private BindingSource _furnitFItBindingSource;

        public FurnitZayavView()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _furnitService = new FurnitService(_dbHelper);
            ThemeManager.UpdateTheme(this);
        }

        private async Task InitializeBindingsAsync()
        {
            var furnitNViewTask = Task.Run(() =>
            {
                _furnitNViewBindingList = new BindingList<FurnitNView>();
                _furnitNViewBindingSource = new BindingSource { DataSource = _furnitNViewBindingList };
            });
            var furnitArtViewTask = Task.Run(() =>
            {
                _furnitArtViewBindingList = new BindingList<FurnitArtView>();
                _furnitArtViewBindingSource = new BindingSource { DataSource = _furnitArtViewBindingList };
            });
            var furnitPachTask = Task.Run(() =>
            {
                _furnitPachBindingList = new BindingList<FurnitPach>();
                _furnitPachBindingSource = new BindingSource { DataSource = _furnitPachBindingList };
            });
            var furnitFTask = Task.Run(() =>
            {
                _furnitFbindingList = new BindingList<FurnitF>();
                _furnitFBindingSource = new BindingSource { DataSource = _furnitFbindingList };
            });
            var furnitFItTask = Task.Run(() =>
            {
                _furnitFItBindingList = new BindingList<FurnitFIt>();
                _furnitFItBindingSource = new BindingSource { DataSource = _furnitFItBindingList };
            });

            await Task.WhenAll(furnitNViewTask, furnitArtViewTask, furnitPachTask, furnitFTask, furnitFItTask);

            //_currentFurnitNViewData = new FurnitNView();
            //_furnitNViewBindingSource.DataSource = _currentFurnitNViewData;
            tbVidFName.DataBindings.Add("Text", _furnitNViewBindingSource, nameof(FurnitNView.VidFName), true, DataSourceUpdateMode.Never);
            tbNZ.DataBindings.Add("Text", _furnitNViewBindingSource, nameof(FurnitNView.n_z), true, DataSourceUpdateMode.Never);
            tbDataFO.DataBindings.Add("Text", _furnitNViewBindingSource, nameof(FurnitNView.data_f_o), true, DataSourceUpdateMode.Never);
            tbBr.DataBindings.Add("Text", _furnitNViewBindingSource, nameof(FurnitNView.br), true, DataSourceUpdateMode.Never);

            //_currentFurnitArtViewData = new List<FurnitArtView>();
            //_furnitArtViewBindingSource.DataSource = _currentFurnitArtViewData;
            gcFurnitArt.DataSource = _furnitArtViewBindingSource;
            gridColumnFurnitArtArticul.FieldName = "articul";
            gridColumnFurnitArtMod.FieldName = "mod";
            gridColumnFurnitArtKol.FieldName = "kol";
            gridColumnFurmitArtNZvet.FieldName = "n_zvet";
            gridColumnFurnitArtTM.FieldName = "tm";
            gridColumnfurnitArtNomZad.FieldName = "nom_zad";
            gridColumnFurnitArtKS.FieldName = "ks";
            gridColumnFurnitArtFaSpecRez.FieldName = "faSpecRez";

            //_currentFurnitPachData = new List<FurnitPach>();
            //_furnitPachBindingSource.DataSource = _currentFurnitPachData;
            gcFurnitPach.DataSource = _furnitPachBindingSource;
            gridColumnFurnitPachNPach.FieldName = "n_pach";
            gridColumnFurnitPachKol.FieldName = "kol";
            gridColumnFurnitPachRazm.FieldName = "razm";

            //_currentFurnitFData = new List<FurnitF>();
            //_furnitFBindingSource.DataSource = _currentFurnitFData;
            gcFurnitF.DataSource = _furnitFBindingSource;
            gridColumnFurnitFkodDr.FieldName = "kod_dr";
            gridColumnFurnitFArt.FieldName = "art";
            gridColumnFurnitFN.FieldName = "n";
            gridColumnFurnitFTEd.FieldName = "t_ed";
            gridColumnFurnitFKolF.FieldName = "kol_f";
            gridColumnFurnitFKolFO.FieldName = "kol_f_o";
            gridColumnFurnitFNPp.FieldName = "n_pp";
            gridColumnFurnitFFfSpecRez.FieldName = "ffSpecRez";

            //_currentFurnitFItData = new List<FurnitFIt>();
            //_furnitFItBindingSource.DataSource = _currentFurnitFItData;
            gcFurnitFIt.DataSource = _furnitFItBindingSource;
            gridColumnFurnitFItNPp.FieldName = "n_pp";
            gridColumnFurnitFItKodO.FieldName = "kod_o";
            gridColumnFurnitFItKodArt.FieldName = "kod_art";
            gridColumnFurnitFItArt.FieldName = "art";
            gridColumnFurnitFItTEd.FieldName = "t_ed";
            gridColumnFurnitFItKolFO.FieldName = "kol_f_o";
            gridColumnFurnitFItKolFBr.FieldName = "kol_f_br";
            gridColumnFurnitFItKolFUp.FieldName = "kol_f_up";
            gridColumnFurnitFItKolFRa.FieldName = "kol_f_ra";
        }
        private async Task WorkDivisionLoadAsync(string caller, string kodFD)
        {
            if (kodFD == " " || kodFD == null)
                return;
            try
            {
                // Загружаем все данные параллельно
                var furnitPachTask = _furnitService.GetFurnitPachByKodFD(kodFD);
                var furnitFTask = _furnitService.GetFurnitFByKodFD(kodFD);
                var furnitFItTask = _furnitService.GetFurnitFItByKodFD(kodFD);

                //var historyRazdelNaklViewTask = _cardByNomService.GetHistoryRazdelNaklViewByIz(iz);


                await Task.WhenAll(furnitPachTask, furnitFTask, furnitFItTask);
                //await Task.WhenAll(naklViewTask, rasInfoTask);
                //await Task.WhenAll(naklViewTask);

                await this.InvokeAsync(() =>
                {
                    // Обновляем списки
                    _furnitPachBindingSource.Clear();
                    LoadList(furnitPachTask.Result, _furnitPachBindingList, nameof(FurnitPach.pach_kod));
                    _furnitPachBindingSource.ResetBindings(false);

                    _furnitFBindingSource.Clear();
                    LoadList(furnitFTask.Result, _furnitFbindingList, nameof(FurnitF.n_pp));
                    _furnitFBindingSource.ResetBindings(false);

                    _furnitFItBindingSource.Clear();
                    LoadList(furnitFItTask.Result, _furnitFItBindingList, nameof(FurnitFIt.n_pp));
                    _furnitFItBindingSource.ResetBindings(false);
                });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных WorkDivisionLoadAsync");

                await this.InvokeAsync(() =>
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void LoadList<T>(List<T> sourceList, BindingList<T> targetList, string idFieldName)
        {
            foreach (var item in sourceList)
            {
                var idProp = typeof(T).GetProperty(idFieldName);
                targetList.Add(item);
            }
        }

        private async Task LoadFurnitNViewDataAsync(string kodF)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _furnitNViewBindingSource.Clear();
                _furnitNViewBindingSource.ResetBindings(false);
                var furnitNData = await _furnitService.GetFurnitNViewByKodF(kodF);
                if (furnitNData != null)
                {
                    await _logger.LogEventAsync($"Получены данные FurnitNView: kod_f={furnitNData.kod_f}", "LoadFurnitNDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentFurnitNViewData = furnitNData;                // Обновляем текущую модель
                        _furnitNViewBindingSource.DataSource = furnitNData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные FurnitNView успешно загружены для PachKod {kodF}", "LoadFurnitNDataAsync");
                    _furnitNViewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные FurnitNView для PachKod {kodF}", "LoadFurnitNDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных FurnitNView для pach_kod {kodF}");
            }
        }
        private async Task LoadFurnitArtViewDataAsync(string kodF)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _furnitArtViewBindingSource.Clear();
                _furnitArtViewBindingSource.ResetBindings(false);
                var furnitArtData = await _furnitService.GetFurnitArtViewByKodF(kodF);
                if (furnitArtData != null)
                {
                    await _logger.LogEventAsync($"Получены данные FurnitArtView: kod_f={furnitArtData[0].kod_f}", "LoadFurnitArtViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentFurnitArtViewData = furnitArtData;                // Обновляем текущую модель
                        _furnitArtViewBindingSource.DataSource = furnitArtData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные FurnitArtView успешно загружены для PachKod {kodF}", "LoadFurnitArtViewDataAsync");
                    _furnitArtViewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные FurnitArtView для PachKod {kodF}", "LoadFurnitArtViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных FurnitArtView для pach_kod {kodF}");
            }
        }
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataByKodF(tbKodF.Text);
            }
        }

        private async void LoadDataByKodF(string kodF)
        {
            //string _kodF = this.tbKodF.Text;
            if (kodF.Length > 0)
            {
                Task furnitNViewTask = LoadFurnitNViewDataAsync(kodF);
                Task furnitArtViewTask = LoadFurnitArtViewDataAsync(kodF);

                await Task.WhenAll(furnitNViewTask, furnitArtViewTask);

                gcFurnitArt.Refresh();

                var selectedRow = _furnitArtViewBindingSource.Current as FurnitArtView;
                //await WorkDivisionLoadAsync(caller: "DataLoad", selectedRow.kod_f_d?? " ".PadRight(12));
                if (selectedRow != null)
                {
                    await WorkDivisionLoadAsync(caller: "DataLoad", selectedRow.kod_f_d);
                }
                else
                {
                    await WorkDivisionLoadAsync(caller: "DataLoad", " ");
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataByKodF(tbKodF.Text);
        }

        private void tbKodF_TextChanged(object sender, EventArgs e)
        {
            LoadDataByKodF(tbKodF.Text);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private async void FurnitZayavView_Load(object sender, EventArgs e)
        {
            tbVidFName.BorderStyle = BorderStyle.None;
            tbNZ.BorderStyle = BorderStyle.None;
            tbDataFO.BorderStyle = BorderStyle.None;
            tbBr.BorderStyle = BorderStyle.None;

            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке UserControl FurnitZayavView");
            }
        }

        private void gcFurnitFIt_Click(object sender, EventArgs e)
        {

        }

        private void gcFurnitArt_Click(object sender, EventArgs e)
        {

        }

        private async void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var selectedRow = _furnitArtViewBindingSource.Current as FurnitArtView;
            //string _kodFD = selectedRow.kod_f_d;
            if (selectedRow != null)
            {
                await WorkDivisionLoadAsync(caller: "DataLoad", selectedRow.kod_f_d);
            }
            else
            {
                await WorkDivisionLoadAsync(caller: "DataLoad", "");
            }

        }
    }
}
