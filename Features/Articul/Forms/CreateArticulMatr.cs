using SewingProduction.Features.Articul.Service;
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

namespace SewingProduction.Features.Articul.Forms
{
    public partial class CreateArticulMatr : CustomForm

    {
        private DatabaseHelper _dbHelper;
        private DbService _dbService;
        private CreateArticulMatrService CreateArticulMatrService = new CreateArticulMatrService();
        private readonly ILogger _logger = new FileLogger();


        private BindingSource _bindingSourceArtMatr;

        public CreateArticulMatr(UserClass user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);

            InitializeComponent();

            _bindingSourceArtMatr = new BindingSource { };
            //if (gridArtMatr != null) gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatr.DataSource = _bindingSourceArtMatr;

        }

        private async void CreateArticulMatr_Load(object sender, EventArgs e)
        {
            //загрузка данных для отображения в гриде
            gridViewArtMatr.ShowLoadingPanel();
            var getArtTask = await CreateArticulMatrService.GetMatrForArticulAsync();
            _bindingSourceArtMatr.DataSource = getArtTask;
            gridViewArtMatr.HideLoadingPanel();

        }
    }
}
