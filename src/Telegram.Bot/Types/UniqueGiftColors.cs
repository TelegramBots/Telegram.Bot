// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about the color scheme for a user's name, message replies and link previews based on a unique gift.</summary>
public partial class UniqueGiftColors
{
    /// <summary>Custom emoji identifier of the unique gift's model</summary>
    [JsonPropertyName("model_custom_emoji_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ModelCustomEmojiId { get; set; } = default!;

    /// <summary>Custom emoji identifier of the unique gift's symbol</summary>
    [JsonPropertyName("symbol_custom_emoji_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SymbolCustomEmojiId { get; set; } = default!;

    /// <summary>Main color used in light themes; RGB format</summary>
    [JsonPropertyName("light_theme_main_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int LightThemeMainColor { get; set; }

    /// <summary>List of 1-3 additional colors used in light themes; RGB format</summary>
    [JsonPropertyName("light_theme_other_colors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] LightThemeOtherColors { get; set; } = default!;

    /// <summary>Main color used in dark themes; RGB format</summary>
    [JsonPropertyName("dark_theme_main_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int DarkThemeMainColor { get; set; }

    /// <summary>List of 1-3 additional colors used in dark themes; RGB format</summary>
    [JsonPropertyName("dark_theme_other_colors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] DarkThemeOtherColors { get; set; } = default!;
}
