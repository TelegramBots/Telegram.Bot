using System.Collections.Generic;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a list of boosts added to a chat by a user.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class UserChatBoosts
{
    /// <summary>
    /// The list of boosts added to the chat by the user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    IEnumerable<ChatBoost> Boosts { get; set; } = default!;
}
