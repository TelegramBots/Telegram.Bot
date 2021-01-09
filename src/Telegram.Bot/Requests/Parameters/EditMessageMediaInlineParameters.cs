using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageMediaAsync" /> method.
    /// </summary>
    public class EditMessageMediaInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the sent message
        /// </summary>
        public string InlineMessageId { get; set; }

        /// <summary>
        ///     A JSON-serialized object for a new media content of the message
        /// </summary>
        public InputMediaBase Media { get; set; }

        /// <summary>
        ///     A JSON-serialized object for an inline keyboard.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        public EditMessageMediaInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Media" /> property.
        /// </summary>
        /// <param name="media">A JSON-serialized object for a new media content of the message</param>
        public EditMessageMediaInlineParameters WithMedia(InputMediaBase media)
        {
            Media = media;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageMediaInlineParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
