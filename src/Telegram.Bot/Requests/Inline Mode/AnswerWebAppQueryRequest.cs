// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the result of an interaction with a <a href="https://core.telegram.org/bots/webapps">Web App</a> and send a corresponding message on behalf of the user to the chat from which the query originated.<para>Returns: A <see cref="SentWebAppMessage"/> object is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AnswerWebAppQueryRequest() : RequestBase<SentWebAppMessage>("answerWebAppQuery")
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonPropertyName("web_app_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WebAppQueryId { get; set; }

    /// <summary>An object describing the message to be sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InlineQueryResult Result { get; set; }
}
