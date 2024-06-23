namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about the connection of the bot with a business account.<para>Returns: A <see cref="BusinessConnection"/> object on success.</para></summary>
public partial class GetBusinessConnectionRequest : RequestBase<BusinessConnection>, IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetBusinessConnectionRequest"/></summary>
    /// <param name="businessConnectionId">Unique identifier of the business connection</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetBusinessConnectionRequest(string businessConnectionId) : this() => BusinessConnectionId = businessConnectionId;

    /// <summary>Instantiates a new <see cref="GetBusinessConnectionRequest"/></summary>
    public GetBusinessConnectionRequest() : base("getBusinessConnection") { }
}
