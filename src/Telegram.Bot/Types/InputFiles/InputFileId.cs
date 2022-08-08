using System;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
public record struct InputFileId(string Value) : IInputFile,
    IEquatable<InputFileId>
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Id;
}
