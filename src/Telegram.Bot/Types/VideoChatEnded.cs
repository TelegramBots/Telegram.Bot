// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a video chat ended in the chat.</summary>
public partial class VideoChatEnded
{
    /// <summary>Video chat duration in seconds</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Duration { get; set; }

    /// <summary>Implicit conversion to int (Duration)</summary>
    public static implicit operator int(VideoChatEnded self) => self.Duration;
    /// <summary>Implicit conversion from int (Duration)</summary>
    public static implicit operator VideoChatEnded(int duration) => new() { Duration = duration };
}
