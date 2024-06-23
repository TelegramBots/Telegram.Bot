namespace Telegram.Bot.Types;

/// <summary>This object represents an animated emoji that displays a random value.</summary>
public partial class Dice
{
    /// <summary>Emoji on which the dice throw animation is based</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Emoji { get; set; } = default!;

    /// <summary>Value of the dice, 1-6 for “🎲”, “🎯” and “🎳” base emoji, 1-5 for “🏀” and “⚽” base emoji, 1-64 for “🎰” base emoji</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Value { get; set; }
}
