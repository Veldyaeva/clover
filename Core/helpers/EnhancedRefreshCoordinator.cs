using DevExpress.XtraReports.Native;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Core.helpers
{
    /// <summary>
    /// Улучшенный координатор обновлений данных с защитой от дребезга, накоплением изменений,
    /// приоритетами и защитой от каскадных обновлений.
    /// </summary>
    public sealed class EnhancedRefreshCoordinator : IDisposable
    {
        private readonly Func<string, Task> _reloadByObjectNameAsync;
        private readonly TimeSpan _debounce;
        private readonly TimeSpan? _throttle;
        private readonly TimeSpan _maxWait;
        private readonly int _maxBatchSize;
        private readonly int _maxParallelReloads;
        private readonly int _maxCascadeDepth;

        private readonly object _lock = new();
        
        // Очередь ожидающих обновлений с приоритетами и временными метками
        private readonly Dictionary<string, PendingRequest> _pending = new(StringComparer.OrdinalIgnoreCase);
        
        // Отслеживание последнего времени выполнения для throttle
        private readonly Dictionary<string, DateTime> _lastExecutionTime = new(StringComparer.OrdinalIgnoreCase);
        
        // Защита от каскадов: отслеживание цепочек обновлений
        private readonly Dictionary<string, int> _cascadeDepth = new(StringComparer.OrdinalIgnoreCase);
        
        // Приоритеты для разных ObjectName (чем выше число, тем выше приоритет)
        private readonly Dictionary<string, int> _priorities = new(StringComparer.OrdinalIgnoreCase);

        private DateTime _batchStartedUtc = DateTime.MinValue;
        private CancellationTokenSource? _debounceCts;
        private CancellationTokenSource? _maxWaitCts;
        private bool _isPaused = false;

        // Single-flight + cancel per object
        private readonly Dictionary<string, CancellationTokenSource> _inflightCts = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, SemaphoreSlim> _objectLocks = new(StringComparer.OrdinalIgnoreCase);
        private readonly SemaphoreSlim _globalGate;

        // Метрики
        private int _totalRequests = 0;
        private int _totalExecutions = 0;
        private int _totalBatches = 0;
        private int _cascadePreventions = 0;

        private sealed class PendingRequest
        {
            public string ObjectName { get; init; } = "";
            public DateTime RequestedAtUtc { get; init; }
            public int Priority { get; init; }
        }

        public EnhancedRefreshCoordinator(
            Func<string, Task> reloadByObjectNameAsync,
            TimeSpan? debounce = null,
            TimeSpan? throttle = null,
            TimeSpan? maxWait = null,
            int maxBatchSize = 50,
            int maxParallelReloads = 2,
            int maxCascadeDepth = 3)
        {
            _reloadByObjectNameAsync = reloadByObjectNameAsync ?? throw new ArgumentNullException(nameof(reloadByObjectNameAsync));
            //_debounce = debounce ?? TimeSpan.FromMilliseconds(500);
            //_throttle = throttle; // null = отключен
            //_maxWait = maxWait ?? TimeSpan.FromSeconds(3);

            _debounce = debounce ?? TimeSpan.FromMilliseconds(500);
            _throttle = throttle ?? TimeSpan.FromMicroseconds(1500); // null = отключен
            _maxWait = maxWait ?? TimeSpan.FromSeconds(2000);
            _maxBatchSize = Math.Max(1, maxBatchSize);
            _maxParallelReloads = Math.Max(1, maxParallelReloads);
            _maxCascadeDepth = Math.Max(1, maxCascadeDepth);
            _globalGate = new SemaphoreSlim(_maxParallelReloads, _maxParallelReloads);
        }

        /// <summary>
        /// Устанавливает приоритет для ObjectName (чем выше число, тем выше приоритет).
        /// </summary>
        public void SetPriority(string objectName, int priority)
        {
            if (string.IsNullOrWhiteSpace(objectName)) return;

            lock (_lock)
            {
                _priorities[objectName] = priority;
            }
        }

        /// <summary>
        /// Запрос обновления для одного ObjectName.
        /// </summary>
        public void Request(string objectName, int? priority = null)
        {
            if (string.IsNullOrWhiteSpace(objectName)) return;

            lock (_lock)
            {
                if (_isPaused)
                {
                    Debug.WriteLine($"[EnhancedRefreshCoordinator] Request ignored (paused): {objectName}");
                    return;
                }

                // Проверка защиты от каскадов
                if (_cascadeDepth.TryGetValue(objectName, out var depth) && depth >= _maxCascadeDepth)
                {
                    _cascadePreventions++;
                    Debug.WriteLine($"[EnhancedRefreshCoordinator] Cascade prevention: {objectName} (depth={depth})");
                    return;
                }

                // Увеличиваем глубину каскада для этого объекта
                _cascadeDepth[objectName] = (_cascadeDepth.TryGetValue(objectName, out var current) ? current : 0) + 1;

                var effectivePriority = priority ?? _priorities.GetValueOrDefault(objectName, 0);
                
                _pending[objectName] = new PendingRequest
                {
                    ObjectName = objectName,
                    RequestedAtUtc = DateTime.UtcNow,
                    Priority = effectivePriority
                };

                _totalRequests++;

                if (_batchStartedUtc == DateTime.MinValue)
                    _batchStartedUtc = DateTime.UtcNow;

                // Перезапускаем debounce-таймер
                _debounceCts?.Cancel();
                _debounceCts = new CancellationTokenSource();
                _ = FireAfterAsync(_debounce, _debounceCts.Token, isMaxWait: false);

                // Запускаем maxWait-таймер, если ещё не запущен
                if (_maxWaitCts == null)
                {
                    _maxWaitCts = new CancellationTokenSource();
                    _ = FireAfterAsync(_maxWait, _maxWaitCts.Token, isMaxWait: true);
                }
            }
        }

        /// <summary>
        /// Батч-запрос для нескольких ObjectName одновременно.
        /// </summary>
        public void RequestBatch(IEnumerable<string> objectNames, int? priority = null)
        {
            if (objectNames == null) return;

            foreach (var objName in objectNames.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                Request(objName, priority);
            }
        }

        /// <summary>
        /// Приостанавливает обработку запросов.
        /// </summary>
        public void Pause()
        {
            lock (_lock)
            {
                _isPaused = true;
                Debug.WriteLine("[EnhancedRefreshCoordinator] Paused");
            }
        }

        /// <summary>
        /// Возобновляет обработку запросов.
        /// </summary>
        public void Resume()
        {
            lock (_lock)
            {
                _isPaused = false;
                Debug.WriteLine("[EnhancedRefreshCoordinator] Resumed");
                
                // Если есть накопленные запросы, запускаем обработку
                if (_pending.Count > 0)
                {
                    _debounceCts?.Cancel();
                    _debounceCts = new CancellationTokenSource();
                    _ = FireAfterAsync(_debounce, _debounceCts.Token, isMaxWait: false);
                }
            }
        }

        /// <summary>
        /// Возвращает количество ожидающих обновлений.
        /// </summary>
        public int GetPendingCount()
        {
            lock (_lock)
            {
                return _pending.Count;
            }
        }

        /// <summary>
        /// Возвращает статистику работы координатора.
        /// </summary>
        public RefreshStatistics GetStatistics()
        {
            lock (_lock)
            {
                return new RefreshStatistics
                {
                    TotalRequests = _totalRequests,
                    TotalExecutions = _totalExecutions,
                    TotalBatches = _totalBatches,
                    CascadePreventions = _cascadePreventions,
                    PendingCount = _pending.Count,
                    InFlightCount = _inflightCts.Count
                };
            }
        }

        private async Task FireAfterAsync(TimeSpan delay, CancellationToken token, bool isMaxWait)
        {
            try
            {
                await Task.Delay(delay, token).ConfigureAwait(false);

                List<PendingRequest> toRun;
                lock (_lock)
                {
                    if (_isPaused || _pending.Count == 0)
                        return;

                    if (isMaxWait && _batchStartedUtc == DateTime.MinValue)
                        return;

                    // Сортируем по приоритету (высокий приоритет первым) и времени запроса
                    toRun = _pending.Values
                        .OrderByDescending(x => x.Priority)
                        .ThenBy(x => x.RequestedAtUtc)
                        .Take(_maxBatchSize)
                        .ToList();

                    // Удаляем обработанные из очереди
                    foreach (var req in toRun)
                    {
                        _pending.Remove(req.ObjectName);
                    }

                    // Если обработали все, сбрасываем таймеры
                    if (_pending.Count == 0)
                    {
                        _batchStartedUtc = DateTime.MinValue;
                        _debounceCts?.Cancel();
                        _debounceCts = null;
                        _maxWaitCts?.Cancel();
                        _maxWaitCts = null;
                    }
                    else if (!isMaxWait)
                    {
                        // Если debounce сработал, но есть ещё запросы - продолжаем ждать maxWait
                        // (не сбрасываем maxWait таймер)
                    }
                }

                if (toRun.Count == 0)
                    return;

                _totalBatches++;
                Debug.WriteLine($"[EnhancedRefreshCoordinator] Executing batch of {toRun.Count} objects");

                // Выполняем обновления с учётом throttle
                foreach (var req in toRun)
                {
                    _ = RunSingleAsync(req.ObjectName);
                }
            }
            catch (OperationCanceledException)
            {
                // Нормальная отмена
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EnhancedRefreshCoordinator] Error in FireAfterAsync: {ex}");
            }
        }

        private SemaphoreSlim GetObjectLock(string objectName)
        {
            lock (_lock)
            {
                if (!_objectLocks.TryGetValue(objectName, out var sem))
                {
                    sem = new SemaphoreSlim(1, 1);
                    _objectLocks[objectName] = sem;
                }
                return sem;
            }
        }

        private async Task RunSingleAsync(string objectName)
        {
            // Проверка throttle
            if (_throttle.HasValue)
            {
                lock (_lock)
                {
                    if (_lastExecutionTime.TryGetValue(objectName, out var lastTime))
                    {
                        var timeSinceLastExecution = DateTime.UtcNow - lastTime;
                        if (timeSinceLastExecution < _throttle.Value)
                        {
                            Debug.WriteLine($"[EnhancedRefreshCoordinator] Throttled: {objectName} (last execution: {timeSinceLastExecution.TotalMilliseconds}ms ago)");
                            lock (_lock)
                            {
                                _cascadeDepth.Remove(objectName);
                            }
                            return;
                        }
                    }
                }
            }

            var objLock = GetObjectLock(objectName);

            await objLock.WaitAsync().ConfigureAwait(false);
            CancellationTokenSource cts;
            try
            {
                lock (_lock)
                {
                    // Отменяем предыдущее выполнение, если есть
                    if (_inflightCts.TryGetValue(objectName, out var old))
                    {
                        old.Cancel();
                        old.Dispose();
                    }

                    cts = new CancellationTokenSource();
                    _inflightCts[objectName] = cts;
                }
            }
            finally
            {
                objLock.Release();
            }

            try
            {
                await _globalGate.WaitAsync().ConfigureAwait(false);
                cts.Token.ThrowIfCancellationRequested();
                try
                {
                    Debug.WriteLine($"[EnhancedRefreshCoordinator] Executing: {objectName}");
                    await _reloadByObjectNameAsync(objectName).ConfigureAwait(false);
                    
                    lock (_lock)
                    {
                        _totalExecutions++;
                        _lastExecutionTime[objectName] = DateTime.UtcNow;
                        
                        // Сбрасываем глубину каскада после успешного выполнения
                        _cascadeDepth.Remove(objectName);
                    }
                }
                finally
                {
                    _globalGate.Release();
                }
            }
            catch (SqlException ex)
            {
                 Debug.WriteLine(
                    $"SQL ERROR {ex.Number}: {ex.Message}\n" +
                    $"Procedure: {ex.Procedure}\n" +
                    $"Line: {ex.LineNumber}");
                throw;
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine($"[EnhancedRefreshCoordinator] Cancelled: {objectName}");
                lock (_lock)
                {
                    _cascadeDepth.Remove(objectName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EnhancedRefreshCoordinator] Reload failed: {objectName}. {ex}");
                
                lock (_lock)
                {
                    // При ошибке тоже сбрасываем глубину каскада, чтобы не блокировать навсегда
                    _cascadeDepth.Remove(objectName);
                }
            }
            finally
            {
                lock (_lock)
                {
                    _inflightCts.Remove(objectName);
                    cts?.Dispose();
                }
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _isPaused = true;
                _debounceCts?.Cancel();
                _maxWaitCts?.Cancel();
                _debounceCts?.Dispose();
                _maxWaitCts?.Dispose();
                _debounceCts = null;
                _maxWaitCts = null;
            }
            List<CancellationTokenSource> inflight;
            List<SemaphoreSlim> locks;

            lock (_lock)
            {
                inflight = _inflightCts.Values.ToList();
                locks = _objectLocks.Values.ToList();
                Debug.WriteLine(inflight.ToString(), "dispose");
            }
            foreach (var cts in _inflightCts.Values)
            {
                cts.Cancel();
                cts.Dispose();
            }

            foreach (var sem in _objectLocks.Values)
                sem.Dispose();

            _globalGate.Dispose();
        }
    }

    /// <summary>
    /// Статистика работы координатора обновлений.
    /// </summary>
    public sealed class RefreshStatistics
    {
        public int TotalRequests { get; init; }
        public int TotalExecutions { get; init; }
        public int TotalBatches { get; init; }
        public int CascadePreventions { get; init; }
        public int PendingCount { get; init; }
        public int InFlightCount { get; init; }
    }
}
