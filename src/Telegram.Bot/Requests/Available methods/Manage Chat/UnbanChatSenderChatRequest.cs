namespace Telegram.Bot.Requests;

/// <summary>Use this method to unban a previously banned channel chat in a supergroup or channel. The bot must be an administrator for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class UnbanChatSenderChatRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target sender chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long SenderChatId { get; set; }

    /// <summary>Instantiates a new <see cref="UnbanChatSenderChatRequest"/></summary>
    public UnbanChatSenderChatRequest() : base("unbanChatSenderChat") { }
}
