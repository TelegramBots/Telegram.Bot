namespace Telegram.Bot.Types.Enums;

/// <summary>Type of <see cref="Poll"/></summary>
/// <remarks>This enum is used only in the library APIs and is not present in types that are coming from Telegram servers for compatibility reasons</remarks>
[JsonConverter(typeof(EnumConverter<PollType>))]
public enum PollType
{
    /// <summary>Regular poll</summary>
    Regular = 1,
    /// <summary>Quiz</summary>
    Quiz,
}
