using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// ChatMember status
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChatMemberStatus
    {
        /// <summary>
        /// Creator of the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "creator")]
        Creator,

        /// <summary>
        /// Administrator of the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "administrator")]
        Administrator,

        /// <summary>
        /// Normal member of the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "member")]
        Member,

        /// <summary>
        /// A <see cref="User"/> who left the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "left")]
        Left,

        /// <summary>
        /// A <see cref="User"/> who was kicked from the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "kicked")]
        Kicked,

        /// <summary>
        /// A <see cref="User"/> who is restricted in the <see cref="Chat"/>
        /// </summary>
        [EnumMember(Value = "restricted")]
        Restricted
    }
}
