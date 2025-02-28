// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object is received when messages are deleted from a connected business account.</summary>
public partial class BusinessMessagesDeleted
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BusinessConnectionId { get; set; } = default!;

    /// <summary>Information about a chat in the business account. The bot may not have access to the chat or the corresponding user.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>The list of identifiers of deleted messages in the chat of the business account</summary>
    [JsonPropertyName("message_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] MessageIds { get; set; } = default!;
}
