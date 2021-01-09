using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendVenueAsync" /> method.
    /// </summary>
    public class SendVenueParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Latitude of the venue
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        ///     Longitude of the venue
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        ///     Name of the venue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Address of the venue
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Foursquare identifier of the venue
        /// </summary>
        public string FoursquareId { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for a custom reply
        ///     keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Foursquare type of the venue, if known. (For example,
        ///     "arts_entertainment/default", "arts_entertainment/aquarium" or "food/icecream".)
        /// </summary>
        public string FoursquareType { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendVenueParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Latitude" /> property.
        /// </summary>
        /// <param name="latitude">Latitude of the venue</param>
        public SendVenueParameters WithLatitude(float latitude)
        {
            Latitude = latitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Longitude" /> property.
        /// </summary>
        /// <param name="longitude">Longitude of the venue</param>
        public SendVenueParameters WithLongitude(float longitude)
        {
            Longitude = longitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Title" /> property.
        /// </summary>
        /// <param name="title">Name of the venue</param>
        public SendVenueParameters WithTitle(string title)
        {
            Title = title;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Address" /> property.
        /// </summary>
        /// <param name="address">Address of the venue</param>
        public SendVenueParameters WithAddress(string address)
        {
            Address = address;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="FoursquareId" /> property.
        /// </summary>
        /// <param name="foursquareId">Foursquare identifier of the venue</param>
        public SendVenueParameters WithFoursquareId(string foursquareId)
        {
            FoursquareId = foursquareId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </param>
        public SendVenueParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendVenueParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for a custom reply
        ///     keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendVenueParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="FoursquareType" /> property.
        /// </summary>
        /// <param name="foursquareType">
        ///     Foursquare type of the venue, if known. (For example,
        ///     "arts_entertainment/default", "arts_entertainment/aquarium" or "food/icecream".)
        /// </param>
        public SendVenueParameters WithFoursquareType(string foursquareType)
        {
            FoursquareType = foursquareType;
            return this;
        }
    }
}
