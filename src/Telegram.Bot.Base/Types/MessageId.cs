using JetBrains.Annotations;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a messageId.
/// </summary>
[PublicAPI]
public class MessageId
{
    /// <summary>
    /// Message identifier in the chat specified in <see cref="Requests.CopyMessageRequest.FromChatId"/>
    /// </summary>
    [JsonPropertyName("message_id")]
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }
}
