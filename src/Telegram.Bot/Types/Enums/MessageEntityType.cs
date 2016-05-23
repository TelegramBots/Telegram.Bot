using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    public enum MessageEntityType
    {
        [EnumMember(Value = "mention")]
        Mention,

        [EnumMember(Value = "hashtag")]
        Hashtag,

        [EnumMember(Value = "bot_command")]
        BotCommand,

        [EnumMember(Value = "url")]
        Url,

        [EnumMember(Value = "email")]
        Email,

        [EnumMember(Value = "bold")]
        Bold,

        [EnumMember(Value = "italic")]
        Italic,

        [EnumMember(Value = "code")]
        Code,

        [EnumMember(Value = "pre")]
        Pre,

        [EnumMember(Value = "text_link")]
        TextLink,

        [EnumMember(Value = "text_mention")]
        TextMention,
    }
}
