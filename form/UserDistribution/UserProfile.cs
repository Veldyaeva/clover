using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB.Helpers;
using System.Windows.Forms;
using SewingProduction.Helpers;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraVerticalGrid;

namespace SewingProduction.form.UserDistribution
{
    public partial class UserProfile : CustomForm
    {
        private readonly UserProfileDataService _userProfileDataService;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
        private readonly UserClass _user;
        public UserProfile(UserClass user) : base(user)
        {
            InitializeComponent();
            _userProfileDataService = new UserProfileDataService(dbHelper);
            _user = user;
            customLabelProfileName.Text = _user.UserName;
            listBoxRole.DataSource = _user.Roles;
            customLabelCompName.Text = Environment.MachineName;
        }

        private void customButtonChangeUser_Click(object sender, EventArgs e)
        {
            _user.ExitUser();
        }

        
        private void xtraTabControlAllProfile_Click(object sender, EventArgs e)
        {

        }


        private void customButtonAllRpofile_Click(object sender, EventArgs e)
        {
        }

        private void customButtonAllRole_Click(object sender, EventArgs e)
        {
            AllRole f = new AllRole(_user);
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        private void customButtonAdminForm_Click(object sender, EventArgs e)
        {
            AdminForm f = new AdminForm(_user);
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        private void customButtonUserHierarchy_Click(object sender, EventArgs e)
        {
            UsersHierarchy f = new UsersHierarchy(_user);
            f.MdiParent = this.MdiParent;
            f.Show();
        }
    }
    public class UserProfileDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public UserProfileDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
    }
}
