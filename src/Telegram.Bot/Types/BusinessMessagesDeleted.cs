namespace Telegram.Bot.Types;

/// <summary>
/// This object is received when messages are deleted from a connected business account.
/// </summary>
public class BusinessMessagesDeleted
{
    /// <summary>
    /// Unique identifier of the business connection
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BusinessConnectionId { get; set; } = default!;

    /// <summary>
    /// Information about a chat in the business account. The bot may not have access to the chat or the corresponding user.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// List of identifiers of deleted messages in the chat of the business account
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] MessageIds { get; set; } = default!;
}
