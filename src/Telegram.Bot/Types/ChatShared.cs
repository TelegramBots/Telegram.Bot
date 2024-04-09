using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about the chat whose identifier was shared with the bot using a
/// <see cref="KeyboardButtonRequestChat"/> button.
/// </summary>
public class ChatShared
{
    /// <summary>
    /// Identifier of the request
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>
    /// Identifier of the shared chat. This number may have more than 32 significant bits and some programming
    /// languages may have difficulty/silent defects in interpreting it. But it has at most 52 significant bits,
    /// so a 64-bit integer or double-precision float type are safe for storing this identifier. The bot may not have
    /// access to the chat and could be unable to use this identifier, unless the chat is already known to the bot by
    /// some other means.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long ChatId { get; set; }

    /// <summary>
    /// Optional. Title of the chat, if the title was requested by the bot.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Optional. Username of the chat, if the username was requested by the bot and available.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    /// <summary>
    /// Optional. Available sizes of the chat photo, if the photo was requested by the bot
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PhotoSize[]? Photo { get; set; }
}
