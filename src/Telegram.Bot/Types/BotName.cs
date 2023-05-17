namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's name.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BotName
{
    /// <summary>
    /// The bot's name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = default!;
}
