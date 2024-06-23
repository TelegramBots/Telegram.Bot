namespace Telegram.Bot.Types;

/// <summary>This object contains information about one member of a chat. Currently, the following 6 types of chat members are supported:<br/><see cref="ChatMemberOwner"/>, <see cref="ChatMemberAdministrator"/>, <see cref="ChatMemberMember"/>, <see cref="ChatMemberRestricted"/>, <see cref="ChatMemberLeft"/>, <see cref="ChatMemberBanned"/></summary>
[CustomJsonPolymorphic("status")]
[CustomJsonDerivedType(typeof(ChatMemberOwner), "creator")]
[CustomJsonDerivedType(typeof(ChatMemberAdministrator), "administrator")]
[CustomJsonDerivedType(typeof(ChatMemberMember), "member")]
[CustomJsonDerivedType(typeof(ChatMemberRestricted), "restricted")]
[CustomJsonDerivedType(typeof(ChatMemberLeft), "left")]
[CustomJsonDerivedType(typeof(ChatMemberBanned), "kicked")]
public abstract partial class ChatMember
{
    /// <summary>The member's status in the chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ChatMemberStatus Status { get; }

    /// <summary>Information about the user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that owns the chat and has all administrator privileges.</summary>
public partial class ChatMemberOwner : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Creator"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Creator;

    /// <summary><see langword="true"/>, if the user's presence in the chat is hidden</summary>
    public bool IsAnonymous { get; set; }

    /// <summary><em>Optional</em>. Custom title for this user</summary>
    public string? CustomTitle { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that has some additional privileges.</summary>
public partial class ChatMemberAdministrator : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Administrator"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Administrator;

    /// <summary><see langword="true"/>, if the bot is allowed to edit administrator privileges of that user</summary>
    public bool CanBeEdited { get; set; }

    /// <summary><see langword="true"/>, if the user's presence in the chat is hidden</summary>
    public bool IsAnonymous { get; set; }

    /// <summary><see langword="true"/>, if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages and ignore slow mode. Implied by any other administrator privilege.</summary>
    public bool CanManageChat { get; set; }

    /// <summary><see langword="true"/>, if the administrator can delete messages of other users</summary>
    public bool CanDeleteMessages { get; set; }

    /// <summary><see langword="true"/>, if the administrator can manage video chats</summary>
    public bool CanManageVideoChats { get; set; }

    /// <summary><see langword="true"/>, if the administrator can restrict, ban or unban chat members, or access supergroup statistics</summary>
    public bool CanRestrictMembers { get; set; }

    /// <summary><see langword="true"/>, if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by the user)</summary>
    public bool CanPromoteMembers { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to change the chat title, photo and other settings</summary>
    public bool CanChangeInfo { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to invite new users to the chat</summary>
    public bool CanInviteUsers { get; set; }

    /// <summary><see langword="true"/>, if the administrator can post stories to the chat</summary>
    public bool CanPostStories { get; set; }

    /// <summary><see langword="true"/>, if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive</summary>
    public bool CanEditStories { get; set; }

    /// <summary><see langword="true"/>, if the administrator can delete stories posted by other users</summary>
    public bool CanDeleteStories { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the administrator can post messages in the channel, or access channel statistics; for channels only</summary>
    public bool CanPostMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the administrator can edit messages of other users and can pin messages; for channels only</summary>
    public bool CanEditMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to pin messages; for groups and supergroups only</summary>
    public bool CanPinMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only</summary>
    public bool CanManageTopics { get; set; }

    /// <summary><em>Optional</em>. Custom title for this user</summary>
    public string? CustomTitle { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that has no additional privileges or restrictions.</summary>
public partial class ChatMemberMember : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Member"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Member;
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that is under certain restrictions in the chat. Supergroups only.</summary>
public partial class ChatMemberRestricted : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Restricted"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Restricted;

    /// <summary><see langword="true"/>, if the user is a member of the chat at the moment of the request</summary>
    public bool IsMember { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send text messages, contacts, giveaways, giveaway winners, invoices, locations and venues</summary>
    public bool CanSendMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send audios</summary>
    public bool CanSendAudios { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send documents</summary>
    public bool CanSendDocuments { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send photos</summary>
    public bool CanSendPhotos { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send videos</summary>
    public bool CanSendVideos { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send video notes</summary>
    public bool CanSendVideoNotes { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send voice notes</summary>
    public bool CanSendVoiceNotes { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send polls</summary>
    public bool CanSendPolls { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline bots</summary>
    public bool CanSendOtherMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to add web page previews to their messages</summary>
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to change the chat title, photo and other settings</summary>
    public bool CanChangeInfo { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to invite new users to the chat</summary>
    public bool CanInviteUsers { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to pin messages</summary>
    public bool CanPinMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to create forum topics</summary>
    public bool CanManageTopics { get; set; }

    /// <summary>Date when restrictions will be lifted for this user, in UTC. If unset, then the user is restricted forever</summary>
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that isn't currently a member of the chat, but may join it themselves.</summary>
public partial class ChatMemberLeft : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Left"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Left;
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that was banned in the chat and can't return to the chat or view chat messages.</summary>
public partial class ChatMemberBanned : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Kicked"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Kicked;

    /// <summary>Date when restrictions will be lifted for this user, in UTC. If unset, then the user is banned forever</summary>
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }
}
