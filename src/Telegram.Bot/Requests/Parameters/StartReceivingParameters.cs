using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.StartReceiving" /> method.
    /// </summary>
    public class StartReceivingParameters : ParametersBase
    {
        /// <summary>
        ///     List the types of updates you want your bot to receive.
        /// </summary>
        public UpdateType[] AllowedUpdates { get; set; }

        /// <summary>
        ///     Sets <see cref="AllowedUpdates" /> property.
        /// </summary>
        /// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
        public StartReceivingParameters WithAllowedUpdates(UpdateType[] allowedUpdates)
        {
            AllowedUpdates = allowedUpdates;
            return this;
        }
    }
}