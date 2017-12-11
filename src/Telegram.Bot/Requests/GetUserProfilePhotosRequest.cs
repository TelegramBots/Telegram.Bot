using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get a list of profile pictures for a user
    /// </summary>
    public class GetUserProfilePhotosRequest : RequestBase<UserProfilePhotos>
    {
        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId{ get; set; }

        /// <summary>
        /// Sequential number of the first photo to be returned. By default, all photos are returned.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Offset { get; set; }

        /// <summary>
        /// Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Limit { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetUserProfilePhotosRequest()
            : base("getUserProfilePhotos")
        { }

        /// <summary>
        /// Initializes a new request with user id
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public GetUserProfilePhotosRequest(int userId)
            : this()
        {
            UserId = userId;
        }
    }
}
