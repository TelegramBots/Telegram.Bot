// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to log out from the cloud Bot API server before launching the bot locally. You must log out the bot before running it locally, otherwise there is no guarantee that the bot will receive updates. After a successful call, you can immediately log in on a local server, but will not be able to log in back to the cloud Bot API server for 10 minutes. Returns True on success. Requires no parameters.
    /// </summary>
    /// <see cref="https://core.telegram.org/bots/api#logout"/>
    public class LogOutRequest : ParameterlessRequest<bool>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public LogOutRequest() : base("logOut")
        { }
    }
}
