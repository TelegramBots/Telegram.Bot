// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about tasks added to a checklist.</summary>
public partial class ChecklistTasksAdded
{
    /// <summary><em>Optional</em>. Message containing the checklist to which the tasks were added. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("checklist_message")]
    public Message? ChecklistMessage { get; set; }

    /// <summary>List of tasks added to the checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChecklistTask[] Tasks { get; set; } = default!;
}
