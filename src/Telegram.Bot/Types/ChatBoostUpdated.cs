namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a boost added to a chat or changed.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostUpdated
{
    /// <summary>
    /// Chat which was boosted
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; } = default!;

    /// <summary>
    /// Information about the chat boost
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatBoost Boost { get; } = default!;
}
