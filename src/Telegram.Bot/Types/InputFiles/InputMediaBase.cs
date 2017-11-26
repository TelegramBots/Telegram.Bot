// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents the content of a media message to be sent
    /// </summary>
    public abstract class InputMediaBase
    {
        /// <summary>
        /// Type of the media
        /// </summary>
        public string Type { get; protected set; }

        /// <summary>
        /// Media to send
        /// </summary>
        public InputMediaType Media { get; set; }

        /// <summary>
        /// Optional. Caption of the photo to be sent, 0-200 characters
        /// </summary>
        public string Caption { get; set; }
    }
}
