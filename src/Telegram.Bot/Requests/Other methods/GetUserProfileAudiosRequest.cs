// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get a list of profile audios for a user.<para>Returns: A <see cref="UserProfileAudios"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetUserProfileAudiosRequest() : RequestBase<UserProfileAudios>("getUserProfileAudios"), IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sequential number of the first audio to be returned. By default, all audios are returned.</summary>
    public int? Offset { get; set; }

    /// <summary>Limits the number of audios to be retrieved. Values between 1-100 are accepted. Defaults to 100.</summary>
    public int? Limit { get; set; }
}
