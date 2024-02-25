using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get a list of profile pictures for a user. Returns a
/// <see cref="UserProfilePhotos"/> object.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetUserProfilePhotosRequest : RequestBase<UserProfilePhotos>, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public required long UserId { get; init; }

    /// <summary>
    /// Sequential number of the first photo to be returned. By default, all photos are returned
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Offset { get; set; }

    /// <summary>
    /// Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Limit { get; set; }

    /// <summary>
    /// Initializes a new request with userId
    /// </summary>
    /// <param name="userId">Unique identifier of the target user</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required parameters")]
    public GetUserProfilePhotosRequest(long userId)
        : this()
    {
        UserId = userId;
    }

    /// <summary>
    /// Initializes a new request with userId
    /// </summary>
    public GetUserProfilePhotosRequest()
        : base("getUserProfilePhotos")
    { }
}
