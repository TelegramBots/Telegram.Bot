using System.IO;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data in
/// the usual way that files are uploaded via the browser
/// </summary>
public class InputFile : IInputFile
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Stream;

    /// <summary>
    /// File content to upload
    /// </summary>
    public Stream Content { get; }

    /// <summary>
    /// Name of a file to upload using multipart/form-data
    /// </summary>
    public string? FileName { get; }

    /// <summary>
    /// This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data
    /// in the usual way that files are uploaded via the browser.
    /// </summary>
    /// <param name="content">File content to upload</param>
    /// <param name="fileName">Name of a file to upload using multipart/form-data</param>
    public InputFile(Stream content, string? fileName = default) =>
        (Content, FileName) = (content, fileName);
}
