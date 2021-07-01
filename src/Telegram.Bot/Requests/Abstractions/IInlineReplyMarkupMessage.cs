using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with an inline reply markup
    /// </summary>
    public interface IInlineReplyMarkupMessage : IReplyMarkupMessage<InlineKeyboardMarkup>
    {
        /// <summary>
        /// An <see cref="InlineKeyboardMarkup">inline keyboard</see>
        /// </summary>
        new InlineKeyboardMarkup? ReplyMarkup { get; set; }
    }
}
