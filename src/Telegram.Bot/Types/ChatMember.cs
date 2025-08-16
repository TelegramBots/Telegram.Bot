// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about one member of a chat. Currently, the following 6 types of chat members are supported:<br/><see cref="ChatMemberOwner"/>, <see cref="ChatMemberAdministrator"/>, <see cref="ChatMemberMember"/>, <see cref="ChatMemberRestricted"/>, <see cref="ChatMemberLeft"/>, <see cref="ChatMemberBanned"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<ChatMember>))]
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
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    /// <summary><em>Optional</em>. Custom title for this user</summary>
    [JsonPropertyName("custom_title")]
    public string? CustomTitle { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that has some additional privileges.</summary>
public partial class ChatMemberAdministrator : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Administrator"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Administrator;

    /// <summary><see langword="true"/>, if the bot is allowed to edit administrator privileges of that user</summary>
    [JsonPropertyName("can_be_edited")]
    public bool CanBeEdited { get; set; }

    /// <summary><see langword="true"/>, if the user's presence in the chat is hidden</summary>
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    /// <summary><see langword="true"/>, if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages, ignore slow mode, and send messages to the chat without paying Telegram Stars. Implied by any other administrator privilege.</summary>
    [JsonPropertyName("can_manage_chat")]
    public bool CanManageChat { get; set; }

    /// <summary><see langword="true"/>, if the administrator can delete messages of other users</summary>
    [JsonPropertyName("can_delete_messages")]
    public bool CanDeleteMessages { get; set; }

    /// <summary><see langword="true"/>, if the administrator can manage video chats</summary>
    [JsonPropertyName("can_manage_video_chats")]
    public bool CanManageVideoChats { get; set; }

    /// <summary><see langword="true"/>, if the administrator can restrict, ban or unban chat members, or access supergroup statistics</summary>
    [JsonPropertyName("can_restrict_members")]
    public bool CanRestrictMembers { get; set; }

    /// <summary><see langword="true"/>, if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by the user)</summary>
    [JsonPropertyName("can_promote_members")]
    public bool CanPromoteMembers { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to change the chat title, photo and other settings</summary>
    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to invite new users to the chat</summary>
    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; set; }

    /// <summary><see langword="true"/>, if the administrator can post stories to the chat</summary>
    [JsonPropertyName("can_post_stories")]
    public bool CanPostStories { get; set; }

    /// <summary><see langword="true"/>, if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive</summary>
    [JsonPropertyName("can_edit_stories")]
    public bool CanEditStories { get; set; }

    /// <summary><see langword="true"/>, if the administrator can delete stories posted by other users</summary>
    [JsonPropertyName("can_delete_stories")]
    public bool CanDeleteStories { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the administrator can post messages in the channel, approve suggested posts, or access channel statistics; for channels only</summary>
    [JsonPropertyName("can_post_messages")]
    public bool CanPostMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the administrator can edit messages of other users and can pin messages; for channels only</summary>
    [JsonPropertyName("can_edit_messages")]
    public bool CanEditMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to pin messages; for groups and supergroups only</summary>
    [JsonPropertyName("can_pin_messages")]
    public bool CanPinMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only</summary>
    [JsonPropertyName("can_manage_topics")]
    public bool CanManageTopics { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the administrator can manage direct messages of the channel and decline suggested posts; for channels only</summary>
    [JsonPropertyName("can_manage_direct_messages")]
    public bool CanManageDirectMessages { get; set; }

    /// <summary><em>Optional</em>. Custom title for this user</summary>
    [JsonPropertyName("custom_title")]
    public string? CustomTitle { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that has no additional privileges or restrictions.</summary>
public partial class ChatMemberMember : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Member"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Member;

    /// <summary><em>Optional</em>. Date when the user's subscription will expire; Unix time</summary>
    [JsonPropertyName("until_date")]
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }
}

/// <summary>Represents a <see cref="ChatMember">chat member</see> that is under certain restrictions in the chat. Supergroups only.</summary>
public partial class ChatMemberRestricted : ChatMember
{
    /// <summary>The member's status in the chat, always <see cref="ChatMemberStatus.Restricted"/></summary>
    public override ChatMemberStatus Status => ChatMemberStatus.Restricted;

    /// <summary><see langword="true"/>, if the user is a member of the chat at the moment of the request</summary>
    [JsonPropertyName("is_member")]
    public bool IsMember { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send text messages, contacts, giveaways, giveaway winners, invoices, locations and venues</summary>
    [JsonPropertyName("can_send_messages")]
    public bool CanSendMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send audios</summary>
    [JsonPropertyName("can_send_audios")]
    public bool CanSendAudios { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send documents</summary>
    [JsonPropertyName("can_send_documents")]
    public bool CanSendDocuments { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send photos</summary>
    [JsonPropertyName("can_send_photos")]
    public bool CanSendPhotos { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send videos</summary>
    [JsonPropertyName("can_send_videos")]
    public bool CanSendVideos { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send video notes</summary>
    [JsonPropertyName("can_send_video_notes")]
    public bool CanSendVideoNotes { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send voice notes</summary>
    [JsonPropertyName("can_send_voice_notes")]
    public bool CanSendVoiceNotes { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send polls and checklists</summary>
    [JsonPropertyName("can_send_polls")]
    public bool CanSendPolls { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline bots</summary>
    [JsonPropertyName("can_send_other_messages")]
    public bool CanSendOtherMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to add web page previews to their messages</summary>
    [JsonPropertyName("can_add_web_page_previews")]
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to change the chat title, photo and other settings</summary>
    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to invite new users to the chat</summary>
    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to pin messages</summary>
    [JsonPropertyName("can_pin_messages")]
    public bool CanPinMessages { get; set; }

    /// <summary><see langword="true"/>, if the user is allowed to create forum topics</summary>
    [JsonPropertyName("can_manage_topics")]
    public bool CanManageTopics { get; set; }

    /// <summary>Date when restrictions will be lifted for this user, in UTC. If unset, then the user is restricted forever</summary>
    [JsonPropertyName("until_date")]
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
    [JsonPropertyName("until_date")]
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }
}
