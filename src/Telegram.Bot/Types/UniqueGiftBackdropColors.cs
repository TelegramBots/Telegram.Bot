// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the colors of the backdrop of a unique gift.</summary>
public partial class UniqueGiftBackdropColors
{
    /// <summary>The color in the center of the backdrop in RGB format</summary>
    [JsonPropertyName("center_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int CenterColor { get; set; }

    /// <summary>The color on the edges of the backdrop in RGB format</summary>
    [JsonPropertyName("edge_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int EdgeColor { get; set; }

    /// <summary>The color to be applied to the symbol in RGB format</summary>
    [JsonPropertyName("symbol_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int SymbolColor { get; set; }

    /// <summary>The color for the text on the backdrop in RGB format</summary>
    [JsonPropertyName("text_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TextColor { get; set; }
}
