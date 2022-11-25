using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
public class InputFileId : IInputFile
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Id;

    /// <summary>
    ///
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    public InputFileId(string id) => Id = id;
}
