using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageLiveLocationAsync" /> method.
    /// </summary>
    public class EditMessageLiveLocationParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; set; }

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
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        public EditMessageLiveLocationParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        public EditMessageLiveLocationParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Latitude" /> property.
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        public EditMessageLiveLocationParameters WithLatitude(float latitude)
        {
            Latitude = latitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Longitude" /> property.
        /// </summary>
        /// <param name="longitude">Longitude of location</param>
        public EditMessageLiveLocationParameters WithLongitude(float longitude)
        {
            Longitude = longitude;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageLiveLocationParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
