using System.IO;

namespace Telegram.Bot.Extensions;

/// <summary>
/// TODO
/// </summary>
public static class InputFileExtensions
{
    /// <summary>
    /// Converts a <see cref="Stream"/> to an <see cref="InputFileStream"/>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static InputFileStream ToInputFile(this Stream stream, string? fileName = default) =>
        new(stream.ThrowIfNull(), fileName);
}
