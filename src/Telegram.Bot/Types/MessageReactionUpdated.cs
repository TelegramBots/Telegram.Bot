using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a change of a reaction on a message performed by a user.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MessageReactionUpdated
{
    /// <summary>
    /// The chat containing the message the user reacted to
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; } = default!;

    /// <summary>
    /// Unique identifier of the message inside the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; } = default!;

    /// <summary>
    /// Optional.The user that changed the reaction, if the user isn't anonymous
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? User { get; set; }

    /// <summary>
    /// Optional.The chat on behalf of which the reaction was changed, if the user is anonymous
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Chat? ActorChat { get; set; }

    /// <summary>
    /// Date of the change
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; } = default!;

    /// <summary>
    /// Previous list of reaction types that were set by the user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ReactionType[] OldReaction { get; } = default!;

    /// <summary>
    /// New list of reaction types that have been set by the user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ReactionType[] NewReaction { get; } = default!;
}
