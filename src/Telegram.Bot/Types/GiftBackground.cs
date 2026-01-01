// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the background of a gift.</summary>
public partial class GiftBackground
{
    /// <summary>Center color of the background in RGB format</summary>
    [JsonPropertyName("center_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int CenterColor { get; set; }

    /// <summary>Edge color of the background in RGB format</summary>
    [JsonPropertyName("edge_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int EdgeColor { get; set; }

    /// <summary>Text color of the background in RGB format</summary>
    [JsonPropertyName("text_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TextColor { get; set; }
}
