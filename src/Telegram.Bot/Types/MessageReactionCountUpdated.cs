// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents reaction changes on a message with anonymous reactions.</summary>
public partial class MessageReactionCountUpdated
{
    /// <summary>The chat containing the message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Unique message identifier inside the chat</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary>Date of the change</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>List of reactions that are present on the message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionCount[] Reactions { get; set; } = default!;
}
