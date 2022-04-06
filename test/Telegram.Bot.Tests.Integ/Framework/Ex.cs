using System;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Tests.Integ.Framework;

public static class Ex
{
    public static async Task<T> WithCancellation<T>(Func<CancellationToken, Task<T>> fn, int timeout = 45)
    {
        using var _ = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
        return await fn(_.Token);
    }

    public static async Task WithCancellation(Func<CancellationToken, Task> fn, int timeout = 45)
    {
        using var _ = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
        await fn(_.Token);
    }
}