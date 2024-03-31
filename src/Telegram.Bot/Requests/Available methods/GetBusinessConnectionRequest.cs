using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to get information about the connection of the bot with a business account.
/// Returns a <see cref="BusinessConnection"/> object on success.
/// </summary>
public class GetBusinessConnectionRequest : RequestBase<BusinessConnection>
{
    /// <summary>
    /// Unique identifier of the business connection
    /// </summary>
    public required string BusinessConnectionId { get; init; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    [SetsRequiredMembers]
    public GetBusinessConnectionRequest(string businessConnectionId)
        : base("getBusinessConnection")
    {
        BusinessConnectionId = businessConnectionId;
    }


    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetBusinessConnectionRequest()
        : base("getBusinessConnection")
    { }
}
