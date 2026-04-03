// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about an option added to a poll.</summary>
public partial class PollOptionAdded
{
    /// <summary><em>Optional</em>. Message containing the poll to which the option was added, if known. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("poll_message")]
    public Message? PollMessage { get; set; }

    /// <summary>Unique identifier of the added option</summary>
    [JsonPropertyName("option_persistent_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string OptionPersistentId { get; set; } = default!;

    /// <summary>Option text</summary>
    [JsonPropertyName("option_text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string OptionText { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the <see cref="OptionText">OptionText</see></summary>
    [JsonPropertyName("option_text_entities")]
    public MessageEntity[]? OptionTextEntities { get; set; }
}
