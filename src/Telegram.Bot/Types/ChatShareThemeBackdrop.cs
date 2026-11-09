// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a backdrop, which can be used as the background of a theme of the screen for sharing a link to a user or a chat.</summary>
public partial class ChatShareThemeBackdrop
{
    /// <summary>Unique identifier of the backdrop</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }

    /// <summary>Name of the backdrop</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Colors of the backdrop</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftBackdropColors Colors { get; set; } = default!;
}
