// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about the connection of the bot with a business account.<para>Returns: A <see cref="BusinessConnection"/> object on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetBusinessConnectionRequest() : RequestBase<BusinessConnection>("getBusinessConnection"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }
}
