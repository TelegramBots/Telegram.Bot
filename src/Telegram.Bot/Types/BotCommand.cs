namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a bot command.
/// </summary>
public partial class BotCommand
{
    /// <summary>
    /// Text of the command; 1-32 characters. Can contain only lowercase English letters, digits and underscores.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Command { get; set; } = default!;

    /// <summary>
    /// Description of the command; 1-256 characters.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;
}
