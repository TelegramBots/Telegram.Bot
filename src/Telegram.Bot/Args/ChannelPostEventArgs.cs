using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing a <see cref="Types.Message"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ChannelPostEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public ChannelPost ChannelPost { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="update">The update.</param>
        internal ChannelPostEventArgs(Update update)
        {
            ChannelPost = update.ChannelPost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="post">The message.</param>
        internal ChannelPostEventArgs(ChannelPost post)
        {
            ChannelPost = post;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UpdateEventArgs"/> to <see cref="MessageEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ChannelPostEventArgs(UpdateEventArgs e) => new ChannelPostEventArgs(e.Update);
    }
}
