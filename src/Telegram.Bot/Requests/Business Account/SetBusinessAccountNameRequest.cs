// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the first and last name of a managed business account. Requires the <em>CanChangeName</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetBusinessAccountNameRequest() : RequestBase<bool>("setBusinessAccountName"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>The new value of the first name for the business account; 1-64 characters</summary>
    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FirstName { get; set; }

    /// <summary>The new value of the last name for the business account; 0-64 characters</summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
}
