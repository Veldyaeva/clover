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
    public partial class ArticulEditAdvance : CustomForm
    {
        private DatabaseHelper _dbHelperAce;
        private DbService _dbService;

        public ArticulEditAdvance(UserClass user) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            _dbService = new DbService(_dbHelperAce);
            InitializeComponent();
            _user = user;
            ThemeManager.UpdateTheme(this);

        }
        

        private void ArticulEditAdvance_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
