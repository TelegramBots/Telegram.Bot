namespace Telegram.Bot.Requests;

/// <summary>Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Pass <em>False</em> for all boolean parameters to demote a user.<para>Returns: </para></summary>
public partial class PromoteChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator's presence in the chat is hidden</summary>
    public bool IsAnonymous { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages and ignore slow mode. Implied by any other administrator privilege.</summary>
    public bool CanManageChat { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can post messages in the channel, or access channel statistics; for channels only</summary>
    public bool CanPostMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can edit messages of other users and can pin messages; for channels only</summary>
    public bool CanEditMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can delete messages of other users</summary>
    public bool CanDeleteMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can post stories to the chat</summary>
    public bool CanPostStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive</summary>
    public bool CanEditStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can delete stories posted by other users</summary>
    public bool CanDeleteStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can manage video chats</summary>
    public bool CanManageVideoChats { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can restrict, ban or unban chat members, or access supergroup statistics</summary>
    public bool CanRestrictMembers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by him)</summary>
    public bool CanPromoteMembers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can change chat title, photo and other settings</summary>
    public bool CanChangeInfo { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can invite new users to the chat</summary>
    public bool CanInviteUsers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can pin messages; for supergroups only</summary>
    public bool CanPinMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only</summary>
    public bool CanManageTopics { get; set; }

    /// <summary>Initializes an instance of <see cref="PromoteChatMemberRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public PromoteChatMemberRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="PromoteChatMemberRequest"/></summary>
    public PromoteChatMemberRequest() : base("promoteChatMember") { }
}
