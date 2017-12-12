using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Abstractions
{
    public interface IReplyMarkupMessage<TMarkup>
        where TMarkup : IReplyMarkup
    {
        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        TMarkup ReplyMarkup { get; set; }
    }
}
