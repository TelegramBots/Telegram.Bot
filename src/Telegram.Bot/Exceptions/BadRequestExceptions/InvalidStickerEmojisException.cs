// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the sticker set is invalid
    /// </summary>
    public class InvalidStickerEmojisException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="InvalidStickerEmojisException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public InvalidStickerEmojisException(string message)
            : base(message)
        {
        }
    }
}