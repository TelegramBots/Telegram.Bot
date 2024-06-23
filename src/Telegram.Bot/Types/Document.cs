namespace Telegram.Bot.Types;

/// <summary>This object represents a general file (as opposed to <see cref="PhotoSize">photos</see>, <see cref="Voice">voice messages</see> and <see cref="Audio">audio files</see>).</summary>
public partial class Document : FileBase
{
    /// <summary><em>Optional</em>. Document thumbnail as defined by sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PhotoSize? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Original filename as defined by sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FileName { get; set; }

    /// <summary><em>Optional</em>. MIME type of the file as defined by sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MimeType { get; set; }
}
