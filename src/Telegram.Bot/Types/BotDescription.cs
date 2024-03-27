namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's description.
/// </summary>
public class BotDescription
{
    /// <summary>
    /// The bot's description
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;
}
