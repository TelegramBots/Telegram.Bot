// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to replace an existing sticker in a sticker set with a new one. The method is equivalent to calling <see cref="TelegramBotClientExtensions.DeleteStickerFromSet">DeleteStickerFromSet</see>, then <see cref="TelegramBotClientExtensions.AddStickerToSet">AddStickerToSet</see>, then <see cref="TelegramBotClientExtensions.SetStickerPositionInSet">SetStickerPositionInSet</see>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ReplaceStickerInSetRequest() : FileRequestBase<bool>("replaceStickerInSet"), IUserTargetable
{
    /// <summary>User identifier of the sticker set owner</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>File identifier of the replaced sticker</summary>
    [JsonPropertyName("old_sticker")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OldSticker { get; set; }

    /// <summary>An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set remains unchanged.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputSticker Sticker { get; set; }
}
