using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// A simple method for testing your bot's auth token.
    /// </summary>
    public class GetMeRequest : RequestBase<User>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetMeRequest()
            : base("getMe")
        { }
    }
}
