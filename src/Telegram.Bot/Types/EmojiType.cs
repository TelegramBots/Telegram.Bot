// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes an emoji. Currently, it can be one of<br/><see cref="EmojiTypeEmoji"/>, <see cref="EmojiTypeCustomEmoji"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<EmojiType>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(EmojiTypeEmoji), "emoji")]
[CustomJsonDerivedType(typeof(EmojiTypeCustomEmoji), "custom_emoji")]
public abstract partial class EmojiType
{
    /// <summary>Type of the emoji</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract EmojiTypeKind Type { get; }
}

/// <summary>A regular emoji.</summary>
public partial class EmojiTypeEmoji : EmojiType
{
    /// <summary>Type of the emoji, always <see cref="EmojiTypeKind.Emoji"/></summary>
    public override EmojiTypeKind Type => EmojiTypeKind.Emoji;

    /// <summary>The emoji</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Emoji { get; set; } = default!;
}

/// <summary>A custom emoji.</summary>
public partial class EmojiTypeCustomEmoji : EmojiType
{
    /// <summary>Type of the emoji, always <see cref="EmojiTypeKind.CustomEmoji"/></summary>
    public override EmojiTypeKind Type => EmojiTypeKind.CustomEmoji;

    /// <summary>Identifier of the custom emoji. Use <see cref="TelegramBotClientExtensions.GetCustomEmojiStickers">GetCustomEmojiStickers</see> to get full information about the sticker.</summary>
    [JsonPropertyName("custom_emoji_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CustomEmojiId { get; set; } = default!;
}
