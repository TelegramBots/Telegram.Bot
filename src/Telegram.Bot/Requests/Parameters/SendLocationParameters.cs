using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendLocationAsync" /> method.
    /// </summary>
    public class SendLocationParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Latitude of location
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        ///     Longitude of location
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        ///     Period in seconds for which the location will be updated. Should be between 60 and 86400.
        /// </summary>
        public int LivePeriod { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard
        ///     or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendLocationParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Latitude" /> property.
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        public SendLocationParameters WithLatitude(float latitude)
        {
            Latitude = latitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Longitude" /> property.
        /// </summary>
        /// <param name="longitude">Longitude of location</param>
        public SendLocationParameters WithLongitude(float longitude)
        {
            Longitude = longitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="LivePeriod" /> property.
        /// </summary>
        /// <param name="livePeriod">Period in seconds for which the location will be updated. Should be between 60 and 86400.</param>
        public SendLocationParameters WithLivePeriod(int livePeriod)
        {
            LivePeriod = livePeriod;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendLocationParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendLocationParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendLocationParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
