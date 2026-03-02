using SewingProduction.Core.helpers;
using SewingProduction.Core.services;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
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

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class TSDAccessManagement : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private readonly ILogger _logger = new FileLogger();
        //private readonly VyazService _vyazService;
        private BindingSource _smenZadanyVyazBindingSource;
        public TSDAccessManagement(UserClass User) : base(User)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();

            //_vyazService = new VyazService(_dbHelper);

            _smenZadanyVyazBindingSource = new BindingSource
            {
                DataSource = new BindingList<SmenZadanyVyaz>()
            };
        }
    }
}
