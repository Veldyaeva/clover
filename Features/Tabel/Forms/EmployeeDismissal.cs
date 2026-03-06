using SewingProduction.Features.Tabel.Services;
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

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class EmployeeDismissal : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _spPodr;
        string _fio;
        string _naimenGr;
        int _currentId;
        int _idGroup;
        string _currentMg;
        int _tab;
        public EmployeeDismissal(string fio, string naimenGr, int currentId, int idGroup, string currentMg, int tab)
        {
            InitializeComponent();
        }
    }
}
