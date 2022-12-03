

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to an article or web page.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultArticle : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be article
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Article;

    /// <summary>
    /// Title of the result
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// Content of the message to be sent
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputMessageContent InputMessageContent { get; }

    /// <summary>
    /// Optional. URL of the result.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Url { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you don't want the URL to be shown in the message.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? HideUrl { get; set; }

    /// <summary>
    /// Optional. Short description of the result.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Description { get; set; }

    /// <inheritdoc cref="Documentation.ThumbUrl" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ThumbUrl { get; set; }

    /// <inheritdoc cref="Documentation.ThumbWidth" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbWidth { get; set; }

    /// <inheritdoc cref="Documentation.ThumbHeight" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbHeight { get; set; }

    /// <summary>
    /// Initializes a new <see cref="InlineQueryResultArticle"/> object
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="title">Title of the result</param>
    /// <param name="inputMessageContent">Content of the message to be sent</param>
    public InlineQueryResultArticle(string id, string title, InputMessageContent inputMessageContent)
        : base(id)
    {
        Title = title;
        InputMessageContent = inputMessageContent;
    }
}
