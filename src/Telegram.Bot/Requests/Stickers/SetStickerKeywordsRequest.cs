// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change search keywords assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetStickerKeywordsRequest() : RequestBase<bool>("setStickerKeywords")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>A list of 0-20 search keywords for the sticker with total length of up to 64 characters</summary>
    public IEnumerable<string>? Keywords { get; set; }
}
