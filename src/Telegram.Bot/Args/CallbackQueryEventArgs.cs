using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing a <see cref="Types.CallbackQuery"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    [Obsolete("This class will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
    public class CallbackQueryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the callback query.
        /// </summary>
        /// <value>
        /// The callback query.
        /// </value>
        public CallbackQuery CallbackQuery { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackQueryEventArgs"/> class.
        /// </summary>
        /// <param name="update">The <see cref="Update"/>.</param>
        internal CallbackQueryEventArgs(Update update)
        {
            CallbackQuery = update.CallbackQuery;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackQueryEventArgs"/> class.
        /// </summary>
        /// <param name="callbackQuery">The callback query.</param>
        internal CallbackQueryEventArgs(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UpdateEventArgs"/> to <see cref="CallbackQueryEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CallbackQueryEventArgs(UpdateEventArgs e) => new CallbackQueryEventArgs(e.Update);
    }
}
