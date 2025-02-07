// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a unique message identifier.</summary>
public partial class MessageId
{
    /// <summary>Unique message identifier. In specific instances (e.g., message containing a video sent to a big chat), the server might automatically schedule a message instead of sending it immediately. In such cases, this field will be 0 and the relevant message will be unusable until it is actually sent</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }

    /// <summary>Implicit conversion to int (Id)</summary>
    public static implicit operator int(MessageId self) => self.Id;
    /// <summary>Implicit conversion from int (Id)</summary>
    public static implicit operator MessageId(int id) => new() { Id = id };
}
