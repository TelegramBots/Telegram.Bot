// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a topic of a direct messages chat.</summary>
public partial class DirectMessagesTopic
{
    /// <summary>Unique identifier of the topic.</summary>
    [JsonPropertyName("topic_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long TopicId { get; set; }

    /// <summary><em>Optional</em>. Information about the user that created the topic. Currently, it is always present</summary>
    public User? User { get; set; }
}
