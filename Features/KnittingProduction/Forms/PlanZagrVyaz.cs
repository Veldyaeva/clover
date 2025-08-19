using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
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

        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            //switch (buttonIndex)
            //{
            //    case 0:
            //        //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
            //        if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
            //            ButtonPreliminaryWd_Click_Internal(sender, e);
            //        break;
            //    case 2:
            //        //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
            //        if (ButtonEditWd.Enabled && ButtonEditWd.Visible)
            //            if (ButtonEditOnlyAdv.Enabled && ButtonEditOnlyAdv.Visible)
            //                await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
            //            else
            //                await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
            //        break;
            //    case 4:
            //        //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
            //        if (ButtonDouble.Enabled && ButtonDouble.Visible)
            //            await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource);
            //        break;
            //    case 6:
            //        //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
            //        if (ButtonArchAndCopyWd.Enabled && ButtonArchAndCopyWd.Visible)
            //            await SetArchiveStatus_Internal(sender, e);//МЕНЯЮ НА АРХИВ для Чирковой
            //        //ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
            //        break;
            //    case 9:
            //        //Debug.WriteLine(PrintButton.Enabled + " " + PrintButton.Visible);
            //        if (PrintButton.Enabled && PrintButton.Visible)
            //            // Отчет технологической схемы разделения труда
            //            PrintWorkDivisionScheme_Click(null, null);
            //        break;
            //    case 11:
            //        if (printButtonPlus.Enabled && printButtonPlus.Visible)
            //            // Отчет технологической схемы разделения труда
            //            printButtonPlus_Click(null, null);
            //        break;

            //}
        }
    }
}
