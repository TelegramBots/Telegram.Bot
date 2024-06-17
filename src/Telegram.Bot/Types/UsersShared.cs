using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about the users whose identifiers were shared with the bot
/// using a <see cref="KeyboardButtonRequestUsers"/> button.
/// </summary>
public class UsersShared
{
    /// <summary>
    /// Identifier of the request
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>
    /// Information about users shared with the bot.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SharedUser[] Users { get; set; } = [];
}
