namespace Telegram.Bot.Requests;

/// <summary>Use this method to upload a file with a sticker for later use in the <see cref="TelegramBotClientExtensions.CreateNewStickerSetAsync">CreateNewStickerSet</see>, <see cref="TelegramBotClientExtensions.AddStickerToSetAsync">AddStickerToSet</see>, or <see cref="TelegramBotClientExtensions.ReplaceStickerInSetAsync">ReplaceStickerInSet</see> methods (the file can be used multiple times).<para>Returns: The uploaded <see cref="File"/> on success.</para></summary>
public partial class UploadStickerFileRequest : FileRequestBase<File>, IUserTargetable
{
    /// <summary>User identifier of sticker file owner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format. See <a href="https://core.telegram.org/stickers">https://core.telegram.org/stickers</a> for technical requirements. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileStream Sticker { get; set; }

    /// <summary>Format of the sticker, must be one of <see cref="StickerFormat.Static">Static</see>, <see cref="StickerFormat.Animated">Animated</see>, <see cref="StickerFormat.Video">Video</see></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required StickerFormat StickerFormat { get; set; }

    /// <summary>Initializes an instance of <see cref="UploadStickerFileRequest"/></summary>
    /// <param name="userId">User identifier of sticker file owner</param>
    /// <param name="sticker">A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format. See <a href="https://core.telegram.org/stickers">https://core.telegram.org/stickers</a> for technical requirements. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="stickerFormat">Format of the sticker, must be one of <see cref="StickerFormat.Static">Static</see>, <see cref="StickerFormat.Animated">Animated</see>, <see cref="StickerFormat.Video">Video</see></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public UploadStickerFileRequest(long userId, InputFileStream sticker, StickerFormat stickerFormat) : this()
    {
        UserId = userId;
        Sticker = sticker;
        StickerFormat = stickerFormat;
    }

    /// <summary>Instantiates a new <see cref="UploadStickerFileRequest"/></summary>
    public UploadStickerFileRequest() : base("uploadStickerFile") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent("sticker", Sticker);
}
