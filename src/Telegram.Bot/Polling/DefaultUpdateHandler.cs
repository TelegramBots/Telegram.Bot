using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Telegram.Bot.Polling;

/// <summary>A very simple <see cref="IUpdateHandler"/> implementation</summary>
/// <remarks>Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions</remarks>
/// <param name="updateHandler">The function to invoke when an update is received</param>
/// <param name="errorHandler">The function to invoke when an error occurs</param>
[PublicAPI]
public class DefaultUpdateHandler(
    Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
    Func<ITelegramBotClient, Exception, HandleErrorSource, CancellationToken, Task> errorHandler) : IUpdateHandler
{
    /// <summary>Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions</summary>
    /// <param name="updateHandler">The function to invoke when an update is received</param>
    /// <param name="errorHandler">The function to invoke when an error occurs</param>
    public DefaultUpdateHandler(
        Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
        Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler)
        : this(updateHandler, (bot, ex, s, ct) => errorHandler(bot, ex, ct))
    { }

    /// <inheritdoc/>
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        => await updateHandler(botClient, update, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        => await errorHandler(botClient, exception, source, cancellationToken).ConfigureAwait(false);
}
