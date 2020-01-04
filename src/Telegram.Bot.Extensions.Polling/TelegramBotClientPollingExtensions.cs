using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot
{
    /// <summary>
    /// Provides extension methods for <see cref="ITelegramBotClient"/> that allow for <see cref="Update"/> polling
    /// </summary>
    public static class TelegramBotClientPollingExtensions
    {
        /// <summary>
        /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> for each.
        /// <para>This method does not block. GetUpdates will be called AFTER the <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> returns</para>
        /// </summary>
        /// <typeparam name="TUpdateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public static void StartReceiving<TUpdateHandler>(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default)
            where TUpdateHandler : IUpdateHandler, new()
        {
            StartReceiving(botClient, new TUpdateHandler(), cancellationToken);
        }

        /// <summary>
        /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> for each.
        /// <para>This method does not block. GetUpdates will be called AFTER the <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> returns</para>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public static void StartReceiving(
            this ITelegramBotClient botClient,
            IUpdateHandler updateHandler,
            CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            Task.Run(async () =>
            {
                try
                {
                    await ReceiveAsync(botClient, updateHandler, cancellationToken);
                }
                catch (Exception ex)
                {
                    await updateHandler.HandleError(botClient, ex, cancellationToken);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> for each.
        /// <para>This method will block if awaited. GetUpdates will be called AFTER the <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> returns</para>
        /// </summary>
        /// <typeparam name="TUpdateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        /// <returns></returns>
        public static Task ReceiveAsync<TUpdateHandler>(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default)
            where TUpdateHandler : IUpdateHandler, new()
        {
            return ReceiveAsync(botClient, new TUpdateHandler(), cancellationToken);
        }

        /// <summary>
        /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> for each.
        /// <para>This method will block if awaited. GetUpdates will be called AFTER the <see cref="IUpdateHandler.HandleUpdate(ITelegramBotClient, Update, CancellationToken)"/> returns</para>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        /// <returns></returns>
        public static async Task ReceiveAsync(
            this ITelegramBotClient botClient,
            IUpdateHandler updateHandler,
            CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            UpdateType[]? allowedUpdates = updateHandler.AllowedUpdates;
            int messageOffset = 0;
            var emptyUpdates = new Update[] { };

            while (!cancellationToken.IsCancellationRequested)
            {
                int timeout = (int)botClient.Timeout.TotalSeconds;
                var updates = emptyUpdates;
                try
                {
                    updates = await botClient.MakeRequestAsync(new GetUpdatesRequest()
                    {
                        Offset = messageOffset,
                        Timeout = timeout,
                        AllowedUpdates = allowedUpdates,
                    }, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Ignore
                }
                catch (Exception ex)
                {
                    await updateHandler.HandleError(botClient, ex, cancellationToken)
                        .ConfigureAwait(false);
                }

                foreach (var update in updates)
                {
                    await updateHandler.HandleUpdate(botClient, update, cancellationToken)
                        .ConfigureAwait(false);

                    messageOffset = update.Id + 1;
                }
            }
        }
    }
}
