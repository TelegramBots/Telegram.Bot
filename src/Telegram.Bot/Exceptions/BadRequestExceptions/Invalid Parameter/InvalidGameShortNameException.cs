// ReSharper disable once CheckNamespace

using System;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the specified game short name is empty, invalid, or wrong(non-existent).
    /// </summary>
    [Obsolete("Custom exceptions will be removed in the next major update")]
    public class InvalidGameShortNameException : InvalidParameterException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="InvalidGameShortNameException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public InvalidGameShortNameException(string message)
            : base("game_short_name", message)
        { }
    }
}
