using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of the <see cref="Chat"/>, from which the inline query was sent
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ChatType
    {
        /// <summary>
        /// Normal one to one <see cref="Chat"/>
        /// </summary>
        Private,

        /// <summary>
        /// Normal groupchat
        /// </summary>
        Group,

        /// <summary>
        /// A channel
        /// </summary>
        Channel,

        /// <summary>
        /// A supergroup
        /// </summary>
        Supergroup,

        /// <summary>
        /// “sender” for a private chat with the inline query sender
        /// </summary>
        Sender
    }
}
