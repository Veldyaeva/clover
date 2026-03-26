using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Reports;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class OtvlRab : CustomForm
    {
        TabOtvlRDataService tabOtvlRDataService = new TabOtvlRDataService();
        private BindingList<TabOtvlRModel> tabOtvlRList = new BindingList<TabOtvlRModel>();
        private int _idGroup = 0;
        private int _idTabno = 0;
        private DateTime? _lastCalendarDate;
        public OtvlRab(UserClass user, int tnid) : base(user)
        {
            InitializeComponent();
            _idGroup = tnid;
        }
        public OtvlRab(UserClass user)
        {
            InitializeComponent();
        }

        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            layoutControlGroupRab.Text = await tabOtvlRDataService.GetNaimenGroupZlAsync(_idGroup);

            var listFio = await tabOtvlRDataService.GetZlSpisokAsync(_idGroup);
            customSearchLookUpEditFio.Properties.DataSource = listFio;
            customSearchLookUpEditFio.Properties.DisplayMember = "Fio";
            customSearchLookUpEditFio.Properties.ValueMember = "Tabno";
            customSearchLookUpEditFio.Properties.NullText = "";
            customSearchLookUpEditFio.EditValue = listFio[0].Tabno;
        }
        private void OtvlRab_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Data Loading
        private void customGridControlTabel_Load(object sender, EventArgs e)
        {
            TogglePairColumns(Priem_t_s, Priem_t_po, false);
            TogglePairColumns(Sort_t_s, Sort_t_po, false);
            TogglePairColumns(Drug_s_s, Drug_s_po, false);
            TogglePairColumns(Otvl_r_s, Otvl_r_po, false);
            ApplyCurrentCalendarFilter();
        }
        private void repositoryItemTimeEditPriem_t_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Priem_t_s, Priem_t_po, !Priem_t_s.Visible);
        }
        private void repositoryItemTimeEditSort_t_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Sort_t_s, Sort_t_po, !Sort_t_s.Visible);
        }
        private void repositoryItemTimeEditOtvl_r_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Otvl_r_s, Otvl_r_po, !Otvl_r_s.Visible);
        }
        private void repositoryItemTimeEditDrug_s_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Drug_s_s, Drug_s_po, !Drug_s_s.Visible);
        }
        private void TogglePairColumns(
        DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colS,
        DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPo,
        bool needShow)
        {
            bandedGridViewTabel.BeginUpdate();
            try
            {
                colS.Visible = needShow;
                colPo.Visible = needShow;
                bandedGridViewTabel.LayoutChanged();
            }
            finally
            {
                bandedGridViewTabel.EndUpdate();
            }
        }
        private async void customSearchLookUpEditFio_EditValueChanged(object sender, EventArgs e)
        {
            _idTabno = Convert.ToInt32(customSearchLookUpEditFio.EditValue);
            tabOtvlRList = new BindingList<TabOtvlRModel>(await tabOtvlRDataService.GetByMgGrTabAsync(_idGroup, _idTabno));
            customGridControlTabel.DataSource = tabOtvlRList;
        }
        #endregion

        #region Summary Hours
        private static string DecimalHoursToHHmm(decimal hours)
        {
            if (hours <= 0m)
                return "0:00";

            var totalMinutes = (int)Math.Round(hours * 60m, MidpointRounding.AwayFromZero);
            var ts = TimeSpan.FromMinutes(totalMinutes);

            return $"{(int)ts.TotalHours} час {ts.Minutes:00} мин";
        }

        private void bandedGridViewTabel_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Info.SummaryItem?.SummaryValue == null) return;

            if (!decimal.TryParse(e.Info.SummaryItem.SummaryValue.ToString(), out var hoursDec))
                return;

            if (e.Column == Priem_t)
                e.Info.DisplayText = "Итого приемки: " + DecimalHoursToHHmm(hoursDec);
            else if (e.Column == Sort_t)
                e.Info.DisplayText = "Итого выкладки: " + DecimalHoursToHHmm(hoursDec);
            else if (e.Column == Drug_s)
                e.Info.DisplayText = "Итого на др.скл.: " + DecimalHoursToHHmm(hoursDec);
            else if (e.Column == Otvl_r)
                e.Info.DisplayText = "Итого отвл.: " + DecimalHoursToHHmm(hoursDec);

            UpdateTotalSumMasked();
        }
        private void UpdateTotalSumMasked()
        {
            decimal total =
                GetSummaryDecimal(Priem_t) +
                GetSummaryDecimal(Sort_t) +
                GetSummaryDecimal(Drug_s) +
                GetSummaryDecimal(Otvl_r);

            customMaskedTextBoxSum.Text = DecimalHoursToHHmm(total);
        }

        private decimal GetSummaryDecimal(DevExpress.XtraGrid.Columns.GridColumn col)
        {
            var v = col.SummaryItem?.SummaryValue;
            if (v == null || v == DBNull.Value) return 0m;

            if (v is decimal d) return d;
            return decimal.TryParse(v.ToString(), out var parsed) ? parsed : 0m;
        }
        private void UpdateSumPeriodText_Day(DateTime day)
        {
            layoutControlItemSum.Text = $"Итог за {day:dd.MM.yyyy}:";
        }

        private void UpdateSumPeriodText_Month(DateTime from, DateTime to)
        {
            layoutControlItemSum.Text = $"Итог за период {from:dd.MM.yyyy} - {to:dd.MM.yyyy}:";
        }
        #endregion

        #region Filter on Calendar
        private bool _monthMode;
        private void customCalendarControlTabel_EditValueChanged(object sender, EventArgs e)
        {
            if (customCalendarControlTabel.EditValue is not DateTime dt)
                return;

            if (_lastCalendarDate.HasValue &&
                (_lastCalendarDate.Value.Year != dt.Year ||
                _lastCalendarDate.Value.Month != dt.Month))
            {
                _monthMode = true;
                ApplyFilter_Month(dt);
            }
            else
            {
                _monthMode = false;
                ApplyFilter_Day(dt);
            }
            _lastCalendarDate = dt;
        }
        private void customButtonAll_Click(object sender, EventArgs e)
        {
            var dt = customCalendarControlTabel.EditValue is DateTime x ? x : DateTime.Today;
            _monthMode = true;
            ApplyFilter_Month(dt);
            _lastCalendarDate = dt;
        }

        private void ApplyFilter_Day(DateTime dt)
        {
            var d0 = dt.Date;
            var d1 = d0.AddDays(1);

            ApplyFilter(d0, d1);
            UpdateSumPeriodText_Day(d0);
        }
        private void ApplyFilter_Month(DateTime dt)
        {
            customCalendarControlTabel.EditValue = new DateTime(dt.Year, dt.Month, 1);
            var m0 = new DateTime(dt.Year, dt.Month, 1);
            var m1 = m0.AddMonths(1);

            ApplyFilter(m0, m1);
            UpdateSumPeriodText_Month(m0, m1.AddDays(-1));
        }

        private void ApplyFilter(DateTime dt1, DateTime dt2)
        {
            bandedGridViewTabel.ActiveFilterString =
                $"[Dat] >= #{dt1:MM/dd/yyyy}# AND [Dat] < #{dt2:MM/dd/yyyy}#";

            bandedGridViewTabel.BeginSort();
            bandedGridViewTabel.SortInfo.Clear();
            bandedGridViewTabel.SortInfo.Add(Dat, DevExpress.Data.ColumnSortOrder.Ascending);
            bandedGridViewTabel.SortInfo.Add(N_r, DevExpress.Data.ColumnSortOrder.Ascending);
            bandedGridViewTabel.EndSort();
            bandedGridViewTabel.UpdateSummary();
        }
        private void ApplyCurrentCalendarFilter()
        {
            if (customCalendarControlTabel.EditValue is not DateTime dt)
                return;

            if (_monthMode) ApplyFilter_Month(dt);
            else ApplyFilter_Day(dt);
        }
        #endregion

        #region Calc Hours
        private static decimal CalcHoursByFromTo(DateTime? timeFrom, DateTime? timeTo)
        {
            if (timeFrom == null || timeTo == null)
                return 0m;

            var from = timeFrom.Value.TimeOfDay;
            var to = timeTo.Value.TimeOfDay;

            TimeSpan diff;

            if (to < from && to != TimeSpan.Zero)
                return 0m;
            else if (to < from && to == TimeSpan.Zero)
                diff = (TimeSpan.FromHours(24) - from) + to;
            else
                diff = to - from;

            if (diff < TimeSpan.Zero)
                return 0m;

            var hours = (decimal)diff.TotalMinutes / 60m;

            return Math.Round(hours, 2, MidpointRounding.AwayFromZero);
        }
        private async void bandedGridViewTabel_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            flagUpdate = false;
            if (e.RowHandle < 0) return;

            var row = bandedGridViewTabel.GetRow(e.RowHandle) as TabOtvlRModel;
            if (row == null) return;
            if (!CanEditRow(row, out var reason))
            {
                ShowEditDenied(reason);
                bandedGridViewTabel.CancelUpdateCurrentRow();
                return;
            }

            if (e.Column == Priem_t_s || e.Column == Priem_t_po)
                row.Priem_t = CalcHoursByFromTo(row.Priem_t_s, row.Priem_t_po);

            if (e.Column == Sort_t_s || e.Column == Sort_t_po)
                row.Sort_t = CalcHoursByFromTo(row.Sort_t_s, row.Sort_t_po);

            if (e.Column == Drug_s_s || e.Column == Drug_s_po)
                row.Drug_s = CalcHoursByFromTo(row.Drug_s_s, row.Drug_s_po);

            if (e.Column == Otvl_r_s || e.Column == Otvl_r_po)
                row.Otvl_r = CalcHoursByFromTo(row.Otvl_r_s, row.Otvl_r_po);

            bandedGridViewTabel.RefreshRow(e.RowHandle);
            bandedGridViewTabel.UpdateSummary();
            UpdateTotalSumMasked();
            await tabelUpdated(row);
        }
        private void bandedGridViewTabel_ShowingEditor(object sender, CancelEventArgs e)
        {
            var row = bandedGridViewTabel.GetFocusedRow() as TabOtvlRModel;
            if (row == null) return;

            if (!CanEditRow(row, out _))
                e.Cancel = true;
        }
        #endregion

        #region Add/Del/Save
        private async void customButtonAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!CanAddRow(out var day, out var reason))
                {
                    ShowEditDenied(reason);
                    return;
                }

                //var day = GetSelectedDayOrTodaySafe();

                var model = new TabOtvlRModel
                {
                    Gr = _idGroup,
                    Tab = _idTabno,
                    Dat = day,
                    Mg = $"{day.Month:00}{day.Year % 100:00}",
                    N_r = GetNextNrForDay(day),
                    OrNew = 1
                };

                int newId = await tabOtvlRDataService.SaveAsync(model); // должен вернуть id
                model.Id = newId;

                tabOtvlRList.Add(model);

                bandedGridViewTabel.RefreshData();

                int rowHandle = bandedGridViewTabel.GetRowHandle(tabOtvlRList.Count - 1);
                bandedGridViewTabel.FocusedRowHandle = rowHandle;

                bandedGridViewTabel.FocusedColumn = Priem_t_s;
                bandedGridViewTabel.ShowEditor();

                bandedGridViewTabel.UpdateSummary();
                UpdateTotalSumMasked();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Ошибка добавления",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customButtonDel_Click(object sender, EventArgs e)
        {
            var row = bandedGridViewTabel.GetFocusedRow() as TabOtvlRModel;
            if (row == null) return;

            if (!CanDeleteRow(row, out var reason))
            {
                ShowEditDenied(reason);
                return;
            }

            var confirm = DevExpress.XtraEditors.XtraMessageBox.Show(
                "Удалить запись?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (row.Id.HasValue && row.Id.Value > 0)
                    await tabOtvlRDataService.DeleteAsync(row);

                tabOtvlRList.Remove(row);

                bandedGridViewTabel.UpdateSummary();
                UpdateTotalSumMasked();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Ошибка удаления",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void bandedGridViewTabel_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            flagUpdate = false;
            if (e.Row is not TabOtvlRModel row) return;
            if (!row.Id.HasValue || row.Id.Value <= 0) return;
            //await tabelUpdated(row);
        }
        private async Task tabelUpdated(TabOtvlRModel model)
        {
            try
            {
                if (flagUpdate) return;

                Debug.WriteLine("Обновление данных");
                NormalizeRowBeforeSave(model);

                await tabOtvlRDataService.SaveAsync(model); // UPDATE по Id
                model.OrNew = 0;

                bandedGridViewTabel.UpdateSummary();
                UpdateTotalSumMasked();
                flagUpdate = true;
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Ошибка сохранения",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Help for Add/Del/Save
        private bool flagUpdate = false;
        private void customGridControlTabel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
        }
        private int GetNextNrForDay(DateTime day)
        {
            int max = 0;

            foreach (var r in tabOtvlRList)
            {
                if (r.Dat.HasValue && r.Dat.Value.Date == day.Date)
                {
                    if (r.N_r.HasValue && r.N_r.Value > max)
                        max = r.N_r.Value;
                }
            }

            return max + 1;
        }
        private DateTime GetSelectedDayOrTodaySafe()
        {
            if (customCalendarControlTabel.EditValue is DateTime dt)
                return dt.Date;

            if (_lastCalendarDate.HasValue)
                return _lastCalendarDate.Value.Date;

            return DateTime.Today.Date;
        }
        private void NormalizeRowBeforeSave(TabOtvlRModel row)
        {
            row.Gr = _idGroup;
            row.Tab = _idTabno;

            if (row.Dat.HasValue)
            {
                row.Dat = row.Dat.Value.Date;
                row.Mg = $"{row.Dat.Value.Month:00}{row.Dat.Value.Year % 100:00}";
            }

            if (!row.N_r.HasValue || row.N_r.Value <= 0)
                row.N_r = row.Dat.HasValue ? GetNextNrForDay(row.Dat.Value) : 1;

        }
        #endregion

        #region Admin Mode
        private bool _isAdminMode = false;
        private void customToggleSwitchAdmin_Toggled(object sender, EventArgs e)
        {
            _isAdminMode = customToggleSwitchAdmin.IsOn;
        }

        private bool CanAddRow(out DateTime day, out string reason)
        {
            day = GetSelectedDayOrTodaySafe();

            if (!CanEditTable(out reason))
                return false;

            if (!CanEditDay(day, out reason))
                return false;

            return true;
        }
        private bool CanEditTable(out string reason)
        {
            reason = string.Empty;

            if (_idTabno <= 0)
            {
                reason = "Не выбран табельный номер.";
                return false;
            }

            if (_monthMode)
            {
                reason = "Для редактирования выберите конкретный день.";
                return false;
            }

            return true;
        }
        private bool CanEditDay(DateTime day, out string reason)
        {
            reason = string.Empty;

            if (_isAdminMode) return true;

            var today = DateTime.Today;
            day = day.Date;

            // всегда можно сегодня
            if (day == today) return true;
            if (DateTime.Now.TimeOfDay <= new TimeSpan(14, 0, 0))
            {
                // вчера
                if (day == today.AddDays(-1)) return true;

                // сегодня понедельник — разрешаем пятницу/сб/вс
                if (today.DayOfWeek == DayOfWeek.Monday)
                {
                    var friday = today.AddDays(-3).Date;
                    var saturday = today.AddDays(-2).Date;
                    var sunday = today.AddDays(-1).Date;

                    if (day == friday || day == saturday || day == sunday)
                        return true;
                }
            }
            reason = "Можно изменять только за сегодня и вчера до 14:00";
            return false;
        }

        private bool CanEditRow(TabOtvlRModel row, out string reason)
        {
            reason = string.Empty;

            if (row == null)
            {
                reason = "Строка не выбрана.";
                return false;
            }

            if (!row.Dat.HasValue)
            {
                reason = "У строки не заполнена дата.";
                return false;
            }

            if (!CanEditDay(row.Dat.Value, out reason))
                return false;

            return true;
        }

        private bool CanDeleteRow(TabOtvlRModel row, out string reason)
        {
            return CanEditRow(row, out reason);
        }
        private void ShowEditDenied(string reason)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(
                reason,
                "Редактирование запрещено",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        #endregion

        private void customButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Blank report = new Blank();
                report.PrintDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Ошибка при открытии бланка на печать: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }
}
