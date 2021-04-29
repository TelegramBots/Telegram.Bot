using System;
using Telegram.Bot.Exceptions;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// <see cref="EventArgs"/> containing an <see cref="Exceptions.ApiRequestException"/>
    /// </summary>
    /// <seealso cref="EventArgs" />
    [Obsolete("This class will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
    public class ReceiveErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the API request exception.
        /// </summary>
        /// <value>
        /// The API request exception.
        /// </value>
        public ApiRequestException ApiRequestException { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveErrorEventArgs"/> class.
        /// </summary>
        /// <param name="apiRequestException">The API request exception.</param>
        internal ReceiveErrorEventArgs(ApiRequestException apiRequestException)
        {
            ApiRequestException = apiRequestException;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Exceptions.ApiRequestException"/> to <see cref="ReceiveErrorEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="Exceptions.ApiRequestException"/></param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ReceiveErrorEventArgs(ApiRequestException e) => new ReceiveErrorEventArgs(e);
    }
}
