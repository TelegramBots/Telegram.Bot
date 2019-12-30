

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a general file to be sent
    /// </summary>
    public class InputMediaDocument : InputMediaBase, IInputMediaThumb
    {
        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <summary>
        /// Initializes a new document media to send with an <see cref="InputMedia"/>
        /// </summary>
        public InputMediaDocument(InputMedia media)
        {
            Type = "document";
            Media = media;
        }
    }
}
