namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a change in auto-delete timer settings.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MessageAutoDeleteTimerChanged
{
    /// <summary>
    /// New auto-delete time for messages in the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageAutoDeleteTime { get; set; }
}
