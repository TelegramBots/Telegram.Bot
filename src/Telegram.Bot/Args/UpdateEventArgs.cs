using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing an <see cref="Types.Update"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    [Obsolete("This class will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
    public class UpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the update.
        /// </summary>
        /// <value>
        /// The update.
        /// </value>
        public Update Update { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEventArgs"/> class.
        /// </summary>
        /// <param name="update">The update.</param>
        public UpdateEventArgs(Update update)
        {
            Update = update;
        }
    }
}
