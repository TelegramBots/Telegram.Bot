using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public class DefaultUpdateHandler : IUpdateHandler
    {
        public UpdateType[]? AllowedUpdates { get; set; }

        private readonly Func<Update, Task> _updateHandler;

        private readonly Func<Exception, Task> _errorHandler;

        public DefaultUpdateHandler(
            Func<Update, Task> updateHandler,
            Func<Exception, Task> errorHandler,
            UpdateType[]? allowedUpdates = default)
        {
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            AllowedUpdates = allowedUpdates;
        }

        public Task HandleUpdate(Update update)
        {
            return _updateHandler(update);
        }

        public Task HandleError(Exception exception)
        {
            return _errorHandler(exception);
        }
    }
}
