namespace Telegram.Bot.Types;

/// <summary>This object represents a list of boosts added to a chat by a user.</summary>
public partial class UserChatBoosts
{
    /// <summary>The list of boosts added to the chat by the user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoost[] Boosts { get; set; } = default!;
}
