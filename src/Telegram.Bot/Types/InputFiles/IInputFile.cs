using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file to send
/// </summary>
[JsonConverter(typeof(InputFileConverter))]
public interface IInputFile
{
    /// <summary>
    /// Type of file to send
    /// </summary>
    FileType FileType { get; }
}
