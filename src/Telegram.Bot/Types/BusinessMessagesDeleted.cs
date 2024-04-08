namespace Telegram.Bot.Types;

/// <summary>
/// This object is received when messages are deleted from a connected business account.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessMessagesDeleted
{
    /// <summary>
    /// Unique identifier of the business connection
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string BusinessConnectionId { get; set; } = default!;

    /// <summary>
    /// Information about a chat in the business account. The bot may not have access to the chat or the corresponding user.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// List of identifiers of deleted messages in the chat of the business account
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int[] MessageIds { get; set; } = default!;
}
