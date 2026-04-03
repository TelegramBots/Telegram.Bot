// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about one answer option in a poll.</summary>
public partial class PollOption
{
    /// <summary>Unique identifier of the option, persistent on option addition and deletion</summary>
    [JsonPropertyName("persistent_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PersistentId { get; set; } = default!;

    /// <summary>Option text, 1-100 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the option <see cref="Text">Text</see>. Currently, only custom emoji entities are allowed in poll option texts</summary>
    [JsonPropertyName("text_entities")]
    public MessageEntity[]? TextEntities { get; set; }

    /// <summary>Number of users who voted for this option; may be 0 if unknown</summary>
    [JsonPropertyName("voter_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int VoterCount { get; set; }

    /// <summary><em>Optional</em>. User who added the option; omitted if the option wasn't added by a user after poll creation</summary>
    [JsonPropertyName("added_by_user")]
    public User? AddedByUser { get; set; }

    /// <summary><em>Optional</em>. Chat that added the option; omitted if the option wasn't added by a chat after poll creation</summary>
    [JsonPropertyName("added_by_chat")]
    public Chat? AddedByChat { get; set; }

    /// <summary><em>Optional</em>. Point in time when the option was added; omitted if the option existed in the original poll</summary>
    [JsonPropertyName("addition_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? AdditionDate { get; set; }
}
