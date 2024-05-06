namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about an edited forum topic.
/// </summary>
public class ForumTopicEdited
{
    /// <summary>
    /// Optional. New name of the topic, if it was edited
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Optional. New identifier of the custom emoji shown as the topic icon, if it was edited; an empty string if the icon was removed
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IconCustomEmojiId { get; set; }
}
