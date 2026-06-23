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

            var connectionName = SettingsManager.GetCurrentConnectionName();

            report.DataSourceDemanded -= Report_DataSourceDemanded;
            report.DataSourceDemanded += Report_DataSourceDemanded;

            ApplyToReportTree(report, connectionName, new HashSet<XtraReport>());
        }

        private static void Report_DataSourceDemanded(object sender, EventArgs e)
        {
            if (sender is not XtraReport report)
                return;

            var connectionName = SettingsManager.GetCurrentConnectionName();
            ApplyToReportTree(report, connectionName, new HashSet<XtraReport>());
        }

        private static void ApplyToReportTree(XtraReport report, string connectionName, ISet<XtraReport> visitedReports)
        {
            if (!visitedReports.Add(report))
                return;

            ApplyDataSources(report, connectionName);

            foreach (var subreport in report.AllControls<XRSubreport>())
            {
                if (subreport.ReportSource != null)
                    ApplyToReportTree(subreport.ReportSource, connectionName, visitedReports);
            }
        }

        private static void ApplyDataSources(XtraReport report, string connectionName)
        {
            ApplySqlDataSource(report.DataSource as SqlDataSource, connectionName);

            if (report.ComponentStorage == null)
                return;

            foreach (IComponent component in report.ComponentStorage)
            {
                if (component is SqlDataSource sqlDataSource)
                    ApplySqlDataSource(sqlDataSource, connectionName);
            }
        }

        private static void ApplySqlDataSource(SqlDataSource sqlDataSource, string connectionName)
        {
            if (sqlDataSource == null)
                return;

            sqlDataSource.ConnectionName = connectionName;
            sqlDataSource.ConnectionParameters = null;
        }
    }
}
