using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SewingProduction.Core.helpers
{
    internal static class ReportConnectionHelper
    {
        public static void Configure(XtraReport report)
        {
            if (report == null)
                return;

            var connectionString = SettingsManager.GetCurrentConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            report.DataSourceDemanded -= Report_DataSourceDemanded;
            report.DataSourceDemanded += Report_DataSourceDemanded;

            ApplyToReportTree(report, connectionString, new HashSet<XtraReport>());
        }

        private static void Report_DataSourceDemanded(object sender, EventArgs e)
        {
            if (sender is not XtraReport report)
                return;

            var connectionString = SettingsManager.GetCurrentConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            ApplyToReportTree(report, connectionString, new HashSet<XtraReport>());
        }

        private static void ApplyToReportTree(XtraReport report, string connectionString, ISet<XtraReport> visitedReports)
        {
            if (!visitedReports.Add(report))
                return;

            ApplyDataSources(report, connectionString);

            foreach (var subreport in report.AllControls<XRSubreport>())
            {
                if (subreport.ReportSource != null)
                    ApplyToReportTree(subreport.ReportSource, connectionString, visitedReports);
            }
        }

        private static void ApplyDataSources(XtraReport report, string connectionString)
        {
            ApplySqlDataSource(report.DataSource as SqlDataSource, connectionString);

            if (report.ComponentStorage == null)
                return;

            foreach (IComponent component in report.ComponentStorage)
            {
                if (component is SqlDataSource sqlDataSource)
                    ApplySqlDataSource(sqlDataSource, connectionString);
            }
        }

        private static void ApplySqlDataSource(SqlDataSource sqlDataSource, string connectionString)
        {
            if (sqlDataSource == null)
                return;

            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(connectionString);
        }
    }
}
