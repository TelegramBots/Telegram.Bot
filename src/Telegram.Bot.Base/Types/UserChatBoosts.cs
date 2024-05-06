using System.Collections.Generic;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a list of boosts added to a chat by a user.
/// </summary>
public class UserChatBoosts
{
    /// <summary>
    /// The list of boosts added to the chat by the user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<ChatBoost> Boosts { get; set; } = default!;
}
