namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a sticker set that was created by the bot.<para>Returns: </para></summary>
public partial class DeleteStickerSetRequest : RequestBase<bool>
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Initializes an instance of <see cref="DeleteStickerSetRequest"/></summary>
    /// <param name="name">Sticker set name</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public DeleteStickerSetRequest(string name) : this() => Name = name;

    /// <summary>Instantiates a new <see cref="DeleteStickerSetRequest"/></summary>
    public DeleteStickerSetRequest() : base("deleteStickerSet") { }
}
