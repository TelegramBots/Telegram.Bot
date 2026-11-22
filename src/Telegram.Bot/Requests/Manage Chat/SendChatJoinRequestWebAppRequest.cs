// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to process a received chat join request query by showing a Mini App to the user before deciding the outcome. Call <see cref="TelegramBotClientExtensions.AnswerChatJoinRequestQuery">AnswerChatJoinRequestQuery</see> to resolve the join request query based on the user interaction with the Mini App.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendChatJoinRequestWebAppRequest() : RequestBase<bool>("sendChatJoinRequestWebApp")
{
    /// <summary>Unique identifier of the join request query</summary>
    [JsonPropertyName("chat_join_request_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ChatJoinRequestQueryId { get; set; }

    /// <summary>An HTTPS URL of a Web App to be opened with additional data as specified in <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">Initializing Web Apps</a></summary>
    [JsonPropertyName("web_app_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WebAppUrl { get; set; }
}
