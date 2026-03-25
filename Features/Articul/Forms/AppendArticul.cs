using SewingProduction.Features.UserDistribution.Helpers;
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
    public partial class AppendArticul : CustomForm
    {
        private int _typeCreate = 0;

        public AppendArticul(UserClass user) : this(user, 0)
        { }

        public AppendArticul(UserClass user, int type) : base(user)
        {
            // 0 - создание,1 - стыковка
            _typeCreate = type;

        }

        public AppendArticul()
        {
            InitializeComponent();
        }



    }
}
