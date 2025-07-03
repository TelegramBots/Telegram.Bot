// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a checklist.</summary>
public partial class Checklist
{
    /// <summary>Title of the checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Title { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the checklist title</summary>
    [JsonPropertyName("title_entities")]
    public MessageEntity[]? TitleEntities { get; set; }

    /// <summary>List of tasks in the checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChecklistTask[] Tasks { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if users other than the creator of the list can add tasks to the list</summary>
    [JsonPropertyName("others_can_add_tasks")]
    public bool OthersCanAddTasks { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if users other than the creator of the list can mark tasks as done or not done</summary>
    [JsonPropertyName("others_can_mark_tasks_as_done")]
    public bool OthersCanMarkTasksAsDone { get; set; }
}
