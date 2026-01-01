// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Converts a given regular gift to Telegram Stars. Requires the <em>CanConvertGiftsToStars</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ConvertGiftToStarsRequest() : RequestBase<bool>("convertGiftToStars"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the regular gift that should be converted to Telegram Stars</summary>
    [JsonPropertyName("owned_gift_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OwnedGiftId { get; set; }
}
