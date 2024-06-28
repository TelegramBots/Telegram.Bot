namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the <see cref="MaskPosition">mask position</see> of a mask sticker. The sticker must belong to a sticker set that was created by the bot.<para>Returns: </para></summary>
public partial class SetStickerMaskPositionRequest : RequestBase<bool>
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>An object with the position where the mask should be placed on faces. Omit the parameter to remove the mask position.</summary>
    public MaskPosition? MaskPosition { get; set; }

    /// <summary>Initializes an instance of <see cref="SetStickerMaskPositionRequest"/></summary>
    /// <param name="sticker">File identifier of the sticker</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetStickerMaskPositionRequest(InputFileId sticker) : this() => Sticker = sticker;

    /// <summary>Instantiates a new <see cref="SetStickerMaskPositionRequest"/></summary>
    public SetStickerMaskPositionRequest() : base("setStickerMaskPosition") { }
}
