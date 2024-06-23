namespace Telegram.Bot.Requests;

/// <summary>Use this method to replace an existing sticker in a sticker set with a new one. The method is equivalent to calling <see cref="TelegramBotClientExtensions.DeleteStickerFromSetAsync">DeleteStickerFromSet</see>, then <see cref="TelegramBotClientExtensions.AddStickerToSetAsync">AddStickerToSet</see>, then <see cref="TelegramBotClientExtensions.SetStickerPositionInSetAsync">SetStickerPositionInSet</see>.<para>Returns: </para></summary>
public partial class ReplaceStickerInSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <summary>User identifier of the sticker set owner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>File identifier of the replaced sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OldSticker { get; set; }

    /// <summary>An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set remains unchanged.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputSticker Sticker { get; set; }

    /// <summary>Initializes an instance of <see cref="ReplaceStickerInSetRequest"/></summary>
    /// <param name="userId">User identifier of the sticker set owner</param>
    /// <param name="name">Sticker set name</param>
    /// <param name="oldSticker">File identifier of the replaced sticker</param>
    /// <param name="sticker">An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set remains unchanged.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public ReplaceStickerInSetRequest(long userId, string name, string oldSticker, InputSticker sticker) : this()
    {
        UserId = userId;
        Name = name;
        OldSticker = oldSticker;
        Sticker = sticker;
    }

    /// <summary>Instantiates a new <see cref="ReplaceStickerInSetRequest"/></summary>
    public ReplaceStickerInSetRequest() : base("replaceStickerInSet") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Sticker.Sticker is InputFileStream ifs ? ToMultipartFormDataContent(ifs.FileName!, ifs) : base.ToHttpContent();
}
