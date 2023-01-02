using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// Represents a ChatId
/// </summary>
[JsonConverter(typeof(ChatIdConverter))]
public class ChatId : IEquatable<ChatId>
{
    /// <summary>
    /// Unique identifier for the chat
    /// </summary>
    public long? Identifier { get; }

    /// <summary>
    /// Username of the supergroup or channel (in the format @channelusername)
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat
    /// </summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public ChatId(long identifier) => Identifier = identifier;

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)
    /// </summary>
    /// <param name="username">Unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)</param>
    /// <exception cref="ArgumentException">
    /// Thrown when string value isn`t number and doesn't start with @
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when string value is <c>null</c></exception>
    public ChatId(string username)
    {
        if (username is null) { throw new ArgumentNullException(nameof(username)); }
        if (username.Length > 1 && username[0] == '@')
        {
            Username = username;
        }
        else if (long.TryParse(
            s: username,
            style: NumberStyles.Integer,
            provider: CultureInfo.InvariantCulture,
            result: out var identifier))
        {
            Identifier = identifier;
        }
        else
        {
            throw new ArgumentException("Username value should be Identifier or Username that starts with @", nameof(username));
        }
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj) =>
        obj switch
        {
            ChatId chatId => this == chatId,
            _ => false,
        };

    /// <inheritdoc />
    public bool Equals(ChatId? other) => this == other;

    /// <summary>
    /// Gets the hash code of this object
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => StringComparer.InvariantCulture.GetHashCode(ToString());

    /// <summary>
    /// Create a <c>string</c> out of a <see cref="ChatId"/>
    /// </summary>
    /// <returns>The <see cref="ChatId"/> as <c>string</c></returns>
    public override string ToString() => (Username ?? Identifier?.ToString(CultureInfo.InvariantCulture))!;

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat
    /// </summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    public static implicit operator ChatId(long identifier) => new(identifier);

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)
    /// </summary>
    /// <param name="username">Unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)</param>
    /// <exception cref="ArgumentException">
    /// Thrown when string value isn`t number and doesn't start with @
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when string value is <c>null</c></exception>
    public static implicit operator ChatId(string username) => new(username);

    /// <summary>
    /// Convert a Chat Object to a <see cref="ChatId"/>
    /// </summary>
    /// <param name="chat"></param>
    [return: NotNullIfNotNull("chat")]
    public static implicit operator ChatId?(Chat? chat) => chat is null ? null : new(chat.Id);

    /// <summary>
    /// Compares two ChatId objects
    /// </summary>
    public static bool operator ==(ChatId? obj1, ChatId? obj2)
    {
        if (obj1 is null || obj2 is null) { return false; }

        if (obj1.Identifier is not null && obj2.Identifier is not null)
        {
            return obj1.Identifier == obj2.Identifier;
        }

        if (obj1.Username is not null && obj2.Username is not null)
        {
            return string.Equals(obj1.Username, obj2.Username, StringComparison.InvariantCulture);
        }

        return false;
    }

    /// <summary>
    /// Compares two ChatId objects
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    public static bool operator !=(ChatId obj1, ChatId obj2) => !(obj1 == obj2);
}
