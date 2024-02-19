namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Message origin type
/// </summary>
[JsonConverter(typeof(MessageOriginTypeConverter))]
public enum MessageOriginType
{
    /// <summary>
    /// Message origin is from a user
    /// </summary>
    User = 1,

    /// <summary>
    /// Message origin is from a hidden user
    /// </summary>
    HiddenUser,

    /// <summary>
    /// Message origin is a chat
    /// </summary>
    Chat,

    /// <summary>
    /// Message origin is a channel
    /// </summary>
    Channel,
}
