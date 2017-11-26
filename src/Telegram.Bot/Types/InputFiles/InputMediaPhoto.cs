// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a photo to be sent
    /// </summary>
    public class InputMediaPhoto : InputMediaBase
    {
        /// <summary>
        /// Initializes a new photo media to send
        /// </summary>
        public InputMediaPhoto()
        {
            Type = "photo";
        }
    }
}
