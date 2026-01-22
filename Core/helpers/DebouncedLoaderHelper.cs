using System;
using System.Threading;
using System.Threading.Tasks;

public sealed class DebouncedLoader : IDisposable
{
    private CancellationTokenSource _userCts = new();
    private readonly CancellationTokenSource _lifetimeCts = new();
    private CancellationTokenSource? _debounceCts;

    private readonly int _delayMs;

    public DebouncedLoader(int delayMs = 300)
    {
        _delayMs = delayMs;
    }

    /// <summary>Вызывать при закрытии формы</summary>
    public void CancelLifetime()
    {
        _lifetimeCts.Cancel();
    }

    /// <summary>Запуск действия с debounce + cancellation</summary>
    public async Task RunAsync(
    Func<CancellationToken, Task> action,
    Func<Exception, Task>? onError = null,
    Func<bool, Task>? onCanceled = null) // true = отмена из-за закрытия формы, false = пользователь
    {
        _debounceCts?.Cancel();
        _debounceCts = new CancellationTokenSource();

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            _debounceCts.Token,
            _userCts.Token,
            _lifetimeCts.Token);

        try
        {
            await Task.Delay(_delayMs, linkedCts.Token);
            await action(linkedCts.Token);
        }
        catch (OperationCanceledException)
        {
            if (onCanceled != null)
            {
                bool byLifetime = _lifetimeCts.IsCancellationRequested;
                await onCanceled(byLifetime);
            }
            // иначе молча
        }
        catch (Exception ex)
        {
            if (onError != null)
                await onError(ex);
        }
    }


    /// <summary>Вызывать при новом пользовательском действии</summary>
    public void CancelUser()
    {
        _userCts.Cancel();
        _userCts.Dispose();
        _userCts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _debounceCts?.Cancel();
        _userCts.Cancel();
        _lifetimeCts.Cancel();
    }
}
