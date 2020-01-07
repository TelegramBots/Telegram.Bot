using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with reply markup
    /// </summary>
    public interface IReplyMarkupMessage<TMarkup>
        where TMarkup : class, IReplyMarkup
    {
        /// <summary>
        /// A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions to remove reply
        /// keyboard or to force a reply from the user.
        /// </summary>
        TMarkup? ReplyMarkup { get; set; }
    }
}
