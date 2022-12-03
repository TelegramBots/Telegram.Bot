namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a messageId.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MessageId
{
    /// <summary>
    /// Message identifier in the chat specified in <see cref="Requests.CopyMessageRequest.FromChatId"/>
    /// </summary>
    [JsonProperty(Required = Required.Always, PropertyName = "message_id")]
    public int Id { get; set; }
}
