using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable CheckNamespace

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to stop a poll which was sent by the bot. On success, the stopped <see cref="Poll"/>
/// with the final results is returned.
/// </summary>
public class StopPollRequest : RequestBase<Poll>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifier of the original message with the poll
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <inheritdoc cref="Documentation.InlineReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, messageId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel (in the format
    /// <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the original message with the poll</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public StopPollRequest(ChatId chatId, int messageId)
        : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public StopPollRequest()
        : base("stopPoll")
    { }
}
