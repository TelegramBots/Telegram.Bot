namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit a non-primary invite link created by the bot. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: The edited invite link as a <see cref="ChatInviteLink"/> object.</para></summary>
public partial class EditChatInviteLinkRequest : RequestBase<ChatInviteLink>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The invite link to edit</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InviteLink { get; set; }

    /// <summary>Invite link name; 0-32 characters</summary>
    public string? Name { get; set; }

    /// <summary>Point in time when the link will expire</summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? ExpireDate { get; set; }

    /// <summary>The maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999</summary>
    public int? MemberLimit { get; set; }

    /// <summary><see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators. If <see langword="true"/>, <see cref="MemberLimit">MemberLimit</see> can't be specified</summary>
    public bool CreatesJoinRequest { get; set; }

    /// <summary>Initializes an instance of <see cref="EditChatInviteLinkRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="inviteLink">The invite link to edit</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditChatInviteLinkRequest(ChatId chatId, string inviteLink) : this()
    {
        ChatId = chatId;
        InviteLink = inviteLink;
    }

    /// <summary>Instantiates a new <see cref="EditChatInviteLinkRequest"/></summary>
    public EditChatInviteLinkRequest() : base("editChatInviteLink") { }
}
