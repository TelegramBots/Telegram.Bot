using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// ChatMember status
    /// </summary>
    public enum ChatMemberStatus
    {
        [EnumMember(Value = "creator")]
        Creator,

        [EnumMember(Value = "administrator")]
        Administrator,

        [EnumMember(Value = "member")]
        Member,

        [EnumMember(Value = "left")]
        Left,

        [EnumMember(Value = "kicked")]
        Kicked,
    }
}
