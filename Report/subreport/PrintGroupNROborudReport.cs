using SewingProduction.Report;
using System;
using System.Data;

namespace SewingProduction
{
    public partial class PrintGroupNROborudReport : ConnectedXtraReport
    {
        public PrintGroupNROborudReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }

        public PrintGroupNROborudReport(DataTable table)
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
            calculatedField1.DataMember = string.Empty;
        }
    }
}
