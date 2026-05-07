using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Microsoft.Extensions.DependencyInjection;
using SewingProduction.Core;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.services;
using SewingProduction.Features.Sprav.DataService;
using SewingProduction.Features.Sprav.Reports;
using SewingProduction.Features.Tabel.Reports;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
namespace SewingProduction.form
{
    /// <summary>
    /// Форма оборудования в бригадах (цехах)
    /// </summary>
    public partial class OborudBrig : CustomForm, IDataUpdatableForm
    {
        private readonly OborudBrigDataService _oborudBrigDataService;
        private readonly IAppServiceBrokerHub _sbHub;
        private readonly string _sbHubOwnerId = $"OborudBrig:{Guid.NewGuid():N}";
        private CancellationTokenSource? _sbLifetimeCts;
        private SqlDependency sqlDependency;
        private SqlConnection connection;
        bool flagStartListening = false; //вкл прослушки
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        public OborudBrig(UserClass user) : base(user)
        {
            InitializeComponent();
            DatabaseHelperSQL dbHelper = new DatabaseHelperSQL();
            _oborudBrigDataService = new OborudBrigDataService(dbHelper);
            _sbHub = AppServices.Services?.GetService<IAppServiceBrokerHub>() ?? new AppServiceBrokerHub();
            //  ThemeManager.UpdateTheme(this);
        }
        #region service broker
        private async void OborudBrig_Load_1(object sender, EventArgs e)
        {
            if (!flagStartListening)
            {
                var tableFields = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase)
                {
                    ["dbo.OborudBrig"] = new[] { "idOB", "idZeh", "kod_ob", "count" }
                };
                await _sbHub.SubscribeAsync(
                    ownerId: _sbHubOwnerId,
                    ownerName: GetType().Name,
                    tableFields: tableFields,
                    onTableChangedAsync: async (table, changed) =>
                    {
                        if (IsDisposed || Disposing)
                            return;
                        if (InvokeRequired)
                        {
                            BeginInvoke(new Action(() => UpdateDataInForm(table)));
                            return;
                        }
                        UpdateDataInForm(table);
                        await Task.CompletedTask;
                    },
                    ct: GetServiceBrokerLifetimeToken());
                flagStartListening = true;
            }
            gridOborud_Load(null, EventArgs.Empty);
        }
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm(string _table)
        {
            gridOborud_Load(null, EventArgs.Empty);
        }

        #endregion

        // Обнолвение таблиц при активации вкладки:
        private void OborudBrig_Activated(object sender, EventArgs e)
        {
            gridZeh_Load(sender, e);
            gridZeh_Click(sender, e);
        }
        // Клик на бригаду
        private void gridBrig_Click(object sender, EventArgs e)
        {
            gridOborud_Load(sender, e);
        }
        // Выбор цеха в таблице цехов:
        private void gridZeh_Click(object sender, EventArgs e)
        {
            gridViewZeh_FocusedRowChanged(sender, null);
            //gridBrig_Load(sender, e);
            //gridOborud_Load(sender, e);
        }
        private void gridViewZeh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridBrig_Load(sender, e);
            gridOborud_Load(sender, e);
        }
        // Управление стерлочками
        private void gridZeh_KeyUp(object sender, KeyEventArgs e)
        {
            gridZeh_Click(sender, e);
        }
        /// <summary>
        /// Таблица цехов:
        /// </summary>
        private void gridZeh_Load(object sender, EventArgs e)
        {
            bindingZeh.DataSource = _oborudBrigDataService.GetZehListFromOborudBrig();
            GridView gridView = gridZeh.MainView as GridView;
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.BestFitColumns();

            gridView.Columns["Цех"].OptionsColumn.ReadOnly = true;
            gridView.Columns["Вид производства"].OptionsColumn.ReadOnly = true;
            gridView.Columns["Адрес"].OptionsColumn.ReadOnly = true;
        }
        /// <summary>
        /// Таблица бригад:
        /// </summary>
        private void gridBrig_Load(object sender, EventArgs e)
        {
            if (gridViewZeh != null && gridViewZeh.RowCount > 0 && gridViewZeh.Columns != null && gridViewZeh.Columns.Count > 0)
            {
                string nameZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();
                int countVievZeh = gridViewZeh.Columns.Count;
                bindingBrig.DataSource = _oborudBrigDataService.GetSpBrigFromOborudBrig(nameZeh, countVievZeh);
                // Получаем доступ к GridView
                GridView gridView = gridBrig.MainView as GridView;
                //Запрет на редактирование
                gridView.OptionsBehavior.Editable = false;
                gridView.BestFitColumns();
            }
        }

        // Таблица оборудования в цехе:
        private void gridOborud_Load(object sender, EventArgs e)
        {
            //если таблица с цехами не пустая
            if (gridViewZeh != null && gridViewZeh.RowCount > 0 && gridViewZeh.Columns != null && gridViewZeh.Columns.Count > 0)
            {
                GridView gridView = gridOborud.MainView as GridView;
                string currentOb = "";
                if (gridView.FocusedRowHandle >= 0)
                {
                    currentOb = gridView.GetRowCellValue(gridView.FocusedRowHandle, "Оборудование").ToString();
                    //currentRowIndex = gridView.FocusedRowHandle;
                }
                GridView gridViewZeh = gridZeh.MainView as GridView;
                string getVid = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Вид производства"]).ToString();
                string getZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();
                int getCount = gridViewZeh.Columns.Count;
                bindingOborud.DataSource = _oborudBrigDataService.GetSpOborudShv(getVid, getCount, getZeh);
                gridView.BestFitColumns();

                gridView.Columns["Оборудование"].OptionsColumn.ReadOnly = true;
                gridView.Columns["Кол-во"].OptionsColumn.ReadOnly = false;
                gridView.Columns["Вид"].OptionsColumn.ReadOnly = true;

                int rowHandle = gridView.LocateByValue("Оборудование", currentOb);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    gridView.FocusedRowHandle = rowHandle;
                    gridView.MakeRowVisible(rowHandle);
                }
            }
        }
        //Редактирование кол-ва оборудования
        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Получаем код оборудования по названию из таблицы оборудований:
            GridView gridViewOborud = gridOborud.MainView as GridView;
            string getOb = gridViewOborud.GetFocusedRowCellValue(gridViewOborud.Columns["Оборудование"]).ToString();

            //Получаем код цеха по названию из таблицы цехов:
            GridView gridViewZeh = gridZeh.MainView as GridView;
            string getZeh = gridViewZeh.GetFocusedRowCellValue(gridViewZeh.Columns["Цех"]).ToString();

            //Получаем отредактированное значение:
            object getValue = e.Value;

            _oborudBrigDataService.UpdateOborudBrig(getOb, getZeh, getValue);
        }
        /// <summary>
        /// Открытие справочника Цехов
        /// </summary>
        private void labelZeh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new SpravZeh(_user, "ZehList", "Справочник Цехов"), "цехаToolStripMenuItem");
            }
        }

        /// <summary>
        /// Открытие справочника Бригад
        /// </summary>
        private void linkLabelBrig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new SpravBrig(_user, "spBrig", "Справочник Бригад"), "бригадыToolStripMenuItem");
            }
        }
        private void OborudBrig_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutdownServiceBroker();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                ShutdownServiceBroker();
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }

        private void ShutdownServiceBroker()
        {
            try { _sbLifetimeCts?.Cancel(); } catch { }
            try { _sbHub.UnsubscribeAsync(_sbHubOwnerId).GetAwaiter().GetResult(); } catch { }
            try { _sbLifetimeCts?.Dispose(); } catch { }
            _sbLifetimeCts = null;
            flagStartListening = false;
        }

        private CancellationToken GetServiceBrokerLifetimeToken()
        {
            _sbLifetimeCts ??= new CancellationTokenSource();
            return _sbLifetimeCts.Token;
        }
        private void customButtonExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OborudBrigReport reportFull = new OborudBrigReport();
                reportFull.CreateDocument();

                OborudBrigReportTotal reportTotal = new OborudBrigReportTotal();
                reportTotal.CreateDocument();

                reportFull.PrintingSystem.Pages.AddRange(reportTotal.PrintingSystem.Pages);

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                    sfd.FileName = "Оборудование.xlsx";

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;

                    DevExpress.XtraPrinting.XlsxExportOptions options =
                        new DevExpress.XtraPrinting.XlsxExportOptions();

                    options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;
                    options.ShowGridLines = true;
                    options.SheetName = "Отчет";

                    reportFull.PrintingSystem.ExportToXlsx(sfd.FileName, options);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName)
                    {
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
