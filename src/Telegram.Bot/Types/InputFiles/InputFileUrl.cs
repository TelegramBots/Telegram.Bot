using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
public class InputFileUrl : IInputFile
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Url;

    /// <summary>
    ///
    /// </summary>
    public string Url { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    public InputFileUrl(string url) => Url = url;
}
