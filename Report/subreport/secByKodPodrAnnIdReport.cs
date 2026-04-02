using SewingProduction.Report;
using System;
using System.Data;

namespace SewingProduction.Report.subreport
{
    public partial class secByKodPodrAnnIdReport : ConnectedXtraReport
    {
        public secByKodPodrAnnIdReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }

        public secByKodPodrAnnIdReport(DataTable table)
        {
            InitializeComponent();
            ApplyPreparedData(table);
        }

        private void ApplyPreparedData(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            RequestParameters = false;
            DataSource = table;
            DataMember = string.Empty;
        }
    }
}
