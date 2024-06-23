namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the result of an interaction with a <a href="https://core.telegram.org/bots/webapps">Web App</a> and send a corresponding message on behalf of the user to the chat from which the query originated.<para>Returns: A <see cref="SentWebAppMessage"/> object is returned.</para></summary>
public partial class AnswerWebAppQueryRequest : RequestBase<SentWebAppMessage>
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WebAppQueryId { get; set; }

    /// <summary>An object describing the message to be sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InlineQueryResult Result { get; set; }

    /// <summary>Initializes an instance of <see cref="AnswerWebAppQueryRequest"/></summary>
    /// <param name="webAppQueryId">Unique identifier for the query to be answered</param>
    /// <param name="result">An object describing the message to be sent</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public AnswerWebAppQueryRequest(string webAppQueryId, InlineQueryResult result) : this()
    {
        WebAppQueryId = webAppQueryId;
        Result = result;
    }

    /// <summary>Instantiates a new <see cref="AnswerWebAppQueryRequest"/></summary>
    public AnswerWebAppQueryRequest() : base("answerWebAppQuery") { }
}
