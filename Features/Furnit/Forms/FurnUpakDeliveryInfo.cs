using SewingProduction.Extensions;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.form
{
    public partial class FurnUpakDeliveryInfo : CustomForm
    {
        // private 
        private readonly DatabaseHelper _dbHelper;
        private readonly FurnitService _furnitService;
        private readonly ILogger _logger = new FileLogger();
        private List<ReestrFurn> _currentReestrFurnData = new List<ReestrFurn>();
        private BindingList<ReestrFurn> _reestrFurnBindingList;
        private BindingSource _reestrFurnBindingSource;
        private List<ReestrFurnSostView> _currentReestrFurnSostViewData = new List<ReestrFurnSostView>();
        private BindingList<ReestrFurnSostView> _reestrFurnSostViewBindingList;
        private BindingSource _reestrFurnSostViewBindingSource;
        private List<ReestrFurnShtr> _currentReestrFurnShtrData = new List<ReestrFurnShtr>();
        private BindingList<ReestrFurnShtr> _reestrFurnShtrBindingList;
        private BindingSource _reestrFurnShtrBindingSource;
        private List<ReestrFurnDeliveryBagView> _currentReestrFurnDeliveryBagViewData = new List<ReestrFurnDeliveryBagView>();
        private BindingList<ReestrFurnDeliveryBagView> _reestrFurnDeliveryBagViewBindingList;
        private BindingSource _reestrFurnDeliveryBagViewBindingSource;
        private List<ReestrFurnDeliveryBagSost> _currentReestrFurnDeliveryBagSostData = new List<ReestrFurnDeliveryBagSost>();
        private BindingList<ReestrFurnDeliveryBagSost> _reestrFurnDeliveryBagSostBindingList;
        private BindingSource _reestrFurnDeliveryBagSostBindingSource;

        public FurnUpakDeliveryInfo(string _kodF)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _furnitService = new FurnitService(_dbHelper);
            tbKodF.Text = _kodF;
            ThemeManager.UpdateTheme(this);

        }
        public FurnUpakDeliveryInfo()
        {
            InitializeComponent();
        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                var reestrFurnTask = Task.Run(() =>
                {
                    _reestrFurnBindingList = new BindingList<ReestrFurn>();
                    _reestrFurnBindingSource = new BindingSource { DataSource = _reestrFurnBindingList };
                });
                var reestrFurnSostViewTask = Task.Run(() =>
                {
                    _reestrFurnSostViewBindingList = new BindingList<ReestrFurnSostView>();
                    _reestrFurnSostViewBindingSource = new BindingSource { DataSource = _reestrFurnSostViewBindingList };
                });
                var reestrFurnShtrTask = Task.Run(() =>
                {
                    _reestrFurnShtrBindingList = new BindingList<ReestrFurnShtr>();
                    _reestrFurnShtrBindingSource = new BindingSource { DataSource = _reestrFurnShtrBindingList };
                });
                var reestrFurnDeliveryBagViewTask = Task.Run(() =>
                {
                    _reestrFurnDeliveryBagViewBindingList = new BindingList<ReestrFurnDeliveryBagView>();
                    _reestrFurnDeliveryBagViewBindingSource = new BindingSource { DataSource = _reestrFurnDeliveryBagViewBindingList };
                });
                var reestrFurnDeliveryBagSostTask = Task.Run(() =>
                {
                    _reestrFurnDeliveryBagSostBindingList = new BindingList<ReestrFurnDeliveryBagSost>();
                    _reestrFurnDeliveryBagSostBindingSource = new BindingSource { DataSource = _reestrFurnDeliveryBagSostBindingList };
                });

                await Task.WhenAll(reestrFurnTask, reestrFurnSostViewTask, reestrFurnShtrTask, reestrFurnDeliveryBagViewTask, reestrFurnDeliveryBagSostTask);

                #region описание gridcontrol "заборные карты"
                gridControlReestrFurn.DataSource = _reestrFurnBindingSource;
                gridColumnReestrFurnRfNOtgrPp.FieldName = "rfNOtgrPp";
                gridColumnReestrFurnRfPachList.FieldName = "rfPachList";
                gridColumnReestrFurnRfMod.FieldName = "rfMod";
                gridColumnReestrFurnRfArticul.FieldName = "rfArticul";
                gridColumnReestrFurnRfGrup.FieldName = "rfGrup";
                gridColumnReestrFurnRfBrig.FieldName = "rfBrig";
                gridColumnReestrFurnRfTipZ.FieldName = "rfTipZ";
                gridColumnReestrFurnRfKodF.FieldName = "rfKodF";
                gridColumnReestrFurnRfKolM.FieldName = "rfKolM";
                gridColumnReestrFurnRfKomp.FieldName = "rfKomp";
                gridColumnReestrFurnRfDateTime.FieldName = "rfDateTime";
                gridColumnReestrFurnRfID.FieldName = "rfID";
                #endregion

                #region описание gridcontrol "состав заборной карты"
                gridControlReestrFurnSostView.DataSource = _reestrFurnSostViewBindingSource;
                gridColumnReestrFurnSostViewRfsDataPrin.FieldName = "rfsDataPrin";
                gridColumnReestrFurnSostViewRfsKolPrin.FieldName = "rfsKolPrin";
                gridColumnReestrFurnSostViewRfsDataOtpr.FieldName = "rfsDataOtpr";
                gridColumnReestrFurnSostViewRfsKolOtpr.FieldName = "rfsKolOtpr";
                gridColumnReestrFurnSostViewRfsTEd.FieldName = "rfsTEd";
                gridColumnReestrFurnSostViewFn.FieldName = "fn";
                gridColumnReestrFurnSostViewRfsKodO.FieldName = "rfsKodO";
                gridColumnReestrFurnSostViewRfsNPp.FieldName = "rfsNPp";
                gridColumnReestrFurnSostViewRfsDate.FieldName = "rfsDate";
                gridColumnReestrFurnSostViewRfsID.FieldName = "rfsID";
                #endregion

                #region описание gridcontrol "ШК заборной карты"
                gridControlReestrFurnShtr.DataSource = _reestrFurnShtrBindingSource;
                gridColumnReestrFurnShtrRfshType.FieldName = "rfshType";
                gridColumnReestrFurnShtrRfshScan.FieldName = "rfScan";
                gridColumnReestrFurnShtrRfshShtr.FieldName = "rfshShtr";
                gridColumnReestrFurnShtrRfshSNum.FieldName = "rfshSNum";
                gridColumnReestrFurnShtrRfshID.FieldName = "rfshID";
                #endregion

                #region описание gridcontrol "места для доставки"
                gridControlReestrFurnDeliveryBagView.DataSource = _reestrFurnDeliveryBagViewBindingSource;
                gridColumnReestrFurnDeliveryBagViewDtDateTime.FieldName = "dtDateTime";
                gridColumnReestrFurnDeliveryBagViewDdName.FieldName = "ddName";
                gridColumnReestrFurnDeliveryBagViewRfdbKolBag.FieldName = "rfdbKolBag";
                gridColumnReestrFurnDeliveryBagViewRfdbKolM.FieldName = "rfdbKolM";
                gridColumnReestrFurnDeliveryBagViewRfdbDate.FieldName = "rfdbDate";
                gridColumnReestrFurnDeliveryBagViewRfdbID.FieldName = "rfdbID";
                gridColumnReestrFurnDeliveryBagViewRfdbDtID.FieldName = "rfdbDtID";
                gridColumnReestrFurnDeliveryBagViewRfdbDdID.FieldName = "rfdbDdID";
                #endregion

                #region описание gridcontrol "ШК транспортировочных мест"
                gridControlReestrFurnDeliveryBagSost.DataSource = _reestrFurnDeliveryBagSostBindingSource;
                gridColumnReestrFurnDeliveryBagSostRfdbsScan.FieldName = "rfbdsScan";
                gridColumnReestrFurnDeliveryBagSostRfdbsShtr.FieldName = "rfbdsShtr";
                gridColumnReestrFurnDeliveryBagSostRfdbsNumber.FieldName = "rfbdsNumber";
                gridColumnReestrFurnDeliveryBagSostRfdbsID.FieldName = "rfbdsID";
                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        private async Task LoadReestrFurnDataAsync(string kodF)
        {
            try
            {
                _reestrFurnBindingSource.Clear();
                _reestrFurnBindingSource.ResetBindings(false);
                var reestrFurnData = await _furnitService.GetReestrFurnByKodF(kodF);
                if (reestrFurnData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ReestrFurn: rfID={reestrFurnData[0].rfID}", "LoadReestrFurnDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentReestrFurnData = reestrFurnData;                // Обновляем текущую модель
                        _reestrFurnBindingSource.DataSource = _currentReestrFurnData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ReestrFurn успешно загружены для kod_f {kodF}", "LoadReestrFurnDataAsync");
                    _reestrFurnBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ReestrFurn для kod_f {kodF}", "LoadReestrFurnDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ReestrFurn для kod_f {kodF}");
            }
        }

        private async Task LoadReestrFurnSostAndShtrDataAsync(int rfID)
        {
            if (rfID == 0)
            {
                _currentReestrFurnSostViewData.Clear();
                _reestrFurnSostViewBindingSource.ResetBindings(false);
                _currentReestrFurnShtrData.Clear();
                _reestrFurnShtrBindingSource.ResetBindings(false);
                return;
            }
            try
            {
                // Загружаем все данные параллельно
                var reestrFurnSostTask = _furnitService.GetReestrFurnSostByRfID(rfID);
                var reestrFurnShtrTask = _furnitService.GetReestrFurnShtrByRfID(rfID);

                await Task.WhenAll(reestrFurnSostTask, reestrFurnShtrTask);

                await this.InvokeAsync(() =>
                {
                    // Обновляем списки
                    _currentReestrFurnSostViewData.Clear();
                    LoadList(reestrFurnSostTask.Result, _reestrFurnSostViewBindingList, nameof(ReestrFurnSostView.rfsID));
                    _reestrFurnSostViewBindingSource.ResetBindings(false);

                    _currentReestrFurnShtrData.Clear();
                    LoadList(reestrFurnShtrTask.Result, _reestrFurnShtrBindingList, nameof(ReestrFurnShtr.rfshID));
                    _reestrFurnShtrBindingSource.ResetBindings(false);
                });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных ReestrFurnSostAndShtrLoadAsync");

                await this.InvokeAsync(() =>
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private async Task LoadReestrFurnDeliveryDataAsync(int rfdbID)
        {
            if (rfdbID == 0)
            {
                _currentReestrFurnDeliveryBagViewData.Clear();
                _reestrFurnSostViewBindingSource.ResetBindings(false);
                _currentReestrFurnDeliveryBagSostData.Clear();
                _reestrFurnSostViewBindingSource.ResetBindings(false);
                return;
            }

            try
            {
                // Загружаем все данные параллельно
                var reestrFurnDeliveryBagTask = _furnitService.GetReestrFurnDeliveryBagByRfDbID(rfdbID);
                var reestrFurnDeliveryBagSostTask = _furnitService.GetReestrFurnDeliveryBagSostByRfDbID(rfdbID);

                await Task.WhenAll(reestrFurnDeliveryBagTask, reestrFurnDeliveryBagSostTask);

                await this.InvokeAsync(() =>
                {
                    // Обновляем списки
                    _currentReestrFurnDeliveryBagViewData.Clear();
                    LoadList(reestrFurnDeliveryBagTask.Result, _reestrFurnDeliveryBagViewBindingList, nameof(ReestrFurnDeliveryBagView.rfdbID));
                    _reestrFurnSostViewBindingSource.ResetBindings(false);

                    _currentReestrFurnDeliveryBagSostData.Clear();
                    LoadList(reestrFurnDeliveryBagSostTask.Result, _reestrFurnDeliveryBagSostBindingList, nameof(ReestrFurnDeliveryBagSost.rfdbsID));
                    _reestrFurnSostViewBindingSource.ResetBindings(false);
                });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных ReestrFurnDeliveryLoadAsync");

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


        //private int GetID(string _xGrid, string _xField)
        //{
        //    int _ID = 0;
        //    switch (_xGrid)
        //    {
        //        case "gcReestrFurn":
        //            try
        //            {
        //                object data = gridView1.GetRow(gridView1.FocusedRowHandle);
        //                if (data != null)
        //                {
        //                    switch (_xField)
        //                    {
        //                        case "rfID":
        //                            _ID = Convert.ToInt32(((DataRowView)data).Row["rfID"]);
        //                            break;
        //                        case "rfRfdbID":
        //                            _ID = Convert.ToInt32(((DataRowView)data).Row["rfRfdbID"]);
        //                            break;
        //                    }

        //                }
        //            }
        //            catch
        //            {
        //                _ID = 0;
        //            }
        //            break;
        //        default:
        //            _ID = 0;
        //            break;
        //    }
        //    return _ID;
        //}
        private async void GetReestrFurn(string _KodF)
        {
            await LoadReestrFurnDataAsync(_KodF);
            GetReestrFurnSostShtrDelivery();
        }
        private async void GetReestrFurnSostShtrDelivery()
        {
            _reestrFurnSostViewBindingSource.Clear();
            _reestrFurnSostViewBindingSource.ResetBindings(false);
            _reestrFurnShtrBindingSource.Clear();
            _reestrFurnShtrBindingSource.ResetBindings(false);
            _reestrFurnDeliveryBagViewBindingSource.Clear();
            _reestrFurnDeliveryBagViewBindingSource.ResetBindings(false);
            _reestrFurnDeliveryBagSostBindingSource.Clear();
            _reestrFurnDeliveryBagSostBindingSource.ResetBindings(false);

            var selectedRow = _reestrFurnBindingSource.Current as ReestrFurn;
            if (selectedRow != null)
            {
                Task LoadReestrFurnSostAndShtrTask = LoadReestrFurnSostAndShtrDataAsync(selectedRow.rfID);
                Task LoadReestrFurnDeliveryTask = LoadReestrFurnDeliveryDataAsync(selectedRow.rfRfdbID);
                await Task.WhenAll(LoadReestrFurnSostAndShtrTask, LoadReestrFurnDeliveryTask);
                //  private async Task ReestrFurnDeliveryLoadAsync(int rfdbID)
            }
            else
            {
                Task LoadReestrFurnSostAndShtrTask = LoadReestrFurnSostAndShtrDataAsync(0);
                Task LoadReestrFurnDeliveryTask = LoadReestrFurnDeliveryDataAsync(0);
                await Task.WhenAll(LoadReestrFurnSostAndShtrTask, LoadReestrFurnDeliveryTask);
            }
            _reestrFurnSostViewBindingSource.ResetBindings(false);
            _reestrFurnShtrBindingSource.ResetBindings(false);
            _reestrFurnDeliveryBagViewBindingSource.ResetBindings(false);
            _reestrFurnDeliveryBagSostBindingSource.ResetBindings(false);
        }
        private async void FurnUpakDeliveryInfo_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы FurnUpakDeliveryInfo");
            }
            if (tbKodF.Text.Length > 0)
            {
                GetReestrFurn(tbKodF.Text);
            }


        }

        private void btnKodFDelivInfo_Click(object sender, EventArgs e)
        {
            GetReestrFurn(tbKodF.Text);
            GetReestrFurnSostShtrDelivery();
        }

        private void gcReestrFurn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetReestrFurnSostShtrDelivery();
        }

        private void tbKodF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetReestrFurn(tbKodF.Text);
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            object data = gridViewReestrFurn.GetRow(gridViewReestrFurn.FocusedRowHandle);
            MessageBox.Show(((DataRowView)data).Row["rfDateTime"].ToString());
        }

        private void FurnUpakDeliveryInfo_Deactivate(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            GetReestrFurn(tbKodF.Text);
            GetReestrFurnSostShtrDelivery();
        }
    }
}
