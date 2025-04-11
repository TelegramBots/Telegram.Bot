// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the username of a managed business account. Requires the <em>CanChangeUsername</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetBusinessAccountUsernameRequest() : RequestBase<bool>("setBusinessAccountUsername"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>The new value of the username for the business account; 0-32 characters</summary>
    public string? Username { get; set; }
}
