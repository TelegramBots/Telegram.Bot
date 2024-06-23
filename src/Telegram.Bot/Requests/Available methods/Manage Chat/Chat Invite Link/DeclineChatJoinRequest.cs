namespace Telegram.Bot.Requests;

/// <summary>Use this method to decline a chat join request. The bot must be an administrator in the chat for this to work and must have the <em>CanInviteUsers</em> administrator right.<para>Returns: </para></summary>
public partial class DeclineChatJoinRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Initializes an instance of <see cref="DeclineChatJoinRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public DeclineChatJoinRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="DeclineChatJoinRequest"/></summary>
    public DeclineChatJoinRequest() : base("declineChatJoinRequest") { }
}
