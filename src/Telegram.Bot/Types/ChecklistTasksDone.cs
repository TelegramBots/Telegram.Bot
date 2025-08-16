// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about checklist tasks marked as done or not done.</summary>
public partial class ChecklistTasksDone
{
    /// <summary><em>Optional</em>. Message containing the checklist whose tasks were marked as done or not done. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("checklist_message")]
    public Message? ChecklistMessage { get; set; }

    /// <summary><em>Optional</em>. Identifiers of the tasks that were marked as done</summary>
    [JsonPropertyName("marked_as_done_task_ids")]
    public int[]? MarkedAsDoneTaskIds { get; set; }

    /// <summary><em>Optional</em>. Identifiers of the tasks that were marked as not done</summary>
    [JsonPropertyName("marked_as_not_done_task_ids")]
    public int[]? MarkedAsNotDoneTaskIds { get; set; }
}
