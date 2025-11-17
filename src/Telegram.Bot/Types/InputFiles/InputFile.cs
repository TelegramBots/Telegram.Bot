// This file is NOT auto-generated
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>A file to send</summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public abstract class InputFile
{
    /// <summary>Type of file to send</summary>
    public abstract FileType FileType { get; }

    /// <summary>Creates an instance of <see cref="InputFile"/> from a string containing a file's URL or file id</summary>
    /// <param name="urlOrFileId">A file's URL or a file id</param>
    /// <returns>An instance of a class that implements <see cref="InputFile"/></returns>
    public static InputFile FromString(string urlOrFileId) =>
        Uri.TryCreate(urlOrFileId, UriKind.Absolute, out var url) ? FromUri(url) : FromFileId(urlOrFileId);

    /// <summary>Creates an <see cref="InputFileStream"/> from an instance <see cref="Stream"/></summary>
    /// <param name="stream">A <see cref="Stream"/> with file data to upload</param>
    /// <param name="fileName">An optional file name. If unspecified, it may be extracted from FileStream</param>
    /// <returns>An instance of <see cref="InputFileStream"/></returns>
    public static InputFileStream FromStream(Stream stream, string? fileName = default) =>
        new(stream.ThrowIfNull(), fileName ?? Path.GetFileName((stream as FileStream)?.Name));

    /// <summary>Creates an <see cref="InputFileUrl"/> from an <see cref="Uri"/></summary>
    /// <param name="url">A URL of a file</param>
    /// <returns>An instance of <see cref="InputFileUrl"/></returns>
    public static InputFileUrl FromUri(Uri url) => new(url.ThrowIfNull());

    /// <summary>Creates an <see cref="InputFileUrl"/> from a URL passed as a <see cref="string"/></summary>
    /// <param name="url">A URL of a file</param>
    /// <returns>An instance of <see cref="InputFileUrl"/></returns>
    public static InputFileUrl FromUri(string url) => new(url.ThrowIfNull());

    /// <summary>Creates an <see cref="InputFileId"/> from a file id</summary>
    /// <param name="fileId">An ID of a file</param>
    /// <returns>An instance of <see cref="InputFileId"/></returns>
    public static InputFileId FromFileId(string fileId) => new(fileId.ThrowIfNull());

    /// <summary>Implicit operator, same as <see cref="FromStream"/></summary>
    public static implicit operator InputFile(Stream stream) => FromStream(stream);

    /// <summary>Implicit operator, same as <see cref="FromString"/>, but returns null for a null string</summary>
    [return:NotNullIfNotNull(nameof(urlOrFileId))]
    public static implicit operator InputFile?(string? urlOrFileId) => urlOrFileId is null ? null : FromString(urlOrFileId);

    /// <summary>Implicit operator, using <see cref="FileBase.FileId"/> property</summary>
    public static implicit operator InputFile(FileBase file) => FromFileId(file.FileId);
}

/// <summary>This object represents a file that is already stored somewhere on the Telegram servers</summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public class InputFileId : InputFile
{
    /// <inheritdoc/>
    public override FileType FileType => FileType.Id;

    /// <summary>A file identifier</summary>
    public required string Id { get; set; }

    /// <summary>This object represents a file that is already stored somewhere on the Telegram servers</summary>
    public InputFileId() { }

    /// <summary>This object represents a file that is already stored somewhere on the Telegram servers</summary>
    /// <param name="id">A file identifier</param>
    [SetsRequiredMembers]
    public InputFileId(string id) => Id = id;

    /// <summary>Implicit operator, same as <see cref="InputFileId(string)"/>, but returns null for a null string</summary>
    [return:NotNullIfNotNull(nameof(fileId))]
    public static implicit operator InputFileId?(string? fileId) => fileId is null ? null : new(fileId);

    /// <summary>Returns the Id</summary>
    public override string ToString() => Id;
}

/// <summary>This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data in the usual way that files are uploaded via the browser</summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public class InputFileStream : InputFile
{
    /// <inheritdoc/>
    public override FileType FileType => FileType.Stream;

    /// <summary>File content to upload</summary>
    public required Stream Content { get; set; }

    /// <summary>Name of a file to upload using multipart/form-data</summary>
    public string? FileName { get; }

    /// <summary>This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data in the usual way that files are uploaded via the browser.</summary>
    /// <param name="content">File content to upload</param>
    /// <param name="fileName">Name of a file to upload using multipart/form-data</param>
    [SetsRequiredMembers]
    public InputFileStream(Stream content, string? fileName = default) => (Content, FileName) = (content, fileName);

    /// <summary>This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data in the usual way that files are uploaded via the browser.</summary>
    public InputFileStream() { }

    /// <summary>Implicit operator, same as <see cref="InputFileStream(Stream,string)"/> without given filename</summary>
    public static implicit operator InputFileStream(Stream stream) => new(stream);

    /// <summary>Returns "stream://realfilename" for a FileStream content, or "stream://0" otherwise</summary>
    public override string ToString() => Content is FileStream fs ? "stream://" + Path.GetFileName(fs.Name) : "stream://0";
}

/// <summary>This object represents an HTTP URL for the file to be sent</summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public class InputFileUrl : InputFile
{
    /// <inheritdoc/>
    public override FileType FileType => FileType.Url;

    /// <summary>HTTP URL for the file to be sent</summary>
    public required Uri Url { get; set; }

    /// <summary>This object represents an HTTP URL for the file to be sent</summary>
    /// <param name="url">HTTP URL for the file to be sent</param>
    [SetsRequiredMembers]
    public InputFileUrl(string url) => Url = new(url);

    /// <summary>This object represents an HTTP URL for the file to be sent</summary>
    /// <param name="uri">HTTP URL for the file to be sent</param>
    [SetsRequiredMembers]
    public InputFileUrl(Uri uri) => Url = uri;

    /// <summary>This object represents an HTTP URL for the file to be sent</summary>
    public InputFileUrl() { }

    /// <summary>Implicit operator, same as <see cref="InputFileUrl(string)"/></summary>
    public static implicit operator InputFileUrl(string url) => new(url);

    /// <summary>Returns the Url</summary>
    public override string ToString() => Url.ToString();
}
