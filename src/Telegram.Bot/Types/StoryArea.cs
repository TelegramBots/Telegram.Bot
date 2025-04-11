// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a clickable area on a story media.</summary>
public partial class StoryArea
{
    /// <summary>Position of the area</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public StoryAreaPosition Position { get; set; } = default!;

    /// <summary>Type of the area</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public StoryAreaType Type { get; set; } = default!;
}
