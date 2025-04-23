using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.form.UserDistribution
{
    public partial class AdminSprav : CustomForm
    {
        public AdminSprav(UserClass user) : base(user)
        {
            InitializeComponent();
        }
    }
}
