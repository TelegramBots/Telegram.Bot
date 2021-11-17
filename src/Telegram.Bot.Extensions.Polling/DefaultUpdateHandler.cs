using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling;

/// <summary>
/// A very simple <see cref="IUpdateHandler"/> implementation
/// </summary>
[PublicAPI]
public class DefaultUpdateHandler : IUpdateHandler
{
    readonly Func<ITelegramBotClient, Update, CancellationToken, Task> _updateHandler;
    readonly Func<ITelegramBotClient, Exception, CancellationToken, Task> _errorHandler;

    /// <summary>
    /// Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions
    /// </summary>
    /// <param name="updateHandler">The function to invoke when an update is received</param>
    /// <param name="errorHandler">The function to invoke when an error occurs</param>
    public DefaultUpdateHandler(
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler)
    {
        _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
        _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    /// <inheritdoc />
    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken
    ) =>
        await _updateHandler(botClient, update, cancellationToken);

    /// <inheritdoc />
    public async Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken
    ) =>
        await _errorHandler(botClient, exception, cancellationToken);
}
