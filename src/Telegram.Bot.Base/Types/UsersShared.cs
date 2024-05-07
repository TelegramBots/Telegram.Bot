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
    /// Identifiers of the shared users. These numbers may have more than 32 significant bits and some
    /// programming languages may have difficulty/silent defects in interpreting them. But they have
    /// at most 52 significant bits, so 64-bit integers or double-precision float types are safe for
    /// storing these identifiers. The bot may not have access to the users and could be unable to use
    /// these identifiers, unless the users are already known to the bot by some other means.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Obsolete($"This property is no longer sent by Telegram, use {nameof(Users)} instead")]
    public long[]? UserIds { get; set; } = [];

    /// <summary>
    /// Information about users shared with the bot.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SharedUser[] Users { get; set; } = [];
}
