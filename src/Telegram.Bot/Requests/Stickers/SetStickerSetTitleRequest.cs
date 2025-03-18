// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the title of a created sticker set.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetStickerSetTitleRequest() : RequestBase<bool>("setStickerSetTitle")
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Sticker set title, 1-64 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }
}
