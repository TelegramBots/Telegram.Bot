// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a task in a checklist.</summary>
public partial class ChecklistTask
{
    /// <summary>Unique identifier of the task</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }

    /// <summary>Text of the task</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the task text</summary>
    [JsonPropertyName("text_entities")]
    public MessageEntity[]? TextEntities { get; set; }

    /// <summary><em>Optional</em>. User that completed the task; omitted if the task wasn't completed by a user</summary>
    [JsonPropertyName("completed_by_user")]
    public User? CompletedByUser { get; set; }

    /// <summary><em>Optional</em>. Chat that completed the task; omitted if the task wasn't completed by a chat</summary>
    [JsonPropertyName("completed_by_chat")]
    public Chat? CompletedByChat { get; set; }

    /// <summary><em>Optional</em>. Point in time when the task was completed; 0 if the task wasn't completed</summary>
    [JsonPropertyName("completion_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? CompletionDate { get; set; }
}
