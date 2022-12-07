using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an HTTP URL for the file to be sent
/// </summary>
public class InputFileUrl : IInputFile
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Url;

    /// <summary>
    /// HTTP URL for the file to be sent
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// This object represents an HTTP URL for the file to be sent
    /// </summary>
    /// <param name="url">HTTP URL for the file to be sent</param>
    public InputFileUrl(string url) => Url = url;

    /// <summary>
    /// This object represents an HTTP URL for the file to be sent
    /// </summary>
    /// <param name="uri">HTTP URL for the file to be sent</param>
    public InputFileUrl(Uri uri) =>
        Url = uri?.AbsoluteUri ?? throw new ArgumentNullException(nameof(uri));
}
