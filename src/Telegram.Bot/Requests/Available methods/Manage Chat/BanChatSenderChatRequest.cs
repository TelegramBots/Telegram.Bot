using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to ban a channel chat in a supergroup or a channel. The owner of the chat will not be able
/// to send messages and join live streams on behalf of the chat, unless it is unbanned first. The bot must be
/// an administrator in the supergroup or channel for this to work and must have the appropriate administrator
/// rights. Returns <see langword="true"/> on success
/// </summary>
public class BanChatSenderChatRequest : RequestBase<bool>, IChatTargetable
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
    /// Date when the sender chat will be unbanned, unix time. If the chat is banned for more than 366 days or
    /// less than 30 seconds from the current time they are considered to be banned forever.
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UntilDate { get; set; }

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
    public BanChatSenderChatRequest(ChatId chatId, long senderChatId)
        : this()
    {
        ChatId = chatId;
        SenderChatId = senderChatId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public BanChatSenderChatRequest()
        : base("banChatSenderChat")
    { }
}
