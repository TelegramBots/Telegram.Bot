namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's name.
/// </summary>
public class BotName
{
    /// <summary>
    /// The bot's name
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}
