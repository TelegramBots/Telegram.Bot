namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's short description.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BotShortDescription
{
    /// <summary>
    /// The bot's short description
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string ShortDescription { get; set; } = default!;
}
