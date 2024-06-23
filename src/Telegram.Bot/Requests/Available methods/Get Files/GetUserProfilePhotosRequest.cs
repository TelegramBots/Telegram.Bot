namespace Telegram.Bot.Requests;

/// <summary>Use this method to get a list of profile pictures for a user.<para>Returns: A <see cref="UserProfilePhotos"/> object.</para></summary>
public partial class GetUserProfilePhotosRequest : RequestBase<UserProfilePhotos>, IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Sequential number of the first photo to be returned. By default, all photos are returned.</summary>
    public int? Offset { get; set; }

    /// <summary>Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100.</summary>
    public int? Limit { get; set; }

    /// <summary>Initializes an instance of <see cref="GetUserProfilePhotosRequest"/></summary>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetUserProfilePhotosRequest(long userId) : this() => UserId = userId;

    /// <summary>Instantiates a new <see cref="GetUserProfilePhotosRequest"/></summary>
    public GetUserProfilePhotosRequest() : base("getUserProfilePhotos") { }
}
