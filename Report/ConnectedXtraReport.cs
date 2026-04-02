using DevExpress.XtraReports.UI;
using SewingProduction.Core.helpers;

namespace SewingProduction.Report
{
    public abstract class ConnectedXtraReport : XtraReport
    {
        protected void UseCurrentConnection()
        {
            ReportConnectionHelper.Configure(this);
        }
    }
}
