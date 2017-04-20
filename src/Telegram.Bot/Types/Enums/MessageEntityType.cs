using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of a <see cref="MessageEntity"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MessageEntityType
    {
        /// <summary>
        /// A mentioned <see cref="User"/>
        /// </summary>
        [EnumMember(Value = "mention")]
        Mention,

        /// <summary>
        /// A searchable Hashtag
        /// </summary>
        [EnumMember(Value = "hashtag")]
        Hashtag,

        /// <summary>
        /// A Bot command
        /// </summary>
        [EnumMember(Value = "bot_command")]
        BotCommand,

        /// <summary>
        /// An url
        /// </summary>
        [EnumMember(Value = "url")]
        Url,

        /// <summary>
        /// An email
        /// </summary>
        [EnumMember(Value = "email")]
        Email,

        /// <summary>
        /// Bold text
        /// </summary>
        [EnumMember(Value = "bold")]
        Bold,

        /// <summary>
        /// Italic text
        /// </summary>
        [EnumMember(Value = "italic")]
        Italic,

        /// <summary>
        /// Monowidth string
        /// </summary>
        [EnumMember(Value = "code")]
        Code,

        /// <summary>
        /// Monowidth block
        /// </summary>
        [EnumMember(Value = "pre")]
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
