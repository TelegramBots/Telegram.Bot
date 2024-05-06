namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the bot's short description.
/// </summary>
public class BotShortDescription
{
    /// <summary>
    /// The bot's short description
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ShortDescription { get; set; } = default!;
}
