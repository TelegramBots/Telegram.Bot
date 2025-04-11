// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Returns the amount of Telegram Stars owned by a managed business account. Requires the <em>CanViewGiftsAndStars</em> business bot right.<para>Returns: <see cref="StarAmount"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetBusinessAccountStarBalanceRequest() : RequestBase<StarAmount>("getBusinessAccountStarBalance"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }
}
