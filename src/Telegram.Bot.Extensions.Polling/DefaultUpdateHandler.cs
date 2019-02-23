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

        private readonly Func<Exception, Task> _exceptionHandler;

        public DefaultUpdateHandler(
            Func<Update, Task> updateHandler,
            Func<Exception, Task> exceptionHandler,
            UpdateType[]? allowedUpdates = default)
        {
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
            AllowedUpdates = allowedUpdates;
        }

        public Task UpdateReceived(Update update)
        {
            return _updateHandler(update);
        }

        public Task ErrorOccurred(Exception exception)
        {
            return _exceptionHandler(exception);
        }
    }
}
