using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one special entity in a text message. For example, hashtags, usernames, URLs, etc.
    /// </summary>
    public class MessageEntity
    {
        /// <summary>
        /// Type of the entity
        /// </summary>
        public MessageEntityType Type { get; set; }

        /// <summary>
        /// Offset in UTF-16 code units to the start of the entity
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Length of the entity in UTF-16 code units
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Optional. For "text_link" only, url that will be opened after user taps on the text
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Optional. For "text_mention" only, the mentioned user (for users without usernames)
        /// </summary>
        public User User { get; set; }
    }
}
