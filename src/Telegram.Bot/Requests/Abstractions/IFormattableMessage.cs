using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with formatted text
    /// </summary>
    public interface IFormattableMessage
    {
        /// <summary>
        /// Mode for parsing entities in the new caption. See <see href="https://core.telegram.org/bots/api#formatting-options">formatting</see> options for more details.
        /// </summary>
        ParseMode? ParseMode { get; set; }
    }
}
