using DevExpress.CodeParser;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Core.Services;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CardByNom.Services;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
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

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class NomLookUp : Form
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly GridHelper _gridHelper;
        private readonly CardByNomService _cardByNomService;
        private readonly DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private List<RasNomList> _currentRasNomListData = new List<RasNomList>();
        private List<RasNomList> rasNomListData = new List<RasNomList>();
        private BindingList<RasNomList> _rasNomListBindingList;
        private BindingSource _rasNomListBindingSource;
        private string locationType;
        private string xNomZadany;
        private string xKo;
        public int SelectedValueNPach { get; private set; }
        public int SelectedValueYearPach { get; private set; }
        public int SelectedValueProizvType { get; private set; }
        public NomLookUp(string _nomZadany, string _ko, string _articul)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _cardByNomService = new CardByNomService(_dbHelper);
            _gridHelper = new GridHelper();
            ThemeManager.UpdateTheme(this);

            xNomZadany = _nomZadany;
            xKo = _ko;

            switch (_ko.Trim().Length, _nomZadany.Trim().Length)
            {
                case ( > 0, 0):
                    customLabel1.Text = "Артикул";
                    customTextBoxEx1.Text = _articul;
                    locationType = "art";
                    break;
                case (0, > 0):
                    customLabel1.Text = "№ задания";
                    customTextBoxEx1.Text = _nomZadany;
                    locationType = "nomzad";
                    break;
                default:
                    customLabel1.Text = "ошибка";
                    customTextBoxEx1.Text = "Повторите поиск";
                    locationType = "";
                    break;
            }
        }

        private async Task LoadRasNomListByNomZadany(string _nomZadany)
        {
            try
            {
                if (_rasNomListBindingSource.Count > 0)
                {
                    _rasNomListBindingSource.Clear();
                    _rasNomListBindingSource.ResetBindings(false);
                }
                
                //var rasNomListData = await _dbService.GetListAsync<RasNomList>($"exec dbo.GetNomListByKodOrNomZadany @xNomZadany = '{_nomZadany}'", new { });
                var rasNomListData = await _cardByNomService.GetRasNomListByNomZadany(_nomZadany);
                if (rasNomListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RasNomList", "LoadRasNomListByNomZadany");

                    await this.InvokeAsync(() =>
                    {
                        //_currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                        _currentRasNomListData = rasNomListData;                // Обновляем текущую модель
                        _rasNomListBindingSource.DataSource = _currentRasNomListData; // Привязываем данные к форме
                        return Task.CompletedTask;
                    });

                    await _logger.LogEventAsync($"Данные RasNomList успешно загружены", "LoadRasLoadRasNomListByNomZadanyNomListDataAsync");
                    //RasCard.Enabled = true ;
                    _rasNomListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RasNomList", "LoadRasNomListByNomZadany");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RasNomList");
            }
        }

        private async Task LoadRasNomListByArticul(string _ko)
        {
            try
            {
                _rasNomListBindingSource.Clear();
                _rasNomListBindingSource.ResetBindings(false);
                var rasNomListData = await _dbService.GetListAsync<RasNomList>($"exec dbo.GetNomListByKodOrNomZadany @xKo = '{_ko}'", new { });
                if (rasNomListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RasNomList", "LoadRasNomListByArticul");

                    await this.InvokeAsync(() =>
                    {
                        //_currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                        _currentRasNomListData = rasNomListData;                // Обновляем текущую модель
                        _rasNomListBindingSource.DataSource = _currentRasNomListData; // Привязываем данные к форме
                        return Task.CompletedTask;
                    });

                    await _logger.LogEventAsync($"Данные RasNomList успешно загружены", "LoadRasNomListByArticul");
                    //RasCard.Enabled = true ;
                    _rasNomListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RasNomList", "LoadRasNomListByArticul");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RasNomList");
            }
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var rasNomListTask = Task.Run(() =>
                {
                    _rasNomListBindingList = new BindingList<RasNomList>();
                    _rasNomListBindingSource = new BindingSource { DataSource = _rasNomListBindingList };
                });

                await Task.WhenAll(rasNomListTask);

                #region описание gridControlRasNomList "список расчетов по номеру задания или артикулу"
                gridControlRasNomList.DataSource = _rasNomListBindingSource;
                gridRasNomListColumnNomZad.FieldName = "nom_zad";
                gridRasNomListColumnNomPach.FieldName = "nom_pach";
                gridRasNomListColumnMinPach.FieldName = "minPach";
                gridRasNomListColumnMaxPach.FieldName = "maxPach";
                gridRasNomListColumnMgZakr.FieldName = "mg_zakr";
                gridRasNomListColumnKod7.FieldName = "kod7";
                gridRasNomListColumnGrupPach.FieldName = "grup_pach";
                gridRasNomListColumnArticulPach.FieldName = "articul_pach";
                gridRasNomListColumnModPach.FieldName = "mod_pach";
                gridRasNomListColumnYearPach.FieldName = "yearPach";
                gridRasNomListColumnProizvType.FieldName = "proizvType";
                gridRasNomListColumnDostZeh.FieldName = "dost_zeh";
                gridRasNomListColumnIDBrig.FieldName = "id_brig";
                gridRasNomListColumnDataR.FieldName = "data_r";

                gridRasNomListColumnNomZad.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnNomPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnMinPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnMaxPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnMgZakr.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnKod7.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnGrupPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnArticulPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnModPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnYearPach.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnProizvType.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnDostZeh.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnIDBrig.OptionsColumn.AllowEdit = false;
                gridRasNomListColumnDataR.OptionsColumn.AllowEdit = false;

                _gridHelper.AutoRowFilterConfig(gridViewRasNomList as GridView, 1);
                #endregion
             }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private void customTextBoxEx1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //}
        }

        private async void NomLookUp_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);
                if (locationType == "nomzad")
                {
                    await LoadRasNomListByNomZadany(xNomZadany);
                }
                else if (locationType == "art")
                {
                    await LoadRasNomListByArticul(xKo);
                }
                if (_rasNomListBindingSource.Count == 1)
                {
                    RasNomList currRow = (RasNomList)_rasNomListBindingSource.Current;
                    if (currRow == null) return;
                    SelectedValueNPach = currRow.minPach;
                    SelectedValueYearPach = currRow.yearPach;
                    SelectedValueProizvType = currRow.proizvType;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingMachinesLoading");
            }
        }

        private void gridControlRasNomList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridViewRasNomList_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            //Point pt = view.GridControl.PointToClient(Control.MousePosition);
            //GridHitInfo hit = view.CalcHitInfo(pt);
            string _xColumn = view.FocusedColumn.ToString();
            int _xRow = view.FocusedRowHandle;
            bool _isDataRow = view.IsDataRow(_xRow);
            if (_isDataRow && _xRow >= 0)
            {
                int _minPach = Convert.ToInt32(view.GetRowCellValue(_xRow, gridRasNomListColumnMinPach));
                if (_minPach == 0)
                {
                    MessageBox.Show("Ошибка выбора расчета");
                    return;
                }
                int _yearPach = Convert.ToInt32(view.GetRowCellValue(_xRow, gridRasNomListColumnYearPach));
                if (_yearPach == 0)
                {
                    MessageBox.Show("Ошибка выбора года расчета");
                    return;
                }
                int _proizvType = Convert.ToInt32(view.GetRowCellValue(_xRow, gridRasNomListColumnProizvType));
                if (_proizvType < 0)
                {
                    MessageBox.Show("Ошибка определения вида производства");
                    return;
                }

                SelectedValueNPach = _minPach;
                SelectedValueYearPach = _yearPach;
                SelectedValueProizvType = _proizvType;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
