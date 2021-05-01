using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Chat
    {
        /// <summary>
        /// Unique identifier for this chat. This number may have more
        /// than 32 significant bits and some programming languages may have
        /// difficulty/silent defects in interpreting it. But it has
        /// at most 52 significant bits, so a signed 64-bit integer
        /// or double-precision float type are safe for storing this identifier.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public long Id { get; set; }

        /// <summary>
        /// Type of chat, can be either “private”, “group”, “supergroup” or “channel”
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatType Type { get; set; }

        /// <summary>
        /// Optional. Title, for supergroups, channels and group chats
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Optional. Username, for private chats, supergroups and channels if available
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Username { get; set; }

        /// <summary>
        /// Optional. First name of the other party in a private chat
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. Last name of the other party in a private chat
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LastName { get; set; }

        /// <summary>
        /// Optional. Chat photo. Returned only in getChat.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChatPhoto Photo { get; set; }

        /// <summary>
        /// Optional. Bio of the other party in a private chat. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Bio { get; set; }

        /// <summary>
        /// Optional. Description, for groups, supergroups and channel chats. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. Primary invite link, for groups, supergroups and channel chats. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string InviteLink { get; set; }

        /// <summary>
        /// Optional. The most recent pinned message (by sending date). Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Message PinnedMessage { get; set; }

        /// <summary>
        /// Optional. Default chat member permissions, for groups and supergroups. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChatPermissions Permissions { get; set; }

        /// <summary>
        /// Optional. For supergroups, the minimum allowed delay between consecutive messages sent by each unpriviledged user. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? SlowModeDelay { get; set; }

        /// <summary>
        /// Optional. For supergroups, name of group sticker set. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string StickerSetName { get; set; }

        /// <summary>
        /// Optional. True, if the bot can change the group sticker set. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? CanSetStickerSet { get; set; }

        /// <summary>
        /// Optional. Unique identifier for the linked chat, i.e. the discussion group identifier for a channel and vice versa; for supergroups and channel chats. This identifier may be greater than 32 bits and some programming languages may have difficulty/silent defects in interpreting it. But it is smaller than 52 bits, so a signed 64 bit integer or double-precision float type are safe for storing this identifier. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long LinkedChatId { get; set; }

        /// <summary>
        /// Optional. For supergroups, the location to which the supergroup is connected. Returned only in <see cref="ITelegramBotClient.GetChatAsync"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChatLocation Location { get; set; }
    }
}
