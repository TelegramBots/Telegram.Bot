using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing a <see cref="Types.Message"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public Message Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="update">The update.</param>
        internal MessageEventArgs(Update update)
        {
            Message = (update.Type == UpdateType.EditedMessage) ? update.EditedMessage : update.Message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal MessageEventArgs(Message message)
        {
            Message = message;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UpdateEventArgs"/> to <see cref="MessageEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator MessageEventArgs(UpdateEventArgs e) => new MessageEventArgs(e.Update);
    }
}
