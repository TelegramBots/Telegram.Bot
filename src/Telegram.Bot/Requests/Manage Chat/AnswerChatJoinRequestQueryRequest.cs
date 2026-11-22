// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to process a received chat join request query.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AnswerChatJoinRequestQueryRequest() : RequestBase<bool>("answerChatJoinRequestQuery")
{
    /// <summary>Unique identifier of the join request query</summary>
    [JsonPropertyName("chat_join_request_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ChatJoinRequestQueryId { get; set; }

    /// <summary>Result of the query. Must be either “approve” to allow the user to join the chat, “decline” to disallow the user to join the chat, or “queue” to leave the decision to other administrators.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Result { get; set; }
}
