using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to delete a message, including service messages, with the following limitations:
    /// <list type="bullet">
    /// <item><description>A message can only be deleted if it was sent less than 48 hours ago.</description></item>
    /// <item><description>A dice message in a private chat can only be deleted if it was sent more than 24 hours ago.</description></item>
    /// <item><description>Bots can delete outgoing messages in private chats, groups, and supergroups.</description></item>
    /// <item><description>Bots can delete incoming messages in private chats.</description></item>
    /// <item><description>Bots granted can_post_messages permissions can delete outgoing messages in channels.</description></item>
    /// <item><description>If the bot is an administrator of a group, it can delete any message there.</description></item>
    /// <item><description>If the bot has can_delete_messages permission in a supergroup or a channel, it can delete any message there.</description></item>
    /// </list>
    /// Returns True on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class DeleteMessageRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Identifier of the message to delete
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int MessageId { get; }

        /// <summary>
        /// Initializes a new request with chatId and messageId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
        /// <param name="messageId">Identifier of the message to delete</param>
        public DeleteMessageRequest(ChatId chatId, int messageId)
            : base("deleteMessage")
        {
            ChatId = chatId;
            MessageId = messageId;
        }
    }
}
