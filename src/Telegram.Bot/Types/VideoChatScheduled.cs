namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a video chat scheduled in the chat.</summary>
public partial class VideoChatScheduled
{
    /// <summary>Point in time when the video chat is supposed to be started by a chat administrator</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime StartDate { get; set; }
}
