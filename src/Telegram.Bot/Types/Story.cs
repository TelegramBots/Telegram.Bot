namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a message about a forwarded story in the chat. Currently holds no information.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Story
{

    /// <summary>
    /// Chat that posted the story
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique identifier for the story in the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Id { get; set; }
}
