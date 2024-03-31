using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about one member of the chat.
/// </summary>
[CustomJsonPolymorphic("status")]
[CustomJsonDerivedType<ChatMemberAdministrator>("administrator")]
[CustomJsonDerivedType<ChatMemberBanned>("kicked")]
[CustomJsonDerivedType<ChatMemberLeft>("left")]
[CustomJsonDerivedType<ChatMemberMember>("member")]
[CustomJsonDerivedType<ChatMemberOwner>("creator")]
[CustomJsonDerivedType<ChatMemberRestricted>("restricted")]
public abstract class ChatMember
{
    /// <summary>
    /// The member's status in the chat.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ChatMemberStatus Status { get; }

    /// <summary>
    /// Information about the user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that owns the chat and has all administrator privileges
/// </summary>
public class ChatMemberOwner : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Creator;

    /// <summary>
    /// Custom title for this user
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomTitle { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user's presence in the chat is hidden
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool IsAnonymous { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that has some additional privileges
/// </summary>
public class ChatMemberAdministrator : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Administrator;

    /// <summary>
    /// <see langword="true"/>, if the bot is allowed to edit administrator privileges of that user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanBeEdited { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user's presence in the chat is hidden
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool IsAnonymous { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can access the chat event log, chat statistics, message statistics
    /// in channels, see channel members, see anonymous administrators in supergroups and ignore slow mode.
    /// Implied by any other administrator privilege
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanManageChat { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can delete messages of other users
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanDeleteMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can manage video chats
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanManageVideoChats { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can restrict, ban or unban chat members
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanRestrictMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can add new administrators with a subset of his own privileges or
    /// demote administrators that he has promoted, directly or indirectly (promoted by administrators that
    /// were appointed by the user)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanPromoteMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can change the chat title, photo and other settings
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can invite new users to the chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can post in the channel, channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPostMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can edit messages of other users, channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanEditMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can pin messages, supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPinMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can post stories in the channel; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPostStories { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can edit stories posted by other users; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanEditStories { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the administrator can delete stories posted by other users; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanDeleteStories { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to create, rename, close, and reopen forum topics;
    /// supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageTopics { get; set; }

    /// <summary>
    /// Optional. Custom title for this user
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomTitle { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that has no additional privileges or restrictions.
/// </summary>
public class ChatMemberMember : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Member;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that is under certain restrictions in the chat. Supergroups only.
/// </summary>
public class ChatMemberRestricted : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Restricted;

    /// <summary>
    /// <see langword="true"/>, if the user is a member of the chat at the moment of the request
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool IsMember { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can change the chat title, photo and other settings
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can invite new users to the chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can pin messages, supergroups only
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanPinMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can send text messages, contacts, locations and venues
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendMessages { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send audios
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendAudios { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send documents
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendDocuments { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send photos
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendPhotos { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send videos
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendVideos { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send video notes
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendVideoNotes { get; set; }

    /// <summary>
    /// <see langword="true" />, if the user is allowed to send voice notes
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendVoiceNotes { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to send polls
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendPolls { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline bots
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanSendOtherMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to add web page previews to their messages
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary>
    /// Date when restrictions will be lifted for this user, UTC time
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to create forum topics
    /// supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageTopics { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that isn't currently a member of the chat, but may join it themselves
/// </summary>
public class ChatMemberLeft : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Left;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that was banned in the chat and can't return to the chat
/// or view chat messages
/// </summary>
public class ChatMemberBanned : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Kicked;

    /// <summary>
    /// Date when restrictions will be lifted for this user, UTC time
    /// </summary>
    [JsonConverter(typeof(BanTimeConverter))]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UntilDate { get; set; }
}
