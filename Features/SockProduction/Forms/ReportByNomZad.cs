using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.SockProduction.Forms
{
    public partial class ReportByNomZad : Form
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        private List<SockZadanySmenList> _currentSockZadanySmenListData = new List<SockZadanySmenList>();
        private List<SockZadanySmenList> sockZadanySmenListData = new List<SockZadanySmenList>();
        private BindingList<SockZadanySmenList> _sockZadanySmenListBindingList;
        private BindingSource _sockZadanySmenListBindingSource;
        public ReportByNomZad()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _vyazService = new VyazService(_dbHelper);
            ThemeManager.UpdateTheme(this);
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var sockZadanySmenListTask = Task.Run(() =>
                {
                    _sockZadanySmenListBindingList = new BindingList<SockZadanySmenList>();
                    _sockZadanySmenListBindingSource = new BindingSource { DataSource = _sockZadanySmenListBindingList };
                });
                //var zadanyListByMachineTask = Task.Run(() =>
                //{
                //    _zadanyListByMachineBindingList = new BindingList<ZadanyListByMachine>();
                //    _zadanyListByMachineBindingSource = new BindingSource { DataSource = _zadanyListByMachineBindingList };
                //});

                //await Task.WhenAll(sockZadanySmenListTask);

                //#region описание блока Задание
                //customTextBoxEx2.DataBindings.Add("Text", _sockZadanySmenListBindingSource, nameof(SockZadanySmenList.kzPszNom), true, DataSourceUpdateMode.Never);
                //customTextBoxEx3.DataBindings.Add("Text", _sockZadanySmenListBindingSource, nameof(SockZadanySmenList.articul), true, DataSourceUpdateMode.Never);
                //customTextBoxEx4.DataBindings.Add("Text", _sockZadanySmenListBindingSource, nameof(SockZadanySmenList.kol), true, DataSourceUpdateMode.Never);
                //customTextBoxEx5.DataBindings.Add("Text", _sockZadanySmenListBindingSource, nameof(SockZadanySmenList.zv_tkan), true, DataSourceUpdateMode.Never);
                //#endregion

                #region описание блока Вязание
                #endregion

                //#region описание comboBox "Список машин"
                //comboBoxKnitMachineList.DataSource = _knitMachineListBindingSource;
                //comboBoxKnitMachineList.SelectedIndex = -1;
                //comboBoxKnitMachineList.ValueMember = "kmlID";
                //comboBoxKnitMachineList.DisplayMember = "kmlNumber";
                //#endregion

                //#region описание gridControlPlanTotalHoursByKnitMachine "общие часы по вяз машинам/зонам"
                //gridControlPlanTotalHoursByKnitMachine.DataSource = _planTotalHoursByKnitMachineBindingSource;
                //gridColumnPlanTotalHoursByKnitMachineKmaNumber.FieldName = "kmaNumber";
                //gridColumnPlanTotalHoursByKnitMachineKmlNumber.FieldName = "kmlNumber";
                //gridColumnPlanTotalHoursByKnitMachineDateZap.FieldName = "DateZap";
                //gridColumnPlanTotalHoursByKnitMachineMgZap.FieldName = "mgZap";
                //gridColumnPlanTotalHoursByKnitMachineHoursTotal.FieldName = "hoursTotal";
                //gridColumnPlanTotalHoursByKnitMachineIdVyazClass.FieldName = "idVyazClass";
                //gridColumnPlanTotalHoursByKnitMachineKnitClass.FieldName = "knitClass";
                //#endregion

                //#region описание gridControlZadanyListByMachine "задания по номеру машины"
                //gridControlZadanyListByMachine.DataSource = _zadanyListByMachineBindingSource;
                //gridColumnZadanyListByMachinePszNom.FieldName = "pszNom";
                //gridColumnZadanyListByMachineNom.FieldName = "nom";
                //gridColumnZadanyListByMachineArticul.FieldName = "articul";
                //gridColumnZadanyListByMachineZvet.FieldName = "zvet";
                //gridColumnZadanyListByMachineKol.FieldName = "kol";
                //gridColumnZadanyListByMachineDatePryazZayav.FieldName = "pryazZayav";
                //gridColumnZadanyListByMachineData_plan.FieldName = "data_plan";
                //gridColumnZadanyListByMachineVid_stir.FieldName = "vid_stir";
                //gridColumnZadanyListByMachineDopr_name.FieldName = "dopr_name";
                //gridColumnZadanyListByMachineKmlID.FieldName = "kmlID";
                //gridColumnZadanyListByMachineSyncSelection.FieldName = "SyncSelection";

                //gridViewRzvPachListByNom.OptionsView.ShowAutoFilterRow = false;
                //gridViewRzvPachListByNom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                //gridViewRzvPachListByNom.OptionsFilter.AllowFilterEditor = false;


                //#endregion

                //#region описание gridControlRzvPachListByNom "пачки по расчету вяз"
                //gridControlRzvPachListByNom.DataSource = _rzvPachListByNomBindingSource;
                //gridColumnRzvPachListByNomNomZad.FieldName = "nomZad";
                //gridColumnRzvPachListByNomNom.FieldName = "nom";
                //gridColumnRzvPachListByNomNom_n.FieldName = "nom_n";
                //gridColumnRzvPachListByNomN_pach.FieldName = "n_pach";
                //gridColumnRzvPachListByNomRazm.FieldName = "razm";
                //gridColumnRzvPachListByNomKol.FieldName = "kol";
                //gridColumnRzvPachListByNomGrad.FieldName = "grad";
                //gridColumnRzvPachListByNomSyncSelection.FieldName = "SyncSelection";
                //#endregion

                //#region описание gridControlArtPrFioProgr "проработки"
                //gridControlArtPrFioProgr.DataSource = _artPrFioProgrViewBindingSource;
                //gridColumnArtPrFioProgrArticul.FieldName = "art_pr";
                //gridColumnArtPrFioProgrRazm.FieldName = "razm";
                //#endregion

                //#region описание gridControlPlanSezonZadanyRazmKol "количество по размерам"
                //gridControlPlanSezonZadanyRazmKol.DataSource = _planSezonZadanyViewBindingSource;
                //gridColumnPlanSezonZadanyRazmKolRazm.FieldName = "razm";
                //gridColumnPlanSezonZadanyRazmKolKol.FieldName = "kol";
                //#endregion

                //#region описание gridControlArtPrKnitMachineViewPr1 "в.м ПР основн."
                //gridControlArtPrKnitMachineViewPr1.DataSource = _artPrKnitMachineViewPr1BindingSource;
                //gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewPr2 "в.м ПР вспомог."
                //gridControlArtPrKnitMachineViewPr2.DataSource = _artPrKnitMachineViewPr2BindingSource;
                //gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewRecom1 "в.м ПР основн."
                //gridControlArtPrKnitMachineViewRecom1.DataSource = _artPrKnitMachineViewRecom1BindingSource;
                //gridColumnArtPrKnitMachineViewRecom1KmlNumber.FieldName = "kmlNumber";
                //gridColumnArtPrKnitMachineViewRecom1AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                //gridColumnArtPrKnitMachineViewRecom1AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewRecom2 "в.м ПР вспомог."
                //gridControlArtPrKnitMachineViewRecom2.DataSource = _artPrKnitMachineViewRecom2BindingSource;
                //gridColumnArtPrKnitMachineViewRecom2KmlNumber.FieldName = "kmlNumber";
                //gridColumnArtPrKnitMachineViewRecom2AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                //gridColumnArtPrKnitMachineViewRecom2AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                //#endregion

                //pictureBoxEskiz.DataBindings.Add("ImageLocation", _vyazPlanViewBindingSource, nameof(VyazPlanView.PictPath), true, DataSourceUpdateMode.Never);

                //#region описание gridControlPZVOperList
                //gridControlPZVOperList.DataSource = _pZVOperListByPachListBindingSource;
                //gridColumnPZVOperListOlPzvID.FieldName = "olPzvID";
                //gridColumnPZVOperListOlPzvIDParent.FieldName = "olPzvIDParent";
                //gridColumnPZVOperListOlPzvIDMlOp.FieldName = "olPzvIDMlOp";
                //gridColumnPZVOperListOlNom.FieldName = "olNom";
                //gridColumnPZVOperListOlNomN.FieldName = "olNomN";
                //gridColumnPZVOperListOlNomZad.FieldName = "olNomZad";
                //gridColumnPZVOperListOlPzvAnnID.FieldName = "olPzvAnnID";
                //gridColumnPZVOperListOlPzvNrID.FieldName = "olPzvNrID";
                //gridColumnPZVOperListOlPzvIdBrig.FieldName = "olPzvIdBrig";
                //gridColumnPZVOperListOlPzvKmlID.FieldName = "olPzvKmlID";
                //gridColumnPZVOperListOlPzvArticul.FieldName = "olPzvArticul";
                //gridColumnPZVOperListOlNPach.FieldName = "olNPach";
                //gridColumnPZVOperListOlNo.FieldName = "olNo";
                //gridColumnPZVOperListOlNpo.FieldName = "olNpo";
                //gridColumnPZVOperListOlOperName.FieldName = "olOperName";
                //gridColumnPZVOperListOlKodOb.FieldName = "olKodOb";
                //gridColumnPZVOperListOlOborudClass.FieldName = "olOborudClass";
                //gridColumnPZVOperListOlRazryd.FieldName = "olRazryd";
                //gridColumnPZVOperListOlSekEd.FieldName = "olSekEd";
                //gridColumnPZVOperListOlKol.FieldName = "olKol";
                //gridColumnPZVOperListOlSekAll.FieldName = "olSekAll";
                //gridColumnPZVOperListOlKmlNumber.FieldName = "olKmlNumber";
                //gridColumnPZVOperListOlPvDateNaznKm.FieldName = "olPvDateNaznKm";
                //gridColumnPZVOperListOlPzvTab.FieldName = "olPzvTab";
                //gridColumnPZVOperListOlPzvDateNaznTab.FieldName = "olPzvDateNaznTab";
                //gridColumnPZVOperListOlPzvDateStart.FieldName = "olPzvDateStart";
                //gridColumnPZVOperListOlPzvDateEnd.FieldName = "olPzvDateEnd";
                //gridColumnPZVOperListOlPzvDateML.FieldName = "olPzvDateML";
                //gridColumnPZVOperListOlPzvDateMast.FieldName = "olPzvDateMast";
                //gridColumnPZVOperListSyncSelection.FieldName = "olSyncSelection";

                //#endregion

                //#region описание блока Информация по операции
                //textBoxOlPzvNom.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNom), true, DataSourceUpdateMode.Never);
                //textBoxOlPzvID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvID), true, DataSourceUpdateMode.Never);
                //textBoxOlPzvIDMlOp.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvIDMlOp), true, DataSourceUpdateMode.Never);
                //textBoxOlPzvAnnID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvAnnID), true, DataSourceUpdateMode.Never);
                //textBoxOlPzvUpdDate.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvUpdDate), true, DataSourceUpdateMode.Never);
                //textBoxOlNo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNo), true, DataSourceUpdateMode.Never);
                //textBoxOlNpo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNpo), true, DataSourceUpdateMode.Never);
                //textBoxOlPzvNrID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvNrID), true, DataSourceUpdateMode.Never);
                //#endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private void customSimpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
