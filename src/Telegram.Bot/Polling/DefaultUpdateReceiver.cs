using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Polling;

/// <summary>
/// A simple <see cref="IUpdateReceiver"/>> implementation that requests new updates and handles them sequentially
/// </summary>
/// <remarks>
/// Constructs a new <see cref="DefaultUpdateReceiver"/> with the specified <see cref="ITelegramBotClient"/>
/// instance and optional <see cref="ReceiverOptions"/>
/// </remarks>
/// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
/// <param name="receiverOptions">Options used to configure getUpdates requests</param>
[PublicAPI]
public class DefaultUpdateReceiver(
    ITelegramBotClient botClient,
    ReceiverOptions? receiverOptions = default) : IUpdateReceiver
{
    static readonly Update[] EmptyUpdates = [];

    readonly ITelegramBotClient _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));

    /// <inheritdoc />
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
                messageOffset = await _botClient.DropPendingUpdatesAsync(
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
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
            request.Timeout = (int) _botClient.Timeout.TotalSeconds;

            var updates = emptyUpdates;
            try
            {
                updates = await _botClient.MakeRequestAsync(
                    request: request,
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception exception)
            {
                try
                {
                    await updateHandler.HandleErrorAsync(
                        botClient: _botClient,
                        exception: exception,
                        source: HandleErrorSource.PollingError,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);
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

                    await updateHandler.HandleUpdateAsync(
                        botClient: _botClient,
                        update: update,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    try
                    {
                        await updateHandler.HandleErrorAsync(
                            botClient: _botClient,
                            exception: ex,
                            source: HandleErrorSource.HandleUpdateError,
                            cancellationToken: cancellationToken
                        ).ConfigureAwait(false);
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

