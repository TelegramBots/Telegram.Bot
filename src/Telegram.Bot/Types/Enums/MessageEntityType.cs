// This file is NOT auto-generated - check https://core.telegram.org/bots/api#messageentity
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of a <see cref="MessageEntity"/></summary>
[JsonConverter(typeof(EnumConverter<MessageEntityType>))]
public enum MessageEntityType
{
    /// <summary>Mention of a <see cref="User"/> via @username</summary>
    Mention = 1,
    /// <summary>#hashtag</summary>
    Hashtag,
    /// <summary>Bot /command</summary>
    BotCommand,
    /// <summary>Plain URL</summary>
    Url,
    /// <summary>Email address</summary>
    Email,
    /// <summary><b>Bold</b> text</summary>
    Bold,
    /// <summary><i>Italic</i> text</summary>
    Italic,
    /// <summary><c>Monowidth</c> text</summary>
    Code,
    /// <summary><c>Monowidth block</c> (see <see cref="MessageEntity.Language"/>)</summary>
    Pre,
    /// <summary>Clickable text link (see <see cref="MessageEntity.Url"/>)</summary>
    TextLink,
    /// <summary>Link to a <see cref="User"/> without using username (see <see cref="MessageEntity.User"/>)</summary>
    /// <remarks>Can be generated from Markdown/Html via text links with URL tg://user?id=USER_ID</remarks>
    TextMention,
    /// <summary>Phone number (like +1-212-555-0123)</summary>
    PhoneNumber,
    /// <summary>$cashtag (like $EUR or $USD) - $ followed by the short currency code</summary>
    Cashtag,
    /// <summary><u>Underlined</u> text</summary>
    Underline,
    /// <summary>Strikethrough text</summary>
    Strikethrough,
    /// <summary>Spoiler text (initially hidden behind blur)</summary>
    Spoiler,
    /// <summary>Inline custom emoji (see <see cref="MessageEntity.CustomEmojiId"/>)</summary>
    CustomEmoji,
    /// <summary>Block quotation</summary>
    Blockquote,
    /// <summary>Block quotation that is initially collapsed and can be expanded</summary>
    ExpandableBlockquote,
    /// <summary>Formatted date and time</summary>
    DateTime,
    /// <summary>Bank card number</summary>
    BankCardNumber,
    /// <summary>Address in the TON blockchain</summary>
    TonAddress,
}
