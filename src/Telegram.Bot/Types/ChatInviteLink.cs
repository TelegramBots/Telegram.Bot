using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// Represents an invite link for a chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatInviteLink
{
    /// <summary>
    /// The invite link. If the link was created by another chat administrator, then the second part of the
    /// link will be replaced with “…”.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string InviteLink { get; set; } = default!;

    /// <summary>
    /// Creator of the link
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User Creator { get; set; } = default!;

    /// <summary>
    /// <see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CreatesJoinRequest { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the link is primary
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsPrimary { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the link is revoked
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Optional. Invite link name
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Optional. Point in time when the link will expire or has been expired
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DateTime? ExpireDate { get; set; }

    /// <summary>
    /// Optional. Maximum number of users that can be members of the chat simultaneously after joining the chat
    /// via this invite link; 1-99999
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MemberLimit { get; set; }

    /// <summary>
    /// Optional. Number of pending join requests created using this link
    /// </summary>
    public int? PendingJoinRequestCount { get; set; }
}
