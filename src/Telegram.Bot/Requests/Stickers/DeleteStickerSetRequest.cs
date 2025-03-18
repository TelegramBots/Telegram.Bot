// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a sticker set that was created by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteStickerSetRequest() : RequestBase<bool>("deleteStickerSet")
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }
}
