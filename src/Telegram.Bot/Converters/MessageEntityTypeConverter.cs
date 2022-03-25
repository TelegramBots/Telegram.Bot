using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MessageEntityTypeConverter : EnumConverter<MessageEntityType>
{
    static readonly IReadOnlyDictionary<string, MessageEntityType> StringToEnum =
        new Dictionary<string, MessageEntityType>
        {
            { "mention", MessageEntityType.Mention },
            { "hashtag", MessageEntityType.Hashtag },
            { "bot_command", MessageEntityType.BotCommand },
            { "url", MessageEntityType.Url },
            { "email", MessageEntityType.Email },
            { "bold", MessageEntityType.Bold },
            { "italic", MessageEntityType.Italic },
            { "code", MessageEntityType.Code },
            { "pre", MessageEntityType.Pre },
            { "text_link", MessageEntityType.TextLink },
            { "text_mention", MessageEntityType.TextMention },
            { "phone_number", MessageEntityType.PhoneNumber },
            { "cashtag", MessageEntityType.Cashtag },
            { "underline", MessageEntityType.Underline },
            { "strikethrough", MessageEntityType.Strikethrough },
            { "spoiler", MessageEntityType.Spoiler },
        };

    static readonly IReadOnlyDictionary<MessageEntityType, string> EnumToString =
        new Dictionary<MessageEntityType, string>
        {
            { 0, "unknown" },
            { MessageEntityType.Mention, "mention" },
            { MessageEntityType.Hashtag, "hashtag" },
            { MessageEntityType.BotCommand, "bot_command" },
            { MessageEntityType.Url, "url" },
            { MessageEntityType.Email, "email" },
            { MessageEntityType.Bold, "bold" },
            { MessageEntityType.Italic, "italic" },
            { MessageEntityType.Code, "code" },
            { MessageEntityType.Pre, "pre" },
            { MessageEntityType.TextLink, "text_link" },
            { MessageEntityType.TextMention, "text_mention" },
            { MessageEntityType.PhoneNumber, "phone_number" },
            { MessageEntityType.Cashtag, "cashtag" },
            { MessageEntityType.Underline, "underline" },
            { MessageEntityType.Strikethrough, "strikethrough" },
            { MessageEntityType.Spoiler, "spoiler" },
        };

    protected override MessageEntityType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(MessageEntityType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}