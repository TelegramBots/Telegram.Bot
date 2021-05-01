using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a request having entities that appear in message text
    /// </summary>
    public interface IEntities
    {

        /// <summary>
        /// List of special entities that appear in message text, which can be specified instead of parse_mode
        /// </summary>
        IEnumerable<MessageEntity> Entities { get; set; }
    }
}
