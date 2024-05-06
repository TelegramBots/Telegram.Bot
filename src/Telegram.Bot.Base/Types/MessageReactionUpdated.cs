using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a change of a reaction on a message performed by a user.
/// </summary>
public class MessageReactionUpdated
{
    /// <summary>
    /// The chat containing the message the user reacted to
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique identifier of the message inside the chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary>
    /// Optional.The user that changed the reaction, if the user isn't anonymous
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// Optional.The chat on behalf of which the reaction was changed, if the user is anonymous
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Chat? ActorChat { get; set; }

    /// <summary>
    /// Date of the change
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>
    /// Previous list of reaction types that were set by the user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionType[] OldReaction { get; set; } = default!;

    /// <summary>
    /// New list of reaction types that have been set by the user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionType[] NewReaction { get; set; } = default!;
}
