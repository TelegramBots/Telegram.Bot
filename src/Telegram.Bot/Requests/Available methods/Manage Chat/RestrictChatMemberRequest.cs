namespace Telegram.Bot.Requests;

/// <summary>Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate administrator rights. Pass <em>True</em> for all permissions to lift restrictions from a user.<para>Returns: </para></summary>
public partial class RestrictChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>An object for new user permissions</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatPermissions Permissions { get; set; }

    /// <summary>Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the <em>CanSendOtherMessages</em> and <em>CanAddWebPagePreviews</em> permissions will imply the <em>CanSendMessages</em>, <em>CanSendAudios</em>, <em>CanSendDocuments</em>, <em>CanSendPhotos</em>, <em>CanSendVideos</em>, <em>CanSendVideoNotes</em>, and <em>CanSendVoiceNotes</em> permissions; the <em>CanSendPolls</em> permission will imply the <em>CanSendMessages</em> permission.</summary>
    public bool UseIndependentChatPermissions { get; set; }

    /// <summary>Date when restrictions will be lifted for the user, in UTC. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever</summary>
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }

    /// <summary>Initializes an instance of <see cref="RestrictChatMemberRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="permissions">An object for new user permissions</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public RestrictChatMemberRequest(ChatId chatId, long userId, ChatPermissions permissions) : this()
    {
        ChatId = chatId;
        UserId = userId;
        Permissions = permissions;
    }

    /// <summary>Instantiates a new <see cref="RestrictChatMemberRequest"/></summary>
    public RestrictChatMemberRequest() : base("restrictChatMember") { }
}
