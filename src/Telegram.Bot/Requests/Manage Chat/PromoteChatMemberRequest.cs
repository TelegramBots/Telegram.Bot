// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Pass <em>False</em> for all boolean parameters to demote a user.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class PromoteChatMemberRequest() : RequestBase<bool>("promoteChatMember"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator's presence in the chat is hidden</summary>
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages, ignore slow mode, and send messages to the chat without paying Telegram Stars. Implied by any other administrator privilege.</summary>
    [JsonPropertyName("can_manage_chat")]
    public bool CanManageChat { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can post messages in the channel, approve suggested posts, or access channel statistics; for channels only</summary>
    [JsonPropertyName("can_post_messages")]
    public bool CanPostMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can edit messages of other users and can pin messages; for channels only</summary>
    [JsonPropertyName("can_edit_messages")]
    public bool CanEditMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can delete messages of other users</summary>
    [JsonPropertyName("can_delete_messages")]
    public bool CanDeleteMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can post stories to the chat</summary>
    [JsonPropertyName("can_post_stories")]
    public bool CanPostStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive</summary>
    [JsonPropertyName("can_edit_stories")]
    public bool CanEditStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can delete stories posted by other users</summary>
    [JsonPropertyName("can_delete_stories")]
    public bool CanDeleteStories { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can manage video chats</summary>
    [JsonPropertyName("can_manage_video_chats")]
    public bool CanManageVideoChats { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can restrict, ban or unban chat members, or access supergroup statistics. For backward compatibility, defaults to <see langword="true"/> for promotions of channel administrators</summary>
    [JsonPropertyName("can_restrict_members")]
    public bool CanRestrictMembers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by him)</summary>
    [JsonPropertyName("can_promote_members")]
    public bool CanPromoteMembers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can change chat title, photo and other settings</summary>
    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can invite new users to the chat</summary>
    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can pin messages; for supergroups only</summary>
    [JsonPropertyName("can_pin_messages")]
    public bool CanPinMessages { get; set; }

    /// <summary>Pass <see langword="true"/> if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only</summary>
    [JsonPropertyName("can_manage_topics")]
    public bool CanManageTopics { get; set; }

    /// <summary>Pass <see langword="true"/> if the administrator can manage direct messages within the channel and decline suggested posts; for channels only</summary>
    [JsonPropertyName("can_manage_direct_messages")]
    public bool CanManageDirectMessages { get; set; }
}
