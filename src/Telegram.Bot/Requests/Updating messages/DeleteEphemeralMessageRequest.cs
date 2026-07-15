// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete an ephemeral message. Note that it is not guaranteed that the user will receive the message deletion event, especially if they are offline.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteEphemeralMessageRequest() : RequestBase<bool>("deleteEphemeralMessage"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup in the format <c>@username</c></summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the user who received the message</summary>
    [JsonPropertyName("receiver_user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ReceiverUserId { get; set; }

    /// <summary>Identifier of the ephemeral message to delete</summary>
    [JsonPropertyName("ephemeral_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int EphemeralMessageId { get; set; }
}
