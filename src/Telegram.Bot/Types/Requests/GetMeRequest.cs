namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get information about the bot
    /// </summary>
    public class GetMeRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new object of the <see cref="GetMeRequest"/> class
        /// </summary>
        public GetMeRequest() : base("getMe") { }
    }
}
