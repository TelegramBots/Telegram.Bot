namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of a <see cref="IInputFile"/>
/// </summary>
[JsonConverter(typeof(FileTypeConverter))]
public enum FileType
{
    /// <summary>
    /// FileStream
    /// </summary>
    Stream = 1,

    /// <summary>
    /// FileId
    /// </summary>
    Id,

    /// <summary>
    /// File Url
    /// </summary>
    Url
}
