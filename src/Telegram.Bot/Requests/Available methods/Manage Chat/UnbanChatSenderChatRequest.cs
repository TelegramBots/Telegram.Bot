using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to unban a previously banned channel chat in a supergroup or channel. The bot must be an
/// administrator for this to work and must have the appropriate administrator rights. Returns <see langword="true"/>
/// on success
/// </summary>
public class UnbanChatSenderChatRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier of the target sender chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long SenderChatId { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and senderChatId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
    /// </param>
    /// <param name="senderChatId">
    /// Unique identifier of the target sender chat
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public UnbanChatSenderChatRequest(ChatId chatId, long senderChatId)
        : this()
    {
        ChatId = chatId;
        SenderChatId = senderChatId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public UnbanChatSenderChatRequest()
        : base("unbanChatSenderChat")
    { }
}
