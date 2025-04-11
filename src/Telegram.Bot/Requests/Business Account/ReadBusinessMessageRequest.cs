// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Marks incoming message as read on behalf of a business account. Requires the <em>CanReadMessages</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ReadBusinessMessageRequest() : RequestBase<bool>("readBusinessMessage"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection on behalf of which to read the message</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the chat in which the message was received. The chat must have been active in the last 24 hours.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier of the message to mark as read</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
