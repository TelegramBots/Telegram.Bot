using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with formatted text
    /// </summary>
    public interface IFormattableMessage
    {
        /// <summary>
        /// Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message
        /// </summary>
        ParseMode ParseMode { get; set; }

        /// <summary>
        /// List of special entities that appear in the caption, which can be specified instead of parse_mode
        /// </summary>
        IEnumerable<MessageEntity> CaptionEntities { get; set; }
    }
}
