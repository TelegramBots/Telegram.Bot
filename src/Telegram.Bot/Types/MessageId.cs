namespace Telegram.Bot.Types;

/// <summary>This object represents a unique message identifier.</summary>
public partial class MessageId
{
    /// <summary>Unique message identifier</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }
}
