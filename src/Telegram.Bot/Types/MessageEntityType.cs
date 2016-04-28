using System.Collections.Generic;

namespace Telegram.Bot.Types
{
    public enum MessageEntityType
    {
        Mention,
        Hashtag,
        BotCommand,
        Url,
        Email,
        Bold,
        Italic,
        Code,
        Pre,
        TextLink,
    }

    internal static class MessageEntityTypeExtensions
    {
        internal static readonly Dictionary<MessageEntityType, string> StringMap =
            new Dictionary<MessageEntityType, string>
            {
                {MessageEntityType.Bold, "bold" },
                {MessageEntityType.BotCommand, "bot_command" },
                {MessageEntityType.Code, "code" },
                {MessageEntityType.Email, "email" },
                {MessageEntityType.Hashtag, "hashtag" },
                {MessageEntityType.Italic, "italic" },
                {MessageEntityType.Mention, "mention" },
                {MessageEntityType.Pre, "pre" },
                {MessageEntityType.TextLink, "text_link" },
                {MessageEntityType.Url, "url" },
            };

        public static string ToMessageEntityString(this MessageEntityType type) => StringMap[type];
    }

}
