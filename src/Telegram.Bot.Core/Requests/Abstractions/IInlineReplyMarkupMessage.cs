using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with inline reply markup
    /// </summary>
    public interface IInlineReplyMarkupMessage : IReplyMarkupMessage<InlineKeyboardMarkup>
    {
        /// <summary>
        /// A JSON-serialized object for an inline keyboard
        /// </summary>
        new InlineKeyboardMarkup ReplyMarkup { get; set; }
    }
}
