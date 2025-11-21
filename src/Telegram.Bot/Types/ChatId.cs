using System.Globalization;
namespace Telegram.Bot.Types;

/// <summary>Represents a ChatId</summary>
[JsonConverter(typeof(ChatIdConverter))]
public class ChatId : IEquatable<ChatId>
{
    /// <summary>Unique identifier for the chat</summary>
    public long? Identifier { get; }

    /// <summary>Username of the supergroup or channel (in the format @channelusername)</summary>
    public string? Username { get; }

    /// <summary>Create a <see cref="ChatId"/> using unique identifier for the chat</summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public ChatId(long identifier) => Identifier = identifier;

	/// <summary>Create a <see cref="ChatId"/> using unique identifier for the chat or username of the supergroup or channel (in the format @channelusername)</summary>
	/// <param name="username">Unique identifier for the chat or username of the supergroup or channel (in the format @channelusername)</param>
	/// <exception cref="ArgumentException">Thrown when string value isn`t number and doesn't start with @</exception>
	/// <exception cref="ArgumentNullException">Thrown when string value is <see langword="null"/></exception>
	public ChatId(string username)
    {
        if (username is null) { throw new ArgumentNullException(nameof(username)); }
        if (username.Length > 1 && username[0] == '@')
            Username = username;
        else if (long.TryParse(username, NumberStyles.Integer, CultureInfo.InvariantCulture, out var identifier))
            Identifier = identifier;
        else
            throw new ArgumentException("Username value should be Identifier or Username that starts with @", nameof(username));
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj) => obj is ChatId chatId && this == chatId;

    /// <inheritdoc/>
    public bool Equals(ChatId? other) => this == other;

    /// <summary>Gets the hash code of this object</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => StringComparer.InvariantCulture.GetHashCode(ToString());

    /// <summary>Create a string out of a <see cref="ChatId"/>/// </summary>
    /// <returns>The <see cref="ChatId"/> as <see langword="string"/></returns>
    public override string ToString() => (Username ?? Identifier?.ToString(CultureInfo.InvariantCulture))!;

    /// <summary>Create a <see cref="ChatId"/> using unique identifier for the chat</summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    public static implicit operator ChatId(long identifier) => new(identifier);

    /// <summary>Create a <see cref="ChatId"/> using unique identifier for the chat or username of the supergroup or channel (in the format @channelusername)</summary>
    /// <param name="username">Unique identifier for the chat or username of the supergroup or channel (in the format @channelusername)</param>
    /// <exception cref="ArgumentException">Thrown when string value isn`t number and doesn't start with @</exception>
    /// <exception cref="ArgumentNullException">Thrown when string value is <see langword="null"/></exception>
    public static implicit operator ChatId(string username) => new(username);

    /// <summary>Convert a <see cref="Chat"/> object to a <see cref="ChatId"/></summary>
    [return: NotNullIfNotNull(nameof(chat))]
    public static implicit operator ChatId?(Chat? chat) => chat is null ? null : new(chat.Id);

    /// <summary>Convert a <see cref="ChatFullInfo"/> Object to a <see cref="ChatId"/></summary>
    [return: NotNullIfNotNull(nameof(chatFullInfo))]
    public static implicit operator ChatId?(ChatFullInfo? chatFullInfo) => chatFullInfo is null ? null : new(chatFullInfo.Id);

    /// <summary>Compares two ChatId objects</summary>
    public static bool operator ==(ChatId? obj1, ChatId? obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;
        if (obj1 is null || obj2 is null) return false;
        return obj1.Identifier == obj2.Identifier && string.Equals(obj1.Username, obj2.Username, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Compares two ChatId objects</summary>
    public static bool operator !=(ChatId obj1, ChatId obj2) => !(obj1 == obj2);
}
