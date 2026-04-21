using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW
    {
        private async Task InvokeAsync(Action action)
        {
            if (this.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object>();
                try
                {
                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            action();
                            tcs.SetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.SetException(ex);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
                await tcs.Task.ConfigureAwait(true);
            }
            else
            {
                action();
            }
        }

        #region Валидация формы и статусная строка
        private bool ValidateForm()
        {
            bool isValid = true;
            statusLabel.Text = ""; // Очищаем статус
            errorProvider1.Clear(); // Очищаем все старые ошибки

            // Проверка поля Артикул
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProvider1.SetError(nameTextBox, "Введите артикул.");
                if (isValid)
                    statusLabel.Text = "Ошибка: Поле 'Артикул' обязательно для заполнения.";
                isValid = false;
            }

            return isValid;
        }
        private async Task ShowStatusMessage(string message, int delayMs = 3000, Color? color = null)
        {
            statusLabel.Text = message;

            if (color.HasValue)
            {
                var originalColor = statusLabel.ForeColor;
                statusLabel.ForeColor = color.Value;

                await Task.Delay(delayMs);
                statusLabel.Text = "";
                statusLabel.ForeColor = originalColor;
            }
            else
            {
                await Task.Delay(delayMs);
                statusLabel.Text = "";
            }
        }
        #endregion

        private void OnDataChanged(object sender, ListChangedEventArgs e)
        {
            // Помечаем изменения только если не идёт начальная загрузка
            if (!_isInitialLoading && (e.ListChangedType != ListChangedType.Reset || _normRaszList.Count > 0 || _normRaskList.Count > 0 || _normKontList.Count > 0))
            {
                _hasUnsavedChanges = true;
            }
        }

        /// <summary>
        /// Обновляет отображение буфера на основе глобального или локального состояния
        /// </summary>
        #region Буфер: отображение и события
        private void UpdateBufferDisplay()
        {
            try
            {
                if (TeamWorkBuffer.HasData)
                {
                    // Проверяем, является ли это комплектом (Kit режим)
                    if (TeamWorkBuffer.BufferIds?.Count > 1)
                    {
                        // Форматируем отображение для комплекта
                        var bufferText = TeamWorkBuffer.BufferText;
                        if (!string.IsNullOrEmpty(bufferText))
                        {
                            // Разделяем части комплекта по разделителю
                            var parts = bufferText.Split(new string[] { "----------------------------" }, StringSplitOptions.RemoveEmptyEntries);

                            if (parts.Length >= 2)
                            {
                                var formattedText = new System.Text.StringBuilder();
                                formattedText.AppendLine("📦 КОМПЛЕКТ (2 части):");
                                formattedText.AppendLine();

                                formattedText.AppendLine("🔸 Часть 1:");
                                formattedText.AppendLine(StringNormalizer.TrimOrEmpty(parts[0]));
                                formattedText.AppendLine();

                                formattedText.AppendLine("🔸 Часть 2:");
                                formattedText.Append(StringNormalizer.TrimOrEmpty(parts[1]));

                                textBoxBuffer.Text = formattedText.ToString();
                            }
                            else
                            {
                                // Если разделитель не найден, показываем как есть
                                textBoxBuffer.Text = "📦 КОМПЛЕКТ:\n\n" + bufferText;
                            }
                        }
                        else
                        {
                            textBoxBuffer.Text = "📦 КОМПЛЕКТ (2 части)";
                        }

                        // Обновляем текст кнопки для комплекта
                        buffer.Text = "Вставить комплект из буфера:";
                    }
                    else
                    {
                        // Обычный режим (одна запись)
                        textBoxBuffer.Text = TeamWorkBuffer.BufferText;
                        buffer.Text = "Вставить из буфера:";
                    }
                }
                else if (_bufferWorkDivision > 0)
                {
                    // Оставляем существующую логику для обратной совместимости
                    // Текст будет установлен в основном методе загрузки
                    buffer.Text = "Вставить из буфера:";
                }
                else
                {
                    textBoxBuffer.Text = "Буфер пуст";
                    buffer.Text = "Вставить из буфера:";
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обновлении отображения буфера");
            }
        }

        /// <summary>
        /// Обработчик события изменения глобального буфера
        /// </summary>
        private void OnBufferChanged(object sender, BufferChangedEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    UpdateBufferDisplay();
                }));
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обработке изменения буфера");
            }
        }
        #endregion

        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null || e.RowHandle < 0)
                return;

            object row = view.GetRow(e.RowHandle);

            if (row is INewable newableRow && newableRow.IsNew)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.HighPriority = true;
                return;
            }

            try
            {
                if (row is IModifiable modifiable && modifiable.IsModified)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка в GridView_RowStyle при проверке IsModified для строки {e.RowHandle}").ConfigureAwait(false);
            }
        }
    }
}
