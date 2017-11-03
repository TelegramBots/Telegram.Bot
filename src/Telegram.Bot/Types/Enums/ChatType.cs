using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of a <see cref="Chat"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChatType
    {
        /// <summary>
        /// Normal one to one <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "private")]
        Private,

        /// <summary>
        /// Normal groupchat
        /// </summary>
        [EnumMember(Value = "group")]
        Group,

        /// <summary>
        /// A channel
        /// </summary>
        [EnumMember(Value = "channel")]
        Channel,

        /// <summary>
        /// A supergroup
        /// </summary>
        [EnumMember(Value = "supergroup")]
        Supergroup
    }
}
