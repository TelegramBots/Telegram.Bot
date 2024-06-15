using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.InlineQueryResults;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send answers to an inline query. On success, <see langword="true"/> is returned.
/// </summary>
/// <remarks>
/// No more than <b>50</b> results per query are allowed.
/// </remarks>

public class AnswerInlineQueryRequest : RequestBase<bool>
{
    /// <summary>
    /// Unique identifier for the answered query
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineQueryId { get; init; }

    /// <summary>
    /// An array of results for the inline query
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InlineQueryResult> Results { get; init; }

    /// <summary>
    /// The maximum amount of time in seconds that the result of the
    /// inline query may be cached on the server. Defaults to 300
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CacheTime { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if results may be cached on the server side only for the user that sent
    /// the query. By default, results may be returned to any user who sends the same query
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsPersonal { get; set; }

    /// <summary>
    /// Pass the offset that a client should send in the next query with the same text to
    /// receive more results. Pass an empty string if there are no more results or if you
    /// don't support pagination. Offset length can't exceed 64 bytes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NextOffset { get; set; }

    /// <summary>
    /// An object describing a button to be shown above inline query results
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineQueryResultsButton? Button { get; set; }

    /// <summary>
    /// Initializes a new request with inlineQueryId and an array of <see cref="InlineQueryResult"/>
    /// </summary>
    /// <param name="inlineQueryId">Unique identifier for the answered query</param>
    /// <param name="results">An array of results for the inline query</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public AnswerInlineQueryRequest(string inlineQueryId, IEnumerable<InlineQueryResult> results)
        : this()
    {
        InlineQueryId = inlineQueryId;
        Results = results;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public AnswerInlineQueryRequest()
        : base("answerInlineQuery")
    { }
}
