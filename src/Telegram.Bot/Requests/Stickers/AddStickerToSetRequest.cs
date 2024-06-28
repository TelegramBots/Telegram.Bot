namespace Telegram.Bot.Requests;

/// <summary>Use this method to add a new sticker to a set created by the bot. Emoji sticker sets can have up to 200 stickers. Other sticker sets can have up to 120 stickers.<para>Returns: </para></summary>
public partial class AddStickerToSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <summary>User identifier of sticker set owner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set isn't changed.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputSticker Sticker { get; set; }

    /// <summary>Initializes an instance of <see cref="AddStickerToSetRequest"/></summary>
    /// <param name="userId">User identifier of sticker set owner</param>
    /// <param name="name">Sticker set name</param>
    /// <param name="sticker">An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set isn't changed.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public AddStickerToSetRequest(long userId, string name, InputSticker sticker) : this()
    {
        UserId = userId;
        Name = name;
        Sticker = sticker;
    }

    /// <summary>Instantiates a new <see cref="AddStickerToSetRequest"/></summary>
    public AddStickerToSetRequest() : base("addStickerToSet") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Sticker.Sticker is InputFileStream ifs ? ToMultipartFormDataContent(ifs.FileName!, ifs) : base.ToHttpContent();
}
