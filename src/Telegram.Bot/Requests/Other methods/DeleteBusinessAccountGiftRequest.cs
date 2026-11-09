// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Deletes a regular gift owned by a managed business account without receiving Telegram Stars for it. Requires the <em>CanConvertGiftsToStars</em> business bot right. The gift can be deleted only if it isn't limited (the field <em>TotalCount</em> of the <see cref="Gift"/> is omitted) and wasn't refunded.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteBusinessAccountGiftRequest() : RequestBase<bool>("deleteBusinessAccountGift"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the regular gift that should be deleted</summary>
    [JsonPropertyName("owned_gift_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OwnedGiftId { get; set; }
}
