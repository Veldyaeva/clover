using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.ComponentModel;

namespace SewingProduction.Report
{
    public partial class NormRaszTest : ConnectedXtraReport
    {
        public NormRaszTest()
        {
            InitializeComponent();
            UseCurrentConnection();
        }

        public NormRaszTest(NormRaszPreparedData preparedData)
        {
            InitializeComponent();
            ApplyPreparedData(preparedData);
        }

        private void ApplyPreparedData(NormRaszPreparedData preparedData)
        {
            if (preparedData == null)
                throw new ArgumentNullException(nameof(preparedData));

            RequestParameters = false;
            _annId.Value = preparedData.AnnId;

            DataSource = preparedData.Header;
            DataMember = string.Empty;

            DetailReport.DataSource = preparedData.Rows;
            DetailReport.DataMember = string.Empty;

            timeHours.DataMember = string.Empty;

            RewriteExpressionBindings();

            xrSubreport2.ParameterBindings.Clear();
            xrSubreport2.ReportSource = new SewingProduction.PrintGroupNROborudReport(preparedData.ByEquipment);

            xrSubreport1.ParameterBindings.Clear();
            xrSubreport1.ReportSource = new SewingProduction.Report.subreport.secByKodPodrAnnIdReport(preparedData.BySection);
        }

        private void RewriteExpressionBindings()
        {
            foreach (var control in AllControls<XRControl>())
            {
                RewriteExpressionBindingPrefix(control, "[annView].");
                RewriteExpressionBindingPrefix(control, "[raszView].");
                RewriteExpressionBindingPrefix(control, "[getImageAndTb].");
            }
        }

        private static void RewriteExpressionBindingPrefix(XRControl control, string prefix)
        {
            foreach (ExpressionBinding binding in control.ExpressionBindings)
            {
                if (!string.IsNullOrEmpty(binding.Expression) &&
                    binding.Expression.Contains(prefix, StringComparison.Ordinal))
                {
                    binding.Expression = binding.Expression.Replace(prefix, string.Empty, StringComparison.Ordinal);
                }
            }
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PrintMlRtReport_BeforePrint(object sender, CancelEventArgs e)
        {
            //this.xrSubreport1.CanShrink = true;
        }

        private void xrTableCell38_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell3_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
