namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a change in auto-delete timer settings.</summary>
public partial class MessageAutoDeleteTimerChanged
{
    /// <summary>New auto-delete time for messages in the chat; in seconds</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageAutoDeleteTime { get; set; }
}
