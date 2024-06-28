namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the thumbnail of a regular or mask sticker set. The format of the thumbnail file must match the format of the stickers in the set.<para>Returns: </para></summary>
public partial class SetStickerSetThumbnailRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>User identifier of the sticker set owner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Format of the thumbnail, must be one of <see cref="StickerFormat.Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="StickerFormat.Animated">Animated</see> for a <b>.TGS</b> animation, or <see cref="StickerFormat.Video">Video</see> for a <b>WEBM</b> video</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required StickerFormat Format { get; set; }

    /// <summary>A <b>.WEBP</b> or <b>.PNG</b> image with the thumbnail, must be up to 128 kilobytes in size and have a width and height of exactly 100px, or a <b>.TGS</b> animation with a thumbnail up to 32 kilobytes in size (see <a href="https://core.telegram.org/stickers#animated-sticker-requirements">https://core.telegram.org/stickers#animated-sticker-requirements</a> for animated sticker technical requirements), or a <b>WEBM</b> video with the thumbnail up to 32 kilobytes in size; see <a href="https://core.telegram.org/stickers#video-sticker-requirements">https://core.telegram.org/stickers#video-sticker-requirements</a> for video sticker technical requirements. Pass a <em>FileId</em> as a String to send a file that already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Animated and video sticker set thumbnails can't be uploaded via HTTP URL. If omitted, then the thumbnail is dropped and the first sticker is used as the thumbnail.</summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary>Initializes an instance of <see cref="SetStickerSetThumbnailRequest"/></summary>
    /// <param name="name">Sticker set name</param>
    /// <param name="userId">User identifier of the sticker set owner</param>
    /// <param name="format">Format of the thumbnail, must be one of <see cref="StickerFormat.Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="StickerFormat.Animated">Animated</see> for a <b>.TGS</b> animation, or <see cref="StickerFormat.Video">Video</see> for a <b>WEBM</b> video</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetStickerSetThumbnailRequest(string name, long userId, StickerFormat format) : this()
    {
        Name = name;
        UserId = userId;
        Format = format;
    }

    /// <summary>Instantiates a new <see cref="SetStickerSetThumbnailRequest"/></summary>
    public SetStickerSetThumbnailRequest() : base("setStickerSetThumbnail") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Thumbnail is InputFileStream ifs ? ToMultipartFormDataContent("thumbnail", ifs) : base.ToHttpContent();
}
