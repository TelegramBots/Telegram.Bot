using System.IO;
using JetBrains.Annotations;
using Telegram.Bot.Converters;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file to send
/// </summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public abstract class InputFile
{
    /// <summary>
    /// Type of file to send
/// </summary>
    public abstract FileType FileType { get; }

    /// <summary>
    /// Creates a correct <see cref="InputFile"/> object
    /// </summary>
    /// <param name="urlOrFileId">A URL or a file id</param>
    /// <returns></returns>
    public static InputFile FromString(string urlOrFileId) =>
        Uri.TryCreate(urlOrFileId, UriKind.Absolute, out var url)
            ? new InputFileUrl(url)
            : new InputFileId(urlOrFileId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static InputFileStream FromStream(Stream stream, string? fileName = default) =>
        new(stream.ThrowIfNull(), fileName);

    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static InputFileUrl FromUri(Uri url) => new(url.ThrowIfNull());

    /// <summary>
    ///
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public static InputFileId FromFileId(string fileId) => new(fileId.ThrowIfNull());

    /// <summary>
    ///
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static explicit operator InputFile(Stream stream) => FromStream(stream);

    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static explicit operator InputFile(Uri url) => FromUri(url);

    /// <summary>
    ///
    /// </summary>
    /// <param name="urlOrFileId"></param>
    /// <returns></returns>
    public static explicit operator InputFile(string urlOrFileId) => FromString(urlOrFileId);
}
