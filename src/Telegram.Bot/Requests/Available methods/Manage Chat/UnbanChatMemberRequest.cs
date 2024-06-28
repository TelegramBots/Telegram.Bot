namespace Telegram.Bot.Requests;

/// <summary>Use this method to unban a previously banned user in a supergroup or channel. The user will <b>not</b> return to the group or channel automatically, but will be able to join via link, etc. The bot must be an administrator for this to work. By default, this method guarantees that after the call the user is not a member of the chat, but will be able to join it. So if the user is a member of the chat they will also be <b>removed</b> from the chat. If you don't want this, use the parameter <see cref="OnlyIfBanned">OnlyIfBanned</see>.<para>Returns: </para></summary>
public partial class UnbanChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Do nothing if the user is not banned</summary>
    public bool OnlyIfBanned { get; set; }

    /// <summary>Initializes an instance of <see cref="UnbanChatMemberRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public UnbanChatMemberRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="UnbanChatMemberRequest"/></summary>
    public UnbanChatMemberRequest() : base("unbanChatMember") { }
}
