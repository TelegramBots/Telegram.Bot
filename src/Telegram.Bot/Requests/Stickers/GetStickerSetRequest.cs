namespace Telegram.Bot.Requests;

/// <summary>Use this method to get a sticker set.<para>Returns: A <see cref="StickerSet"/> object is returned.</para></summary>
public partial class GetStickerSetRequest : RequestBase<StickerSet>
{
    /// <summary>Name of the sticker set</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Initializes an instance of <see cref="GetStickerSetRequest"/></summary>
    /// <param name="name">Name of the sticker set</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetStickerSetRequest(string name) : this() => Name = name;

    /// <summary>Instantiates a new <see cref="GetStickerSetRequest"/></summary>
    public GetStickerSetRequest() : base("getStickerSet") { }
}
