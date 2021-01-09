using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageLiveLocationAsync" /> method.
    /// </summary>
    public class EditMessageLiveLocationInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the sent message
        /// </summary>
        public string InlineMessageId { get; set; }

        /// <summary>
        ///     Latitude of location
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        ///     Longitude of location
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        ///     A JSON-serialized object for an inline keyboard.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        public EditMessageLiveLocationInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Latitude" /> property.
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        public EditMessageLiveLocationInlineParameters WithLatitude(float latitude)
        {
            Latitude = latitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Longitude" /> property.
        /// </summary>
        /// <param name="longitude">Longitude of location</param>
        public EditMessageLiveLocationInlineParameters WithLongitude(float longitude)
        {
            Longitude = longitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageLiveLocationInlineParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
