// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes an update about a user stopping message generation.</summary>
public partial class MessageGenerationStopped
{
    /// <summary>Chat in which the message is generated</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary><em>Optional</em>. Unique identifier of the message thread in which the message is generated</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Unique identifier of the message draft which was stopped</summary>
    [JsonPropertyName("draft_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int DraftId { get; set; }
}
