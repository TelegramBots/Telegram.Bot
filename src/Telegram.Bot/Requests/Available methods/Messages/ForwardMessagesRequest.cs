using System.Collections.Generic;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to forward multiple messages of any kind. If some of the specified messages can't be found
/// or forwarded, they are skipped. Service messages and messages with protected content can't be forwarded.
/// Album grouping is kept for forwarded messages.
/// On success, an array of <see cref="MessageId"/> of the sent messages is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ForwardMessagesRequest : RequestBase<MessageId[]>, IChatTargetable
{
    /// <summary>
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Unique identifier for the chat where the original messages were sent 
    /// (or channel username in the format <c>@channelusername</c>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatId FromChatId { get; }

    /// <summary>
    /// Identifiers of 1-100 messages in the chat from_chat_id to forward.
    /// The identifiers must be specified in a strictly increasing order.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<int> MessageIds { get; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, fromChatId and messageIds
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original messages were sent 
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages in the chat from_chat_id to forward.
    /// The identifiers must be specified in a strictly increasing order.
    /// </param>
    public ForwardMessagesRequest(ChatId chatId, ChatId fromChatId, IEnumerable<int> messageIds)
        :base("forwardMessages")
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageIds = messageIds;
    }
}
