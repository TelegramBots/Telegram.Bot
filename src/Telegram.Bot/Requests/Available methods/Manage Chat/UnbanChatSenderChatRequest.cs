using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to unban a previously banned channel chat in a supergroup or channel. The bot must be an
/// administrator for this to work and must have the appropriate administrator rights. Returns <see langword="true"/>
/// on success
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class UnbanChatSenderChatRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Unique identifier of the target sender chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long SenderChatId { get; }

    /// <summary>
    /// Initializes a new request with chatId and senderChatId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
    /// </param>
    /// <param name="senderChatId">
    /// Unique identifier of the target sender chat
    /// </param>
    public UnbanChatSenderChatRequest(ChatId chatId, long senderChatId)
        : base("unbanChatSenderChat")
    {
        ChatId = chatId;
        SenderChatId = senderChatId;
    }
}
