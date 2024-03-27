
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a forum topic.
/// </summary>
public class ForumTopic
{
    /// <summary>
    /// Unique identifier of the forum topic
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageThreadId { get; set; }

    /// <summary>
    /// Name of the topic
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Color of the topic icon in RGB format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Color IconColor { get; set; }

    /// <summary>
    /// Optional. Unique identifier of the custom emoji shown as the topic icon
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IconCustomEmojiId { get; set; }
}
