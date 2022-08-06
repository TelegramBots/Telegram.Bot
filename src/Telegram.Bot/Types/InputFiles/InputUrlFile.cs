using Newtonsoft.Json;
using System;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A file identifier
/// </summary>
[JsonConverter(typeof(InputUrlFileConverter))]

public struct InputUrlFile : IInputFile,
    IEquatable<InputUrlFile>
{
    /// <inheritdoc/>
    public FileType FileType => FileType.Url;

    /// <summary>
    /// Id of a file that exists on Telegram servers
    /// </summary>
    public readonly string value;

    /// <summary>
    ///  A file identifier
    /// </summary>
    /// <param name="url"></param>
    public InputUrlFile(string url)
    {
        value = url;
    }

    /// <inheritdoc/>
    public bool Equals(InputUrlFile other) => value.Equals(other.value, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override bool Equals(object obj) => this == (InputUrlFile)obj;

    /// <inheritdoc/>
    public override int GetHashCode() => value.GetHashCode();

    /// <inheritdoc/>
    public static bool operator ==(InputUrlFile left, InputUrlFile right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(InputUrlFile left, InputUrlFile right) => !(left == right);
}
