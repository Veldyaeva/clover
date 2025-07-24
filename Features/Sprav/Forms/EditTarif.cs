using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.IdentityModel.Tokens;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Sprav
{
    public partial class EditTarif : Form
    {
        private readonly TarifDataService _tarifService;
        public EditTarif(UserClass user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            _tarifService = new TarifDataService(dbService, dbHelper);
        }

        private async void customGridControlZp_Load(object sender, EventArgs e)
        {
            var tableTarif = await _tarifService.LoadTarifList();
            customGridControlZp.DataSource = tableTarif;
            customGridControlZp.RefreshDataSource();
        }

        private async void customGridControlTR_Load(object sender, EventArgs e)
        {
            var tableTarif = await _tarifService.LoadTarifRabotList();
            customGridControlTR.DataSource = tableTarif;
            customGridControlTR.RefreshDataSource();
        }
        public void Filter(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (customRadioButtonAll.Checked == true)
            {
                gridViewZp.ActiveFilterString = string.Empty;
                return;
            }
            if (customRadioButtonMay.Checked == true)
            {
                filter += "[firm] like 'may'";
            }
            else if (customRadioButtonExp.Checked == true)
            {
                filter += "[firm] like 'exp'";
            }
            else if (customRadioButtonAceKle.Checked == true)
            {
                filter += "[firm] like 'ace/cle'";
            }
            gridViewZp.ActiveFilterString = filter;
        }
    }
    public class TarifDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public TarifDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }
        public async Task<List<TarifModel>> LoadTarifList()
        {
            string query = @$"SELECT  TOP 1 with TIES * 
                            FROM proizv_view_constants pvc 
                            WHERE describe IS NOT NULL 
                            ORDER BY rank() over(partition by pvc.pc_id order by pvc.pc_id, pvc.begin_dt desc)";
            return await _dbService.GetListAsync<TarifModel>(query, new Dictionary<string, object>());
        }
        public async Task<List<TarifRabotModel>> LoadTarifRabotList()
        {
            string query = @$"SELECT * FROM sp_ras_rabot  
                            WHERE text IS NOT NULL ";
            return await _dbService.GetListAsync<TarifRabotModel>(query, new Dictionary<string, object>());
        }
    }
    public class TarifModel : INotifyPropertyChanged
    {
        private string _describe;
        [Column("describe")]
        public string describe
        {
            get => _describe;
            set { if (_describe != value) { _describe = value; OnPropertyChanged(nameof(describe)); } }
        }
        private DateTime _begin_dt;
        [Column("begin_dt")]
        public DateTime begin_dt
        {
            get => _begin_dt;
            set { if (_begin_dt != value) { _begin_dt = value; OnPropertyChanged(nameof(begin_dt)); } }
        }

        private decimal _value_numeric;
        [Column("value_numeric")]
        public decimal value_numeric
        {
            get => _value_numeric;
            set { if (_value_numeric != value) { _value_numeric = value; OnPropertyChanged(nameof(value_numeric)); } }
        }

        private int _value_integer;
        [Column("value_integer")]
        public int value_integer
        {
            get => _value_integer;
            set { if (_value_integer != value) { _value_integer = value; OnPropertyChanged(nameof(value_integer)); } }
        }

        private float _value_float;
        [Column("value_float")]
        public float value_float
        {
            get => _value_float;
            set { if (_value_float != value) { _value_float = value; OnPropertyChanged(nameof(value_float)); } }
        }

        private string _value_character;
        [Column("value_character")]
        public string value_character
        {
            get => _value_character;
            set { if (_value_character != value) { _value_character = value; OnPropertyChanged(nameof(value_character)); } }
        }

        private DateTime _value_datetime;
        [Column("value_datetime")]
        public DateTime value_datetime
        {
            get => _value_datetime;
            set { if (_value_datetime != value) { _value_datetime = value; OnPropertyChanged(nameof(value_datetime)); } }
        }

        private string _firm;
        [Column("firm")]
        public string firm
        {
            get => _firm;
            set { if (_firm != value) { _firm = value; OnPropertyChanged(nameof(firm)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    public class TarifRabotModel : INotifyPropertyChanged
    {
        private int _id_kod_o;
        [Column("id_kod_o")]
        public int id_kod_o
        {
            get => _id_kod_o;
            set { if (_id_kod_o != value) { _id_kod_o = value; OnPropertyChanged(nameof(id_kod_o)); } }
        }
        private string _Text;
        [Column("Text")]
        public string Text
        {
            get => _Text;
            set { if (_Text != value) { _Text = value; OnPropertyChanged(nameof(Text)); } }
        }

        private int _prizn_podr;
        [Column("prizn_podr")]
        public int prizn_podr
        {
            get => _prizn_podr;
            set { if (_prizn_podr != value) { _prizn_podr = value; OnPropertyChanged(nameof(prizn_podr)); } }
        }

        private decimal _tarif;
        [Column("tarif")]
        public decimal tarif
        {
            get => _tarif;
            set { if (_tarif != value) { _tarif = value; OnPropertyChanged(nameof(tarif)); } }
        }

        private string _ed_izm;
        [Column("ed_izm")]
        public string ed_izm
        {
            get => _ed_izm;
            set { if (_ed_izm != value) { _ed_izm = value; OnPropertyChanged(nameof(ed_izm)); } }
        }

        private decimal _koef_chas;
        [Column("koef_chas")]
        public decimal koef_chas
        {
            get => _koef_chas;
            set { if (_koef_chas != value) { _koef_chas = value; OnPropertyChanged(nameof(koef_chas)); } }
        }

        private int _razr;
        [Column("razr")]
        public int razr
        {
            get => _razr;
            set { if (_razr != value) { _razr = value; OnPropertyChanged(nameof(razr)); } }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
