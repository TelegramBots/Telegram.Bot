using System;
using System.Linq;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestConfigurations
    {
        public string ApiToken { get; set; }

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

        public string Currency { get; set; }

        public string Prices { get; set; }

        public int[] PricesArray {
            get
            {
                if (_prices == null)
                {
                    _prices = Prices
                        .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(p =>
                        {
                            var isParsed = int.TryParse(p.Trim(), out var price);
                            return (isParsed, price);
                        })
                        .Where(p => p.isParsed)
                        .Select(p => p.price)
                        .ToArray();
                }

                return _prices;
            }
        }

        public int? StickerOwnerUserId { get; set; }

        public string RegularMemberUserId { get; set; }

        public string RegularMemberUserName { get; set; }

        public string RegularMemberPrivateChatId { get; set; }

        private string[] _allowedUsers;

        private int[] _prices;
    }
}
