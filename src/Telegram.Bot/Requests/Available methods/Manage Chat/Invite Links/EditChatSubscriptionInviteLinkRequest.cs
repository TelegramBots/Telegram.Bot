// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit a subscription invite link created by the bot. The bot must have the <em>CanInviteUsers</em> administrator rights.<para>Returns: The edited invite link as a <see cref="ChatInviteLink"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditChatSubscriptionInviteLinkRequest() : RequestBase<ChatInviteLink>("editChatSubscriptionInviteLink"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The invite link to edit</summary>
    [JsonPropertyName("invite_link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InviteLink { get; set; }

    /// <summary>Invite link name; 0-32 characters</summary>
    public string? Name { get; set; }
}
