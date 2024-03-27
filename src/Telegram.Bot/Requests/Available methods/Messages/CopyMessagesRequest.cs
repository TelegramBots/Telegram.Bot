using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
public class CopyMessagesRequest : RequestBase<MessageId[]>, IChatTargetable
{
    /// <summary>
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier for the chat where the original messages were sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId FromChatId { get; init; }

    /// <summary>
    /// Identifiers of 1-100 messages in the chat <see cref="FromChatId"/> to copy.
    /// The identifiers must be specified in a strictly increasing order.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageThreadId { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ProtectContent { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> to copy the messages without their captions
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
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
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public CopyMessagesRequest(ChatId chatId, ChatId fromChatId, int[] messageIds)
        : this()
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageIds = messageIds;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public CopyMessagesRequest()
        : base("copyMessages")
    { }
}
