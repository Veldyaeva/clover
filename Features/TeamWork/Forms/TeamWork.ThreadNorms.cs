using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonsPanelControl;
using SewingProduction.Features.UserDistribution.Helpers;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private void InitializeThreadNormsButton()
        {
            if (layoutControlGroup8 == null)
            {
                return;
            }

            if (FindButtonByTag(layoutControlGroup8, "wd:thread-norms") != null)
            {
                return;
            }

            var button = new GroupBoxButton(
                "Нормы ниток",
                true,
                null,
                ButtonStyle.PushButton,
                string.Empty,
                -1,
                true,
                null,
                true,
                false,
                true,
                "wd:thread-norms",
                -1);

            layoutControlGroup8.CustomHeaderButtons.Add(button);
        }

        private void OpenThreadNormsForm()
        {
            using var form = new ThreadNormsForm(User ?? new UserClass());
            form.ShowDialog(this);
        }
    }
}
