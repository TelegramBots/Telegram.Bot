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
        internal long? Identifier;
        internal string Username;

        /// <summary>
        /// Create a <see cref="ChatId"/> using an identifier
        /// </summary>
        /// <param name="identifier">The Identifier</param>
        public ChatId(long identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Create a <see cref="ChatId"/> using an user name
        /// </summary>
        /// <param name="username">The user name</param>
        public ChatId(string username)
        {
            if (username.Length > 1 && username.Substring(0, 1) == "@")
            {
                Username = username;
            }
            else if (long.TryParse(username, out long identifier))
            {
                Identifier = identifier;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var chatId = obj as ChatId;
            return long.Equals(this.Identifier ?? 0, chatId.Identifier) || string.Equals(this.Username ?? string.Empty, chatId.Username);
        }

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
        /// Create a <see cref="ChatId"/> out of an user name
        /// </summary>
        /// <param name="username">The user name</param>
        public static implicit operator ChatId(string username) => new ChatId(username);

        /// <summary>
        /// Create a <c>string</c> out of a <see cref="ChatId"/>
        /// </summary>
        /// <param name="chatid">The <see cref="ChatId"/></param>
        public static implicit operator string(ChatId chatid) => chatid.Username ?? chatid.Identifier.ToString();
    }
}
