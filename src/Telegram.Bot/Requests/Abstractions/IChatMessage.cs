using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a request having chat_id parameter
    /// </summary>
    public interface IChatMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        ChatId ChatId { get; }
    }
}
