using System.Collections.Generic;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete multiple messages simultaneously.
/// If some of the specified messages can't be found, they are skipped.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeleteMessagesRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Identifiers of 1-100 messages to delete. See <see cref="DeleteMessageRequest"/>
    /// for limitations on which messages can be deleted
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<int> MessageIds { get; }

    /// <summary>
    /// Initializes a new request with chatId and messageIds
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages to delete. See <see cref="DeleteMessageRequest"/>
    /// for limitations on which messages can be deleted
    /// </param>
    public DeleteMessagesRequest(ChatId chatId, IEnumerable<int> messageIds)
        :base("deleteMessages")
    {
        ChatId = chatId;
        MessageIds = messageIds;
    }
}
