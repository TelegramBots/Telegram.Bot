using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of a <see cref="MessageEntity"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MessageEntityType
    {
        /// <summary>
        /// A mentioned <see cref="User"/>
        /// </summary>
        Mention,

        /// <summary>
        /// A searchable Hashtag
        /// </summary>
        Hashtag,

        /// <summary>
        /// A Bot command
        /// </summary>
        [EnumMember(Value = "bot_command")]
        BotCommand,

        /// <summary>
        /// An url
        /// </summary>
        Url,

        /// <summary>
        /// An email
        /// </summary>
        Email,

        /// <summary>
        /// Bold text
        /// </summary>
        Bold,

        /// <summary>
        /// Italic text
        /// </summary>
        Italic,

        /// <summary>
        /// Monowidth string
        /// </summary>
        Code,

        /// <summary>
        /// Monowidth block
        /// </summary>
        Pre,

        /// <summary>
        /// Clickable text urls
        /// </summary>
        [EnumMember(Value = "text_link")]
        TextLink,

        /// <summary>
        /// Mentions for a <see cref="User"/> without <see cref="User.Username"/>
        /// </summary>
        [EnumMember(Value = "text_mention")]
        TextMention,
    }
}
