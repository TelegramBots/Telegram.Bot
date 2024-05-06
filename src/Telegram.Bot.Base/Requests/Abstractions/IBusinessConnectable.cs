namespace Telegram.Bot.Requests.Abstractions;

/// <summary>
/// Indicates that a request is sent on behalf of a business connection
/// </summary>
public interface IBusinessConnectable
{
    /// <summary>
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </summary>
    string? BusinessConnectionId { get; }
}
