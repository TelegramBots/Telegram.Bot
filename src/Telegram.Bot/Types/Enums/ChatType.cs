using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of a Chat
    /// </summary>
    public enum ChatType
    {
        [EnumMember(Value = "private")]
        Private,

        [EnumMember(Value = "group")]
        Group,

        [EnumMember(Value = "channel")]
        Channel,

        [EnumMember(Value = "supergroup")]
        Supergroup
    }
}
