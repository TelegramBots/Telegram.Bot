// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a sticker from a set created by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteStickerFromSetRequest() : RequestBase<bool>("deleteStickerFromSet")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }
}
