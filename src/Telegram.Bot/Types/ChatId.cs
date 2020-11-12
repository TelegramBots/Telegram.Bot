using System;
using Newtonsoft.Json;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a ChatId
    /// </summary>
    [JsonConverter(typeof(ChatIdConverter))]
    public class ChatId
    {
        /// <summary>
        /// Unique identifier for the chat
        /// </summary>
        public readonly long Identifier;

        /// <summary>
        /// Username of the channel (in the format @channelusername)
        /// </summary>
        public readonly string Username;

        /// <summary>
        /// Create a <see cref="ChatId"/> using an identifier
        /// </summary>
        /// <param name="identifier">The Identifier</param>
        public ChatId(long identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Create a <see cref="ChatId"/> using an identifier
        /// </summary>
        /// <param name="chatId">The Identifier</param>
        public ChatId(int chatId)
        {
            Identifier = chatId;
        }

        /// <summary>
        /// Create a <see cref="ChatId"/> using an user name
        /// </summary>
        /// <param name="username">The user name</param>
        /// <exception cref="ArgumentException">Thrown when string value isn`t number and doesn't start with @</exception>
        public ChatId(string username)
        {
            if (username.Length > 1 && username.StartsWith("@"))
            {
                Username = username;
            }
            else if (long.TryParse(username, out long identifier))
            {
                Identifier = identifier;
            }
            else
            {
                throw new ArgumentException($"Username value should be Identifier or Username that starts with @");
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ChatId chatId)
            {
                return this == chatId;
            }

            return ((string) this).Equals(obj?.ToString());
        }

        /// <summary>
        /// Gets the hash code of this object
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => ((string)this).GetHashCode();

        /// <summary>
        /// Create a <c>string</c> out of a <see cref="ChatId"/>
        /// </summary>
        /// <returns>The <see cref="ChatId"/> as <c>string</c></returns>
        public override string ToString() => this;

        /// <summary>
        /// Create a <see cref="ChatId"/> out of an identifier
        /// </summary>
        /// <param name="identifier">The identifier</param>
        public static implicit operator ChatId(long identifier) => new ChatId(identifier);

        /// <summary>
        /// Create a <see cref="ChatId"/> out of an identifier
        /// </summary>
        /// <param name="chatId">The identifier</param>
        public static implicit operator ChatId(int chatId) => new ChatId(chatId);

        /// <summary>
        /// Create a <see cref="ChatId"/> out of an user name
        /// </summary>
        /// <param name="username">The user name</param>
        public static implicit operator ChatId(string username) => new ChatId(username);

        /// <summary>
        /// Create a <c>string</c> out of a <see cref="ChatId"/>
        /// </summary>
        /// <param name="chatid">The <see cref="ChatId"/>The ChatId</param>
        public static implicit operator string(ChatId chatid) => chatid.Username ?? chatid.Identifier.ToString();

        /// <summary>
        /// Compares 2 ChatId objects
        /// </summary>
        public static bool operator ==(ChatId obj1, ChatId obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
            {
                return false;
            }

            // checking by Identifier is more consistent but we should check that its value isn`t default
            if (obj1.Identifier != 0)
            {
                return obj1.Identifier == obj2.Identifier || obj1.Username == obj2.Username;
            }
            return obj1.Identifier == obj2.Identifier && obj1.Username == obj2.Username;
        }

        /// <summary>
        /// Compares 2 ChatId objects
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(ChatId obj1, ChatId obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Convert a Chat Object to a <see cref="ChatId"/>
        /// </summary>
        /// <param name="chat"></param>
        public static implicit operator ChatId(Chat chat) =>
            chat.Id != default ? chat.Id : (ChatId)("@" + chat.Username);
    }
}
