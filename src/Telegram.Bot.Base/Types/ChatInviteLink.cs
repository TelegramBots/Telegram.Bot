using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// Represents an invite link for a chat.
/// </summary>
public class ChatInviteLink
{
    /// <summary>
    /// The invite link. If the link was created by another chat administrator, then the second part of the
    /// link will be replaced with “…”.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InviteLink { get; set; } = default!;

    /// <summary>
    /// Creator of the link
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User Creator { get; set; } = default!;

    /// <summary>
    /// <see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool CreatesJoinRequest { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the link is primary
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool IsPrimary { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the link is revoked
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Optional. Invite link name
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Optional. Point in time when the link will expire or has been expired
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ExpireDate { get; set; }

    /// <summary>
    /// Optional. Maximum number of users that can be members of the chat simultaneously after joining the chat
    /// via this invite link; 1-99999
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MemberLimit { get; set; }

    /// <summary>
    /// Optional. Number of pending join requests created using this link
    /// </summary>
    public int? PendingJoinRequestCount { get; set; }
}
