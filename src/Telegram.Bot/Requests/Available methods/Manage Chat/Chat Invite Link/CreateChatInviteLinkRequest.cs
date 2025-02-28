// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to create an additional invite link for a chat. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. The link can be revoked using the method <see cref="TelegramBotClientExtensions.RevokeChatInviteLink">RevokeChatInviteLink</see>.<para>Returns: The new invite link as <see cref="ChatInviteLink"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class CreateChatInviteLinkRequest() : RequestBase<ChatInviteLink>("createChatInviteLink"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Invite link name; 0-32 characters</summary>
    public string? Name { get; set; }

    /// <summary>Point in time when the link will expire</summary>
    [JsonPropertyName("expire_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? ExpireDate { get; set; }

    /// <summary>The maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999</summary>
    [JsonPropertyName("member_limit")]
    public int? MemberLimit { get; set; }

    /// <summary><see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators. If <see langword="true"/>, <see cref="MemberLimit">MemberLimit</see> can't be specified</summary>
    [JsonPropertyName("creates_join_request")]
    public bool CreatesJoinRequest { get; set; }
}
