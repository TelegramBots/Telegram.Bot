// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the theme of the screen for sharing a link to a user or a chat. Currently, it can be one of<br/><see cref="ChatShareThemeEmoji"/>, <see cref="ChatShareThemeUniqueGift"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<ChatShareTheme>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(ChatShareThemeEmoji), "emoji")]
[CustomJsonDerivedType(typeof(ChatShareThemeUniqueGift), "unique_gift")]
public abstract partial class ChatShareTheme
{
    /// <summary>Type of the theme</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ChatShareThemeType Type { get; }
}

/// <summary>The theme with emoji placed on a background.</summary>
public partial class ChatShareThemeEmoji : ChatShareTheme
{
    /// <summary>Type of the theme, always <see cref="ChatShareThemeType.Emoji"/></summary>
    public override ChatShareThemeType Type => ChatShareThemeType.Emoji;

    /// <summary>The emoji; 0-6 regular or custom emoji</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public EmojiType[] Emojis { get; set; } = default!;

    /// <summary><em>Optional</em>. Identifier of the backdrop used as the background; omitted if the default background is used. Use <see cref="TelegramBotClientExtensions.GetChatShareThemeBackdrops">GetChatShareThemeBackdrops</see> to get details about the backdrop.</summary>
    [JsonPropertyName("background_backdrop_id")]
    public int? BackgroundBackdropId { get; set; }
}

/// <summary>The theme based on a unique gift owned by the user or the chat.</summary>
public partial class ChatShareThemeUniqueGift : ChatShareTheme
{
    /// <summary>Type of the theme, always <see cref="ChatShareThemeType.UniqueGift"/></summary>
    public override ChatShareThemeType Type => ChatShareThemeType.UniqueGift;

    /// <summary>The gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGift Gift { get; set; } = default!;
}
