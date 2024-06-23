namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a sticker from a set created by the bot.<para>Returns: </para></summary>
public partial class DeleteStickerFromSetRequest : RequestBase<bool>
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>Initializes an instance of <see cref="DeleteStickerFromSetRequest"/></summary>
    /// <param name="sticker">File identifier of the sticker</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public DeleteStickerFromSetRequest(InputFileId sticker) : this() => Sticker = sticker;

    /// <summary>Instantiates a new <see cref="DeleteStickerFromSetRequest"/></summary>
    public DeleteStickerFromSetRequest() : base("deleteStickerFromSet") { }
}
