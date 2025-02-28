// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the bot's short description.</summary>
public partial class BotShortDescription
{
    /// <summary>The bot's short description</summary>
    [JsonPropertyName("short_description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ShortDescription { get; set; } = default!;

    /// <summary>Implicit conversion to string (ShortDescription)</summary>
    public static implicit operator string(BotShortDescription self) => self.ShortDescription;
    /// <summary>Implicit conversion from string (ShortDescription)</summary>
    public static implicit operator BotShortDescription(string shortDescription) => new() { ShortDescription = shortDescription };
}
