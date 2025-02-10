using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;

        public TeamWork_AdvanceTW()
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper("ace");//Properties.Settings.Default.ACEConnectionString);
            _artNormService = new ArtNormService(dbHelper);
            UpdateTheme(this);

        }

        private void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {

        }
    }
}
