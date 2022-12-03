namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of a <see cref="MessageEntity"/>
/// </summary>
[JsonConverter(typeof(MessageEntityTypeConverter))]
public enum MessageEntityType
{
    /// <summary>
    /// A mentioned <see cref="User"/>
    /// </summary>
    Mention = 1,

    /// <summary>
    /// A searchable Hashtag
    /// </summary>
    Hashtag,

    /// <summary>
    /// A Bot command
    /// </summary>
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
    TextMention,

    /// <summary>
    /// Phone number
    /// </summary>
    PhoneNumber,

    /// <summary>
    /// A cashtag (e.g. $EUR, $USD) - $ followed by the short currency code
    /// </summary>
    Cashtag,

    /// <summary>
    /// Underlined text
    /// </summary>
    Underline,

    /// <summary>
    /// Strikethrough text
    /// </summary>
    Strikethrough,

    /// <summary>
    /// Spoiler message
    /// </summary>
    Spoiler,

    /// <summary>
    /// Inline custom emoji stickers
    /// </summary>
    CustomEmoji,
}
