// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represent a list of gifts.</summary>
public partial class GiftList
{
    /// <summary>The list of gifts</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Gift[] Gifts { get; set; } = default!;

    /// <summary>Implicit conversion to Gift[] (Gifts)</summary>
    public static implicit operator Gift[](GiftList self) => self.Gifts;
    /// <summary>Implicit conversion from Gift[] (Gifts)</summary>
    public static implicit operator GiftList(Gift[] gifts) => new() { Gifts = gifts };
}
