using System.Collections.Generic;
using System.Linq;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// ChatMember status
    /// </summary>
    public enum ChatMemberStatus
    {
        Creator,
        Administrator,
        Member,
        Left,
        Kicked,
    }

    internal static class ChatMemberStatusExtension
    {
        private static readonly Dictionary<ChatMemberStatus, string> Map = new Dictionary<ChatMemberStatus, string>
        {
            {ChatMemberStatus.Creator, "creator"},
            {ChatMemberStatus.Administrator, "administrator"},
            {ChatMemberStatus.Member, "member"},
            {ChatMemberStatus.Left, "left"},
            {ChatMemberStatus.Kicked, "kicked"},
        };

        internal static string ToStatusString(this ChatMemberStatus status)
        {
            return Map[status];
        }

        internal static ChatMemberStatus ToChatMemberStatus(this string status)
        {
            return Map.FirstOrDefault(s => s.Value == status).Key;
        }
    }
}
