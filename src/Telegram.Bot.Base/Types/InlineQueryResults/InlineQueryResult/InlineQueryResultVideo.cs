using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a page containing an embedded video player or a video file. By default, this
/// video file will be sent by the user with an optional caption. Alternatively, you can use
/// <see cref="InlineQueryResultVideo.InputMessageContent"/> to send a message with the specified
/// content instead of the video.
/// </summary>
/// <remarks>
/// If an <see cref="InlineQueryResultVideo"/> message contains an embedded video (e.g., YouTube),
/// you <b>must</b> replace its content using <see cref="InlineQueryResultVideo.InputMessageContent"/>.
/// </remarks>

public class InlineQueryResultVideo : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be video
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    /// <summary>
    /// A valid URL for the embedded video player or video file
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string VideoUrl { get; init; }

    /// <summary>
    /// Mime type of the content of video url, “text/html” or “video/mp4”
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string MimeType { get; init; }

    /// <summary>
    /// URL of the thumbnail (jpeg only) for the video
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThumbnailUrl { get; init; }

    /// <summary>
    /// Title for the result
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <inheritdoc cref="Documentation.Caption" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Documentation.ParseMode" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.CaptionEntities" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Optional. Video width
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? VideoWidth { get; set; }

    /// <summary>
    /// Optional. Video height
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? VideoHeight { get; set; }

    /// <summary>
    /// Optional. Video duration in seconds
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? VideoDuration { get; set; }

    /// <summary>
    /// Optional. Short description of the result
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Optional. Content of the message to be sent instead of the video. This field is
    /// <b>required</b> if <see cref="InlineQueryResultVideo"/> is used to send an
    /// HTML-page as a result (e.g., a YouTube video).
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="videoUrl">A valid URL for the embedded video player</param>
    /// <param name="thumbnailUrl">Url of the thumbnail for the result</param>
    /// <param name="title">Title of the result</param>
    /// <param name="inputMessageContent">
    /// Content of the message to be sent instead of the video. This field is <b>required</b> if
    /// <see cref="InlineQueryResultVideo"/> is used to send an HTML-page as a result
    /// (e.g., a YouTube video).
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultVideo(
        string id,
        string videoUrl,
        string thumbnailUrl,
        string title,
        InputMessageContent? inputMessageContent = default) : base(id)
    {
        VideoUrl = videoUrl;
        MimeType = inputMessageContent is null ? "video/mp4" : "text/html";
        ThumbnailUrl = thumbnailUrl;
        Title = title;
        InputMessageContent = inputMessageContent;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InlineQueryResultVideo()
    { }
}
