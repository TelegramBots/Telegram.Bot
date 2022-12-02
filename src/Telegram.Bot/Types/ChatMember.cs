using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about one member of the chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
[JsonConverter(typeof(ChatMemberConverter))]
public abstract class ChatMember
{
    /// <summary>
    /// The member's status in the chat.
    /// </summary>
    [JsonProperty]
    public abstract ChatMemberStatus Status { get; }

    /// <summary>
    /// Information about the user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User User { get; set; } = default!;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that owns the chat and has all administrator privileges
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberOwner : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Creator;

    /// <summary>
    /// Custom title for this user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? CustomTitle { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user's presence in the chat is hidden
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsAnonymous { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that has some additional privileges
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberAdministrator : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Administrator;

    /// <summary>
    /// <see langword="true"/>, if the bot is allowed to edit administrator privileges of that user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanBeEdited { get; set; }

    /// <summary>
    /// Custom title for this user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? CustomTitle { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user's presence in the chat is hidden
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsAnonymous { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can access the chat event log, chat statistics, message statistics
    /// in channels, see channel members, see anonymous administrators in supergroups and ignore slow mode.
    /// Implied by any other administrator privilege
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanManageChat { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can post in the channel, channels only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanPostMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can edit messages of other users, channels only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanEditMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can delete messages of other users
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanDeleteMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can manage video chats
    /// </summary>
    [Obsolete("This property will be removed in the next major version, use CanManageVideoChat instead")]
    [JsonProperty(Required = Required.Always)]
    public bool CanManageVoiceChats { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can manage video chats
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanManageVideoChats { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can restrict, ban or unban chat members
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanRestrictMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can add new administrators with a subset of his own privileges or
    /// demote administrators that he has promoted, directly or indirectly (promoted by administrators that
    /// were appointed by the user)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanPromoteMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can change the chat title, photo and other settings
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can invite new users to the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can pin messages, supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanPinMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to create, rename, close, and reopen forum topics;
    /// supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanManageTopics { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that has no additional privileges or restrictions.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberMember : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Member;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that is under certain restrictions in the chat. Supergroups only.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberRestricted : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Restricted;

    /// <summary>
    /// <see langword="true"/>, if the user is a member of the chat at the moment of the request
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsMember { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can change the chat title, photo and other settings
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can invite new users to the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can pin messages, supergroups only
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanPinMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can send text messages, contacts, locations and venues
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanSendMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can send audios, documents, photos, videos, video notes and voice notes,
    /// implies <see cref="CanSendMessages"/>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanSendMediaMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to send polls
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanSendPolls { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user can send animations, games, stickers and use inline bots,
    /// implies <see cref="CanSendMediaMessages"/>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanSendOtherMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if user may add web page previews to his messages,
    /// implies <see cref="CanSendMediaMessages"/>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary>
    /// Date when restrictions will be lifted for this user, UTC time
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(BanTimeUnixDateTimeConverter))]
    public DateTime? UntilDate { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to create forum topics
    /// supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanManageTopics { get; set; }
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that isn't currently a member of the chat, but may join it themselves
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberLeft : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Left;
}

/// <summary>
/// Represents a <see cref="ChatMember"/> that was banned in the chat and can't return to the chat
/// or view chat messages
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatMemberBanned : ChatMember
{
    /// <inheritdoc />
    public override ChatMemberStatus Status => ChatMemberStatus.Kicked;

    /// <summary>
    /// Date when restrictions will be lifted for this user, UTC time
    /// </summary>
    [JsonConverter(typeof(BanTimeUnixDateTimeConverter))]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DateTime? UntilDate { get; set; }
}
