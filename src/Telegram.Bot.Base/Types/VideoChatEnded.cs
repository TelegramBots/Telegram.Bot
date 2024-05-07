namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a video chat ended in the chat.
/// </summary>
public class VideoChatEnded
{
    /// <summary>
    /// Video chat duration in seconds
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Duration { get; set; }
}
