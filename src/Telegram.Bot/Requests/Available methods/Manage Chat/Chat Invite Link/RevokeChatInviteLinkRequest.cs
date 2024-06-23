namespace Telegram.Bot.Requests;

/// <summary>Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new link is automatically generated. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: The revoked invite link as <see cref="ChatInviteLink"/> object.</para></summary>
public partial class RevokeChatInviteLinkRequest : RequestBase<ChatInviteLink>, IChatTargetable
{
    /// <summary>Unique identifier of the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The invite link to revoke</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InviteLink { get; set; }

    /// <summary>Initializes an instance of <see cref="RevokeChatInviteLinkRequest"/></summary>
    /// <param name="chatId">Unique identifier of the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="inviteLink">The invite link to revoke</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public RevokeChatInviteLinkRequest(ChatId chatId, string inviteLink) : this()
    {
        ChatId = chatId;
        InviteLink = inviteLink;
    }

    /// <summary>Instantiates a new <see cref="RevokeChatInviteLinkRequest"/></summary>
    public RevokeChatInviteLinkRequest() : base("revokeChatInviteLink") { }
}
