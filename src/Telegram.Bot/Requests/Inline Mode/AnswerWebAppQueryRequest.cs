using Telegram.Bot.Types.InlineQueryResults;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the result of an interaction with a
/// <a href="https://core.telegram.org/bots/webapps">Web App</a> and send a corresponding message on behalf of the
/// user to the chat from which the query originated. On success, a <see cref="SentWebAppMessage"/> object is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AnswerWebAppQueryRequest : RequestBase<SentWebAppMessage>
{
    /// <summary>
    /// Unique identifier for the query to be answered
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string WebAppQueryId { get; }

    /// <summary>
    /// An object describing the message to be sent
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InlineQueryResult Result { get; }

    /// <summary>
    /// Initializes a new request with <see cref="WebAppQueryId"/> and a <see cref="InlineQueryResult"/>
    /// </summary>
    /// <param name="webAppQueryId">Unique identifier for the query to be answered</param>
    /// <param name="result">An object describing the message to be sent</param>
    public AnswerWebAppQueryRequest(string webAppQueryId, InlineQueryResult result)
        : base("answerWebAppQuery")
    {
        WebAppQueryId = webAppQueryId;
        Result = result;
    }
}
