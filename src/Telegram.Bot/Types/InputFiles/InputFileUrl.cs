using System;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
public record struct InputFileUrl(string Value) : IInputFile,
    IEquatable<InputFileUrl>
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Url;
}
