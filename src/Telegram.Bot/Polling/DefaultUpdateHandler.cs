using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Extensions;

namespace Telegram.Bot.Polling;

/// <summary>
/// A very simple <see cref="IUpdateHandler"/> implementation
/// </summary>
[PublicAPI]
public class DefaultUpdateHandler : IUpdateHandler
{
    readonly Func<ITelegramBotClient, Update, CancellationToken, Task> _updateHandler;
    readonly Func<ITelegramBotClient, ErrorContext, CancellationToken, Task> _errorHandler;

    /// <summary>
    /// Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions
    /// </summary>
    /// <param name="updateHandler">The function to invoke when an update is received</param>
    /// <param name="errorHandler">The function to invoke when an error occurs</param>
    public DefaultUpdateHandler(
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, ErrorContext, CancellationToken, Task> errorHandler)
    {
        _updateHandler = updateHandler.ThrowIfNull();
        _errorHandler = errorHandler.ThrowIfNull();
    }

    /// <inheritdoc />
    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken
    ) =>
        await _updateHandler(botClient, update, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task HandleErrorAsync(
        ITelegramBotClient botClient,
        ErrorContext context,
        CancellationToken cancellationToken
    ) =>
        await _errorHandler(botClient, context, cancellationToken).ConfigureAwait(false);
}
