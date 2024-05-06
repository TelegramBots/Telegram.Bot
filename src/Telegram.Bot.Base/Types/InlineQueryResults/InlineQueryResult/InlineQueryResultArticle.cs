using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to an article or web page.
/// </summary>
public class InlineQueryResultArticle : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be article
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Article;

    /// <summary>
    /// Title of the result
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// Content of the message to be sent
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMessageContent InputMessageContent { get; init; }

    /// <summary>
    /// Optional. URL of the result.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you don't want the URL to be shown in the message.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HideUrl { get; set; }

    /// <summary>
    /// Optional. Short description of the result.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailUrl" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ThumbnailUrl { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailWidth" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ThumbnailWidth { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailHeight" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ThumbnailHeight { get; set; }

    /// <summary>
    /// Initializes a new <see cref="InlineQueryResultArticle"/> object
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="title">Title of the result</param>
    /// <param name="inputMessageContent">Content of the message to be sent</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultArticle(string id, string title, InputMessageContent inputMessageContent)
        : base(id)
    {
        Title = title;
        InputMessageContent = inputMessageContent;
    }

    /// <summary>
    /// Initializes a new <see cref="InlineQueryResultArticle"/> object
    /// </summary>
    public InlineQueryResultArticle()
    { }
}
