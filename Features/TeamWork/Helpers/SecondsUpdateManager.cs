using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Linq;

namespace SewingProduction.Helpers
{
    public class SecondsUpdateManager
    {
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger;
        private CancellationTokenSource _updateCts;

        public SecondsUpdateManager(ArtNormService artNormService, ILogger logger)
        {
            _artNormService = artNormService;
            _logger = logger;
        }

        /// <summary>
        /// Запускает обновление секунд с индикатором прогресса и периодическими проверками
        /// </summary>
        /// <param name="annId">ID разделения труда</param>
        /// <param name="gridView">GridView для обновления</param>
        /// <param name="bindingList">Список данных</param>
        /// <param name="statusCallback">Callback для отображения статуса</param>
        public async Task StartSecondsUpdateAsync(int annId, GridView gridView, System.ComponentModel.BindingList<ArtNormN> bindingList, Action<string> statusCallback = null)
        {
            // Отменяем предыдущее обновление если оно было
            _updateCts?.Cancel();
            _updateCts = new CancellationTokenSource();
            var token = _updateCts.Token;

            try
            {
                statusCallback?.Invoke("⏳ Пересчёт секунд...");
                
                // Сначала получаем текущие данные для немедленного обновления UI
                var currentAnn = await _artNormService.GetArtNormDataById(annId);
                if (currentAnn != null)
                {
                    UpdateAnnInGridView(currentAnn, gridView, bindingList);
                    statusCallback?.Invoke("📊 Обновление локальных данных...");
                }

                // Запускаем SQL процедуру пересчёта (если она ещё не запущена)
                await _artNormService.ExecutePztOperUpdateAsync();
                
                // Периодически проверяем обновления с увеличивающимся интервалом
                var delays = new[] { 1000, 2000, 3000, 5000, 10000, 15000 }; // мс
                int attemptCount = 0;
                DateTime lastUpdate = currentAnn?.dateUpdate ?? DateTime.MinValue;

                statusCallback?.Invoke("🔄 Ожидание завершения пересчёта на сервере...");

                foreach (var delay in delays)
                {
                    if (token.IsCancellationRequested) return;
                    
                    await Task.Delay(delay, token);
                    attemptCount++;
                    
                    try
                    {
                        var updatedAnn = await _artNormService.GetArtNormDataById(annId);
                        if (updatedAnn != null)
                        {
                            // Проверяем, обновились ли данные
                            bool dataChanged = HasDataChanged(currentAnn, updatedAnn);
                            
                            if (dataChanged)
                            {
                                currentAnn = updatedAnn;
                                UpdateAnnInGridView(currentAnn, gridView, bindingList);
                                statusCallback?.Invoke($"✅ Секунды обновлены (попытка {attemptCount})");
                                await _logger.LogEventAsync($"Секунды успешно обновлены для AnnID: {annId} за {attemptCount} попыток", "SecondsUpdateManager");
                                return;
                            }
                            
                            statusCallback?.Invoke($"⏳ Проверка {attemptCount}/{delays.Length}...");
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка при проверке обновления секунд (попытка {attemptCount})");
                    }
                }

                // Если за все попытки данные не обновились
                statusCallback?.Invoke("⚠️ Пересчёт может занять больше времени");
                await _logger.LogEventAsync($"Превышено время ожидания обновления секунд для AnnID: {annId}", "SecondsUpdateManager");
            }
            catch (OperationCanceledException)
            {
                statusCallback?.Invoke("❌ Обновление отменено");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении секунд");
                statusCallback?.Invoke("❌ Ошибка при обновлении секунд");
            }
        }

        /// <summary>
        /// Обновляет запись ArtNormN в GridView и BindingList
        /// </summary>
        private void UpdateAnnInGridView(ArtNormN updatedAnn, GridView gridView, System.ComponentModel.BindingList<ArtNormN> bindingList)
        {
            if (gridView?.GridControl?.InvokeRequired == true)
            {
                gridView.GridControl.Invoke((MethodInvoker)(() => UpdateAnnInGridView(updatedAnn, gridView, bindingList)));
                return;
            }

            try
            {
                // Найти и обновить запись в BindingList
                var existingItem = bindingList?.FirstOrDefault(x => x.AnnID == updatedAnn.AnnID);
                if (existingItem != null)
                {
                    existingItem.CopyPropertiesFrom(updatedAnn);
                    
                    // Обновить конкретную строку в GridView
                    int rowHandle = gridView.LocateByValue("AnnID", updatedAnn.AnnID);
                    if (rowHandle >= 0)
                    {
                        gridView.RefreshRow(rowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обновлении GridView");
            }
        }

        /// <summary>
        /// Проверяет, изменились ли ключевые поля секунд
        /// </summary>
        private bool HasDataChanged(ArtNormN oldData, ArtNormN newData)
        {
            if (oldData == null || newData == null) return true;

            return oldData.Sek != newData.Sek ||
                   oldData.SekShv != newData.SekShv ||
                   oldData.SekVyaz != newData.SekVyaz ||
                   oldData.SekVyaz3 != newData.SekVyaz3 ||
                   oldData.SekVyaz5 != newData.SekVyaz5 ||
                   oldData.SekVyaz6 != newData.SekVyaz6 ||
                   oldData.SekVyaz7 != newData.SekVyaz7 ||
                   oldData.SekVyaz10 != newData.SekVyaz10 ||
                   oldData.SekVyaz12 != newData.SekVyaz12 ||
                   oldData.SekVyaz14 != newData.SekVyaz14 ||
                   oldData.SekVyaz70 != newData.SekVyaz70 ||
                   oldData.SekVyaz71 != newData.SekVyaz71 ||
                   oldData.SekVyaz72 != newData.SekVyaz72 ||
                   oldData.SekVyaz62 != newData.SekVyaz62 ||
                   oldData.SekVyaz57 != newData.SekVyaz57 ||
                   oldData.SekVyaz18 != newData.SekVyaz18 ||
                   oldData.SekVyazo != newData.SekVyazo ||
                   oldData.dateUpdate != newData.dateUpdate ||
                   Math.Abs(oldData.Seb - newData.Seb) > 0.01m; // для decimal
        }

        /// <summary>
        /// Отменяет текущее обновление
        /// </summary>
        public void CancelUpdate()
        {
            _updateCts?.Cancel();
        }

        public void Dispose()
        {
            _updateCts?.Cancel();
            _updateCts?.Dispose();
        }
    }
} 