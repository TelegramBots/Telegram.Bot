using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Polling;

/// <summary>
/// A simple <see cref="IUpdateReceiver"/>> implementation that requests new updates and handles them sequentially
/// </summary>
[PublicAPI]
public class DefaultUpdateReceiver : IUpdateReceiver
{
    static readonly Update[] EmptyUpdates = Array.Empty<Update>();

    readonly ITelegramBotClient _botClient;
    readonly ReceiverOptions? _receiverOptions;

    /// <summary>
    /// Constructs a new <see cref="DefaultUpdateReceiver"/> with the specified <see cref="ITelegramBotClient"/>>
    /// instance and optional <see cref="ReceiverOptions"/>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    public DefaultUpdateReceiver(
        ITelegramBotClient botClient,
        ReceiverOptions? receiverOptions = default)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _receiverOptions = receiverOptions;
    }

    /// <inheritdoc />
    public async Task ReceiveAsync(
        IUpdateHandler updateHandler,
        CancellationToken cancellationToken = default)
    {
        if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

        var allowedUpdates = _receiverOptions?.AllowedUpdates;
        var limit = _receiverOptions?.Limit ?? default;
        var messageOffset = _receiverOptions?.Offset ?? 0;
        var emptyUpdates = EmptyUpdates;

        if (_receiverOptions?.ThrowPendingUpdates is true)
        {
            try
            {
                messageOffset = await _botClient.ThrowOutPendingUpdatesAsync(
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            var timeout = (int) _botClient.Timeout.TotalSeconds;
            var updates = emptyUpdates;
            try
            {
                var request = new GetUpdatesRequest
                {
                    Limit = limit,
                    Offset = messageOffset,
                    Timeout = timeout,
                    AllowedUpdates = allowedUpdates,
                };
                updates = await _botClient.MakeRequestAsync(
                    request: request,
                    cancellationToken:
                    cancellationToken
                ).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                try
                {
                    await updateHandler.HandlePollingErrorAsync(
                        botClient: _botClient,
                        exception: exception,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }

            foreach (var update in updates)
            {
                try
                {
                    await updateHandler.HandleUpdateAsync(
                        botClient: _botClient,
                        update: update,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                    messageOffset = update.Id + 1;
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }
        }
    }
}
