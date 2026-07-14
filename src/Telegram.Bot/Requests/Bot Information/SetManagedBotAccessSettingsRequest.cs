// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the access settings of a managed bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetManagedBotAccessSettingsRequest() : RequestBase<bool>("setManagedBotAccessSettings"), IUserTargetable
{
    /// <summary>User identifier of the managed bot whose access settings will be changed</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Pass <see langword="true"/> if only selected users can access the bot. The bot's owner can always access it.</summary>
    [JsonPropertyName("is_access_restricted")]
    public required bool IsAccessRestricted { get; set; }

    /// <summary>A list of up to 10 identifiers of users who will have access to the bot in addition to its owner. Ignored if <see cref="IsAccessRestricted">IsAccessRestricted</see> is <see langword="false"/>.</summary>
    [JsonPropertyName("added_user_ids")]
    public IEnumerable<long>? AddedUserIds { get; set; }
}
