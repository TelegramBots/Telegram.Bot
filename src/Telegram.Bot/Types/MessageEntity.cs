namespace Telegram.Bot.Types;

/// <summary>This object represents one special entity in a text message. For example, hashtags, usernames, URLs, etc.</summary>
public partial class MessageEntity
{
    /// <summary>Type of the entity. Currently, can be “mention” (<c>@username</c>), “hashtag” (<c>#hashtag</c>), “cashtag” (<c>$USD</c>), <see cref="BotCommand"/> (<c>/start@JobsBot</c>), “url” (<c>https://telegram.org</c>), “email” (<c>do-not-reply@telegram.org</c>), “PhoneNumber” (<c>+1-212-555-0123</c>), “bold” (<b>bold text</b>), “italic” (<em>italic text</em>), “underline” (underlined text), “strikethrough” (strikethrough text), “spoiler” (spoiler message), “blockquote” (block quotation), “ExpandableBlockquote” (collapsed-by-default block quotation), “code” (monowidth string), “pre” (monowidth block), “TextLink” (for clickable text URLs), “TextMention” (for users <a href="https://telegram.org/blog/edit#new-mentions">without usernames</a>), “CustomEmoji” (for inline custom emoji stickers)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public MessageEntityType Type { get; set; }

    /// <summary>Offset in <a href="https://core.telegram.org/api/entities#entity-length">UTF-16 code units</a> to the start of the entity</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Offset { get; set; }

    /// <summary>Length of the entity in <a href="https://core.telegram.org/api/entities#entity-length">UTF-16 code units</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Length { get; set; }

    /// <summary><em>Optional</em>. For <see cref="MessageEntityType.TextLink"/> only, URL that will be opened after user taps on the text</summary>
    public string? Url { get; set; }

    /// <summary><em>Optional</em>. For <see cref="MessageEntityType.TextMention"/> only, the mentioned user</summary>
    public User? User { get; set; }

    /// <summary><em>Optional</em>. For <see cref="MessageEntityType.Pre"/> only, the programming language of the entity text</summary>
    public string? Language { get; set; }

    /// <summary><em>Optional</em>. For <see cref="MessageEntityType.CustomEmoji"/> only, unique identifier of the custom emoji. Use <see csref="TelegramBotClientExtensions.GetCustomEmojiStickersAsync">GetCustomEmojiStickers</see> to get full information about the sticker</summary>
    public string? CustomEmojiId { get; set; }
}
