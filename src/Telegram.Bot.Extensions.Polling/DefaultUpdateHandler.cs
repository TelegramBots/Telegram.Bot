using System;
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

        private readonly Func<Update, Task> _updateHandler;

        private readonly Func<Exception, Task> _errorHandler;

        /// <summary>
        /// Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions
        /// </summary>
        /// <param name="updateHandler">The function to invoke when an update is received</param>
        /// <param name="errorHandler">The function to invoke when an error occurs</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        public DefaultUpdateHandler(
            Func<Update, Task> updateHandler,
            Func<Exception, Task> errorHandler,
            UpdateType[]? allowedUpdates = default)
        {
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            AllowedUpdates = allowedUpdates;
        }

        /// <summary>
        /// Handles an <see cref="Update"/>
        /// </summary>
        /// <param name="update">The <see cref="Update"/> to handle</param>
        /// <returns></returns>
        public Task HandleUpdate(Update update)
        {
            return _updateHandler(update);
        }

        /// <summary>
        /// Handles an <see cref="Exception"/>
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to handle</param>
        /// <returns></returns>
        public Task HandleError(Exception exception)
        {
            return _errorHandler(exception);
        }
    }
}
