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

        public ChatId(long identifier)
        {
            Identifier = identifier;
        }

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

        public override string ToString() => this;

        public static implicit operator ChatId(long identifier) => new ChatId(identifier);

        public static implicit operator ChatId(string username) => new ChatId(username);

        public static implicit operator string(ChatId chatid) => chatid.Username ?? chatid.Identifier.ToString();
    }
}
