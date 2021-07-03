using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// A simple <see cref="IUpdateReceiver"/>> implementation that requests new updates and handles them sequentially
    /// </summary>
    public class DefaultUpdateReceiver : IUpdateReceiver
    {
        static readonly Update[] EmptyUpdates = Array.Empty<Update>();

        readonly ITelegramBotClient _botClient;
        readonly ReceiverOptions? _receiveOptions;

        /// <summary>
        /// Constructs a new <see cref="DefaultUpdateReceiver"/> with the specified <see cref="ITelegramBotClient"/>>
        /// instance and optional <see cref="ReceiverOptions"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="receiveOptions">Options used to configure getUpdates requests</param>
        public DefaultUpdateReceiver(
            ITelegramBotClient botClient,
            ReceiverOptions? receiveOptions = default)
        {
            _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
            _receiveOptions = receiveOptions;
        }

        /// <inheritdoc />
        public async Task ReceiveAsync(
            IUpdateHandler updateHandler,
            CancellationToken cancellationToken = default)
        {
            if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

            var allowedUpdates = _receiveOptions?.AllowedUpdates;
            var limit = _receiveOptions?.Limit ?? default;
            var messageOffset = _receiveOptions?.Offset ?? 0;
            var emptyUpdates = EmptyUpdates;

            if (_receiveOptions?.ThrowPendingUpdates is true)
            {
                try
                {
                    messageOffset = await _botClient.ThrowOutPendingUpdatesAsync(
                        cancellationToken: cancellationToken
                    );
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
                    updates = await _botClient.MakeRequestAsync(request, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Ignore
                }
                catch (Exception ex)
                {
                    try
                    {
                        await updateHandler.HandleErrorAsync(_botClient, ex, cancellationToken)
                            .ConfigureAwait(false);
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
                        await updateHandler.HandleUpdateAsync(_botClient, update, cancellationToken)
                            .ConfigureAwait(false);

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
}
