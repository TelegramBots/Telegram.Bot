namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a video chat ended in the chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VideoChatEnded
{
    /// <summary>
    /// Video chat duration; in seconds
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Duration { get; set; }
}
