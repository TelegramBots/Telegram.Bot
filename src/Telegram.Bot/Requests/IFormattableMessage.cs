using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests
{
    public interface IFormattableMessage
    {
        /// <summary>
        /// Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message
        /// </summary>
        ParseMode ParseMode { get; set; }
    }
}
