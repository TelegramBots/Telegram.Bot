using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputFiles;

/// <summary>
/// Used for sending files to Telegram
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
[JsonConverter(typeof(InputFileConverter))]
public class InputTelegramFile : InputFileStream
{
    /// <summary>
    /// Id of a file that exists on Telegram servers
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FileId { get; private protected set; }

    /// <summary>
    ///  Constructs an <see cref="InputTelegramFile"/> with a <see cref="FileType"/>
    /// </summary>
    protected InputTelegramFile(FileType fileType)
        : base(fileType)
    { }

    /// <summary>
    /// Constructs an <see cref="InputTelegramFile"/> from a <see cref="Stream"/> and a file name
    /// </summary>
    /// <param name="content">A <see cref="Stream"/> containing a file to send</param>
    /// <param name="fileName">A name of the file</param>
    public InputTelegramFile(Stream content, string? fileName = default)
        : base(content, fileName)
    { }

    /// <summary>
    /// Constructs an <see cref="InputTelegramFile"/> with a <paramref name="fileId"/>
    /// </summary>
    /// <param name="fileId">A file identifier</param>
    public InputTelegramFile(string fileId) : base(FileType.Id) => FileId = fileId;

    /// <summary>
    /// Constructs an <see cref="InputTelegramFile"/> from a <see cref="Stream"/>
    /// </summary>
    /// <param name="stream">A <see cref="Stream"/> containing a file to send</param>
    [return: NotNullIfNotNull("stream")]
    public static implicit operator InputTelegramFile?(Stream? stream) =>
        stream is null ? default : new InputTelegramFile(stream);

    /// <summary>
    /// Constructs an <see cref="InputTelegramFile"/> with a <paramref name="fileId"/>
    /// </summary>
    /// <param name="fileId">A file identifier</param>
    [return: NotNullIfNotNull("fileId")]
    public static implicit operator InputTelegramFile?(string? fileId) =>
        fileId is null ? default : new InputTelegramFile(fileId);
}