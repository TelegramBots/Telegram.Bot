// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a chat background.</summary>
public partial class ChatBackground
{
    /// <summary>Type of the background</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public BackgroundType Type { get; set; } = default!;

    /// <summary>Implicit conversion to BackgroundType (Type)</summary>
    public static implicit operator BackgroundType(ChatBackground self) => self.Type;
    /// <summary>Implicit conversion from BackgroundType (Type)</summary>
    public static implicit operator ChatBackground(BackgroundType type) => new() { Type = type };
}
