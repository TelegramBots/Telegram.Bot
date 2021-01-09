using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.StopMessageLiveLocationAsync" /> method.
    /// </summary>
    public class StopMessageLiveLocationInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Identifier of the inline message
        /// </summary>
        public string InlineMessageId { get; set; }

        /// <summary>
        ///     A JSON-serialized object for an inline keyboard.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        public StopMessageLiveLocationInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public StopMessageLiveLocationInlineParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
