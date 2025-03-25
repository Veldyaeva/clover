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

namespace SewingProduction.form.UserDistribution
{
    public partial class UserProfile : CustomForm
    {
        public UserProfile()
        {
            InitializeComponent();
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
