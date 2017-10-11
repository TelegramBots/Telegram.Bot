using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get an users profile photos
    /// </summary>
    public class GetUserProfilePhotosRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserProfilePhotosRequest"/> class
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="offset">Sequential number of the first photo to be returned. By default, all photos are returned.</param>
        /// <param name="limit">Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.</param>
        public GetUserProfilePhotosRequest(int userId, int? offset = null, int limit = 100) : base("getUserProfilePhotos", 
            new Dictionary<string, object>
            {
                {"user_id", userId},
                {"offset", offset},
                {"limit", limit}
            })
        {

        }
    }
}
