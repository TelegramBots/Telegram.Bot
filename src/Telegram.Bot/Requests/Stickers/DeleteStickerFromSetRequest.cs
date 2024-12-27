namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a sticker from a set created by the bot.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteStickerFromSetRequest() : RequestBase<bool>("deleteStickerFromSet")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }
}
