namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetUserProfilePhotosAsync" /> method.
    /// </summary>
    public class GetUserProfilePhotosParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Sequential number of the first photo to be returned. By default, all photos are returned.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public GetUserProfilePhotosParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Offset" /> property.
        /// </summary>
        /// <param name="offset">Sequential number of the first photo to be returned. By default, all photos are returned.</param>
        public GetUserProfilePhotosParameters WithOffset(int offset)
        {
            Offset = offset;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Limit" /> property.
        /// </summary>
        /// <param name="limit">Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100.</param>
        public GetUserProfilePhotosParameters WithLimit(int limit)
        {
            Limit = limit;
            return this;
        }
    }
}