// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new link is automatically generated. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: The revoked invite link as <see cref="ChatInviteLink"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RevokeChatInviteLinkRequest() : RequestBase<ChatInviteLink>("revokeChatInviteLink"), IChatTargetable
{
    /// <summary>Unique identifier of the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The invite link to revoke</summary>
    [JsonPropertyName("invite_link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InviteLink { get; set; }
}
