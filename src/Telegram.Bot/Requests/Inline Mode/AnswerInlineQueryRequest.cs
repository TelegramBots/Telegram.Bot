using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send answers to an inline query. On success, <see langword="true"/> is returned.
/// </summary>
/// <remarks>
/// No more than <b>50</b> results per query are allowed.
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AnswerInlineQueryRequest : RequestBase<bool>
{
    /// <summary>
    /// Unique identifier for the answered query
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string InlineQueryId { get; }

    /// <summary>
    /// An array of results for the inline query
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<InlineQueryResult> Results { get; }

    /// <summary>
    /// The maximum amount of time in seconds that the result of the
    /// inline query may be cached on the server. Defaults to 300
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? CacheTime { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if results may be cached on the server side only for the user that sent
    /// the query. By default, results may be returned to any user who sends the same query
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsPersonal { get; set; }

    /// <summary>
    /// Pass the offset that a client should send in the next query with the same text to
    /// receive more results. Pass an empty string if there are no more results or if you
    /// don't support pagination. Offset length can't exceed 64 bytes
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? NextOffset { get; set; }

    /// <summary>
    /// A JSON-serialized object describing a button to be shown above inline query results
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineQueryResultsButton? Button { get; set; }

    /// <summary>
    /// Initializes a new request with inlineQueryId and an array of <see cref="InlineQueryResult"/>
    /// </summary>
    /// <param name="inlineQueryId">Unique identifier for the answered query</param>
    /// <param name="results">An array of results for the inline query</param>
    public AnswerInlineQueryRequest(string inlineQueryId, IEnumerable<InlineQueryResult> results)
        : base("answerInlineQuery")
    {
        InlineQueryId = inlineQueryId;
        Results = results;
    }
}
