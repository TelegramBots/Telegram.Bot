// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a list of boosts added to a chat by a user.</summary>
public partial class UserChatBoosts
{
    /// <summary>The list of boosts added to the chat by the user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoost[] Boosts { get; set; } = default!;

    /// <summary>Implicit conversion to ChatBoost[] (Boosts)</summary>
    public static implicit operator ChatBoost[](UserChatBoosts self) => self.Boosts;
    /// <summary>Implicit conversion from ChatBoost[] (Boosts)</summary>
    public static implicit operator UserChatBoosts(ChatBoost[] boosts) => new() { Boosts = boosts };
}
