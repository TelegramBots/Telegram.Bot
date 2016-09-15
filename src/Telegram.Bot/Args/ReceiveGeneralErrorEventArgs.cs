using System;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing a general <see cref="Exception"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ReceiveGeneralErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveGeneralErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The general exception.</param>
        internal ReceiveGeneralErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Exception"/> to <see cref="ReceiveGeneralErrorEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/></param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ReceiveGeneralErrorEventArgs(Exception e) => new ReceiveGeneralErrorEventArgs(e);
    }
}
