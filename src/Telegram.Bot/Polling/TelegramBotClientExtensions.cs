using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Polling;

internal static class TelegramBotClientExtensions
{
    /// <summary>
    /// Will attempt to throw the last update using offset set to -1.
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Update ID of the last <see cref="Update"/> increased by 1 if there were any
    /// </returns>
    internal static async Task<int> ThrowOutPendingUpdatesAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default)
    {
        var request = new GetUpdatesRequest
        {
            Limit = 1,
            Offset = -1,
            Timeout = 0,
            AllowedUpdates = Array.Empty<UpdateType>(),
        };
        var updates = await botClient.MakeRequestAsync(request: request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

#if NET6_0_OR_GREATER
        if (updates.Length > 0) { return updates[^1].Id + 1; }
#else
        if (updates.Length > 0) { return updates[updates.Length - 1].Id + 1; }
#endif
        return 0;
    }
}
