using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about the user whose identifier was shared with the bot using a
/// <see cref="KeyboardButtonRequestUser"/> button.
/// </summary>
[Obsolete($"This type is deprecated, {nameof(UserShared)} is used instead")]
public class UserShared
{
    /// <summary>
    /// Identifier of the request
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>
    /// Identifier of the shared user. This number may have more than 32 significant bits and some programming
    /// languages may have difficulty/silent defects in interpreting it. But it has at most 52 significant bits,
    /// so a 64-bit integer or double-precision float type are safe for storing this identifier. The bot may not have
    /// access to the user and could be unable to use this identifier, unless the user is already known to the bot by
    /// some other means.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long UserId { get; set; }
}
