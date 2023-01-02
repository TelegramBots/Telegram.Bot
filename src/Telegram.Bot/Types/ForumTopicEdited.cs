namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about an edited forum topic.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ForumTopicEdited
{
    /// <summary>
    /// Optional. New name of the topic, if it was edited
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Optional. New identifier of the custom emoji shown as the topic icon, if it was edited; an empty string if the icon was removed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IconCustomEmojiId { get; set; }
}
