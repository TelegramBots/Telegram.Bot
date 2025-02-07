// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the bot's description.</summary>
public partial class BotDescription
{
    /// <summary>The bot's description</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;

    /// <summary>Implicit conversion to string (Description)</summary>
    public static implicit operator string(BotDescription self) => self.Description;
    /// <summary>Implicit conversion from string (Description)</summary>
    public static implicit operator BotDescription(string description) => new() { Description = description };
}
