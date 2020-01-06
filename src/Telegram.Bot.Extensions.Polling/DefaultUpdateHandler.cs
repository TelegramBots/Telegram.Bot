using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// A very simple <see cref="IUpdateHandler"/> implementation
    /// </summary>
    public class DefaultUpdateHandler : IUpdateHandler
    {
        /// <summary>
        /// Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates
        /// </summary>
        public UpdateType[]? AllowedUpdates { get; set; }

        private readonly Func<ITelegramBotClient, Update, CancellationToken, Task> _updateHandler;

        private readonly Func<ITelegramBotClient, Exception, CancellationToken, Task> _errorHandler;

        /// <summary>
        /// Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions
        /// </summary>
        /// <param name="updateHandler">The function to invoke when an update is received</param>
        /// <param name="errorHandler">The function to invoke when an error occurs</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        public DefaultUpdateHandler(
            Func<ITelegramBotClient, Update, CancellationToken, Task> updateHandler,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> errorHandler,
            UpdateType[]? allowedUpdates = default)
        {
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            AllowedUpdates = allowedUpdates;
        }

        /// <inheritdoc />
        public Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            return _updateHandler(botClient, update, cancellationToken);
        }

        /// <inheritdoc />
        public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return _errorHandler(botClient, exception, cancellationToken);
        }
    }
}
