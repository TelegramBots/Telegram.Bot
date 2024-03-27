using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an HTTP URL for the file to be sent
/// </summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public class InputFileUrl : InputFile
{
    /// <inheritdoc/>
    public override FileType FileType => FileType.Url;

    /// <summary>
    /// HTTP URL for the file to be sent
    /// </summary>
    public required Uri Url { get; init; }

    /// <summary>
    /// This object represents an HTTP URL for the file to be sent
    /// </summary>
    /// <param name="url">HTTP URL for the file to be sent</param>
    [SetsRequiredMembers]
    public InputFileUrl(string url)
        => Url = new(url);

    /// <summary>
    /// This object represents an HTTP URL for the file to be sent
    /// </summary>
    /// <param name="uri">HTTP URL for the file to be sent</param>
    [SetsRequiredMembers]
    public InputFileUrl(Uri uri)
        => Url = uri;

    /// <summary>
    /// This object represents an HTTP URL for the file to be sent
    /// </summary>
    public InputFileUrl()
    { }
}
