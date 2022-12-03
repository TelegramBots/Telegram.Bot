using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a video file stored on the Telegram servers. By default, this video file will
/// be sent by the user with an optional caption. Alternatively, you can use
/// <see cref="InlineQueryResultCachedVideo.InputMessageContent"/> to send a message with
/// the specified content instead of the video.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultCachedVideo : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be video
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    /// <summary>
    /// A valid file identifier for the video file
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string VideoFileId { get; }

    /// <summary>
    /// Title for the result
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// Optional. Short description of the result
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Description { get; set; }

    /// <inheritdoc cref="Documentation.Caption" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Documentation.ParseMode" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.CaptionEntities" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="videoFileId">A valid file identifier for the video file</param>
    /// <param name="title">Title of the result</param>
    public InlineQueryResultCachedVideo(string id, string videoFileId, string title)
        : base(id)
    {
        VideoFileId = videoFileId;
        Title = title;
    }
}
