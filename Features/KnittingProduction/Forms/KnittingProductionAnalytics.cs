using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingProductionAnalytics : CustomForm//, IThemeable
    {
        public FormManager _formManager;
        public KnittingProductionAnalytics()
        {
            InitializeComponent();

            //Form mainForm = Application.OpenForms["SpMainForm"];
            //_formManager = new FormManager(mainForm, mainMenu, _user);

            // ThemeManager.UpdateTheme(this);
        }
        public void OpenForm(Form form, object sender = null)
        {
            _formManager.OpenForm(form, sender);
        }
        private void customSimpleButton7_Click(object sender, EventArgs e)
        {
            //OpenForm(new PlanZagrVyazCheck(), sender);

            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new PlanZagrVyazCheck());
            }
        }
    }
}
