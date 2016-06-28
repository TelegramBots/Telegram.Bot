using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing a <see cref="Types.ChosenInlineResult"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ChosenInlineResultEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the chosen inline result.
        /// </summary>
        /// <value>
        /// The chosen inline result.
        /// </value>
        public ChosenInlineResult ChosenInlineResult { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChosenInlineResultEventArgs"/> class.
        /// </summary>
        /// <param name="update">The update.</param>
        internal ChosenInlineResultEventArgs(Update update)
        {
            ChosenInlineResult = update.ChosenInlineResult;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChosenInlineResultEventArgs"/> class.
        /// </summary>
        /// <param name="chosenInlineResult">The chosen inline result.</param>
        internal ChosenInlineResultEventArgs(ChosenInlineResult chosenInlineResult)
        {
            ChosenInlineResult = chosenInlineResult;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UpdateEventArgs"/> to <see cref="ChosenInlineResultEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ChosenInlineResultEventArgs(UpdateEventArgs e) => new ChosenInlineResultEventArgs(e.Update);
    }
}
