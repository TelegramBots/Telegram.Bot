// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate administrator rights. Pass <em>True</em> for all permissions to lift restrictions from a user.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RestrictChatMemberRequest() : RequestBase<bool>("restrictChatMember"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>An object for new user permissions</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatPermissions Permissions { get; set; }

    /// <summary>Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the <em>CanSendOtherMessages</em> and <em>CanAddWebPagePreviews</em> permissions will imply the <em>CanSendMessages</em>, <em>CanSendAudios</em>, <em>CanSendDocuments</em>, <em>CanSendPhotos</em>, <em>CanSendVideos</em>, <em>CanSendVideoNotes</em>, and <em>CanSendVoiceNotes</em> permissions; the <em>CanSendPolls</em> permission will imply the <em>CanSendMessages</em> permission.</summary>
    [JsonPropertyName("use_independent_chat_permissions")]
    public bool UseIndependentChatPermissions { get; set; }

    /// <summary>Date when restrictions will be lifted for the user, in UTC. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever</summary>
    [JsonPropertyName("until_date")]
    [JsonConverter(typeof(BanTimeConverter))]
    public DateTime? UntilDate { get; set; }
}
