using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a file that is already stored somewhere on the Telegram servers
/// </summary>
public class InputFileId : IInputFile
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Id;

    /// <summary>
    /// A file identifier
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// This object represents a file that is already stored somewhere on the Telegram servers
    /// </summary>
    /// <param name="id">A file identifier</param>
    public InputFileId(string id) => Id = id;
}
