// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a rich formatted text. Currently, it can be either a String for plain text, an Array of <see cref="RichText"/>, or any of the following types:<br/><see cref="RichTextBold"/>, <see cref="RichTextItalic"/>, <see cref="RichTextUnderline"/>, <see cref="RichTextStrikethrough"/>, <see cref="RichTextSpoiler"/>, <see cref="RichTextDateTime"/>, <see cref="RichTextTextMention"/>, <see cref="RichTextSubscript"/>, <see cref="RichTextSuperscript"/>, <see cref="RichTextMarked"/>, <see cref="RichTextCode"/>, <see cref="RichTextCustomEmoji"/>, <see cref="RichTextMathematicalExpression"/>, <see cref="RichTextUrl"/>, <see cref="RichTextEmailAddress"/>, <see cref="RichTextPhoneNumber"/>, <see cref="RichTextBankCardNumber"/>, <see cref="RichTextMention"/>, <see cref="RichTextHashtag"/>, <see cref="RichTextCashtag"/>, <see cref="RichTextBotCommand"/>, <see cref="RichTextAnchor"/>, <see cref="RichTextAnchorLink"/>, <see cref="RichTextReference"/>, <see cref="RichTextReferenceLink"/></summary>
[JsonConverter(typeof(RichTextConverter))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(RichTextArray))]
[CustomJsonDerivedType(typeof(RichTextText))]
[CustomJsonDerivedType(typeof(RichTextBold), "bold")]
[CustomJsonDerivedType(typeof(RichTextItalic), "italic")]
[CustomJsonDerivedType(typeof(RichTextUnderline), "underline")]
[CustomJsonDerivedType(typeof(RichTextStrikethrough), "strikethrough")]
[CustomJsonDerivedType(typeof(RichTextSpoiler), "spoiler")]
[CustomJsonDerivedType(typeof(RichTextDateTime), "date_time")]
[CustomJsonDerivedType(typeof(RichTextTextMention), "text_mention")]
[CustomJsonDerivedType(typeof(RichTextSubscript), "subscript")]
[CustomJsonDerivedType(typeof(RichTextSuperscript), "superscript")]
[CustomJsonDerivedType(typeof(RichTextMarked), "marked")]
[CustomJsonDerivedType(typeof(RichTextCode), "code")]
[CustomJsonDerivedType(typeof(RichTextCustomEmoji), "custom_emoji")]
[CustomJsonDerivedType(typeof(RichTextMathematicalExpression), "mathematical_expression")]
[CustomJsonDerivedType(typeof(RichTextUrl), "url")]
[CustomJsonDerivedType(typeof(RichTextEmailAddress), "email_address")]
[CustomJsonDerivedType(typeof(RichTextPhoneNumber), "phone_number")]
[CustomJsonDerivedType(typeof(RichTextBankCardNumber), "bank_card_number")]
[CustomJsonDerivedType(typeof(RichTextMention), "mention")]
[CustomJsonDerivedType(typeof(RichTextHashtag), "hashtag")]
[CustomJsonDerivedType(typeof(RichTextCashtag), "cashtag")]
[CustomJsonDerivedType(typeof(RichTextBotCommand), "bot_command")]
[CustomJsonDerivedType(typeof(RichTextAnchor), "anchor")]
[CustomJsonDerivedType(typeof(RichTextAnchorLink), "anchor_link")]
[CustomJsonDerivedType(typeof(RichTextReference), "reference")]
[CustomJsonDerivedType(typeof(RichTextReferenceLink), "reference_link")]
public abstract partial class RichText
{
    /// <summary>Type of the rich text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract RichTextType Type { get; }
}

/// <summary>A bold text.</summary>
public partial class RichTextBold : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Bold"/></summary>
    public override RichTextType Type => RichTextType.Bold;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>An italicized text.</summary>
public partial class RichTextItalic : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Italic"/></summary>
    public override RichTextType Type => RichTextType.Italic;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>An underlined text.</summary>
public partial class RichTextUnderline : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Underline"/></summary>
    public override RichTextType Type => RichTextType.Underline;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A strikethrough text.</summary>
public partial class RichTextStrikethrough : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Strikethrough"/></summary>
    public override RichTextType Type => RichTextType.Strikethrough;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A text covered by a spoiler.</summary>
public partial class RichTextSpoiler : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Spoiler"/></summary>
    public override RichTextType Type => RichTextType.Spoiler;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>Formatted date and time.</summary>
public partial class RichTextDateTime : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.DateTime"/></summary>
    public override RichTextType Type => RichTextType.DateTime;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The DateTime associated with the entity</summary>
    [JsonPropertyName("unix_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime UnixTime { get; set; }

    /// <summary>The string that defines the formatting of the date and time. See <a href="https://core.telegram.org/bots/api#date-time-entity-formatting">date-time entity formatting</a> for more details.</summary>
    [JsonPropertyName("date_time_format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string DateTimeFormat { get; set; } = default!;
}

/// <summary>A mention of a Telegram user by their identifier.</summary>
public partial class RichTextTextMention : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.TextMention"/></summary>
    public override RichTextType Type => RichTextType.TextMention;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The mentioned user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>A subscript text.</summary>
public partial class RichTextSubscript : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Subscript"/></summary>
    public override RichTextType Type => RichTextType.Subscript;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A superscript text.</summary>
public partial class RichTextSuperscript : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Superscript"/></summary>
    public override RichTextType Type => RichTextType.Superscript;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A marked text.</summary>
public partial class RichTextMarked : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Marked"/></summary>
    public override RichTextType Type => RichTextType.Marked;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A monowidth text.</summary>
public partial class RichTextCode : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Code"/></summary>
    public override RichTextType Type => RichTextType.Code;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A custom emoji.</summary>
public partial class RichTextCustomEmoji : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.CustomEmoji"/></summary>
    public override RichTextType Type => RichTextType.CustomEmoji;

    /// <summary>Unique identifier of the custom emoji. Use <see cref="TelegramBotClientExtensions.GetCustomEmojiStickers">GetCustomEmojiStickers</see> to get full information about the sticker.</summary>
    [JsonPropertyName("custom_emoji_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CustomEmojiId { get; set; } = default!;

    /// <summary>Alternative emoji for the custom emoji</summary>
    [JsonPropertyName("alternative_text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string AlternativeText { get; set; } = default!;
}

/// <summary>A mathematical expression.</summary>
public partial class RichTextMathematicalExpression : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.MathematicalExpression"/></summary>
    public override RichTextType Type => RichTextType.MathematicalExpression;

    /// <summary>The expression in LaTeX format</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Expression { get; set; } = default!;
}

/// <summary>A text with a link.</summary>
public partial class RichTextUrl : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Url"/></summary>
    public override RichTextType Type => RichTextType.Url;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>URL of the link</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;
}

/// <summary>A text with an email address.</summary>
public partial class RichTextEmailAddress : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.EmailAddress"/></summary>
    public override RichTextType Type => RichTextType.EmailAddress;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The email address</summary>
    [JsonPropertyName("email_address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string EmailAddress { get; set; } = default!;
}

/// <summary>A text with a phone number.</summary>
public partial class RichTextPhoneNumber : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.PhoneNumber"/></summary>
    public override RichTextType Type => RichTextType.PhoneNumber;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The phone number</summary>
    [JsonPropertyName("phone_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PhoneNumber { get; set; } = default!;
}

/// <summary>A text with a bank card number.</summary>
public partial class RichTextBankCardNumber : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.BankCardNumber"/></summary>
    public override RichTextType Type => RichTextType.BankCardNumber;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The bank card number</summary>
    [JsonPropertyName("bank_card_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BankCardNumber { get; set; } = default!;
}

/// <summary>A mention by a username.</summary>
public partial class RichTextMention : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Mention"/></summary>
    public override RichTextType Type => RichTextType.Mention;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The username</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Username { get; set; } = default!;
}

/// <summary>A hashtag.</summary>
public partial class RichTextHashtag : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Hashtag"/></summary>
    public override RichTextType Type => RichTextType.Hashtag;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The hashtag</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Hashtag { get; set; } = default!;
}

/// <summary>A cashtag.</summary>
public partial class RichTextCashtag : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Cashtag"/></summary>
    public override RichTextType Type => RichTextType.Cashtag;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The cashtag</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Cashtag { get; set; } = default!;
}

/// <summary>A bot command.</summary>
public partial class RichTextBotCommand : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.BotCommand"/></summary>
    public override RichTextType Type => RichTextType.BotCommand;

    /// <summary>The text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The bot command</summary>
    [JsonPropertyName("bot_command")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BotCommand { get; set; } = default!;
}

/// <summary>An anchor.</summary>
public partial class RichTextAnchor : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Anchor"/></summary>
    public override RichTextType Type => RichTextType.Anchor;

    /// <summary>The name of the anchor</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}

/// <summary>A link to an anchor.</summary>
public partial class RichTextAnchorLink : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.AnchorLink"/></summary>
    public override RichTextType Type => RichTextType.AnchorLink;

    /// <summary>The link text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The name of the anchor. If the name is empty, then the link brings back to the top of the message.</summary>
    [JsonPropertyName("anchor_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string AnchorName { get; set; } = default!;
}

/// <summary>A reference.</summary>
public partial class RichTextReference : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Reference"/></summary>
    public override RichTextType Type => RichTextType.Reference;

    /// <summary>Text of the reference</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The name of the reference</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}

/// <summary>A link to a reference.</summary>
public partial class RichTextReferenceLink : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.ReferenceLink"/></summary>
    public override RichTextType Type => RichTextType.ReferenceLink;

    /// <summary>The link text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>The name of the reference</summary>
    [JsonPropertyName("reference_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ReferenceName { get; set; } = default!;
}
