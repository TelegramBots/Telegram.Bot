// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>The member's status in the chat</summary>
[JsonConverter(typeof(EnumConverter<ChatMemberStatus>))]
public enum ChatMemberStatus
{
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that owns the chat and has all administrator privileges.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberOwner"/>)</i></summary>
    Creator = 1,
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that has some additional privileges.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberAdministrator"/>)</i></summary>
    Administrator,
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that has no additional privileges or restrictions.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberMember"/>)</i></summary>
    Member,
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that isn't currently a member of the chat, but may join it themselves.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberLeft"/>)</i></summary>
    Left,
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that was banned in the chat and can't return to the chat or view chat messages.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberBanned"/>)</i></summary>
    Kicked,
    /// <summary>Represents a <see cref="ChatMember">chat member</see> that is under certain restrictions in the chat. Supergroups only.<br/><br/><i>(<see cref="ChatMember"/> can be cast into <see cref="ChatMemberRestricted"/>)</i></summary>
    Restricted,
}
