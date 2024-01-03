using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to copy messages of any kind. If some of the specified messages can't be found or copied,
/// they are skipped. Service messages, giveaway messages, giveaway winners messages, and invoice messages
/// can't be copied. A quiz <see cref="Poll"/> can be copied only if the value of the field
/// <see cref="Poll.CorrectOptionId">CorrectOptionId</see> is known to the bot. The method is analogous
/// to the method <see cref="ForwardMessagesRequest"/>, but the copied messages don't have a link
/// to the original message. Album grouping is kept for copied messages.
/// On success, an array of <see cref="MessageId"/> of the sent messages is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CopyMessagesRequest : RequestBase<MessageId[]>, IChatTargetable
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
    /// Identifiers of 1-100 messages in the chat <see cref="FromChatId"/> to copy.
    /// The identifiers must be specified in a strictly increasing order.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int[] MessageIds { get; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> to copy the messages without their captions
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RemoveCaption { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, fromChatId and messageIds
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original messages were sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages in the chat <see cref="FromChatId"/> to copy.
    /// The identifiers must be specified in a strictly increasing order.
    /// </param>
    public CopyMessagesRequest(ChatId chatId, ChatId fromChatId, int[] messageIds)
        : base("copyMessages")
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageIds = messageIds;
    }
}
