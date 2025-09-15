// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a task to add to a checklist.</summary>
public partial class InputChecklistTask
{
    /// <summary>Unique identifier of the task; must be positive and unique among all task identifiers currently present in the checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }

    /// <summary>Text of the task; 1-100 characters after entities parsing</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Mode for parsing entities in the text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the text, which can be specified instead of <see cref="ParseMode">ParseMode</see>. Currently, only <em>bold</em>, <em>italic</em>, <em>underline</em>, <em>strikethrough</em>, <em>spoiler</em>, and <em>CustomEmoji</em> entities are allowed.</summary>
    [JsonPropertyName("text_entities")]
    public MessageEntity[]? TextEntities { get; set; }
}
