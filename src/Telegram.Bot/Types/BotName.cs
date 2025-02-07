// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the bot's name.</summary>
public partial class BotName
{
    /// <summary>The bot's name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Implicit conversion to string (Name)</summary>
    public static implicit operator string(BotName self) => self.Name;
    /// <summary>Implicit conversion from string (Name)</summary>
    public static implicit operator BotName(string name) => new() { Name = name };
}
