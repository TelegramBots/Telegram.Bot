using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a file that is already stored somewhere on the Telegram servers
/// </summary>
[JsonConverter(typeof(InputFileConverter))]
[PublicAPI]
public class InputFileId : InputFile
{
    /// <inheritdoc/>
    public override FileType FileType => FileType.Id;

    /// <summary>
    /// A file identifier
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// This object represents a file that is already stored somewhere on the Telegram servers
    /// </summary>
    public InputFileId()
    {}

    /// <summary>
    /// This object represents a file that is already stored somewhere on the Telegram servers
    /// </summary>
    /// <param name="id">A file identifier</param>
    [SetsRequiredMembers]
    public InputFileId(string id)
        => Id = id;
}
