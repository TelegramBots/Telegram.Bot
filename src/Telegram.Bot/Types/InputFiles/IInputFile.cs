using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file to send
/// </summary>
public interface IInputFile
{
    /// <summary>
    /// Type of file to send
    /// </summary>
    FileType FileType { get; }
}
