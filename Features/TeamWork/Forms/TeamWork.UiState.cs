using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        /// <summary>
        /// Очищает элементы управления поиском на форме
        /// </summary>
        private void ClearSearchControls()
        {
            try
            {
                // Очищаем текстовые поля поиска, если они есть на форме
                // Например, если есть searchTextEdit
                var searchControls = this.Controls.Find("searchTextEdit", true);
                foreach (Control control in searchControls)
                {
                    if (control is TextEdit textEdit)
                    {
                        textEdit.Text = string.Empty;
                    }
                }

                // Очищаем другие элементы поиска
                var comboBoxes = this.Controls.OfType<ComboBoxEdit>().Where(cb => cb.Name.Contains("search", StringComparison.OrdinalIgnoreCase));
                foreach (var comboBox in comboBoxes)
                {
                    comboBox.SelectedIndex = -1;
                    comboBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при очистке элементов управления поиском");
            }
        }

        /// <summary>
        /// Показывает статусное сообщение пользователю
        /// </summary>
        private void ShowStatusMessage(string message)
        {
            try
            {
                // Можно показать в статусной строке или временно в заголовке
                this.Text = $"Нормативные расценки - {message}";

                // Через 3 секунды сбрасываем заголовок
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = "Нормативные расценки"));
                    }
                    else
                    {
                        this.Text = "Нормативные расценки";
                    }
                });
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отображении статусного сообщения");
            }
        }

        private void ApplyAnnGridRowStyling()
        {
            try
            {
                if (ANNgridView == null) return;
                ANNgridView.RowStyle -= ANNgridView_RowStyle;
                ANNgridView.RowStyle += ANNgridView_RowStyle;
            }
            catch { }
        }

        private void ANNgridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null) return;
                if (e.RowHandle < 0) return;

                // Подсветка фокусной строки
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.Coral;//.FromArgb(255, 255, 230);
                    e.Appearance.BackColor2 = Color.Coral;//.FromArgb(255, 240, 200);
                    e.HighPriority = true;
                    return;
                }
            }
            catch { }
        }

        private void FocusFirstResultAndLoadRelated()
        {
            try
            {
                var gv = ANNgridView;
                if (gv == null) return;

                // Не трогаем фокус, если пользователь вводит текст в строке автoфильтра или любой активной ячейке
                if (gv.ActiveEditor != null)
                {
                    return;
                }

                if (gv.DataRowCount <= 0)
                {
                    // опционально: очистить связанные таблицы, если нужен пустой показ
                    return;
                }

                // Если текущая фокусная строка видима после фильтра, ничего не делаем
                int currentHandle = gv.FocusedRowHandle;
                if (gv.IsValidRowHandle(currentHandle) && gv.GetVisibleIndex(currentHandle) >= 0)
                {
                    return;
                }

                // Иначе переходим на первую видимую строку результата
                int firstHandle = gv.GetVisibleRowHandle(0);
                if (!gv.IsValidRowHandle(firstHandle)) return;

                gv.BeginUpdate();
                try
                {
                    gv.FocusedRowHandle = firstHandle;
                    gv.MakeRowVisible(firstHandle);
                }
                finally
                {
                    gv.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка в FocusFirstResultAndLoadRelated");
            }
        }

        /// <summary>
        /// Отображает статус обновления секунд
        /// </summary>
        private void ShowSecondsUpdateStatus(string message)
        {
            try
            {
                // Ищем statusLabel или используем заголовок формы
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke((MethodInvoker)(() => statusLabel.Text = message));
                    }
                    else
                    {
                        statusLabel.Text = message;
                    }
                }
                else
                {
                    // Используем заголовок формы как индикатор
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = $"Нормативные расценки - {message}"));
                    }
                    else
                    {
                        this.Text = $"Нормативные расценки - {message}";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отображении статуса обновления секунд");
            }
        }

        /// <summary>
        /// Очищает статус обновления
        /// </summary>
        private void ClearSecondsUpdateStatus()
        {
            try
            {
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke((MethodInvoker)(() => statusLabel.Text = string.Empty));
                    }
                    else
                    {
                        statusLabel.Text = string.Empty;
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = "Нормативные расценки"));
                    }
                    else
                    {
                        this.Text = "Нормативные расценки";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при очистке статуса");
            }
        }

        /// <summary>
        /// Применяет результат утверждения к строке грида.
        /// </summary>
        private void ApplyApprovalToGridRow(GridView gridView, int rowHandle, DateTime approvedAt, int status, string statusText)
        {
            if (gridView == null || rowHandle < 0)
            {
                return;
            }

            gridView.SetRowCellValue(rowHandle, "dateUpdate", approvedAt);
            gridView.SetRowCellValue(rowHandle, "status", status);
            gridView.SetRowCellValue(rowHandle, "StatusText", statusText);
            gridView.RefreshRow(rowHandle);
        }

        /// <summary>
        /// Обновляет строку грида по AnnID, если она найдена.
        /// </summary>
        private bool TryRefreshRowByAnnId(GridView gridView, int annId)
        {
            if (gridView == null || annId <= 0)
            {
                return false;
            }

            int rowHandle = gridView.LocateByValue("AnnID", annId);
            if (rowHandle < 0)
            {
                return false;
            }

            gridView.RefreshRow(rowHandle);
            return true;
        }

        /// <summary>
        /// Фокусирует строку грида по AnnID, прокручивает к ней и обновляет.
        /// </summary>
        private bool TryFocusAndRefreshRowByAnnId(GridView gridView, int annId)
        {
            if (gridView == null || annId <= 0)
            {
                return false;
            }

            int rowHandle = gridView.LocateByValue("AnnID", annId);
            if (rowHandle < 0)
            {
                return false;
            }

            gridView.BeginUpdate();
            try
            {
                gridView.FocusedRowHandle = rowHandle;
                gridView.MakeRowVisible(rowHandle);
                gridView.RefreshRow(rowHandle);
            }
            finally
            {
                gridView.EndUpdate();
            }

            return true;
        }
    }
}
