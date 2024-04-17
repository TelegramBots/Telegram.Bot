namespace Telegram.Bot.Types;

/// <summary>
/// This object describes a message that was deleted or is otherwise inaccessible to the bot.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InaccessibleMessage : MaybeInaccessibleMessage
{
    /// <summary>
    /// Chat the message belonged to
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique message identifier inside the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; set; }

    /// <summary>
    /// Always 0. The field can be used to differentiate regular and inaccessible messages.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Date { get; set; }
}
