using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a request having entities that appear in the caption
    /// </summary>
    public interface ICaptionEntities
    {
        /// <summary>
        /// List of special entities that appear in the caption, which can be specified instead of <see cref="Types.Enums.ParseMode"/>
        /// </summary>
        IEnumerable<MessageEntity>? CaptionEntities { get; set; }
    }
}
