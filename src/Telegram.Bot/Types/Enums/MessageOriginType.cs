namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Message origin type
/// </summary>
[JsonConverter(typeof(MessageOriginTypeConverter))]
public enum MessageOriginType
{
    /// <summary>
    ///
    /// </summary>
    User = 1,

    /// <summary>
    ///
    /// </summary>
    HiddenUser,

    /// <summary>
    ///
    /// </summary>
    Chat,

    /// <summary>
    ///
    /// </summary>
    Channel,
}
