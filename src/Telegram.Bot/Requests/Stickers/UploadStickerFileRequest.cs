// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to upload a file with a sticker for later use in the <see cref="TelegramBotClientExtensions.CreateNewStickerSet">CreateNewStickerSet</see>, <see cref="TelegramBotClientExtensions.AddStickerToSet">AddStickerToSet</see>, or <see cref="TelegramBotClientExtensions.ReplaceStickerInSet">ReplaceStickerInSet</see> methods (the file can be used multiple times).<para>Returns: The uploaded <see cref="TGFile"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UploadStickerFileRequest() : FileRequestBase<TGFile>("uploadStickerFile"), IUserTargetable
{
    /// <summary>User identifier of sticker file owner</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format. See <a href="https://core.telegram.org/stickers">https://core.telegram.org/stickers</a> for technical requirements. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileStream Sticker { get; set; }

    /// <summary>Format of the sticker, must be one of <see cref="StickerFormat.Static">Static</see>, <see cref="StickerFormat.Animated">Animated</see>, <see cref="StickerFormat.Video">Video</see></summary>
    [JsonPropertyName("sticker_format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required StickerFormat StickerFormat { get; set; }
}
