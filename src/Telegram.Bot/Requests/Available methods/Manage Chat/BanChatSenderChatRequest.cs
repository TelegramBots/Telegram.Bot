namespace Telegram.Bot.Requests;

/// <summary>Use this method to ban a channel chat in a supergroup or a channel. Until the chat is <see cref="TelegramBotClientExtensions.UnbanChatSenderChatAsync">unbanned</see>, the owner of the banned chat won't be able to send messages on behalf of <b>any of their channels</b>. The bot must be an administrator in the supergroup or channel for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class BanChatSenderChatRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target sender chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long SenderChatId { get; set; }

    /// <summary>Initializes an instance of <see cref="BanChatSenderChatRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="senderChatId">Unique identifier of the target sender chat</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public BanChatSenderChatRequest(ChatId chatId, long senderChatId) : this()
    {
        ChatId = chatId;
        SenderChatId = senderChatId;
    }

    /// <summary>Instantiates a new <see cref="BanChatSenderChatRequest"/></summary>
    public BanChatSenderChatRequest() : base("banChatSenderChat") { }
}
