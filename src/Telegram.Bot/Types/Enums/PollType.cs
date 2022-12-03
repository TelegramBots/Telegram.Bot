namespace Telegram.Bot.Types.Enums;

/// <summary>
/// <see cref="Poll"/> type
/// <remarks>
/// This enum is used only in the library APIs and is not present in types that are coming from
/// Telegram servers for compatibility reasons
/// </remarks>
/// </summary>
[JsonConverter(typeof(PollTypeConverter))]
public enum PollType
{
    /// <summary>
    /// Regular poll
    /// </summary>
    Regular = 1,

    /// <summary>
    /// Quiz
    /// </summary>
    Quiz
}
