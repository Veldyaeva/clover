using DevExpress.ChartRangeControlClient.Core;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.Office.Utils;
using DevExpress.Utils.Gesture;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGauges.Core.Styles;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Card.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraTab;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using SewingProduction.Core.Services;
using SewingProduction.Extensions;
using SewingProduction.Features.Articul;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CardByNom.Services;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.form;
using SewingProduction.form.TeamWork.Interfaces;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction
{
    public partial class CardByNom : CustomForm, IThemeable
    {
        public int fspecrez, uspecrez;
        public string fkodfd, ukodfd;

        private readonly DatabaseHelper _dbHelper;
        private readonly CardByNomService _cardByNomService;
        private readonly FurnitService _furnitService;
        private readonly MatrixService _matrixService;
        private readonly RaskrService _raskrService;
        private readonly SockService _sockService;
        private readonly ILogger _logger = new FileLogger();
        private RasInfo _currentRasInfoData = new RasInfo();
        private ChipInfo _currentChipInfoData = new ChipInfo();
        private FurnitZayavCheckByPachKod _currentFurnitZayavCheckData = new FurnitZayavCheckByPachKod();
        private List<NaklView> _currentNaklViewData = new List<NaklView>();
        private List<HistoryRazdelNaklViewByIz> _currentHistoryRazdelNaklViewData = new List<HistoryRazdelNaklViewByIz>();
        private List<PlanSezonOtdelkaView> _currentPlanSezonOtdelkaViewData = new List<PlanSezonOtdelkaView>();
        private List<ProizvCombIzd> _currentProizvCombIzdSPData = new List<ProizvCombIzd>();
        private List<ProizvCombIzd> _currentProizvCombIzdVZPData = new List<ProizvCombIzd>();
        private BindingList<NaklView> _naklViewByPachKodBindingList;
        private BindingSource _naklViewByPachKodBindingSource;
        private BindingList<RasInfo> _rasInfoByPachKodBindingList;
        private BindingSource _rasInfoByPachKodBindingSource;
        private BindingList<ChipInfo> _chipInfoByPachKodBindingList;
        private BindingSource _chipInfoByPachKodBindingSource;
        private BindingList<FurnitZayavCheckByPachKod> _furnitZayavCheckByPachKodBindingList;
        private BindingSource _furnitZayavCheckByPachKodBindingSource;
        private BindingList<HistoryRazdelNaklViewByIz> _historyRazdelNaklViewByIzBindingList;
        private BindingSource _historyRazdelNaklViewByIzBindingSource;
        private BindingList<PlanSezonOtdelkaView> _planSezonOtdelkaViewByPachKodBindingList;
        private BindingSource _planSezonOtdelkaViewByPachKodBindingSource;

        private BindingList<ProizvCombIzd> _proizvCombIzdSPByPachKodBindingList;
        private BindingSource _proizvCombIzdSPByPachKodBindingSource;
        private BindingList<ProizvCombIzd> _proizvCombIzdVZPByPachKodBindingList;
        private BindingSource _proizvCombIzdVZPByPachKodBindingSource;

        // список смен по заданию. Носки
        private List<SockZadanySmenList> _currentSockZadanySmenListData = new List<SockZadanySmenList>();
        private List<SockZadanySmenList> sockZadanySmenListData = new List<SockZadanySmenList>();
        private BindingList<SockZadanySmenList> _sockZadanySmenListBindingList;
        private BindingSource _sockZadanySmenListBindingSource;

        // общая информация по заданию. Носки
        private List<SockZadanyInfo> _currentSockKnitZadanyInfoData = new List<SockZadanyInfo>();
        private List<SockZadanyInfo> sockKnitZadanyInfoData = new List<SockZadanyInfo>();
        private BindingList<SockZadanyInfo> _sockKnitZadanyInfoBindingList;
        private BindingSource _sockKnitZadanyInfoBindingSource;

        // список обслуживаний оборудования во время вязания задания. Носки
        private List<SockServiceList> _currentSockServiceListData = new List<SockServiceList>();
        private List<SockServiceList> sockServiceListData = new List<SockServiceList>();
        private BindingList<SockServiceList> _sockServiceListBindingList;
        private BindingSource _sockServiceListBindingSource;

        // список простоев оборудования во время вязания задания. Носки
        private List<SockDownTimeList> _currentSockDownTimeListData = new List<SockDownTimeList>();
        private List<SockDownTimeList> sockDownTimeListData = new List<SockDownTimeList>();
        private BindingList<SockDownTimeList> _sockDownTimeListBindingList;
        private BindingSource _sockDownTimeListBindingSource;

        // браки по заданию. Носки
        private List<SockDefectList> _currentSockDefectListData = new List<SockDefectList>();
        private List<SockDefectList> sockDefectListData = new List<SockDefectList>();
        private BindingList<SockDefectList> _sockDefectListBindingList;
        private BindingSource _sockDefectListBindingSource;
        public CardByNom()
        {

            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _cardByNomService = new CardByNomService(_dbHelper);
            _furnitService = new FurnitService(_dbHelper);
            _matrixService = new MatrixService(_dbHelper);
            _raskrService = new RaskrService(_dbHelper);
            _sockService = new SockService(_dbHelper);
            //ApplyTheme();
            ThemeManager.UpdateTheme(this);
            //// Инициализация привязок данных
            //_naklViewByNomSource.DataSource = _naklView;
            //if (gcNaklList != null)
            //{
            //    gcNaklList.DataSource = _naklViewByNomSource;
            //}
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var naklViewByPachKodTask = Task.Run(() =>
                {
                    _naklViewByPachKodBindingList = new BindingList<NaklView>();
                    _naklViewByPachKodBindingSource = new BindingSource { DataSource = _naklViewByPachKodBindingList };
                });
                var rasInfoByPachKodTask = Task.Run(() =>
                {
                    _rasInfoByPachKodBindingList = new BindingList<RasInfo>();
                    _rasInfoByPachKodBindingSource = new BindingSource { DataSource = _rasInfoByPachKodBindingList };
                });
                var chipInfoByNomZadTask = Task.Run(() =>
                {
                    _chipInfoByPachKodBindingList = new BindingList<ChipInfo>();
                    _chipInfoByPachKodBindingSource = new BindingSource { DataSource = _chipInfoByPachKodBindingList };
                });
                var historyRazdelNaklViewByIzTask = Task.Run(() =>
                {
                    _historyRazdelNaklViewByIzBindingList = new BindingList<HistoryRazdelNaklViewByIz>();
                    _historyRazdelNaklViewByIzBindingSource = new BindingSource { DataSource = _historyRazdelNaklViewByIzBindingList };
                });
                var furnitZayavCheckByPachKodTask = Task.Run(() =>
                {
                    _furnitZayavCheckByPachKodBindingList = new BindingList<FurnitZayavCheckByPachKod>();
                    _furnitZayavCheckByPachKodBindingSource = new BindingSource { DataSource = _furnitZayavCheckByPachKodBindingList };
                });
                var planSezonOtdelkaViewByPachKodTask = Task.Run(() =>
                {
                    _planSezonOtdelkaViewByPachKodBindingList = new BindingList<PlanSezonOtdelkaView>();
                    _planSezonOtdelkaViewByPachKodBindingSource = new BindingSource { DataSource = _planSezonOtdelkaViewByPachKodBindingList };
                });
                var proizvCombIzdSPByPachKodTask = Task.Run(() =>
                {
                    _proizvCombIzdSPByPachKodBindingList = new BindingList<ProizvCombIzd>();
                    _proizvCombIzdSPByPachKodBindingSource = new BindingSource { DataSource = _proizvCombIzdSPByPachKodBindingList };
                });
                var proizvCombIzdVZPByPachKodTask = Task.Run(() =>
                {
                    _proizvCombIzdVZPByPachKodBindingList = new BindingList<ProizvCombIzd>();
                    _proizvCombIzdVZPByPachKodBindingSource = new BindingSource { DataSource = _proizvCombIzdVZPByPachKodBindingList };
                });

                var sockZadanySmenListTask = Task.Run(() =>
                {
                    _sockZadanySmenListBindingList = new BindingList<SockZadanySmenList>();
                    _sockZadanySmenListBindingSource = new BindingSource { DataSource = _sockZadanySmenListBindingList };
                });

                var sockKnitZadanyInfoTask = Task.Run(() =>
                {
                    _sockKnitZadanyInfoBindingList = new BindingList<SockZadanyInfo>();
                    _sockKnitZadanyInfoBindingSource = new BindingSource { DataSource = _sockKnitZadanyInfoBindingList };
                });

                var sockServiceListTask = Task.Run(() =>
                {
                    _sockServiceListBindingList = new BindingList<SockServiceList>();
                    _sockServiceListBindingSource = new BindingSource { DataSource = _sockServiceListBindingList };
                });
                var sockMachiheDownTimeListTask = Task.Run(() =>
                {
                    _sockDownTimeListBindingList = new BindingList<SockDownTimeList>();
                    _sockDownTimeListBindingSource = new BindingSource { DataSource = _sockDownTimeListBindingList };
                });
                var sockDefectListTask = Task.Run(() =>
                {
                    _sockDefectListBindingList = new BindingList<SockDefectList>();
                    _sockDefectListBindingSource = new BindingSource { DataSource = _sockDefectListBindingList };
                });
                await Task.WhenAll(naklViewByPachKodTask, rasInfoByPachKodTask, chipInfoByNomZadTask, historyRazdelNaklViewByIzTask
                        , furnitZayavCheckByPachKodTask, planSezonOtdelkaViewByPachKodTask, proizvCombIzdSPByPachKodTask
                        , proizvCombIzdVZPByPachKodTask
                        , sockZadanySmenListTask, sockKnitZadanyInfoTask, sockServiceListTask
                        , sockMachiheDownTimeListTask, sockDefectListTask);
                //await Task.WhenAll(naklViewByPachKodTask, rasInfoByPachKodTask, historyRazdelNaklViewByIzTask);

                //_currentRasInfoData = new RasInfoByPachKod();
                //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData;

                //_currentChipInfoData = new ChipInfoByNomZad();
                //_chipInfoByPachKodBindingSource.DataSource = _currentChipInfoData;

                //_currentFurnitZayavCheckData = new FurnitZayavCheckByPachKod();
                //_furnitZayavCheckByPachKodBindingSource.DataSource = _currentFurnitZayavCheckData;

                //_currentNaklViewData = new List<NaklViewByPachKod>();
                //_naklViewByPachKodBindingSource.DataSource = _currentNaklViewData;
                //gcNaklList.DataSource = _naklViewByPachKodBindingSource;

                //_currentHistoryRazdelNaklViewData = new List<HistoryRazdelNaklViewByIz>();
                //_historyRazdelNaklViewByIzBindingSource.DataSource = _currentHistoryRazdelNaklViewData;
                //gcNaklList.DataSource = _naklViewByPachKodBindingSource;

                //_currentPlanSezonOtdelkaViewData = new List<PlanSezonOtdelkaView>();
                //_planSezonOtdelkaViewByPachKodBindingSource.DataSource = _currentPlanSezonOtdelkaViewData;


                //this.xtraTabControl1.Enabled = true;
                #region заполнение блока "карточка расчета"
                tbRzuNom.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuNom), true, DataSourceUpdateMode.Never);
                tbPsaNomZad.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaNomZad), true, DataSourceUpdateMode.Never);
                tbRzuPach.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuPach), true, DataSourceUpdateMode.Never);
                tbRzuKol.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuKol), true, DataSourceUpdateMode.Never);
                tbRzuDostZeh.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDostZeh), true, DataSourceUpdateMode.Never);
                tbRzuArticul.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuArticul), true, DataSourceUpdateMode.Never);
                tbRzuMod.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuMod), true, DataSourceUpdateMode.Never);
                tbPsaPrn.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaPrn), true, DataSourceUpdateMode.Never);
                tbPsaKodZv1.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaKodZv1), true, DataSourceUpdateMode.Never);
                tbPsaKodZv2.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaKodZv2), true, DataSourceUpdateMode.Never);
                tbArtGrup.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.ArtGrup), true, DataSourceUpdateMode.Never);
                tbSostPoln.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.SostPoln), true, DataSourceUpdateMode.Never);
                tbPsaTbID.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaTbID), true, DataSourceUpdateMode.Never);
                tbPsaMenName.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaMenName), true, DataSourceUpdateMode.Never);
                tbPsaNameSbit.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaNameSbit), true, DataSourceUpdateMode.Never);
                tbArtTradeMark.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.ArtTradeMark), true, DataSourceUpdateMode.Never);
                tbPsaNN.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaNN), true, DataSourceUpdateMode.Never);
                tbPsaYear.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaYear), true, DataSourceUpdateMode.Never);
                tbPsaPsaID.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaPsaID), true, DataSourceUpdateMode.Never);
                tbPsaPsaIDOsn.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaPsaIDOsn), true, DataSourceUpdateMode.Never);
                tbPsaKombIzd.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaKombIzd), true, DataSourceUpdateMode.Never);
                tbPsaKombOsn.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaKombOsn), true, DataSourceUpdateMode.Never);
                psaSezName.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaSezName), true, DataSourceUpdateMode.Never);
                pbEskiz.DataBindings.Add("ImageLocation", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PictPath), true, DataSourceUpdateMode.Never);

                cbIsChip.DataBindings.Add("Checked", _chipInfoByPachKodBindingSource, nameof(ChipInfo.isChip), true, DataSourceUpdateMode.Never);
                #endregion

                #region заполонение блока "контрольные даты"
                mtbPsaDataZap.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaDataZap), true, DataSourceUpdateMode.Never);
                mtbRzuData1С.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuData1C), true, DataSourceUpdateMode.Never);
                mtbPsaDataCdPlan.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PsaDataCdPlan), true, DataSourceUpdateMode.Never);
                mtbRzuDataCdUt.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataCdUt), true, DataSourceUpdateMode.Never);
                mtbRzuDataR.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataR), true, DataSourceUpdateMode.Never);
                tbPszRpcNom.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PszRpcNom), true, DataSourceUpdateMode.Never);
                mtbRzuDataZeh.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataZeh), true, DataSourceUpdateMode.Never);
                mtbRzuDataRab.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataRab), true, DataSourceUpdateMode.Never);
                mtbRzuDataUp.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataUp), true, DataSourceUpdateMode.Never);
                mtbRzuDataCd.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataCd), true, DataSourceUpdateMode.Never);
                #endregion

                #region заполнение блока "отделка/доп обработка"
                #region принт
                cbPszPrintPlan.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PszPrintPlan), true, DataSourceUpdateMode.Never);
                cbRzuPrintFact.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuPrintFact), true, DataSourceUpdateMode.Never);
                mtbRzuDataRasp.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataRasp), true, DataSourceUpdateMode.Never);
                mtbRzuDataPrP.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataPrP), true, DataSourceUpdateMode.Never);
                mtbRzuDataPrR.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataPrR), true, DataSourceUpdateMode.Never);
                mtbRzuDataPrPe.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataPrPe), true, DataSourceUpdateMode.Never);
                mtbRzuDataPrKm.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataPrKm), true, DataSourceUpdateMode.Never);
                mtbRzuDataPrCd.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataPrCd), true, DataSourceUpdateMode.Never);
                #endregion
                #region вышивка
                cbPszVishPlan.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PszVishPlan), true, DataSourceUpdateMode.Never);
                cbRzuVishFact.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuVishFact), true, DataSourceUpdateMode.Never);
                mtbRzuDataRasv.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataRasv), true, DataSourceUpdateMode.Never);
                mtbRzuDataVP.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataVP), true, DataSourceUpdateMode.Never);
                mtbRzuDataVR.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataVR), true, DataSourceUpdateMode.Never);
                mtbRzuDataVChi.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataVChi), true, DataSourceUpdateMode.Never);
                mtbRzuDataVCd.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataVCd), true, DataSourceUpdateMode.Never);
                #endregion
                #region стирка
                cbPszStirPlan.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.PszStirPlan), true, DataSourceUpdateMode.Never);
                cbRzuStirFact.DataBindings.Add("Checked", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuStirFact), true, DataSourceUpdateMode.Never);
                mtbRzuDataStP.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataStP), true, DataSourceUpdateMode.Never);
                mtbRzuDataStR.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataStR), true, DataSourceUpdateMode.Never);
                mtbRzuDataStCd.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuDataStCd), true, DataSourceUpdateMode.Never);
                mtbRzuVidStir.DataBindings.Add("Text", _rasInfoByPachKodBindingSource, nameof(Features.CardByNom.Models.RasInfo.RzuVidStir), true, DataSourceUpdateMode.Never);
                #endregion
                #region описание gridcontrol "отделка"
                gridControlOtdelka.DataSource = _planSezonOtdelkaViewByPachKodBindingSource;
                gridColumnOtdelkaPsaFieldName.FieldName = "psa_field_name";
                gridColumnOtdelkaKolSlZv.FieldName = "kol_sl_zv";
                gridColumnOtdelkaFrtNaimen.FieldName = "frt_naimen";
                gridColumnOtdelkaCaption.FieldName = "caption";
                gridColumnOtdelkaViNaim.FieldName = "vi_naim";
                #endregion
                #endregion

                #region описание gridcontrol "накладные"
                gridControlNaklList.DataSource = _naklViewByPachKodBindingSource;
                gridColumnNaklIzNakl.FieldName = "IzNakl";
                gridColumnNaklIzDate.FieldName = "IzDate";
                gridColumnNaklDateIzm.FieldName = "DateIzm";
                gridColumnNaklDostData.FieldName = "DostDate";
                gridColumnNaklDostN.FieldName = "DostN";
                gridColumnNaklDatePrint.FieldName = "DatePrint";
                gridColumnNaklGlNomer.FieldName = "GlNomer";
                gridColumnNaklSklNaimen.FieldName = "SklNaimen";
                gridColumnNaklPrich.FieldName = "Prich";
                gridColumnNaklCountBefore.FieldName = "CountBefore";
                gridColumnNaklCountAfter.FieldName = "CountAfter";
                gridColumnNaklArticul.FieldName = "Articul";
                gridColumnNaklMod.FieldName = "Mod";
                gridColumnNaklChipInUT.FieldName = "ChipInUT";
                gridColumnNaklChipPech.FieldName = "ChipPech";
                gridColumnNaklChipScan.FieldName = "ChipScan";
                gridColumnNaklChipOtgr.FieldName = "ChipOtgr";
                #endregion

                #region описание gridcontrol "история деления накладных"
                gridControlPartNaklList.DataSource = _historyRazdelNaklViewByIzBindingSource;
                gridColumnNaklPartID.FieldName = "id";
                gridColumnNaklPartDateIzm.FieldName = "data_izm";
                gridColumnNaklPartSklOtgrOld.FieldName = "skl_otgr_b";
                gridColumnNaklPartSklOtgrNew.FieldName = "skl_otgr_c";
                gridColumnNaklPartIzOld.FieldName = "iz_b";
                gridColumnNaklPartIzNew.FieldName = "iz_c";
                gridColumnNaklPartCountBefore.FieldName = "kol_b";
                gridColumnNaklPartCountAfter.FieldName = "kol_c";
                gridColumnNaklPartCountNew.FieldName = "kol_new";
                gridColumnNaklPartMod.FieldName = "mod";
                gridColumnNaklPartRazm.FieldName = "razm";
                gridColumnNaklPartIzObPrch.FieldName = "iz_ob_prch";
                gridColumnNaklPartStatus.FieldName = "status";
                gridColumnNaklPartCompName.FieldName = "komp_name";
                gridColumnNaklPartCompDel.FieldName = "komp_del";
                gridColumnNaklPartSklID1COld.FieldName = "skl_id_1c_b";
                gridColumnNaklPartSklID1CNew.FieldName = "skl_id_1c_c";
                gridColumnNaklPartPrichSokr.FieldName = "prich_sokr";
                gridColumnNaklPartNPach.FieldName = "n_pach";
                #endregion

                #region заполнение блока "условия для создания заявок"
                tbFurnZayav.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.furnZayav), true, DataSourceUpdateMode.Never);
                tbData_f_o.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_f_o), true, DataSourceUpdateMode.Never);
                tbFZSozdStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.fZSozdStat), true, DataSourceUpdateMode.Never);
                tbData_f_z.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_f_z), true, DataSourceUpdateMode.Never);
                tbFZSobrStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.fZSobrStat), true, DataSourceUpdateMode.Never);

                tbUpakZayav.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.upakZayav), true, DataSourceUpdateMode.Never);
                tbData_f_o_u.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_f_o_u), true, DataSourceUpdateMode.Never);
                tbUZSozdStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.uZSozdStat), true, DataSourceUpdateMode.Never);
                tbData_f_z_u.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_f_z_u), true, DataSourceUpdateMode.Never);
                tbUZSobrStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.uZSobrStat), true, DataSourceUpdateMode.Never);

                mtbData_zeh.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_zeh), true, DataSourceUpdateMode.Never);
                tbIs_got.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.is_got), true, DataSourceUpdateMode.Never);

                mtbData_cd.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.data_cd), true, DataSourceUpdateMode.Never);
                tbOtgrStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.otgrStat), true, DataSourceUpdateMode.Never);
                tbDatZayav.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.datZayav), true, DataSourceUpdateMode.Never);

                tbFurnKKStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.furnKKStat), true, DataSourceUpdateMode.Never);
                tbUpakKKStat.DataBindings.Add("Text", _furnitZayavCheckByPachKodBindingSource, nameof(FurnitZayavCheckByPachKod.upakKKStat), true, DataSourceUpdateMode.Never);
                #endregion

                #region описание gridcontrol "детали отделки: ШП"
                gridControlProizvCombIzdSP.DataSource = _proizvCombIzdSPByPachKodBindingSource;
                gridColumnProizvCombIzdSPPszNom.FieldName = "PszNom";
                gridColumnProizvCombIzdSPPszZvet.FieldName = "PszZvet";
                gridColumnProizvCombIzdSPRzuGrup.FieldName = "RzuGrup";
                gridColumnProizvCombIzdSPRzuArticul.FieldName = "RzuArticul";
                gridColumnProizvCombIzdSpRzuMod.FieldName = "RzuMod";
                gridColumnProizvCombIzdSpRzuRazm.FieldName = "RzuRazm";
                gridColumnProizvCombIzdSPKolRab.FieldName = "KolRab";
                gridColumnProizvCombIzdSPKolItog.FieldName = "KolItog";
                gridColumnProizvCombIzdSPKolRaskr.FieldName = "KolRaskr";
                gridColumnProizvCombIzdSPRzuDataRab.FieldName = "RzuDataRab";
                gridColumnProizvCombIzdSPKolGI.FieldName = "KolGI";
                gridColumnProizvCombIzdSPNIz.FieldName = "NIz";
                gridColumnProizvCombIzdSPNDostData.FieldName = "NDostData";
                gridColumnProizvCombIzdSPKolFurnPrinSkl.FieldName = "KolFurnPrinSkl";
                gridColumnProizvCombIzdSPDateFurnPrihSkl.FieldName = "DateFurnPrinSkl";
                #endregion

                #region описание gridcontrol "детали отделки: ВЗП"
                gridControlProizvCombIzdVZP.DataSource = _proizvCombIzdVZPByPachKodBindingSource;
                gridColumnProizvCombIzdVZPPszNom.FieldName = "PszNom";
                gridColumnProizvCombIzdVZPPszZvet.FieldName = "PszZvet";
                gridColumnProizvCombIzdVZPRzvGrup.FieldName = "RzvGrup";
                gridColumnProizvCombIzdVZPRzvArticul.FieldName = "RzvArticul";
                gridColumnProizvCombIzdVZPRzvMod.FieldName = "RzvMod";
                gridColumnProizvCombIzdVZPRzvRazm.FieldName = "RzvRazm";
                gridColumnProizvCombIzdVZPKolItog.FieldName = "KolItog";
                gridColumnProizvCombIzdVZPNIz.FieldName = "NIz";
                gridColumnProizvCombIzdVZPKolVyaz.FieldName = "KolVyaz";
                gridColumnProizvCombIzdVZPKolOtparka.FieldName = "KolOtparka";
                gridColumnProizvCombIzdVZPKolGI.FieldName = "KolGI";
                gridColumnProizvCombIzdVZPKolFurnPrinSkl.FieldName = "KolFurnPrinSkl";
                gridColumnProizvCombIzdVZPRzvDateOkonV.FieldName = "RzvDateOkonv";
                gridColumnProizvCombIzdVZPNDostData.FieldName = "NDostData";
                gridColumnProizvCombIzdVZPDateFurnPrihSkl.FieldName = "DateFurnPrinSkl";
                #endregion

                #region TabPage Носки. Отчет по заданию. Задание
                //TextBoxNomZad.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kzPszNom), true, DataSourceUpdateMode.Never);
                //TextBoxArticul.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.articul), true, DataSourceUpdateMode.Never);
                //TextBoxKol.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kol), true, DataSourceUpdateMode.Never);
                //TextBoxColor.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.zv_tkan), true, DataSourceUpdateMode.Never);
                //TextBoxPachList.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.pach), true, DataSourceUpdateMode.Never);

                #endregion

                #region TabPage Носки. Отчет по заданию. Вязание
                TextBoxTabFio.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.rList), true, DataSourceUpdateMode.Never);
                TextBoxAreaNumber.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kmaNumber), true, DataSourceUpdateMode.Never);
                TextBoxMachineNumber.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kmlNumber), true, DataSourceUpdateMode.Never);
                TextBoxKnitStartDate.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.dateStart), true, DataSourceUpdateMode.Never);
                TextBoxKnitEndDate.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.dateEnd), true, DataSourceUpdateMode.Never);
                TextBoxPachList.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.pach), true, DataSourceUpdateMode.Never);
                TextBoxKolPlanZadany.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kol), true, DataSourceUpdateMode.Never);
                TextBoxKolFactSmen.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kolFakt), true, DataSourceUpdateMode.Never);
                TextBoxKolFactZadany.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kolFactZadany), true, DataSourceUpdateMode.Never);
                TextBoxKolFactDelta.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kolFactDelta), true, DataSourceUpdateMode.Never);
                TextBoxDefectWeight.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kgDefects), true, DataSourceUpdateMode.Never);
                TextBoxDefectCount.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.kolDefects), true, DataSourceUpdateMode.Never);

                //TextBoxDownTimeList.DataBindings.Add("Text", _sockKnitZadanyInfoBindingSource, nameof(Features.CardByNom.Models.SockZadanyInfo.dtList), true, DataSourceUpdateMode.Never);
                _sockKnitZadanyInfoBindingSource.CurrentItemChanged += (_, __) => RecalcFromModel();
                _sockKnitZadanyInfoBindingSource.PositionChanged += (_, __) => RecalcFromModel();
                RecalcFromModel();
                #endregion

                #region TabPage Носки. Отчет по заданию. gridControl Смены
                gridControlSockZadanySmenList.DataSource = _sockZadanySmenListBindingSource;
                gridSockZadanySmenListColumnKzDateAdd.FieldName = "kzDateAdd";
                gridSockZadanySmenListColumnKwsTabStart.FieldName = "kwsTabStart";
                gridSockZadanySmenListColumnFioSt.FieldName = "fioSt";
                gridSockZadanySmenListColumnKzDateEnd.FieldName = "kzDateEnd";
                gridSockZadanySmenListColumnKwsTabEnd.FieldName = "kwsTabEnd";
                gridSockZadanySmenListColumnFioEn.FieldName = "fioEn";
                gridSockZadanySmenListColumnKolFact.FieldName = "kolFakt";
                gridSockZadanySmenListColumnChasVyaz.FieldName = "chasVyaz";
                gridSockZadanySmenListColumnDiffPeriod.FieldName = "DiffPeriod";
                gridSockZadanySmenListColumnKmaNumber.FieldName = "kmaNumber";
                gridSockZadanySmenListColumnKmlNumber.FieldName = "kmlNumber";
                gridSockZadanySmenListColumnKmlInvNumber.FieldName = "kmlInvNumber";
                gridSockZadanySmenListColumnKzID.FieldName = "kzID";
                gridSockZadanySmenListColumnKzKwsID.FieldName = "kzKwsID";
                gridSockZadanySmenListColumnKzKmlID.FieldName = "kzKmlID";
                gridSockZadanySmenListColumnKzKmaID.FieldName = "kzKmaID";
                gridSockZadanySmenListColumnKzEnded.FieldName = "kzEnded";
                gridSockZadanySmenListColumnDivider.FieldName = "divider";
                gridSockZadanySmenListColumnKwsKmsID.FieldName = "kwsKmsID";
                #endregion

                #region TabPage Носки. Отчет по заданию. gridControl Обслуживание оборудования
                gridControlSockServiceList.DataSource = _sockServiceListBindingSource;
                gridSockServiceListColumnKzPszNom.FieldName = "kzPszNom";
                gridSockServiceListColumnKmaNumber.FieldName = "kmaNumber";
                gridSockServiceListColumnKmlInvNum.FieldName = "kmlInvNum";
                gridSockServiceListColumnKmlNumber.FieldName = "kmlNumber";
                gridSockServiceListColumnTextObS.FieldName = "text_ob_s";
                gridSockServiceListColumnDirectorName.FieldName = "directorName";
                gridSockServiceListColumnDate.FieldName = "date";
                gridSockServiceListColumnResultName.FieldName = "resultName";
                gridSockServiceListColumnResultText.FieldName = "ResultText";
                gridSockServiceListColumnDateEnd.FieldName = "dateEnd";
                gridSockServiceListColumnMechanic.FieldName = "mechanic";
                gridSockServiceListColumnDiffPeriod.FieldName = "DiffPeriod";
                #endregion

                #region TabPage Носки. Отчет по заданию. gridControl Простои оборудования
                gridControlSockDownTimeList.DataSource = _sockDownTimeListBindingSource;
                gridSockDownTimeListColumnKzPszNom.FieldName = "kzPszNom";
                gridSockDownTimeListColumnKmaNumber.FieldName = "kmaNumber";
                gridSockDownTimeListColumnKmlInvNumber.FieldName = "kmlInvNumber";
                gridSockDownTimeListColumnKmlNumber.FieldName = "kmlNumber";
                gridSockDownTimeListColumnKmlID.FieldName = "kmlID";
                gridSockDownTimeListColumnTextObS.FieldName = "text_ob_s";
                gridSockDownTimeListColumnKdtlDateStart.FieldName = "kdtlDateStart";
                gridSockDownTimeListColumnKdtlDateEnd.FieldName = "kdtlDateEnd";
                gridSockDownTimeListColumnDaysDiff.FieldName = "DaysDiff";
                gridSockDownTimeListColumnTimeDiff.FieldName = "TimeDiff";
                gridSockDownTimeListColumnDiffPeriod.FieldName = "DiffPeriod";
                #endregion

                #region TabPage Носки. Отчет по заданию. gridControl Причины брака
                gridControlSockDefectList.DataSource = _sockDefectListBindingSource;
                gridSockDefectListColumnVspdid.FieldName = "vspdid";
                gridSockDefectListColumnNomZadany.FieldName = "nom_zadany";
                gridSockDefectListColumnIsdefect.FieldName = "isdefect";
                gridSockDefectListColumnKg.FieldName = "kg";
                gridSockDefectListColumnKolAll.FieldName = "kolAll";
                gridSockDefectListColumnKolDefect.FieldName = "kolDefect";
                gridSockDefectListColumnIdspj.FieldName = "idspj";
                gridSockDefectListColumnIdndsp.FieldName = "id_ndsp";
                gridSockDefectListColumnNamedefect.FieldName = "namedefect";
                #endregion

                //nameTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Articul), true, DataSourceUpdateMode.OnPropertyChanged);
                //groupTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Group), true, DataSourceUpdateMode.OnPropertyChanged);
                //modelTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Mod), true, DataSourceUpdateMode.OnPropertyChanged);
                //secTimeTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Sek), true, DataSourceUpdateMode.OnPropertyChanged);

                //designerComboBox.DataBindings.Add("SelectedValue", bindingSource1, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                //constructorComboBox.DataBindings.Add("SelectedValue", bindingSource1, nameof(ArtNormN.Constr), true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        private void RecalcFromModel()
        {
            if (_sockKnitZadanyInfoBindingSource.Current is Features.CardByNom.Models.SockZadanyInfo m
                && m.dateStart != null && m.dateEnd != null)
            {
                var ts = m.dateEnd.Value - m.dateStart.Value;
                var sign = ts < TimeSpan.Zero ? "-" : "";
                ts = ts.Duration();
                TextBoxKnitTotalTime.Text = sign + ts.ToString(@"d' д 'hh\:mm\:ss");
            }
            else
            {
                TextBoxKnitTotalTime.Clear();
            }
        }

        private async Task WorkDivisionLoadAsync(string caller, string pachKod)
        {
            //if (pachKod == " ")
            //    return;
            //try
            //{
            //    //_pachKod = GetPachKod();
            //    // Загружаем все данные параллельно
            //    var rasInfoTask = _cardByNomService.GetRasInfoByPachKod(pachKod);
            //    var naklViewTask = _cardByNomService.GetNaklViewByPachKod(pachKod);
            //    var furnitZayavCheckTask = _furnitService.GetFurnitZayavCheckByPachKod(pachKod);

            //    //var historyRazdelNaklViewTask = _cardByNomService.GetHistoryRazdelNaklViewByIz(iz);


            //    await Task.WhenAll(naklViewTask, rasInfoTask, furnitZayavCheckTask);
            //    //await Task.WhenAll(naklViewTask, rasInfoTask);
            //    //await Task.WhenAll(naklViewTask);

            //    await this.InvokeAsync(() =>
            //    {
            //        // Обновляем списки
            //        _currentNaklViewData.Clear();
            //        LoadList(naklViewTask.Result, _naklViewByPachKodList, nameof(NaklViewByPachKod.Iz));
            //        _naklViewByPachKodBindingSource.ResetBindings(false);
            //        // _currentRasInfoData.Clear();


            //        //  bool isCopyOrBuffer = _mode == (int)Mode.ArchAndCopy || caller == "buffer";

            //        //LoadList(raszTask.Result, _normRaszList, nameof(NormRasz.nrId), isCopyOrBuffer);
            //        //LoadList(raskTask.Result, _normRaskList, nameof(NormRask.id), isCopyOrBuffer);
            //        //LoadList(kontList, _normKontList, nameof(NormKont.kontId), isCopyOrBuffer);
            //        //LoadList(dopObrList, _normDopObrList, nameof(NormDopObr.dopObrId), isCopyOrBuffer);

            //        // Обновляем привязки
            //        //_normRaszBindingSource.ResetBindings(false);


            //        // Обновляем данные RasInfo через биндинг
            //        var rasInfoData = rasInfoTask.Result;
            //        if (rasInfoData != null)
            //        {
            //            _currentRasInfoData = rasInfoData;
            //        }
            //        else
            //        {
            //            _currentRasInfoData = new RasInfoByPachKod();
            //        }
            //        _rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData;

            //        // Обновляем данные FurnitZayavCheck через биндинг
            //        var FurnitZayavCheckData = furnitZayavCheckTask.Result;
            //        if (FurnitZayavCheckData != null)
            //        {
            //            _currentFurnitZayavCheckData = FurnitZayavCheckData;
            //        }
            //        else
            //        {
            //            _currentFurnitZayavCheckData = new FurnitZayavCheckByPachKod();
            //        }
            //        _furnitZayavCheckByPachKodBindingSource.DataSource = _currentFurnitZayavCheckData;

            //        // _rasInfoByPachKodBindingSource.ResetBindings(false);
            //        // Обновляем данные NaklView через биндинг
            //        //var naklViewData = naklViewTask.Result;
            //        //if (naklViewData != null)
            //        //{
            //        //    _currentNaklViewData = naklViewData;
            //        //    _naklViewByPachKodBindingSource.DataSource = _currentNaklViewData;
            //        //}
            //    });

            //    var selectedRow = new NaklViewByPachKod();
            //    if (_naklViewByPachKodBindingSource.Current == null && _naklViewByPachKodBindingSource.Count > 0)
            //    {
            //        selectedRow = _naklViewByPachKodBindingSource[0] as NaklViewByPachKod;
            //    }
            //    else
            //    {
            //        selectedRow = _naklViewByPachKodBindingSource.Current as NaklViewByPachKod;
            //    }


            //    _currentHistoryRazdelNaklViewData.Clear();
            //    if (selectedRow != null)
            //    {
            //        var historyRazdelNaklViewTask = _cardByNomService.GetHistoryRazdelNaklViewByIz(selectedRow.Iz);
            //        LoadList(historyRazdelNaklViewTask.Result, _historyRazdelNaklViewByIzList, nameof(HistoryRazdelNaklViewByIz.id));
            //    }
            //    _historyRazdelNaklViewByIzBindingSource.ResetBindings(false);
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных WorkDivisionLoadAsync");

            //    await this.InvokeAsync(() =>
            //    {
            //        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    });
            //}
        }

        private void LoadList<T>(List<T> sourceList, BindingList<T> targetList, string idFieldName)
        {
            foreach (var item in sourceList)
            {
                var idProp = typeof(T).GetProperty(idFieldName);
                targetList.Add(item);
            }
        }

        //private void gridView4_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        //{
        //    PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
        //    pb.PageSettings.Landscape = true;
        //}

        private string GetPachKod()
        {
            string pachKod = string.Concat(tbYearPach.Text, tbNomPach.Text.PadLeft(6));
            return pachKod;
        }
        private async Task UpdateFurnitUpak()
        {

            //string pachKod = GetPachKod();
            await LoadFurnitZayavCheckDataAsync(GetPachKod());

            var selectedRow = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            if (selectedRow != null)
            {
                if (selectedRow.is_got == "--".ToString())
                {
                    tbIs_got.ForeColor = Color.Red;
                    mtbData_zeh.ForeColor = Color.Red;
                }
                else
                {
                    tbIs_got.ForeColor = Color.Black;
                    mtbData_zeh.ForeColor = Color.Black;
                }

                if (selectedRow.otgrStat == 'V'.ToString())
                {
                    tbOtgrStat.ForeColor = Color.Red;
                    mtbData_cd.ForeColor = Color.Red;
                }
                else
                {
                    tbOtgrStat.ForeColor = Color.Black;
                    mtbData_cd.ForeColor = Color.Black;
                }


                furnitZayavViewFurnit.Text = (selectedRow.FKodFD ?? " ".PadRight(12)).Substring(0, 12);
                furnitZayavViewFurnit.ViewType = "r";
                furnitZayavViewFurnit.Refresh();

                furnitZayavViewUpak.Text = (selectedRow.UKodFD ?? " ".PadRight(12)).Substring(0, 12);
                furnitZayavViewUpak.ViewType = "r";
                furnitZayavViewUpak.Refresh();

                fspecrez = selectedRow.FSpecRez;
                uspecrez = selectedRow.USpecRez;
                fkodfd = selectedRow.FKodFD;
                ukodfd = selectedRow.UKodFD;


                if (selectedRow.furnKKStat != "V" && selectedRow.is_furnit == 1)
                {
                    tbFurnKKStat.ForeColor = Color.Red;
                    simpleButtonFurnKKPrint.ForeColor = Color.Red;
                }
                else
                {
                    tbFurnKKStat.ForeColor = Color.Black;
                    simpleButtonFurnKKPrint.ForeColor = Color.Black;
                }

                if (selectedRow.upakKKStat != "V" && selectedRow.is_upak == 1)
                {
                    tbUpakKKStat.ForeColor = Color.Red;
                    simpleButtonUpakKKPrint.ForeColor = Color.Red;
                }
                else
                {
                    tbUpakKKStat.ForeColor = Color.Black;
                    simpleButtonUpakKKPrint.ForeColor = Color.Black;
                }
            }
        }
        private async Task UpdateProizvCombIzd()
        {
            Task proizvCombIzdSPTask = LoadProizvCombIzdSPDataAsync(GetPachKod());
            Task proizvCombIzdVZPTask = LoadPProizvCombIzdVZPDataAsync(GetPachKod());
            await Task.WhenAll(proizvCombIzdSPTask, proizvCombIzdVZPTask);
        }
        private async void CardByNom_Load(object sender, EventArgs e)
        {
            //ThemeManager.UpdateTheme(this);

            this.tbYearPach.Text = Convert.ToString(DateTime.Now.Year);
            customRadioGroup2.SelectedIndex = 0;
            customRadioGroup3.SelectedIndex = 0;
            customRadioGroup3_SelectedIndexChanged(sender, e);

            //tbNomPach.Select();

            try
            {
                Task bindingsTask = InitializeBindingsAsync();

                await Task.WhenAll(bindingsTask);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы CardByNom");
            }

        }

        /// <summary>
        /// Загрузка данных ANN по ID с групповым обновлением UI.
        /// </summary>
        private async Task LoadRasInfoByPachKodDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                //RasCard.Enabled = false;
                _rasInfoByPachKodBindingSource.Clear();
                _rasInfoByPachKodBindingSource.ResetBindings(false);
                var rasInfoData = await _raskrService.GetRasInfoByPachKod(pachKod);
                if (rasInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RasInfo: PachKod={rasInfoData.RzuPach}, Articul={rasInfoData.RzuArticul}", "LoadRasInfoByPachKodDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                        _currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        _rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные RasInfo успешно загружены для PachKod {pachKod}", "LoadRasInfoByPachKodDataAsync");
                    //RasCard.Enabled = true ;
                    _rasInfoByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RasInfo для PachKod {pachKod}", "LoadRasInfoByPachKodDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RasInfo для pach_kod {pachKod}");
            }
        }
        private async Task LoadRasInfoByNomZadDataAsync(string nomZad)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                //RasCard.Enabled = false;
                _rasInfoByPachKodBindingSource.Clear();
                _rasInfoByPachKodBindingSource.ResetBindings(false);
                var rasInfoData = await _raskrService.GetRasInfoByNomZad(nomZad);
                if (rasInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RasInfo: nomZad={rasInfoData.PsaNomZad}, Articul={rasInfoData.RzuArticul}", "LoadRasInfoByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        //_rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                        _currentRasInfoData = rasInfoData;                // Обновляем текущую модель
                        _rasInfoByPachKodBindingSource.DataSource = _currentRasInfoData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные RasInfo успешно загружены для NomZad {nomZad}", "LoadRasInfoByNomZadDataAsync");
                    //RasCard.Enabled = true ;
                    _rasInfoByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RasInfo для NomZad {nomZad}", "LoadRasInfoByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RasInfo для NomZad {nomZad}");
            }
        }
        private async Task LoadChipInfoByPachKodDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _chipInfoByPachKodBindingSource.Clear();
                _chipInfoByPachKodBindingSource.ResetBindings(false);
                var chipInfoData = await _cardByNomService.GetChipInfoByPachKod(pachKod);
                if (chipInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ChipInfo: PachKod={pachKod}, isChip={chipInfoData.isChip}", "LoadChipInfoDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentChipInfoData = chipInfoData;                // Обновляем текущую модель
                        _chipInfoByPachKodBindingSource.DataSource = _currentChipInfoData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ChipInfo успешно загружены для PachKod {pachKod}", "LoadChipInfoDataAsync");
                    _chipInfoByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ChipInfo для PachKod {pachKod}", "LoadChipInfoDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ChipInfo для pach_kod {pachKod}");
            }
        }

        private async Task LoadChipInfoByNomZadDataAsync(string nomZad)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _chipInfoByPachKodBindingSource.Clear();
                _chipInfoByPachKodBindingSource.ResetBindings(false);
                var chipInfoData = await _cardByNomService.GetChipInfoByNomZad(nomZad);
                if (chipInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ChipInfo: nomZad={nomZad}, isChip={chipInfoData.isChip}", "LoadChipInfoByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentChipInfoData = chipInfoData;                // Обновляем текущую модель
                        _chipInfoByPachKodBindingSource.DataSource = _currentChipInfoData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ChipInfo успешно загружены для NomZad {nomZad}", "LoadChipInfoByNomZadDataAsync");
                    _chipInfoByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ChipInfo для NomZad {nomZad}", "LoadChipInfoByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ChipInfo для NomZad {nomZad}");
            }
        }
        private async Task LoadPlanSezonOtdelkaViewDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _planSezonOtdelkaViewByPachKodBindingSource.Clear();
                _planSezonOtdelkaViewByPachKodBindingSource.ResetBindings(false);
                var planSezonOtdelkaViewData = await _matrixService.GetPlanSezonOtdelkaViewByPachKod(pachKod);
                if (planSezonOtdelkaViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные view_plan_sezon_otdelka: nn={planSezonOtdelkaViewData[0].nn}", "LoadPlanSezonOtdelkaViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentPlanSezonOtdelkaViewData = planSezonOtdelkaViewData;                // Обновляем текущую модель
                        _planSezonOtdelkaViewByPachKodBindingSource.DataSource = _currentPlanSezonOtdelkaViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные view_plan_sezon_otdelka успешно загружены для PachKod {pachKod}", "LoadPlanSezonOtdelkaViewDataAsync");
                    _planSezonOtdelkaViewByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные view_plan_sezon_otdelka для PachKod {pachKod}", "LoadPlanSezonOtdelkaViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных view_plan_sezon_otdelka для pach_kod {pachKod}");
            }
        }
        private async Task LoadFurnitZayavCheckDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _furnitZayavCheckByPachKodBindingSource.Clear();
                _furnitZayavCheckByPachKodBindingSource.ResetBindings(false);
                var FurnitZayavCheckData = await _furnitService.GetFurnitZayavCheckByPachKod(pachKod);
                if (FurnitZayavCheckData != null)
                {
                    await _logger.LogEventAsync($"Получены данные FurnitZayavCheck: PachKod={FurnitZayavCheckData.pach_kod}", "LoadFurnitZayavCheckDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentFurnitZayavCheckData = FurnitZayavCheckData;                // Обновляем текущую модель
                        _furnitZayavCheckByPachKodBindingSource.DataSource = _currentFurnitZayavCheckData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные FurnitZayavCheck успешно загружены для PachKod {pachKod}", "LoadFurnitZayavCheckDataAsync");
                    _furnitZayavCheckByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные FurnitZayavCheck для PachKod {pachKod}", "LoadFurnitZayavCheckDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных FurnitZayavCheck для pach_kod {pachKod}");
            }
        }
        private async Task LoadNaklViewDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _naklViewByPachKodBindingSource.Clear();
                _naklViewByPachKodBindingSource.ResetBindings(false);
                var naklViewData = await _cardByNomService.GetNaklViewByPachKod(pachKod);
                if (naklViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные NaklView: iz={naklViewData[0].Iz}, Articul={naklViewData[0].Articul}", "LoadNaklViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentNaklViewData = naklViewData;                // Обновляем текущую модель
                        _naklViewByPachKodBindingSource.DataSource = _currentNaklViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные Naklview успешно загружены для PachKod {pachKod}", "LoadNaklViewDataAsync");
                    _naklViewByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные Naklview для PachKod {pachKod}", "LoadNaklViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных Naklview для pach_kod {pachKod}");
            }
        }
        private async Task LoadNaklViewByNomZadDataAsync(string nomZad)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                _naklViewByPachKodBindingSource.Clear();
                _naklViewByPachKodBindingSource.ResetBindings(false);
                var naklViewData = await _cardByNomService.GetNaklViewByNomZad(nomZad);
                if (naklViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные NaklView: nomZad={naklViewData[0].nom_zad}, Articul={naklViewData[0].Articul}", "LoadNaklViewByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentNaklViewData = naklViewData;                // Обновляем текущую модель
                        _naklViewByPachKodBindingSource.DataSource = _currentNaklViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные Naklview успешно загружены для NomZad {nomZad}", "LoadNaklViewByNomZadDataAsync");
                    _naklViewByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные Naklview для NomZad {nomZad}", "LoadNaklViewByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных Naklview для NomZad {nomZad}");
            }
        }
        private async Task LoadHistoryRazdelNaklViewDataAsync(string iz)
        {
            try
            {
                var historyRazdelNaklViewData = await _cardByNomService.GetHistoryRazdelNaklViewByIz(iz);
                if (historyRazdelNaklViewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные History_razdel_nakl_view: iz_b={historyRazdelNaklViewData[0].iz_b}", "LoadHistoryRazdelNaklViewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentHistoryRazdelNaklViewData = historyRazdelNaklViewData;                // Обновляем текущую модель
                        _historyRazdelNaklViewByIzBindingSource.DataSource = _currentHistoryRazdelNaklViewData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные History_razdel_nakl_view успешно загружены для iz {iz}", "LoadHistoryRazdelNaklViewDataAsync");
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные History_razdel_nakl_view для iz {iz}", "LoadHistoryRazdelNaklViewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных History_razdel_nakl_view для iz {iz}");
            }
        }

        private async Task LoadProizvCombIzdSPDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                int vidPr = 1; // 1 - ШП, 2 - ВЗП

                _proizvCombIzdSPByPachKodBindingSource.Clear();
                _proizvCombIzdSPByPachKodBindingSource.ResetBindings(false);
                var proizvCombIzdSPData = await _cardByNomService.GetProizvCombIzdByPachKod(pachKod, vidPr);
                if (proizvCombIzdSPData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ProizvCombIzd: psa_id_osn={proizvCombIzdSPData[0].PsaIDOsn}", "LoadPProizvCombIzdDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentProizvCombIzdSPData = proizvCombIzdSPData;                // Обновляем текущую модель
                        _proizvCombIzdSPByPachKodBindingSource.DataSource = _currentProizvCombIzdSPData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ProizvCombIzd успешно загружены для psa_id_osn {pachKod}", "LoadPProizvCombIzdDataAsync");
                    _proizvCombIzdSPByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ProizvCombIzd для psa_id_osn {pachKod}", "LoadPProizvCombIzdDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ProizvCombIzd для pach_kod {pachKod}");
            }
        }
        private async Task LoadPProizvCombIzdVZPDataAsync(string pachKod)
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                //       int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;

                //       await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                int vidPr = 2; // 1 - ШП, 2 - ВЗП

                _proizvCombIzdVZPByPachKodBindingSource.Clear();
                _proizvCombIzdVZPByPachKodBindingSource.ResetBindings(false);
                var proizvCombIzdVZPData = await _cardByNomService.GetProizvCombIzdByPachKod(pachKod, vidPr);
                if (proizvCombIzdVZPData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ProizvCombIzd: psa_id_osn={proizvCombIzdVZPData[0].PsaIDOsn}", "LoadPProizvCombIzdVZPDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentProizvCombIzdVZPData = proizvCombIzdVZPData;                // Обновляем текущую модель
                        _proizvCombIzdVZPByPachKodBindingSource.DataSource = _currentProizvCombIzdVZPData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные ProizvCombIzd успешно загружены для psa_id_osn {pachKod}", "LoadPProizvCombIzdVZPDataAsync");
                    _proizvCombIzdVZPByPachKodBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ProizvCombIzd для psa_id_osn {pachKod}", "LoadPProizvCombIzdVZPDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ProizvCombIzd для pach_kod {pachKod}");
            }
        }

        private async Task LoadSockKnitZadanyInfoDataAsync(string nomZad)
        {
            try
            {
                _sockKnitZadanyInfoBindingSource.Clear();
                _sockKnitZadanyInfoBindingSource.ResetBindings(false);
                var sockKnitZadanyInfoData = await _sockService.GetSockKnitZadanyInfo(nomZad);
                if (sockKnitZadanyInfoData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SockKnitZadanyInfo: nomZad='{nomZad}'", "LoadSockKnitZadanyInfoDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSockKnitZadanyInfoData = sockKnitZadanyInfoData;                // Обновляем текущую модель
                        _sockKnitZadanyInfoBindingSource.DataSource = _currentSockKnitZadanyInfoData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SockKnitZadanyInfo успешно загружены для nomZad {nomZad}", "LoadSockKnitZadanyInfoDataAsync");
                    _sockKnitZadanyInfoBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SockKnitZadanyInfo для nomZad {nomZad}", "LoadSockKnitZadanyInfoDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SockKnitZadanyInfo для nomZad {nomZad}");
            }
        }
        private async Task LoadSockZadanySmenListDataAsync(string nomZad)
        {
            try
            {
                _sockZadanySmenListBindingSource.Clear();
                _sockZadanySmenListBindingSource.ResetBindings(false);
                var sockZadanySmenListData = await _sockService.GetSockZadanySmenListByNomZad(nomZad);
                if (sockZadanySmenListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SockZadanySmenList: nomZad='{nomZad}'", "LoadSockZadanySmenListDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSockZadanySmenListData = sockZadanySmenListData;                // Обновляем текущую модель
                        _sockZadanySmenListBindingSource.DataSource = _currentSockZadanySmenListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SockZadanySmenList успешно загружены для nomZad {nomZad}", "LoadSockZadanySmenListDataAsync");
                    _sockZadanySmenListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SockZadanySmenList для nomZad {nomZad}", "LoadSockZadanySmenListDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SockZadanySmenList для nomZad {nomZad}");
            }
        }
        private async Task LoadSockServiceListByNomZadDataAsync(string nomZad)
        {
            try
            {
                _sockServiceListBindingSource.Clear();
                _sockServiceListBindingSource.ResetBindings(false);
                var sockServiceListData = await _sockService.GetSockServiceListByNomZad(nomZad);
                if (sockServiceListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SockServiceList: nomZad='{nomZad}'", "LoadSockServiceListByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSockServiceListData = sockServiceListData;                // Обновляем текущую модель
                        _sockServiceListBindingSource.DataSource = _currentSockServiceListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SockServiceList успешно загружены для nomZad {nomZad}", "LoadSockServiceListByNomZadDataAsync");
                    _sockServiceListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SockServiceList для nomZad {nomZad}", "LoadSockServiceListByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SockServiceList для nomZad {nomZad}");
            }
        }
        private async Task LoadSockDownTimeListByNomZadDataAsync(string nomZad)
        {
            try
            {
                _sockDownTimeListBindingSource.Clear();
                _sockDownTimeListBindingSource.ResetBindings(false);
                var sockDownTimeListData = await _sockService.GetSockDownTimeListByNomZad(nomZad);
                if (sockDownTimeListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SockServiceList: nomZad='{nomZad}'", "LoadSockDownTimeListByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSockDownTimeListData = sockDownTimeListData;                // Обновляем текущую модель
                        _sockDownTimeListBindingSource.DataSource = _currentSockDownTimeListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SockDownTimeList успешно загружены для nomZad {nomZad}", "LoadSockDownTimeListByNomZadDataAsync");
                    _sockDownTimeListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SockDownTimeList для nomZad {nomZad}", "LoadSockDownTimeListByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SockDownTimeList для nomZad {nomZad}");
            }
        }

        private async Task LoadSockDefectListByNomZadDataAsync(string nomZad)
        {
            try
            {
                _sockDefectListBindingSource.Clear();
                _sockDefectListBindingSource.ResetBindings(false);
                var sockDefectListData = await _sockService.GetSockDefectListByNomZad(nomZad);
                if (sockDefectListData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SockDefectList: nomZad='{nomZad}'", "LoadSockDefectListByNomZadDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSockDefectListData = sockDefectListData;                // Обновляем текущую модель
                        _sockDefectListBindingSource.DataSource = _currentSockDefectListData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SockDefectList успешно загружены для nomZad {nomZad}", "LoadSockDefectListByNomZadDataAsync");
                    _sockDefectListBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SockDefectList для nomZad {nomZad}", "LoadSockDefectListByNomZadDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SockDefectList для nomZad {nomZad}");
            }
        }
        private async void tbNomPach_KeyDown(object sender, KeyEventArgs e)
        {
            RasInfo.PageVisible = true;
            FurnInfo.PageVisible = true;
            WorkInfo.PageVisible = true;
            OtdelkaInfo.PageVisible = true;
            SockZadanyInfo.PageVisible = false;

            if (e.KeyCode == Keys.Enter)
            {
                xtraTabControl1.Enabled = true;
                xtraTabControl1.Refresh();
                //WorkDivisionLoadAsync(caller: "DataLoad", GetPachKod());
                //Task wDLoadTask = WorkDivisionLoadAsync(caller: "DataLoad", GetPachKod());
                Task rasInfoLoadTask = LoadRasInfoByPachKodDataAsync(GetPachKod());
                Task naklViewLoadTask = LoadNaklViewDataAsync(GetPachKod());
                Task chipInfoLoadTask = LoadChipInfoByPachKodDataAsync(GetPachKod());
                Task planSezonOtdelkaLoadTask = LoadPlanSezonOtdelkaViewDataAsync(GetPachKod());
                //await Task.WhenAll(wDLoadTask, rasInfoLoadTask, naklViewLoadTask);
                await Task.WhenAll(rasInfoLoadTask, naklViewLoadTask, chipInfoLoadTask, planSezonOtdelkaLoadTask);

                if (_currentRasInfoData.RzuNom == 0)
                {
                    xtraTabControl1.Enabled = false;
                    MessageBox.Show("Поиск не дал результатов. Измените параметры и повторите попытку");
                }
                else
                {
                    var selectedRow = _rasInfoByPachKodBindingSource.Current as RasInfo;
                    int _psaPsaIDOsn = selectedRow.PsaPsaIDOsn;
                    string _psaKombIzd = selectedRow.PsaKombIzd;
                    int _psaTkIdSet = selectedRow.PsaTkIdSet;

                    if (_psaPsaIDOsn != 0
                        && (("V;F").IndexOf(_psaKombIzd) >= 0 || _psaTkIdSet == 1)
                        && (_psaTkIdSet == 0))
                    {
                        OtdelkaInfo.PageVisible = true;
                    }
                    else
                    {
                        OtdelkaInfo.PageVisible = false;
                    }

                    ////string nomZad = _currentRasInfoData.PsaNomZad;

                    ////string queryIsChip = $"SELECT dbo.checkChipNakl('', '{NomZad}') AS isChip ";
                    //var selectedRow = _rasInfoByPachKodBindingSource.Current as RasInfoByPachKod;
                    //string queryIsChip = $"SELECT dbo.checkChipNakl('', '{selectedRow.PsaNomZad}') AS isChip ";
                    //var dtIsChip = _dbHelper.ExecuteQuery(queryIsChip);
                    //bsIsChip.DataSource = dtIsChip;
                    //this.cbIsChip.DataBindings.Clear();
                    //this.cbIsChip.DataBindings.Add("Checked", dtIsChip, "isChip");

                    //string queryOtdelkaList = $"SELECT vpso.psa_field_name, vpso.kol_sl_zv, vpso.frt_naimen, DetIzdName, VidIzdName ";
                    //queryOtdelkaList += $" FROM View_plan_sezon_otdelka vpso ";
                    //queryOtdelkaList += $" WHERE vpso.nn = '{_currentRasInfoData.PsaNN}' ";
                    //var dtOtdelkaList = _dbHelper.ExecuteQuery(queryOtdelkaList);
                    //bsOtdelkaList.DataSource = dtOtdelkaList;

                    if (xtraTabControl1.SelectedTabPageIndex == 1)
                    {
                        UpdateFurnitUpak();
                    }
                    if (xtraTabControl1.SelectedTabPageIndex == 3)
                    {
                        UpdateProizvCombIzd();
                    }
                }

            }
        }
        private void btnNaklPrint_Click(object sender, EventArgs e)
        {
            //string iz = GetIzNakl();
            NaklReport report1 = new NaklReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_naklIz"].Value = selectedRow.Iz;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        //private void btnNaklPart_Click(object sender, EventArgs e)
        //{
        //    if (btnNaklPart.Text == "Показать информацию по делению накладной")
        //    {
        //        btnNaklPart.Text = "Скрыть информацию по делению накладной";
        //        string iz = GetIzNakl();

        //        string queryPartNaklList = $"SELECT * FROM View_History_razdel_nakl where iz_b = '{iz}' ";
        //        queryPartNaklList += $" order by id";
        //        var dtPartNaklList = _dbHelper.ExecuteQuery(queryPartNaklList);

        //        this.gcPartNaklList.Location = this.gcNaklList.Location;
        //        this.gcPartNaklList.Size = this.gcNaklList.Size;
        //        this.gcPartNaklList.BringToFront();
        //        this.gcPartNaklList.Visible = true;
        //    }
        //    else
        //    {
        //        btnNaklPart.Text = "Показать информацию по делению накладной";
        //        this.gcNaklList.BringToFront();
        //        this.gcPartNaklList.Visible = false;
        //    }

        //}
        private void btnPrintMLRTAll_Click(object sender, EventArgs e)
        {
            //int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            MlRtReport report1 = new MlRtReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
            report1.Parameters["_isChip"].Value = IsChip;
            report1.Parameters["_isUpak"].Value = 0;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

        }
        private void btnPrintMLRTUpak_Click(object sender, EventArgs e)
        {
            int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            MlRtReport report1 = new MlRtReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
            report1.Parameters["_isChip"].Value = IsChip;
            report1.Parameters["_isUpak"].Value = 1;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnReestrListPrint_Click(object sender, EventArgs e)
        {
            int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            string RzuPachList = this.tbRzuPach.Text;
            ReestrListReport report1 = new ReestrListReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
            report1.Parameters["_rzuPachList"].Value = RzuPachList;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void xtraTabControl1_Selecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            //    //button11_Click(sender, e);
            //    if (xtraTabControl1.SelectedTabPageIndex == 1)
            //    {
            //        UpdateFurnitUpak();
            //    }
            //    if (xtraTabControl1.SelectedTabPageIndex == 3)
            //    {
            //        UpdateProizvCombIzd();
            //    }
        }
        private void btnZayavFurnPrint_Click(object sender, EventArgs e)
        {
            FurnUpakZayavReport report1 = new FurnUpakZayavReport();
            report1.RequestParameters = false;
            var selectedRowFurn = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            report1.Parameters["_kodF"].Value = selectedRowFurn.FKodFD.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnZayavUpakPrint_Click(object sender, EventArgs e)
        {
            FurnUpakZayavReport report1 = new FurnUpakZayavReport();
            report1.RequestParameters = false;
            var selectedRowUpak = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            report1.Parameters["_kodF"].Value = selectedRowUpak.UKodFD.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnFurnUpakDeliveryInfoShow_Click(object sender, EventArgs e)
        {
            var selectedRow = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            FurnUpakDeliveryInfo FDI = new FurnUpakDeliveryInfo(selectedRow.FKodFD.Substring(0, 12));
            DialogResult result = FDI.ShowDialog();
            // Обработка результата, возвращенного модальной формой
            if (result == DialogResult.OK)
            {
                // Действия при успешном завершении работы модальной формы
                //MessageBox.Show("OK");
            }
            else
            {
                // Действия при отмене или другом результате
                //MessageBox.Show("Cancel");
            }
        }
        private void btnFullKKPrint_Click(object sender, EventArgs e)
        {
            FullConfectionCardReport report1 = new FullConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_psaid"].Value = selectedRowRas.PsaPsaID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnUpakKKPrint_Click(object sender, EventArgs e)
        {
            ConfectionCardReport report1 = new ConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowUpak = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_kodFD"].Value = selectedRowUpak.UKodFD;
            report1.Parameters["_nomZad"].Value = selectedRowRas.PsaNomZad;
            report1.Parameters["_vidF"].Value = 2;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(selectedRowUpak.USpecRez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        private void sbProizvCombIzdSP_Click(object sender, EventArgs e)
        {
            ////gcProizvCombIzdSP.ShowPrintPreview();
            //gridView6.GridControl.PrintingSystem.PageSettings.Landscape = true;
            //gcProizvCombIzdSP.ShowRibbonPrintPreview();
            ////gcProizvCombIzdSP.PrintDialog();

            //// Настройка параметров перед вызовом ShowRibbonPrintPreview
            //gridView6.OptionsPrint.ExpandAllGroups = true;
            //gridView6.OptionsPrint.PrintDetails = true;
            //gridView6.GridControl.PrintingSystem.PageSettings.Landscape = true;
            //gridView6.GridControl.PrintingSystem.PageSettings.Margins = new Margins(25, 25, 25, 25);
            //gcProizvCombIzdSP.ShowRibbonPrintPreview();

            gridControlProizvCombIzdSP.ShowPrintPreview();

        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tablePanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void simpleButtonPrintNaklXtraReport_Click(object sender, EventArgs e)
        {
            NaklReport report1 = new NaklReport();
            report1.RequestParameters = false;
            //report1.Parameters["_naklIz"].Value = _currentNaklViewData[0].Iz;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_naklIz"].Value = selectedRow.Iz;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private async void simpleButtonNaklPart_Click(object sender, EventArgs e)
        {

            if (simpleButtonNaklPart.Text == "Показать информацию по делению накладной")
            {
                simpleButtonNaklPart.Text = "Скрыть информацию по делению накладной";
                simpleButtonPrintNaklXtraReport.Enabled = false;

                var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
                await LoadHistoryRazdelNaklViewDataAsync(selectedRow.Iz);

                this.gridControlPartNaklList.Location = this.gridControlNaklList.Location;
                this.gridControlPartNaklList.Size = this.gridControlNaklList.Size;
                this.gridControlPartNaklList.BringToFront();
                this.gridControlPartNaklList.Visible = true;
            }
            else
            {
                simpleButtonNaklPart.Text = "Показать информацию по делению накладной";
                simpleButtonPrintNaklXtraReport.Enabled = true;
                this.gridControlNaklList.BringToFront();
                this.gridControlPartNaklList.Visible = false;
            }
            gridControlNaklList.Refresh();
        }

        private void simpleButtonPrintMLRTAll_Click(object sender, EventArgs e)
        {
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            MlRtReport report1 = new MlRtReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            if (selectedRow != null && Convert.ToInt32(tbRzuNom.Text) != 0)
            {
                report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
                report1.Parameters["_isChip"].Value = IsChip;
                report1.Parameters["_isUpak"].Value = 0;
                ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                reportPrintTool1.ShowPreviewDialog();
            }
            else
            {
                MessageBox.Show("Не выбран расчет для печати");
            }
        }

        private void simpleButtonPrintMLRTUpak_Click(object sender, EventArgs e)
        {
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            MlRtReport report1 = new MlRtReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
            report1.Parameters["_isChip"].Value = IsChip;
            report1.Parameters["_isUpak"].Value = 1;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonReestrListPrint_Click(object sender, EventArgs e)
        {
            string RzuPachList = this.tbRzuPach.Text;
            ReestrListReport report1 = new ReestrListReport();
            report1.RequestParameters = false;
            var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_rzuNom"].Value = selectedRow.Nom;
            report1.Parameters["_rzuPachList"].Value = RzuPachList;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonFullKKPrint_Click(object sender, EventArgs e)
        {
            FullConfectionCardReport report1 = new FullConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_psaid"].Value = selectedRowRas.PsaPsaID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonFurnKKPrint_Click(object sender, EventArgs e)
        {
            ConfectionCardReport report1 = new ConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowFurn = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_kodFD"].Value = selectedRowFurn.FKodFD;
            report1.Parameters["_nomZad"].Value = selectedRowRas.PsaNomZad;
            report1.Parameters["_vidF"].Value = 1;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(selectedRowFurn.FSpecRez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonZayavFurnPrint_Click(object sender, EventArgs e)
        {
            FurnUpakZayavReport report1 = new FurnUpakZayavReport();
            report1.RequestParameters = false;
            var selectedRowFurn = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            report1.Parameters["_kodF"].Value = selectedRowFurn.FKodFD.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonUpakKKPrint_Click(object sender, EventArgs e)
        {
            ConfectionCardReport report1 = new ConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowUpak = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_kodFD"].Value = selectedRowUpak.UKodFD;
            report1.Parameters["_nomZad"].Value = selectedRowRas.PsaNomZad;
            report1.Parameters["_vidF"].Value = 2;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(selectedRowUpak.USpecRez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonZayavUpakPrint_Click(object sender, EventArgs e)
        {
            FurnUpakZayavReport report1 = new FurnUpakZayavReport();
            report1.RequestParameters = false;
            var selectedRowUpak = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            report1.Parameters["_kodF"].Value = selectedRowUpak.UKodFD.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void simpleButtonFurnUpakDeliveryInfoShow_Click(object sender, EventArgs e)
        {
            var selectedRow = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            FurnUpakDeliveryInfo FDI = new FurnUpakDeliveryInfo(selectedRow.FKodFD.Substring(0, 12));
            DialogResult result = FDI.ShowDialog();
            // Обработка результата, возвращенного модальной формой
            if (result == DialogResult.OK)
            {
                // Действия при успешном завершении работы модальной формы
                //MessageBox.Show("OK");
            }
            else
            {
                // Действия при отмене или другом результате
                //MessageBox.Show("Cancel");
            }
        }

        private void simpleButtonUpakDeliveryInfoShow_Click(object sender, EventArgs e)
        {
            var selectedRow = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            FurnUpakDeliveryInfo FDI = new FurnUpakDeliveryInfo(selectedRow.UKodFD.Substring(0, 12));
            DialogResult result = FDI.ShowDialog();
            // Обработка результата, возвращенного модальной формой
            if (result == DialogResult.OK)
            {
                // Действия при успешном завершении работы модальной формы
                //MessageBox.Show("OK");
            }
            else
            {
                // Действия при отмене или другом результате
                //MessageBox.Show("Cancel");
            }
        }

        private async void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            //    //button11_Click(sender, e);
            //    if (xtraTabControl1.SelectedTabPageIndex == 1)
            //    {
            //        UpdateFurnitUpak();
            //    }
            //    if (xtraTabControl1.SelectedTabPageIndex == 3)
            //    {
            //        UpdateProizvCombIzd();
            //    }


            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "FurnInfo":
                    await UpdateFurnitUpak();
                    break;

                case "OtdelkaInfo":
                    await UpdateProizvCombIzd();
                    break;
            }
        }

        private void simpleButtonFullKKPrint_Click_1(object sender, EventArgs e)
        {
            FullConfectionCardReport report1 = new FullConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_psaid"].Value = selectedRowRas.PsaPsaID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }
        private void btnFurnKKPrint_Click(object sender, EventArgs e)
        {
            //string iz = GetIzNakl();
            //PrintNaklReport report1 = new PrintNaklReport();
            //report1.RequestParameters = false;
            //report1.Parameters["_naklIz"].Value = iz;
            //ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            //reportPrintTool1.ShowPreviewDialog();

            //string nomzad = tbPsaNomZad.Text;
            //int vidf = 1;
            ConfectionCardReport report1 = new ConfectionCardReport();
            report1.RequestParameters = false;
            var selectedRowFurn = _furnitZayavCheckByPachKodBindingSource.Current as FurnitZayavCheckByPachKod;
            var selectedRowRas = _rasInfoByPachKodBindingSource.Current as RasInfo;
            report1.Parameters["_kodFD"].Value = selectedRowFurn.FKodFD;
            report1.Parameters["_nomZad"].Value = selectedRowRas.PsaNomZad;
            report1.Parameters["_vidF"].Value = 1;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(selectedRowFurn.FSpecRez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void customRadioGroup3_EditValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show($"1 - {customRadioGroup3.EditValue}");
        }

        private void customRadioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show($"2 - {customRadioGroup3.SelectedIndex}");
            int SelectedSign = customRadioGroup3.SelectedIndex;
            switch (SelectedSign)
            {
                case 0: //  по № задания
                    layoutControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    tbNomZad.Select();
                    break;
                case 1: //  по № пачки
                    layoutControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    tbNomPach.Select();
                    break;
                default:
                    layoutControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }
        }

        private void customRadioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show($"2 - {customRadioGroup2.SelectedIndex}");
        }

        private async void tbNomZad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RasInfo.PageVisible = true;
                FurnInfo.PageVisible = false;
                WorkInfo.PageVisible = false;
                OtdelkaInfo.PageVisible = false;
                SockZadanyInfo.PageVisible = true;

                Task rasInfoLoadTask = LoadRasInfoByNomZadDataAsync(tbNomZad.Text);
                Task naklViewLoadTask = LoadNaklViewByNomZadDataAsync(tbNomZad.Text);
                Task chipInfoLoadTask = LoadChipInfoByNomZadDataAsync(tbNomZad.Text);
                Task sockKnitZadanyInfoLoadTask = LoadSockKnitZadanyInfoDataAsync(tbNomZad.Text);
                Task sockZadanySmenListLoadTask = LoadSockZadanySmenListDataAsync(tbNomZad.Text);
                Task sockServiceListByNomZadLoadTask = LoadSockServiceListByNomZadDataAsync(tbNomZad.Text);
                Task sockDownTimeListByNomZadLoadTask = LoadSockDownTimeListByNomZadDataAsync(tbNomZad.Text);
                Task sockDefectListByNomZadLoadTask = LoadSockDefectListByNomZadDataAsync(tbNomZad.Text);
                await Task.WhenAll(rasInfoLoadTask, sockKnitZadanyInfoLoadTask, sockZadanySmenListLoadTask
                    , sockServiceListByNomZadLoadTask, sockDownTimeListByNomZadLoadTask
                    , sockDefectListByNomZadLoadTask);
                var selectedRow = _sockKnitZadanyInfoBindingSource.Current as SockZadanyInfo;
                if (selectedRow != null)
                {
                    xtraTabControl1.Enabled = true;
                }
                else
                {
                    xtraTabControl1.Enabled = false;
                }
                //_sockKnitZadanyInfoBindingSource.ResetBindings(false);
                //MessageBox.Show(TextBoxNomZad.Text);

                //xtraTabControl1.Enabled = true;
                //xtraTabControl1.Refresh();
                ////WorkDivisionLoadAsync(caller: "DataLoad", GetPachKod());
                ////Task wDLoadTask = WorkDivisionLoadAsync(caller: "DataLoad", GetPachKod());
                //Task rasInfoLoadTask = LoadRasInfoDataAsync(GetPachKod());
                //Task naklViewLoadTask = LoadNaklViewDataAsync(GetPachKod());
                //Task chipInfoLoadTask = LoadChipInfoDataAsync(GetPachKod());
                //Task planSezonOtdelkaLoadTask = LoadPlanSezonOtdelkaViewDataAsync(GetPachKod());
                ////await Task.WhenAll(wDLoadTask, rasInfoLoadTask, naklViewLoadTask);
                //await Task.WhenAll(rasInfoLoadTask, naklViewLoadTask, chipInfoLoadTask, planSezonOtdelkaLoadTask);

                //if (_currentRasInfoData.RzuNom == 0)
                //{
                //    xtraTabControl1.Enabled = false;
                //    MessageBox.Show("Поиск не дал результатов. Измените параметры и повторите попытку");
                //}
                //else
                //{
                //    var selectedRow = _rasInfoByPachKodBindingSource.Current as RasInfoByPachKod;
                //    int _psaPsaIDOsn = selectedRow.PsaPsaIDOsn;
                //    string _psaKombIzd = selectedRow.PsaKombIzd;
                //    int _psaTkIdSet = selectedRow.PsaTkIdSet;

                //    if (_psaPsaIDOsn != 0
                //        && (("V;F").IndexOf(_psaKombIzd) >= 0 || _psaTkIdSet == 1)
                //        && (_psaTkIdSet == 0))
                //    {
                //        OtdelkaInfo.PageVisible = true;
                //    }
                //    else
                //    {
                //        OtdelkaInfo.PageVisible = false;
                //    }

                //    ////string nomZad = _currentRasInfoData.PsaNomZad;

                //    ////string queryIsChip = $"SELECT dbo.checkChipNakl('', '{NomZad}') AS isChip ";
                //    //var selectedRow = _rasInfoByPachKodBindingSource.Current as RasInfoByPachKod;
                //    //string queryIsChip = $"SELECT dbo.checkChipNakl('', '{selectedRow.PsaNomZad}') AS isChip ";
                //    //var dtIsChip = _dbHelper.ExecuteQuery(queryIsChip);
                //    //bsIsChip.DataSource = dtIsChip;
                //    //this.cbIsChip.DataBindings.Clear();
                //    //this.cbIsChip.DataBindings.Add("Checked", dtIsChip, "isChip");

                //    //string queryOtdelkaList = $"SELECT vpso.psa_field_name, vpso.kol_sl_zv, vpso.frt_naimen, DetIzdName, VidIzdName ";
                //    //queryOtdelkaList += $" FROM View_plan_sezon_otdelka vpso ";
                //    //queryOtdelkaList += $" WHERE vpso.nn = '{_currentRasInfoData.PsaNN}' ";
                //    //var dtOtdelkaList = _dbHelper.ExecuteQuery(queryOtdelkaList);
                //    //bsOtdelkaList.DataSource = dtOtdelkaList;

                //    if (xtraTabControl1.SelectedTabPageIndex == 1)
                //    {
                //        UpdateFurnitUpak();
                //    }
                //    if (xtraTabControl1.SelectedTabPageIndex == 3)
                //    {
                //        UpdateProizvCombIzd();
                //    }
                //}

            }
        }

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    MessageBox.Show("Печать");
                    //tbNomZad.Text
                    //NaklReport report1 = new NaklReport();
                    //report1.RequestParameters = false;
                    ////report1.Parameters["_naklIz"].Value = _currentNaklViewData[0].Iz;
                    //var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
                    //report1.Parameters["_naklIz"].Value = selectedRow.Iz;
                    //ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                    //reportPrintTool1.ShowPreviewDialog();
                    break;
            }
        }



        //private void gcNaklList1_RowCellStyle(object sender, MaskInputRejectedEventArgs e)
        //{
        //    if (e.Column.FieldName == "Field2")
        //    {
        //        var data = gridView1.GetRow(e.RowHandle) as Sample;
        //        if (data == null)
        //            return;

        //        if (data.Field2 < 0)
        //            e.Appearance.ForeColor = Color.Red;
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    XtraReport1 xtpt = new XtraReport1();
        //    xtpt.LoadLayout(Application.StartupPath + "..\\XtraReport1.repx");
        //    ReportDesignTool tool = new ReportDesignTool(xtpt);
        //    tool.ShowDesignerDialog();
        //}
    }
}
