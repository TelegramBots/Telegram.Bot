using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot;

/// <summary>
/// Provides extension methods for <see cref="ITelegramBotClient"/> that allow for <see cref="Update"/> polling
/// </summary>
public static class TelegramBotClientPollingExtensions
{
    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method does not block. GetUpdates will be called AFTER the
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> returns
    /// </para>
    /// </summary>
    /// <typeparam name="TUpdateHandler">
    /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
    /// </typeparam>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    [PublicAPI]
    public static void StartReceiving<TUpdateHandler>(
        this ITelegramBotClient botClient,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) where TUpdateHandler : IUpdateHandler, new() =>
        StartReceiving(
            botClient: botClient,
            updateHandler: new TUpdateHandler(),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  <paramref name="updateHandler"/>
    /// for each.
    /// <para>
    /// This method does not block. GetUpdates will be called AFTER the <paramref name="updateHandler"/> returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    [PublicAPI]
    public static void StartReceiving(
        this ITelegramBotClient botClient,
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        StartReceiving(
            botClient: botClient,
            updateHandler: new DefaultUpdateHandler(
                updateHandler: updateHandler,
                errorHandler: errorHandler
            ),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  <paramref name="updateHandler"/>
    /// for each.
    /// <para>
    /// This method does not block. GetUpdates will be called AFTER the <paramref name="updateHandler"/> returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    [PublicAPI]
    public static void StartReceiving(
        this ITelegramBotClient botClient,
        Action<ITelegramBotClient, Update, CancellationToken> updateHandler,
        Action<ITelegramBotClient, Exception, CancellationToken> errorHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        StartReceiving(
            botClient: botClient,
            updateHandler: new DefaultUpdateHandler(
                updateHandler: (bot, update, token) =>
                {
                    updateHandler.Invoke(bot, update, token);
                    return Task.CompletedTask;
                },
                errorHandler: (bot, exception, token) =>
                {
                    errorHandler.Invoke(bot, exception, token);
                    return Task.CompletedTask;
                }
            ),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method does not block. GetUpdates will be called AFTER the
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">
    /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
    /// </param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    [PublicAPI]
    public static void StartReceiving(
        this ITelegramBotClient botClient,
        IUpdateHandler updateHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (botClient is null) { throw new ArgumentNullException(nameof(botClient)); }
        if (updateHandler is null) { throw new ArgumentNullException(nameof(updateHandler)); }

        // ReSharper disable once MethodSupportsCancellation
        Task.Run(async () =>
        {
            try
            {
                await ReceiveAsync(
                    botClient: botClient,
                    updateHandler: updateHandler,
                    receiverOptions: receiverOptions,
                    cancellationToken: cancellationToken
                );
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                try
                {
                    await updateHandler.HandleErrorAsync(
                        botClient: botClient,
                        exception: ex,
                        cancellationToken: cancellationToken
                    );
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }
        });
    }

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method will block if awaited. GetUpdates will be called AFTER the
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> returns
    /// </para>
    /// </summary>
    /// <typeparam name="TUpdateHandler">
    /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
    /// </typeparam>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that will be completed when cancellation will be requested through
    /// <paramref name="cancellationToken"/>
    /// </returns>
    [PublicAPI]
    public static async Task ReceiveAsync<TUpdateHandler>(
        this ITelegramBotClient botClient,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) where TUpdateHandler : IUpdateHandler, new() =>
        await ReceiveAsync(
            botClient: botClient,
            updateHandler: new TUpdateHandler(),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method will block if awaited. GetUpdates will be called AFTER the <paramref name="updateHandler"/>
    /// returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that will be completed when cancellation will be requested through
    /// <paramref name="cancellationToken"/>
    /// </returns>
    [PublicAPI]
    public static async Task ReceiveAsync(
        this ITelegramBotClient botClient,
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        await ReceiveAsync(
            botClient: botClient,
            updateHandler: new DefaultUpdateHandler(
                updateHandler: updateHandler,
                errorHandler: errorHandler
            ),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method will block if awaited. GetUpdates will be called AFTER the <paramref name="updateHandler"/>
    /// returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">Delegate used for processing <see cref="Update"/>s</param>
    /// <param name="errorHandler">Delegate used for processing polling errors</param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that will be completed when cancellation will be requested through
    /// <paramref name="cancellationToken"/>
    /// </returns>
    [PublicAPI]
    public static async Task ReceiveAsync(
        this ITelegramBotClient botClient,
        Action<ITelegramBotClient, Update, CancellationToken> updateHandler,
        Action<ITelegramBotClient, Exception, CancellationToken> errorHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        await ReceiveAsync(
            botClient: botClient,
            updateHandler: new DefaultUpdateHandler(
                updateHandler: (bot, update, token) =>
                {
                    updateHandler.Invoke(bot, update, token);
                    return Task.CompletedTask;
                },
                errorHandler: (bot, exception, token) =>
                {
                    errorHandler.Invoke(bot, exception, token);
                    return Task.CompletedTask;
                }
            ),
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );

    /// <summary>
    /// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> for each.
    /// <para>
    /// This method will block if awaited. GetUpdates will be called AFTER the
    /// <see cref="IUpdateHandler.HandleUpdateAsync"/> returns
    /// </para>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="updateHandler">
    /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
    /// </param>
    /// <param name="receiverOptions">Options used to configure getUpdates requests</param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that will be completed when cancellation will be requested through
    /// <paramref name="cancellationToken"/>
    /// </returns>
    [PublicAPI]
    public static async Task ReceiveAsync(
        this ITelegramBotClient botClient,
        IUpdateHandler updateHandler,
        ReceiverOptions? receiverOptions = default,
        CancellationToken cancellationToken = default
    ) =>
        await new DefaultUpdateReceiver(botClient: botClient, receiverOptions: receiverOptions)
            .ReceiveAsync(updateHandler: updateHandler, cancellationToken: cancellationToken);
}
