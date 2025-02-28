// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>This object represents one result of an inline query. Telegram clients currently support results of the following 20 types:<br/><see cref="InlineQueryResultCachedAudio"/>, <see cref="InlineQueryResultCachedDocument"/>, <see cref="InlineQueryResultCachedGif"/>, <see cref="InlineQueryResultCachedMpeg4Gif"/>, <see cref="InlineQueryResultCachedPhoto"/>, <see cref="InlineQueryResultCachedSticker"/>, <see cref="InlineQueryResultCachedVideo"/>, <see cref="InlineQueryResultCachedVoice"/>, <see cref="InlineQueryResultArticle"/>, <see cref="InlineQueryResultAudio"/>, <see cref="InlineQueryResultContact"/>, <see cref="InlineQueryResultGame"/>, <see cref="InlineQueryResultDocument"/>, <see cref="InlineQueryResultGif"/>, <see cref="InlineQueryResultLocation"/>, <see cref="InlineQueryResultMpeg4Gif"/>, <see cref="InlineQueryResultPhoto"/>, <see cref="InlineQueryResultVenue"/>, <see cref="InlineQueryResultVideo"/>, <see cref="InlineQueryResultVoice"/><br/><b>Note:</b> All URLs passed in inline query results will be available to end users and therefore must be assumed to be <b>public</b>.</summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InlineQueryResult>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InlineQueryResultArticle), "article")]
[CustomJsonDerivedType(typeof(InlineQueryResultAudio), "audio")]
[CustomJsonDerivedType(typeof(InlineQueryResultContact), "contact")]
[CustomJsonDerivedType(typeof(InlineQueryResultGame), "game")]
[CustomJsonDerivedType(typeof(InlineQueryResultDocument), "document")]
[CustomJsonDerivedType(typeof(InlineQueryResultGif), "gif")]
[CustomJsonDerivedType(typeof(InlineQueryResultLocation), "location")]
[CustomJsonDerivedType(typeof(InlineQueryResultMpeg4Gif), "mpeg4_gif")]
[CustomJsonDerivedType(typeof(InlineQueryResultPhoto), "photo")]
[CustomJsonDerivedType(typeof(InlineQueryResultVenue), "venue")]
[CustomJsonDerivedType(typeof(InlineQueryResultVideo), "video")]
[CustomJsonDerivedType(typeof(InlineQueryResultVoice), "voice")]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedSticker), "sticker")]
public abstract partial class InlineQueryResult
{
    /// <summary>Type of the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InlineQueryResultType Type { get; }

    /// <summary>Unique identifier for this result, 1-64 bytes</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Id { get; set; }

    /// <summary><em>Optional</em>. <a href="https://core.telegram.org/bots/features#inline-keyboards">Inline keyboard</a> attached to the message</summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResult"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    [SetsRequiredMembers]
    protected InlineQueryResult(string id) => Id = id;

    /// <summary>Instantiates a new <see cref="InlineQueryResult"/></summary>
    protected InlineQueryResult() { }
}

/// <summary>Represents a link to an article or web page.</summary>
public partial class InlineQueryResultArticle : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Article"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Article;

    /// <summary>Title of the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Content of the message to be sent</summary>
    [JsonPropertyName("input_message_content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMessageContent InputMessageContent { get; set; }

    /// <summary><em>Optional</em>. URL of the result</summary>
    public string? Url { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Url of the thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Thumbnail width</summary>
    [JsonPropertyName("thumbnail_width")]
    public int? ThumbnailWidth { get; set; }

    /// <summary><em>Optional</em>. Thumbnail height</summary>
    [JsonPropertyName("thumbnail_height")]
    public int? ThumbnailHeight { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultArticle"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="title">Title of the result</param>
    /// <param name="inputMessageContent">Content of the message to be sent</param>
    [SetsRequiredMembers]
    public InlineQueryResultArticle(string id, string title, InputMessageContent inputMessageContent) : base(id)
    {
        Title = title;
        InputMessageContent = inputMessageContent;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultArticle"/></summary>
    public InlineQueryResultArticle() { }
}

/// <summary>Represents a link to a photo. By default, this photo will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the photo.</summary>
public partial class InlineQueryResultPhoto : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Photo"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Photo;

    /// <summary>A valid URL of the photo. Photo must be in <b>JPEG</b> format. Photo size must not exceed 5MB</summary>
    [JsonPropertyName("photo_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PhotoUrl { get; set; }

    /// <summary>URL of the thumbnail for the photo</summary>
    [JsonPropertyName("thumbnail_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Width of the photo</summary>
    [JsonPropertyName("photo_width")]
    public int? PhotoWidth { get; set; }

    /// <summary><em>Optional</em>. Height of the photo</summary>
    [JsonPropertyName("photo_height")]
    public int? PhotoHeight { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Caption of the photo to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the photo caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the photo</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultPhoto"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="photoUrl">A valid URL of the photo. Photo must be in <b>JPEG</b> format. Photo size must not exceed 5MB</param>
    /// <param name="thumbnailUrl">URL of the thumbnail for the photo</param>
    [SetsRequiredMembers]
    public InlineQueryResultPhoto(string id, string photoUrl, string thumbnailUrl) : base(id)
    {
        PhotoUrl = photoUrl;
        ThumbnailUrl = thumbnailUrl;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultPhoto"/></summary>
    public InlineQueryResultPhoto() { }
}

/// <summary>Represents a link to an animated GIF file. By default, this animated GIF file will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the animation.</summary>
public partial class InlineQueryResultGif : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Gif"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Gif;

    /// <summary>A valid URL for the GIF file</summary>
    [JsonPropertyName("gif_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string GifUrl { get; set; }

    /// <summary><em>Optional</em>. Width of the GIF</summary>
    [JsonPropertyName("gif_width")]
    public int? GifWidth { get; set; }

    /// <summary><em>Optional</em>. Height of the GIF</summary>
    [JsonPropertyName("gif_height")]
    public int? GifHeight { get; set; }

    /// <summary><em>Optional</em>. Duration of the GIF in seconds</summary>
    [JsonPropertyName("gif_duration")]
    public int? GifDuration { get; set; }

    /// <summary>URL of the static (JPEG or GIF) or animated (MPEG4) thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. MIME type of the thumbnail, must be one of “image/jpeg”, “image/gif”, or “video/mp4”. Defaults to “image/jpeg”</summary>
    [JsonPropertyName("thumbnail_mime_type")]
    public string? ThumbnailMimeType { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the GIF file to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the GIF animation</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultGif"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="gifUrl">A valid URL for the GIF file</param>
    /// <param name="thumbnailUrl">URL of the static (JPEG or GIF) or animated (MPEG4) thumbnail for the result</param>
    [SetsRequiredMembers]
    public InlineQueryResultGif(string id, string gifUrl, string thumbnailUrl) : base(id)
    {
        GifUrl = gifUrl;
        ThumbnailUrl = thumbnailUrl;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultGif"/></summary>
    public InlineQueryResultGif() { }
}

/// <summary>Represents a link to a video animation (H.264/MPEG-4 AVC video without sound). By default, this animated MPEG-4 file will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the animation.</summary>
public partial class InlineQueryResultMpeg4Gif : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Mpeg4Gif"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Mpeg4Gif;

    /// <summary>A valid URL for the MPEG4 file</summary>
    [JsonPropertyName("mpeg4_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Mpeg4Url { get; set; }

    /// <summary><em>Optional</em>. Video width</summary>
    [JsonPropertyName("mpeg4_width")]
    public int? Mpeg4Width { get; set; }

    /// <summary><em>Optional</em>. Video height</summary>
    [JsonPropertyName("mpeg4_height")]
    public int? Mpeg4Height { get; set; }

    /// <summary><em>Optional</em>. Video duration in seconds</summary>
    [JsonPropertyName("mpeg4_duration")]
    public int? Mpeg4Duration { get; set; }

    /// <summary>URL of the static (JPEG or GIF) or animated (MPEG4) thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. MIME type of the thumbnail, must be one of “image/jpeg”, “image/gif”, or “video/mp4”. Defaults to “image/jpeg”</summary>
    [JsonPropertyName("thumbnail_mime_type")]
    public string? ThumbnailMimeType { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the MPEG-4 file to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the video animation</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultMpeg4Gif"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="mpeg4Url">A valid URL for the MPEG4 file</param>
    /// <param name="thumbnailUrl">URL of the static (JPEG or GIF) or animated (MPEG4) thumbnail for the result</param>
    [SetsRequiredMembers]
    public InlineQueryResultMpeg4Gif(string id, string mpeg4Url, string thumbnailUrl) : base(id)
    {
        Mpeg4Url = mpeg4Url;
        ThumbnailUrl = thumbnailUrl;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultMpeg4Gif"/></summary>
    public InlineQueryResultMpeg4Gif() { }
}

/// <summary>Represents a link to a page containing an embedded video player or a video file. By default, this video file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the video.</summary>
/// <remarks>If an InlineQueryResultVideo message contains an embedded video (e.g., YouTube), you <b>must</b> replace its content using <see cref="InputMessageContent">InputMessageContent</see>.</remarks>
public partial class InlineQueryResultVideo : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Video"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    /// <summary>A valid URL for the embedded video player or video file</summary>
    [JsonPropertyName("video_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string VideoUrl { get; set; }

    /// <summary>MIME type of the content of the video URL, “text/html” or “video/mp4”</summary>
    [JsonPropertyName("mime_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string MimeType { get; set; }

    /// <summary>URL of the thumbnail (JPEG only) for the video</summary>
    [JsonPropertyName("thumbnail_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThumbnailUrl { get; set; }

    /// <summary>Title for the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the video to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the video caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Video width</summary>
    [JsonPropertyName("video_width")]
    public int? VideoWidth { get; set; }

    /// <summary><em>Optional</em>. Video height</summary>
    [JsonPropertyName("video_height")]
    public int? VideoHeight { get; set; }

    /// <summary><em>Optional</em>. Video duration in seconds</summary>
    [JsonPropertyName("video_duration")]
    public int? VideoDuration { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the video. This field is <b>required</b> if InlineQueryResultVideo is used to send an HTML-page as a result (e.g., a YouTube video).</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultVideo"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="videoUrl">A valid URL for the embedded video player or video file</param>
    /// <param name="thumbnailUrl">URL of the thumbnail (JPEG only) for the video</param>
    /// <param name="title">Title for the result</param>
    /// <param name="inputMessageContent"><em>Optional</em>. Content of the message to be sent instead of the video. This field is <b>required</b> if InlineQueryResultVideo is used to send an HTML-page as a result (e.g., a YouTube video).</param>
    [SetsRequiredMembers]
    public InlineQueryResultVideo(string id, string videoUrl, string thumbnailUrl, string title, InputMessageContent? inputMessageContent = default) : base(id)
    {
        VideoUrl = videoUrl;
        ThumbnailUrl = thumbnailUrl;
        Title = title;
        InputMessageContent = inputMessageContent;
        MimeType = inputMessageContent is null ? "video/mp4" : "text/html";
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultVideo"/></summary>
    public InlineQueryResultVideo() { }
}

/// <summary>Represents a link to an MP3 audio file. By default, this audio file will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the audio.</summary>
public partial class InlineQueryResultAudio : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Audio"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Audio;

    /// <summary>A valid URL for the audio file</summary>
    [JsonPropertyName("audio_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string AudioUrl { get; set; }

    /// <summary>Title</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the audio caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Performer</summary>
    public string? Performer { get; set; }

    /// <summary><em>Optional</em>. Audio duration in seconds</summary>
    [JsonPropertyName("audio_duration")]
    public int? AudioDuration { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the audio</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultAudio"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="audioUrl">A valid URL for the audio file</param>
    /// <param name="title">Title</param>
    [SetsRequiredMembers]
    public InlineQueryResultAudio(string id, string audioUrl, string title) : base(id)
    {
        AudioUrl = audioUrl;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultAudio"/></summary>
    public InlineQueryResultAudio() { }
}

/// <summary>Represents a link to a voice recording in an .OGG container encoded with OPUS. By default, this voice recording will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the the voice message.</summary>
public partial class InlineQueryResultVoice : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Voice"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Voice;

    /// <summary>A valid URL for the voice recording</summary>
    [JsonPropertyName("voice_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string VoiceUrl { get; set; }

    /// <summary>Recording title</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the voice message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Recording duration in seconds</summary>
    [JsonPropertyName("voice_duration")]
    public int? VoiceDuration { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the voice recording</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultVoice"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="voiceUrl">A valid URL for the voice recording</param>
    /// <param name="title">Recording title</param>
    [SetsRequiredMembers]
    public InlineQueryResultVoice(string id, string voiceUrl, string title) : base(id)
    {
        VoiceUrl = voiceUrl;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultVoice"/></summary>
    public InlineQueryResultVoice() { }
}

/// <summary>Represents a link to a file. By default, this file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the file. Currently, only <b>.PDF</b> and <b>.ZIP</b> files can be sent using this method.</summary>
public partial class InlineQueryResultDocument : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Document"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Document;

    /// <summary>A valid URL for the file</summary>
    [JsonPropertyName("document_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string DocumentUrl { get; set; }

    /// <summary>Title for the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the document to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the document caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>MIME type of the content of the file, either “application/pdf” or “application/zip”</summary>
    [JsonPropertyName("mime_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string MimeType { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the file</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary><em>Optional</em>. URL of the thumbnail (JPEG only) for the file</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Thumbnail width</summary>
    [JsonPropertyName("thumbnail_width")]
    public int? ThumbnailWidth { get; set; }

    /// <summary><em>Optional</em>. Thumbnail height</summary>
    [JsonPropertyName("thumbnail_height")]
    public int? ThumbnailHeight { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultDocument"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="documentUrl">A valid URL for the file</param>
    /// <param name="title">Title for the result</param>
    /// <param name="mimeType">MIME type of the content of the file, either “application/pdf” or “application/zip”</param>
    [SetsRequiredMembers]
    public InlineQueryResultDocument(string id, string documentUrl, string title, string mimeType) : base(id)
    {
        DocumentUrl = documentUrl;
        Title = title;
        MimeType = mimeType;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultDocument"/></summary>
    public InlineQueryResultDocument() { }
}

/// <summary>Represents a location on a map. By default, the location will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the location.</summary>
public partial class InlineQueryResultLocation : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Location"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Location;

    /// <summary>Location latitude in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; set; }

    /// <summary>Location longitude in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; set; }

    /// <summary>Location title</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. The radius of uncertainty for the location, measured in meters; 0-1500</summary>
    [JsonPropertyName("horizontal_accuracy")]
    public double? HorizontalAccuracy { get; set; }

    /// <summary><em>Optional</em>. Period in seconds during which the location can be updated, should be between 60 and 86400, or 0x7FFFFFFF for live locations that can be edited indefinitely.</summary>
    [JsonPropertyName("live_period")]
    public int? LivePeriod { get; set; }

    /// <summary><em>Optional</em>. For live locations, a direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</summary>
    public int? Heading { get; set; }

    /// <summary><em>Optional</em>. For live locations, a maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</summary>
    [JsonPropertyName("proximity_alert_radius")]
    public int? ProximityAlertRadius { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the location</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary><em>Optional</em>. Url of the thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Thumbnail width</summary>
    [JsonPropertyName("thumbnail_width")]
    public int? ThumbnailWidth { get; set; }

    /// <summary><em>Optional</em>. Thumbnail height</summary>
    [JsonPropertyName("thumbnail_height")]
    public int? ThumbnailHeight { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultLocation"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="latitude">Location latitude in degrees</param>
    /// <param name="longitude">Location longitude in degrees</param>
    /// <param name="title">Location title</param>
    [SetsRequiredMembers]
    public InlineQueryResultLocation(string id, double latitude, double longitude, string title) : base(id)
    {
        Latitude = latitude;
        Longitude = longitude;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultLocation"/></summary>
    public InlineQueryResultLocation() { }
}

/// <summary>Represents a venue. By default, the venue will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the venue.</summary>
public partial class InlineQueryResultVenue : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Venue"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Venue;

    /// <summary>Latitude of the venue location in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; set; }

    /// <summary>Longitude of the venue location in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; set; }

    /// <summary>Title of the venue</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Address of the venue</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Address { get; set; }

    /// <summary><em>Optional</em>. Foursquare identifier of the venue if known</summary>
    [JsonPropertyName("foursquare_id")]
    public string? FoursquareId { get; set; }

    /// <summary><em>Optional</em>. Foursquare type of the venue, if known. (For example, “arts_entertainment/default”, “arts_entertainment/aquarium” or “food/icecream”.)</summary>
    [JsonPropertyName("foursquare_type")]
    public string? FoursquareType { get; set; }

    /// <summary><em>Optional</em>. Google Places identifier of the venue</summary>
    [JsonPropertyName("google_place_id")]
    public string? GooglePlaceId { get; set; }

    /// <summary><em>Optional</em>. Google Places type of the venue. (See <a href="https://developers.google.com/places/web-service/supported_types">supported types</a>.)</summary>
    [JsonPropertyName("google_place_type")]
    public string? GooglePlaceType { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the venue</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary><em>Optional</em>. Url of the thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Thumbnail width</summary>
    [JsonPropertyName("thumbnail_width")]
    public int? ThumbnailWidth { get; set; }

    /// <summary><em>Optional</em>. Thumbnail height</summary>
    [JsonPropertyName("thumbnail_height")]
    public int? ThumbnailHeight { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultVenue"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="latitude">Latitude of the venue location in degrees</param>
    /// <param name="longitude">Longitude of the venue location in degrees</param>
    /// <param name="title">Title of the venue</param>
    /// <param name="address">Address of the venue</param>
    [SetsRequiredMembers]
    public InlineQueryResultVenue(string id, double latitude, double longitude, string title, string address) : base(id)
    {
        Latitude = latitude;
        Longitude = longitude;
        Title = title;
        Address = address;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultVenue"/></summary>
    public InlineQueryResultVenue() { }
}

/// <summary>Represents a contact with a phone number. By default, this contact will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the contact.</summary>
public partial class InlineQueryResultContact : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Contact"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Contact;

    /// <summary>Contact's phone number</summary>
    [JsonPropertyName("phone_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PhoneNumber { get; set; }

    /// <summary>Contact's first name</summary>
    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FirstName { get; set; }

    /// <summary><em>Optional</em>. Contact's last name</summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary><em>Optional</em>. Additional data about the contact in the form of a <a href="https://en.wikipedia.org/wiki/VCard">vCard</a>, 0-2048 bytes</summary>
    public string? Vcard { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the contact</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary><em>Optional</em>. Url of the thumbnail for the result</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    /// <summary><em>Optional</em>. Thumbnail width</summary>
    [JsonPropertyName("thumbnail_width")]
    public int? ThumbnailWidth { get; set; }

    /// <summary><em>Optional</em>. Thumbnail height</summary>
    [JsonPropertyName("thumbnail_height")]
    public int? ThumbnailHeight { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultContact"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    [SetsRequiredMembers]
    public InlineQueryResultContact(string id, string phoneNumber, string firstName) : base(id)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultContact"/></summary>
    public InlineQueryResultContact() { }
}

/// <summary>Represents a <a href="https://core.telegram.org/bots/api#games">Game</a>.</summary>
public partial class InlineQueryResultGame : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Game"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Game;

    /// <summary>Short name of the game</summary>
    [JsonPropertyName("game_short_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string GameShortName { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultGame"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="gameShortName">Short name of the game</param>
    [SetsRequiredMembers]
    public InlineQueryResultGame(string id, string gameShortName) : base(id) => GameShortName = gameShortName;

    /// <summary>Instantiates a new <see cref="InlineQueryResultGame"/></summary>
    public InlineQueryResultGame() { }
}

/// <summary>Represents a link to a photo stored on the Telegram servers. By default, this photo will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the photo.</summary>
public partial class InlineQueryResultCachedPhoto : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Photo"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Photo;

    /// <summary>A valid file identifier of the photo</summary>
    [JsonPropertyName("photo_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PhotoFileId { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Caption of the photo to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the photo caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the photo</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedPhoto"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="photoFileId">A valid file identifier of the photo</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedPhoto(string id, string photoFileId) : base(id) => PhotoFileId = photoFileId;

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedPhoto"/></summary>
    public InlineQueryResultCachedPhoto() { }
}

/// <summary>Represents a link to an animated GIF file stored on the Telegram servers. By default, this animated GIF file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with specified content instead of the animation.</summary>
public partial class InlineQueryResultCachedGif : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Gif"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Gif;

    /// <summary>A valid file identifier for the GIF file</summary>
    [JsonPropertyName("gif_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string GifFileId { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the GIF file to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the GIF animation</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedGif"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="gifFileId">A valid file identifier for the GIF file</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedGif(string id, string gifFileId) : base(id) => GifFileId = gifFileId;

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedGif"/></summary>
    public InlineQueryResultCachedGif() { }
}

/// <summary>Represents a link to a video animation (H.264/MPEG-4 AVC video without sound) stored on the Telegram servers. By default, this animated MPEG-4 file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the animation.</summary>
public partial class InlineQueryResultCachedMpeg4Gif : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Mpeg4Gif"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Mpeg4Gif;

    /// <summary>A valid file identifier for the MPEG4 file</summary>
    [JsonPropertyName("mpeg4_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Mpeg4FileId { get; set; }

    /// <summary><em>Optional</em>. Title for the result</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Caption of the MPEG-4 file to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the video animation</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedMpeg4Gif"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="mpeg4FileId">A valid file identifier for the MPEG4 file</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedMpeg4Gif(string id, string mpeg4FileId) : base(id) => Mpeg4FileId = mpeg4FileId;

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedMpeg4Gif"/></summary>
    public InlineQueryResultCachedMpeg4Gif() { }
}

/// <summary>Represents a link to a sticker stored on the Telegram servers. By default, this sticker will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the sticker.</summary>
public partial class InlineQueryResultCachedSticker : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Sticker"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Sticker;

    /// <summary>A valid file identifier of the sticker</summary>
    [JsonPropertyName("sticker_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string StickerFileId { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the sticker</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedSticker"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="stickerFileId">A valid file identifier of the sticker</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedSticker(string id, string stickerFileId) : base(id) => StickerFileId = stickerFileId;

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedSticker"/></summary>
    public InlineQueryResultCachedSticker() { }
}

/// <summary>Represents a link to a file stored on the Telegram servers. By default, this file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the file.</summary>
public partial class InlineQueryResultCachedDocument : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Document"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Document;

    /// <summary>A valid file identifier for the file</summary>
    [JsonPropertyName("document_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string DocumentFileId { get; set; }

    /// <summary>Title for the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Caption of the document to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the document caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the file</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedDocument"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="documentFileId">A valid file identifier for the file</param>
    /// <param name="title">Title for the result</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedDocument(string id, string documentFileId, string title) : base(id)
    {
        DocumentFileId = documentFileId;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedDocument"/></summary>
    public InlineQueryResultCachedDocument() { }
}

/// <summary>Represents a link to a video file stored on the Telegram servers. By default, this video file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the video.</summary>
public partial class InlineQueryResultCachedVideo : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Video"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    /// <summary>A valid file identifier for the video file</summary>
    [JsonPropertyName("video_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string VideoFileId { get; set; }

    /// <summary>Title for the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Short description of the result</summary>
    public string? Description { get; set; }

    /// <summary><em>Optional</em>. Caption of the video to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the video caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the video</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedVideo"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="videoFileId">A valid file identifier for the video file</param>
    /// <param name="title">Title for the result</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedVideo(string id, string videoFileId, string title) : base(id)
    {
        VideoFileId = videoFileId;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedVideo"/></summary>
    public InlineQueryResultCachedVideo() { }
}

/// <summary>Represents a link to a voice message stored on the Telegram servers. By default, this voice message will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the voice message.</summary>
public partial class InlineQueryResultCachedVoice : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Voice"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Voice;

    /// <summary>A valid file identifier for the voice message</summary>
    [JsonPropertyName("voice_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string VoiceFileId { get; set; }

    /// <summary>Voice message title</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary><em>Optional</em>. Caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the voice message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the voice message</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedVoice"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="voiceFileId">A valid file identifier for the voice message</param>
    /// <param name="title">Voice message title</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedVoice(string id, string voiceFileId, string title) : base(id)
    {
        VoiceFileId = voiceFileId;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedVoice"/></summary>
    public InlineQueryResultCachedVoice() { }
}

/// <summary>Represents a link to an MP3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the audio.</summary>
public partial class InlineQueryResultCachedAudio : InlineQueryResult
{
    /// <summary>Type of the result, always <see cref="InlineQueryResultType.Audio"/></summary>
    public override InlineQueryResultType Type => InlineQueryResultType.Audio;

    /// <summary>A valid file identifier for the audio file</summary>
    [JsonPropertyName("audio_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string AudioFileId { get; set; }

    /// <summary><em>Optional</em>. Caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the audio caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Content of the message to be sent instead of the audio</summary>
    [JsonPropertyName("input_message_content")]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultCachedAudio"/></summary>
    /// <param name="id">Unique identifier for this result, 1-64 bytes</param>
    /// <param name="audioFileId">A valid file identifier for the audio file</param>
    [SetsRequiredMembers]
    public InlineQueryResultCachedAudio(string id, string audioFileId) : base(id) => AudioFileId = audioFileId;

    /// <summary>Instantiates a new <see cref="InlineQueryResultCachedAudio"/></summary>
    public InlineQueryResultCachedAudio() { }
}
