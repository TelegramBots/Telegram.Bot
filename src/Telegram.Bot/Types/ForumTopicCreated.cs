namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a new forum topic created in the chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ForumTopicCreated
{
    /// <summary>
    /// Name of the topic
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Color of the topic icon in RGB format
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int IconColor { get; set; }

    /// <summary>
    /// Optional. Unique identifier of the custom emoji shown as the topic icon
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IconCustomEmojiId { get; set; }
}
