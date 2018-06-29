using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Type of a <see cref="MessageEntity"/>
    /// </summary>
    [JsonConverter(typeof(MessageEntityTypeConverter))]
    public sealed class MessageEntityType
    {
        /// <summary>
        /// Original string value of <see cref="MessageEntity"/> type
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Type of a <see cref="MessageEntity"/>
        /// </summary>
        public EntityType Type { get; }

        private MessageEntityType(string value, EntityType entityType)
        {
            Value = value;
            Type = entityType;
        }

        private bool Equals(MessageEntityType other)
        {
            var known = Type != EntityType.Unknown &&
                        other.Type != EntityType.Unknown;

            return known && Type == other.Type ||
                   !known && Value == other.Value;
        }

        public override bool Equals(object obj) =>
            obj is MessageEntityType other && Equals(other);

        public override int GetHashCode() =>
            unchecked(((Value?.GetHashCode() ?? 0) * 397) ^ (int) Type);

        public static bool operator ==(MessageEntityType left, MessageEntityType right) =>
            Equals(left, right);

        public static bool operator !=(MessageEntityType left, MessageEntityType right) =>
            !Equals(left, right);

        internal static MessageEntityType Unknown(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new MessageEntityType(value, EntityType.Unknown);
        }

        /// <summary>
        /// A mentioned <see cref="User"/>
        /// </summary>
        public static readonly MessageEntityType Mention = new MessageEntityType("mention", EntityType.Mention);

        /// <summary>
        /// A searchable Hashtag
        /// </summary>
        public static readonly MessageEntityType Hashtag = new MessageEntityType("hashtag", EntityType.Hashtag);

        /// <summary>
        /// A Bot command
        /// </summary>
        public static readonly MessageEntityType BotCommand = new MessageEntityType("bot_command", EntityType.BotCommand);

        /// <summary>
        /// An url
        /// </summary>
        public static readonly MessageEntityType Url = new MessageEntityType("url", EntityType.Url);

        /// <summary>
        /// An email
        /// </summary>
        public static readonly MessageEntityType Email = new MessageEntityType("email", EntityType.Email);

        /// <summary>
        /// Bold text
        /// </summary>
        public static readonly MessageEntityType Bold = new MessageEntityType("bold", EntityType.Bold);

        /// <summary>
        /// Italic text
        /// </summary>
        public static readonly MessageEntityType Italic = new MessageEntityType("italic", EntityType.Italic);

        /// <summary>
        /// Monowidth string
        /// </summary>
        public static readonly MessageEntityType Code = new MessageEntityType("code", EntityType.Code);

        /// <summary>
        /// Monowidth block
        /// </summary>
        public static readonly MessageEntityType Pre = new MessageEntityType("pre", EntityType.Pre);

        /// <summary>
        /// Clickable text urls
        /// </summary>
        public static readonly MessageEntityType TextLink = new MessageEntityType("text_link", EntityType.TextLink);

        /// <summary>
        /// Mentions for a <see cref="User"/> without <see cref="User.Username"/>
        /// </summary>
        public static readonly MessageEntityType TextMention = new MessageEntityType("text_mention", EntityType.TextMention);

        /// <summary>
        /// Phone number
        /// </summary>
        public static readonly MessageEntityType PhoneNumber = new MessageEntityType("phone_number", EntityType.PhoneNumber);
    }

    internal static class MessageEntityTypeExtensions
    {
        private static readonly IDictionary<string, MessageEntityType> StringToEnum =
            new Dictionary<string, MessageEntityType>
            {
                { "mention", MessageEntityType.Mention },
                { "hashtag", MessageEntityType.Hashtag },
                { "bot_command", MessageEntityType.BotCommand },
                { "url", MessageEntityType.Url },
                { "email", MessageEntityType.Email },
                { "bold", MessageEntityType.Bold },
                { "italic", MessageEntityType.Italic },
                { "code", MessageEntityType.Code },
                { "pre", MessageEntityType.Pre },
                { "text_link", MessageEntityType.TextLink },
                { "text_mention", MessageEntityType.TextMention },
                { "phone_number", MessageEntityType.PhoneNumber },
            };

        internal static MessageEntityType ToMessageType(this string value) =>
            StringToEnum.TryGetValue(value, out var messageEntityType)
                ? messageEntityType
                : MessageEntityType.Unknown(value);
    }
}
