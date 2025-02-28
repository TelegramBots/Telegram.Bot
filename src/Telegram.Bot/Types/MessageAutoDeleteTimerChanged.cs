// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a change in auto-delete timer settings.</summary>
public partial class MessageAutoDeleteTimerChanged
{
    /// <summary>New auto-delete time for messages in the chat; in seconds</summary>
    [JsonPropertyName("message_auto_delete_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageAutoDeleteTime { get; set; }

    /// <summary>Implicit conversion to int (MessageAutoDeleteTime)</summary>
    public static implicit operator int(MessageAutoDeleteTimerChanged self) => self.MessageAutoDeleteTime;
    /// <summary>Implicit conversion from int (MessageAutoDeleteTime)</summary>
    public static implicit operator MessageAutoDeleteTimerChanged(int messageAutoDeleteTime) => new() { MessageAutoDeleteTime = messageAutoDeleteTime };
}
