// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to add a new sticker to a set created by the bot. Emoji sticker sets can have up to 200 stickers. Other sticker sets can have up to 120 stickers.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AddStickerToSetRequest() : FileRequestBase<bool>("addStickerToSet"), IUserTargetable
{
    /// <summary>User identifier of sticker set owner</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set isn't changed.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputSticker Sticker { get; set; }
}
