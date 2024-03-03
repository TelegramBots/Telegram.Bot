namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a user boosting a chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostAdded
{
    /// <summary>
    /// Number of boosts added by the user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int BoostCount { get; set; }
}
