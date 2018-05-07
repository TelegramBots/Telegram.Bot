using System;
using System.Linq;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public class TestConfigurations
    {
        public string ApiToken { get; set; }

        public string Socks5Host { get; set; }

        public int Socks5Port { get; set; }

        public string AllowedUserNames { get; set; }

        public string[] AllowedUserNamesArray
        {
            get
            {
                if (_allowedUsers == null)
                {
                    _allowedUsers = AllowedUserNames
                        .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(n => n.Trim())
                        .ToArray();
                }
                return _allowedUsers;
            }
        }

        public string SuperGroupChatId { get; set; }

        public string ChannelChatId { get; set; }

        public string PaymentProviderToken { get; set; }

        public long? TesterPrivateChatId { get; set; }

        public int? StickerOwnerUserId { get; set; }

        public string RegularGroupMemberId { get; set; }

        private string[] _allowedUsers;
    }
}
