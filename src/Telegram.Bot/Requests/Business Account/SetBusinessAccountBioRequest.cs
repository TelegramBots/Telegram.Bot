// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the bio of a managed business account. Requires the <em>CanChangeBio</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetBusinessAccountBioRequest() : RequestBase<bool>("setBusinessAccountBio"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>The new value of the bio for the business account; 0-140 characters</summary>
    public string? Bio { get; set; }
}
