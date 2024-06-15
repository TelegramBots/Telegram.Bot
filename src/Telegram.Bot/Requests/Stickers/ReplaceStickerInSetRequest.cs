// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to replace an existing sticker in a sticker set with a new one. The method is equivalent to
/// calling <see cref="DeleteStickerFromSetRequest"/>, then <see cref="AddStickerToSetRequest"/>,
/// then <see cref="SetStickerPositionInSetRequest"/>. Returns True on success.
/// </summary>
public class ReplaceStickerInSetRequest : RequestBase<bool>
{
    /// <summary>
    /// User identifier of the sticker set owner
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; init; }

    /// <summary>
    /// File identifier of the replaced sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OldSticker { get; init; }

    /// <summary>
    /// An object with information about the added sticker. If exactly the same sticker had already been added to the
    /// set, then the set remains unchanged.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputSticker Sticker { get; init; }

    /// <summary>
    /// Initializes a new request with name
    /// </summary>
    public ReplaceStickerInSetRequest()
        : base("replaceStickerInSet")
    { }
}
