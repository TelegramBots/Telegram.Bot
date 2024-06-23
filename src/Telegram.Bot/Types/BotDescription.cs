namespace Telegram.Bot.Types;

/// <summary>This object represents the bot's description.</summary>
public partial class BotDescription
{
    /// <summary>The bot's description</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;
}
