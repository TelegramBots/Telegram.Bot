using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents reaction changes on a message with anonymous reactions.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MessageReactionCountUpdated
{
    /// <summary>
    /// The chat containing the message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique message identifier inside the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; set; } = default!;

    /// <summary>
    /// Date of the change
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; } = default!;

    /// <summary>
    /// List of reactions that are present on the message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ReactionCount[] Reactions { get; set; } = default!;
}
