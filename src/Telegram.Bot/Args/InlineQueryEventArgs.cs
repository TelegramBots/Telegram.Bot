using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing an <see cref="Types.InlineQuery"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class InlineQueryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the inline query.
        /// </summary>
        /// <value>
        /// The inline query.
        /// </value>
        public InlineQuery InlineQuery { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineQueryEventArgs"/> class.
        /// </summary>
        /// <param name="update">The update.</param>
        internal InlineQueryEventArgs(Update update)
        {
            InlineQuery = update.InlineQuery;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineQueryEventArgs"/> class.
        /// </summary>
        /// <param name="inlineQuery">The inline query.</param>
        internal InlineQueryEventArgs(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UpdateEventArgs"/> to <see cref="InlineQueryEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineQueryEventArgs(UpdateEventArgs e) => new InlineQueryEventArgs(e.Update);
    }
}
