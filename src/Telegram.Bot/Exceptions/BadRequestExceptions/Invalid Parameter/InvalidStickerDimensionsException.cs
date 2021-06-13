// ReSharper disable once CheckNamespace

using System;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the sticker set is invalid
    /// </summary>
    [Obsolete("Custom exceptions will be removed in the next major update")]
    public class InvalidStickerDimensionsException : InvalidParameterException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="InvalidStickerDimensionsException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public InvalidStickerDimensionsException(string message)
            : base("png_sticker", message)
        {
        }
    }
}
