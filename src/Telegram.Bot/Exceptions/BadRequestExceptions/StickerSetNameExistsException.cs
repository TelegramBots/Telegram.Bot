// ReSharper disable once CheckNamespace

using System;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the sticker set is invalid
    /// </summary>
    [Obsolete("Custom exceptions will be removed in the next major update")]
    public class StickerSetNameExistsException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="StickerSetNameExistsException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public StickerSetNameExistsException(string message)
            : base(message)
        {
        }
    }
}
