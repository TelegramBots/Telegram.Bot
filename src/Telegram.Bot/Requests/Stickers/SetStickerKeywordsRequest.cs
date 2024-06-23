namespace Telegram.Bot.Requests;

/// <summary>Use this method to change search keywords assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.<para>Returns: </para></summary>
public partial class SetStickerKeywordsRequest : RequestBase<bool>
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>A list of 0-20 search keywords for the sticker with total length of up to 64 characters</summary>
    public IEnumerable<string>? Keywords { get; set; }

    /// <summary>Initializes an instance of <see cref="SetStickerKeywordsRequest"/></summary>
    /// <param name="sticker">File identifier of the sticker</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetStickerKeywordsRequest(InputFileId sticker) : this() => Sticker = sticker;

    /// <summary>Instantiates a new <see cref="SetStickerKeywordsRequest"/></summary>
    public SetStickerKeywordsRequest() : base("setStickerKeywords") { }
}
