namespace Telegram.Bot.Requests;

/// <summary>Use this method to ban a user in a group, a supergroup or a channel. In the case of supergroups and channels, the user will not be able to return to the chat on their own using invite links, etc., unless <see cref="TelegramBotClientExtensions.UnbanChatMemberAsync">unbanned</see> first. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class BanChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Date when the user will be unbanned, in UTC. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever. Applied for supergroups and channels only.</summary>
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }

    /// <summary>Pass <see langword="true"/> to delete all messages from the chat for the user that is being removed. If <see langword="false"/>, the user will be able to see messages in the group that were sent before the user was removed. Always <see langword="true"/> for supergroups and channels.</summary>
    public bool RevokeMessages { get; set; }

    /// <summary>Initializes an instance of <see cref="BanChatMemberRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public BanChatMemberRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="BanChatMemberRequest"/></summary>
    public BanChatMemberRequest() : base("banChatMember") { }
}
