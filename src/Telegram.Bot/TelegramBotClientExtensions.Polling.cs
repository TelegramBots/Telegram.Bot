using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Polling;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot;

/// <summary>Provides extension methods for <see cref="ITelegramBotClient"/> that allow for <see cref="Update"/> polling</summary>
[PublicAPI]
public static partial class TelegramBotClientExtensions
{
    /// <summary>Drop all pending updates</summary>
    /// <param name="botClient"></param>
    /// <param name="cancellationToken"></param>
    public static async Task DropPendingUpdates(this ITelegramBotClient botClient, CancellationToken cancellationToken = default)
    {
        var updates = await botClient.GetUpdates(-1, 1, 0, allowedUpdates: null, cancellationToken).ConfigureAwait(false);
        if (updates.Length > 0)
            await botClient.GetUpdates(updates[^1].Id + 1, 1, 0, allowedUpdates: null, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdateAsync">IUpdateHandler.HandleUpdateAsync</see> for each.
    /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
    /// <typeparam name="TUpdateHandler"> The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public static void StartReceiving<TUpdateHandler>(this ITelegramBotClient botClient, ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default) where TUpdateHandler : IUpdateHandler, new()
        => StartReceiving(botClient, new TUpdateHandler(), receiverOptions, cancellationToken);

    /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  <paramref name="updateHandler"/> for each.
    /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public static void StartReceiving(this ITelegramBotClient botClient,
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, HandleErrorSource, CancellationToken, Task> errorHandler,
        ReceiverOptions? receiverOptions = default, CancellationToken cancellationToken = default)
        => StartReceiving(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken);

    /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  <paramref name="updateHandler"/> for each.
    /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public static void StartReceiving(this ITelegramBotClient botClient,
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler,
        ReceiverOptions? receiverOptions = default, CancellationToken cancellationToken = default)
        => StartReceiving(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken);

    /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  <paramref name="updateHandler"/> for each.
    /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public static void StartReceiving(this ITelegramBotClient botClient,
        Action<ITelegramBotClient, Update, CancellationToken> updateHandler,
        Action<ITelegramBotClient, Exception, CancellationToken> errorHandler,
        ReceiverOptions? receiverOptions = default, CancellationToken cancellationToken = default)
        => StartReceiving(botClient, new DefaultUpdateHandler(
                (bot, update, token) => { updateHandler(bot, update, token); return Task.CompletedTask; },
                (bot, exception, source, token) => { errorHandler(bot, exception, token); return Task.CompletedTask; }
            ), receiverOptions, cancellationToken);

    /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdateAsync">IUpdateHandler.HandleUpdateAsync</see> for each.
    /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public static void StartReceiving(this ITelegramBotClient botClient, IUpdateHandler updateHandler, ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (botClient is null) { throw new ArgumentNullException(nameof(botClient)); }
        if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

        // ReSharper disable once MethodSupportsCancellation
        _ = Task.Run(async () =>
        {
            try
            {
                await ReceiveAsync(botClient, updateHandler, receiverOptions, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                try
                {
                    await updateHandler.HandleErrorAsync(botClient, ex, HandleErrorSource.FatalError, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }
        }, cancellationToken);
    }

    /// <summary>Wait for incoming <see cref="Update"/>s, invoking <see cref="IUpdateHandler.HandleUpdateAsync">IUpdateHandler.HandleUpdateAsync</see> for each.
    /// <para>This method will block if awaited. It runs a polling loop, calling GetUpdates then your handler for each update</para></summary>
    /// <typeparam name="TUpdateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    /// <returns>A <see cref="Task"/> that will be completed when cancellation will be requested through <paramref name="cancellationToken"/></returns>
    public static async Task ReceiveAsync<TUpdateHandler>(this ITelegramBotClient botClient, ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default) where TUpdateHandler : IUpdateHandler, new()
        => await ReceiveAsync(botClient, new TUpdateHandler(), receiverOptions, cancellationToken).ConfigureAwait(false);

    /// <summary>Wait for incoming <see cref="Update"/>s, invoking <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>This method will block if awaited. It runs a polling loop, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    /// <returns>A <see cref="Task"/> that will be completed when cancellation will be requested through <paramref name="cancellationToken"/></returns>
    public static async Task ReceiveAsync(this ITelegramBotClient botClient,
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler,
        ReceiverOptions? receiverOptions = default, CancellationToken cancellationToken = default)
        => await ReceiveAsync(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken).ConfigureAwait(false);

    /// <summary>Wait for incoming <see cref="Update"/>s, invoking <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>This method will block if awaited. It runs a polling loop, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    /// <returns>A <see cref="Task"/> that will be completed when cancellation will be requested through <paramref name="cancellationToken"/></returns>
    public static async Task ReceiveAsync(this ITelegramBotClient botClient,
        Action<ITelegramBotClient, Update, CancellationToken> updateHandler,
        Action<ITelegramBotClient, Exception, CancellationToken> errorHandler,
        ReceiverOptions? receiverOptions = default, CancellationToken cancellationToken = default)
        => await ReceiveAsync(botClient, new DefaultUpdateHandler(
                (bot, update, token) => { updateHandler(bot, update, token); return Task.CompletedTask; },
                (bot, exception, source, token) => { errorHandler(bot, exception, token); return Task.CompletedTask; }
            ), receiverOptions, cancellationToken).ConfigureAwait(false);

    /// <summary>Wait for incoming <see cref="Update"/>s, invoking <see cref="IUpdateHandler.HandleUpdateAsync">IUpdateHandler.HandleUpdateAsync</see> for each.
    /// <para>This method will block if awaited. It runs a polling loop, calling GetUpdates then your handler for each update</para></summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    /// <returns>A <see cref="Task"/> that will be completed when cancellation will be requested through <paramref name="cancellationToken"/></returns>
    public static async Task ReceiveAsync(this ITelegramBotClient botClient, IUpdateHandler updateHandler, ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

        var allowedUpdates = receiverOptions?.AllowedUpdates;
        var limit = receiverOptions?.Limit ?? 100;
        var messageOffset = receiverOptions?.Offset ?? 0;
        var emptyUpdates = Array.Empty<Update>();

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
        var request = new Requests.GetUpdatesRequest
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
