// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a checklist to create.</summary>
public partial class InputChecklist
{
    /// <summary>Title of the checklist; 1-255 characters after entities parsing</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Title { get; set; } = default!;

    /// <summary><em>Optional</em>. Mode for parsing entities in the title. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the title, which can be specified instead of <see cref="ParseMode">ParseMode</see>. Currently, only <em>bold</em>, <em>italic</em>, <em>underline</em>, <em>strikethrough</em>, <em>spoiler</em>, and <em>CustomEmoji</em> entities are allowed.</summary>
    [JsonPropertyName("title_entities")]
    public MessageEntity[]? TitleEntities { get; set; }

    /// <summary>List of 1-30 tasks in the checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputChecklistTask> Tasks { get; set; } = default!;

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if other users can add tasks to the checklist</summary>
    [JsonPropertyName("others_can_add_tasks")]
    public bool OthersCanAddTasks { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if other users can mark tasks as done or not done in the checklist</summary>
    [JsonPropertyName("others_can_mark_tasks_as_done")]
    public bool OthersCanMarkTasksAsDone { get; set; }
}
