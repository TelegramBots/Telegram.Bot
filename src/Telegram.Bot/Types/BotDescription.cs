namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's description.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BotDescription
{
    /// <summary>
    /// The bot's description
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Description { get; set; } = default!;
}
