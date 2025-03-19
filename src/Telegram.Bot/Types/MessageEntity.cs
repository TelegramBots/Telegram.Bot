// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents one special entity in a text message. For example, hashtags, usernames, URLs, etc.</summary>
public partial class MessageEntity
{
    /// <summary>Type of the entity. Currently, can be <see cref="MessageEntityType.Mention">Mention</see> (<c>@username</c>), <see cref="MessageEntityType.Hashtag">Hashtag</see> (<c>#hashtag</c> or <c>#hashtag@chatusername</c>), <see cref="MessageEntityType.Cashtag">Cashtag</see> (<c>$USD</c> or <c>$USD@chatusername</c>), <see cref="MessageEntityType.BotCommand">BotCommand</see> (<c>/start@JobsBot</c>), <see cref="MessageEntityType.Url">Url</see> (<c>https://telegram.org</c>), <see cref="MessageEntityType.Email">Email</see> (<c>do-not-reply@telegram.org</c>), <see cref="MessageEntityType.PhoneNumber">PhoneNumber</see> (<c>+1-212-555-0123</c>), <see cref="MessageEntityType.Bold">Bold</see> (<b>bold text</b>), <see cref="MessageEntityType.Italic">Italic</see> (<em>italic text</em>), <see cref="MessageEntityType.Underline">Underline</see> (underlined text), <see cref="MessageEntityType.Strikethrough">Strikethrough</see> (strikethrough text), <see cref="MessageEntityType.Spoiler">Spoiler</see> (spoiler message), <see cref="MessageEntityType.Blockquote">Blockquote</see> (block quotation), <see cref="MessageEntityType.ExpandableBlockquote">ExpandableBlockquote</see> (collapsed-by-default block quotation), <see cref="MessageEntityType.Code">Code</see> (monowidth string), <see cref="MessageEntityType.Pre">Pre</see> (monowidth block), <see cref="MessageEntityType.TextLink">TextLink</see> (for clickable text URLs), <see cref="MessageEntityType.TextMention">TextMention</see> (for users <a href="https://telegram.org/blog/edit#new-mentions">without usernames</a>), <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> (for inline custom emoji stickers)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public MessageEntityType Type { get; set; }

    /// <summary>Offset in <a href="https://core.telegram.org/api/entities#entity-length">UTF-16 code units</a> to the start of the entity</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Offset { get; set; }

    /// <summary>Length of the entity in <a href="https://core.telegram.org/api/entities#entity-length">UTF-16 code units</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Length { get; set; }

    /// <summary><em>Optional</em>. For “TextLink” only, URL that will be opened after user taps on the text</summary>
    public string? Url { get; set; }

    /// <summary><em>Optional</em>. For “TextMention” only, the mentioned user</summary>
    public User? User { get; set; }

    /// <summary><em>Optional</em>. For “pre” only, the programming language of the entity text</summary>
    public string? Language { get; set; }

    /// <summary><em>Optional</em>. For “CustomEmoji” only, unique identifier of the custom emoji. Use <see cref="TelegramBotClientExtensions.GetCustomEmojiStickers">GetCustomEmojiStickers</see> to get full information about the sticker</summary>
    [JsonPropertyName("custom_emoji_id")]
    public string? CustomEmojiId { get; set; }
}
