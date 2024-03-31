using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the content of a media message to be sent
/// </summary>
public abstract class InputMedia
{
    /// <summary>
    /// Type of the media
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputMediaType Type { get; }

    /// <summary>
    /// File to send. Pass a file_id to send a file that exists on the Telegram servers (recommended),
    /// pass an HTTP URL for Telegram to get a file from the Internet, or pass "attach://&lt;file_attach_name&gt;"
    /// to upload a new one using multipart/form-data under &lt;file_attach_name%gt; name.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Media { get; init; }

    /// <summary>
    /// Optional. Caption of the photo to be sent, 0-1024 characters
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>
    /// Optional. List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in a caption
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <summary>
    /// Initialize an object
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    protected InputMedia(InputFile media) => Media = media;

    /// <summary>
    /// Initialize an object
    /// </summary>
    protected InputMedia()
    { }
}
