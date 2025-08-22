using SewingProduction.Core.Services;
using SewingProduction.Features.CardByNom.Services;
using SewingProduction.Features.Furnit.Services;
using SewingProduction.Helpers;
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
    public partial class PlanZagrVyaz : CustomForm, IThemeable
    {
        private readonly DatabaseHelper _dbHelper;
        public PlanZagrVyaz()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            ThemeManager.UpdateTheme(this);
        }
    }
}
