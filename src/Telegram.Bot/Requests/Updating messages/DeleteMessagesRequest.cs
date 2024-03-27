using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete multiple messages simultaneously.
/// If some of the specified messages can't be found, they are skipped.
/// Returns <see langword="true"/> on success.
/// </summary>
public class DeleteMessagesRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifiers of 1-100 messages to delete. See <see cref="DeleteMessageRequest"/>
    /// for limitations on which messages can be deleted
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; init; }

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
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public DeleteMessagesRequest(ChatId chatId, IEnumerable<int> messageIds)
        : this()
    {
        ChatId = chatId;
        MessageIds = messageIds;
    }

    /// <summary>
    /// Initializes a new request with chatId and messageIds
    /// </summary>
    public DeleteMessagesRequest()
        : base("deleteMessages")
    { }
}
