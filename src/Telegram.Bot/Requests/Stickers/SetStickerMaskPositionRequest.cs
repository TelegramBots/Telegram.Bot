// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the <see cref="MaskPosition">mask position</see> of a mask sticker. The sticker must belong to a sticker set that was created by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetStickerMaskPositionRequest() : RequestBase<bool>("setStickerMaskPosition")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>An object with the position where the mask should be placed on faces. Omit the parameter to remove the mask position.</summary>
    [JsonPropertyName("mask_position")]
    public MaskPosition? MaskPosition { get; set; }
}
