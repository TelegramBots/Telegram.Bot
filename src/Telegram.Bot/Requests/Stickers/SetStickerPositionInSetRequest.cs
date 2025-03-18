// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to move a sticker in a set created by the bot to a specific position.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetStickerPositionInSetRequest() : RequestBase<bool>("setStickerPositionInSet")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>New sticker position in the set, zero-based</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Position { get; set; }
}
