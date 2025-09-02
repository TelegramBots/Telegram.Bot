// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Transfers Telegram Stars from the business account balance to the bot's balance. Requires the <em>CanTransferStars</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class TransferBusinessAccountStarsRequest() : RequestBase<bool>("transferBusinessAccountStars"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Number of Telegram Stars to transfer; 1-10000</summary>
    [JsonPropertyName("star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long StarCount { get; set; }
}
