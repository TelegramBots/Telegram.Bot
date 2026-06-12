// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to process a received chat join request query by showing a Mini App to the user before deciding the outcome.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendChatJoinRequestWebAppRequest() : RequestBase<bool>("sendChatJoinRequestWebApp")
{
    /// <summary>Unique identifier of the join request query</summary>
    [JsonPropertyName("chat_join_request_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ChatJoinRequestQueryId { get; set; }

    /// <summary>The URL of the Mini App to be opened</summary>
    [JsonPropertyName("web_app_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WebAppUrl { get; set; }
}
