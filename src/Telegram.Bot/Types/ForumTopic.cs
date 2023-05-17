using Telegram.Bot.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a forum topic.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ForumTopic
{
    /// <summary>
    /// Unique identifier of the forum topic
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageThreadId { get; set; }

    /// <summary>
    /// Name of the topic
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Color of the topic icon in RGB format
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(ColorConverter))]
    public Color IconColor { get; set; }

    /// <summary>
    /// Optional. Unique identifier of the custom emoji shown as the topic icon
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IconCustomEmojiId { get; set; }
}
