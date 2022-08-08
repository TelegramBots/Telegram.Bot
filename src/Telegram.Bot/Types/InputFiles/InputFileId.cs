using System;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
public struct InputFileId : IInputFile,
    IEquatable<InputFileId>
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Id;

    /// <summary>
    /// Id of a file that exists on Telegram servers
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
    public readonly string value;

    /// <summary>
    ///  A file identifier
    /// </summary>
    /// <param name="fileId"></param>
    public InputFileId(string fileId)
    {
        value = fileId;
    }

    /// <inheritdoc/>
    public bool Equals(InputFileId other) => value.Equals(other.value, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => this == (InputFileId)obj;

    /// <inheritdoc/>
    public override int GetHashCode() =>
#if NETCOREAPP3_1_OR_GREATER
        value.GetHashCode(StringComparison.Ordinal);
#else
        value.GetHashCode();
#endif

    /// <inheritdoc/>
    public static bool operator ==(InputFileId left, InputFileId right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(InputFileId left, InputFileId right) => !(left == right);
}
