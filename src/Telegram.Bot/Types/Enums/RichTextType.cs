// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the rich text</summary>
[JsonConverter(typeof(EnumConverter<RichTextType>))]
public enum RichTextType
{
    /// <summary>An array of <see cref="RichText"/></summary>
    Array = -1,
    /// <summary>Plain text</summary>
    Text,
    /// <summary>A bold text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextBold"/>)</i></summary>
    Bold,
    /// <summary>An italicized text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextItalic"/>)</i></summary>
    Italic,
    /// <summary>An underlined text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextUnderline"/>)</i></summary>
    Underline,
    /// <summary>A strikethrough text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextStrikethrough"/>)</i></summary>
    Strikethrough,
    /// <summary>A text covered by a spoiler.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextSpoiler"/>)</i></summary>
    Spoiler,
    /// <summary>Formatted date and time.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextDateTime"/>)</i></summary>
    DateTime,
    /// <summary>A mention of a Telegram user by their identifier.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextTextMention"/>)</i></summary>
    TextMention,
    /// <summary>A subscript text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextSubscript"/>)</i></summary>
    Subscript,
    /// <summary>A superscript text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextSuperscript"/>)</i></summary>
    Superscript,
    /// <summary>A marked text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextMarked"/>)</i></summary>
    Marked,
    /// <summary>A monowidth text.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextCode"/>)</i></summary>
    Code,
    /// <summary>A custom emoji.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextCustomEmoji"/>)</i></summary>
    CustomEmoji,
    /// <summary>A mathematical expression.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextMathematicalExpression"/>)</i></summary>
    MathematicalExpression,
    /// <summary>A text with a link.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextUrl"/>)</i></summary>
    Url,
    /// <summary>A text with an email address.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextEmailAddress"/>)</i></summary>
    EmailAddress,
    /// <summary>A text with a phone number.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextPhoneNumber"/>)</i></summary>
    PhoneNumber,
    /// <summary>A text with a bank card number.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextBankCardNumber"/>)</i></summary>
    BankCardNumber,
    /// <summary>A mention by a username.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextMention"/>)</i></summary>
    Mention,
    /// <summary>A hashtag.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextHashtag"/>)</i></summary>
    Hashtag,
    /// <summary>A cashtag.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextCashtag"/>)</i></summary>
    Cashtag,
    /// <summary>A bot command.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextBotCommand"/>)</i></summary>
    BotCommand,
    /// <summary>An anchor.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextAnchor"/>)</i></summary>
    Anchor,
    /// <summary>A link to an anchor.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextAnchorLink"/>)</i></summary>
    AnchorLink,
    /// <summary>A reference.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextReference"/>)</i></summary>
    Reference,
    /// <summary>A link to a reference.<br/><br/><i>(<see cref="RichText"/> can be cast into <see cref="RichTextReferenceLink"/>)</i></summary>
    ReferenceLink,
}
