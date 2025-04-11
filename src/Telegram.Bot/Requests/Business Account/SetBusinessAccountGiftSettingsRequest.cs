// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the privacy settings pertaining to incoming gifts in a managed business account. Requires the <em>CanChangeGiftSettings</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetBusinessAccountGiftSettingsRequest() : RequestBase<bool>("setBusinessAccountGiftSettings"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Pass <see langword="true"/>, if a button for sending a gift to the user or by the business account must always be shown in the input field</summary>
    [JsonPropertyName("show_gift_button")]
    public required bool ShowGiftButton { get; set; }

    /// <summary>Types of gifts accepted by the business account</summary>
    [JsonPropertyName("accepted_gift_types")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required AcceptedGiftTypes AcceptedGiftTypes { get; set; }
}
