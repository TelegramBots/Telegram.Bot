using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with reply markup
    /// </summary>
    public interface IReplyMarkupMessage<TMarkup>
        where TMarkup : IReplyMarkup
    {
        /// <summary>
        /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>, <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to <see cref="ForceReplyMarkup">force a reply</see> from the user.
        /// </summary>
        TMarkup? ReplyMarkup { get; set; }
    }
}
