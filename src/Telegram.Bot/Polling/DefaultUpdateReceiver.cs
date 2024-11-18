using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Polling;

/// <summary>A simple <see cref="IUpdateReceiver"/>> implementation that requests new updates and handles them sequentially</summary>
/// <remarks>Constructs a new <see cref="DefaultUpdateReceiver"/> with the specified <see cref="ITelegramBotClient"/> instance and optional <see cref="ReceiverOptions"/></remarks>
/// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
/// <param name="receiverOptions">Options used to configure getUpdates requests</param>
[PublicAPI]
public class DefaultUpdateReceiver(ITelegramBotClient botClient, ReceiverOptions? receiverOptions = default) : IUpdateReceiver
{
    private static readonly Update[] EmptyUpdates = [];

    /// <inheritdoc/>
    public async Task ReceiveAsync(IUpdateHandler updateHandler, CancellationToken cancellationToken = default)
    {
        if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

        var allowedUpdates = receiverOptions?.AllowedUpdates;
        var limit = receiverOptions?.Limit ?? 100;
        var messageOffset = receiverOptions?.Offset ?? 0;
        var emptyUpdates = EmptyUpdates;

        if (receiverOptions?.DropPendingUpdates is true)
        {
            try
            {
                var updates = await botClient.GetUpdates(-1, 1, 0, [], cancellationToken).ConfigureAwait(false);
                messageOffset = updates.Length == 0 ? 0 : updates[^1].Id + 1;
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
        }
        var request = new GetUpdatesRequest
        {
            Limit = limit,
            Offset = messageOffset,
            AllowedUpdates = allowedUpdates,
        };
        while (!cancellationToken.IsCancellationRequested)
        {
            request.Timeout = (int)botClient.Timeout.TotalSeconds;

            var updates = emptyUpdates;
            try
            {
                updates = await botClient.SendRequest(request, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception exception)
            {
                try
                {
                    await updateHandler.HandleErrorAsync(botClient, exception, HandleErrorSource.PollingError, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            foreach (var update in updates)
            {
                try
                {
                    request.Offset = update.Id + 1;
                    await updateHandler.HandleUpdateAsync(botClient, update, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    try
                    {
                        await updateHandler.HandleErrorAsync(botClient, ex, HandleErrorSource.HandleUpdateError, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // ignored
                    }
                }
            }
        }
    }
}

