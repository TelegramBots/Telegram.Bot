using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of a <see cref="MessageEntity"/>
    /// </summary>
    // This enum is not touched during serialization from
    // CLR types and is needed only for convenience so
    // switch statement could be used for checking messages
    // entity type.
    // Non-default json converter is here just in case if
    // someone wants to serialize this enum because entity
    // types from the Bot API are in snake case.
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum EntityType
    {
        /// <summary>
        /// Unknown entity type
        /// </summary>
        Unknown,

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
        TextLink,

        /// <summary>
        /// Mentions for a <see cref="User"/> without <see cref="User.Username"/>
        /// </summary>
        [EnumMember(Value = "text_mention")]
        TextMention,

        /// <summary>
        /// Phone number
        /// </summary>
        [EnumMember(Value = "phone_number")]
        PhoneNumber,
    }
}
