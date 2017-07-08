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
                    _allowedUsers = AllowedUserNames.Split(',');
                }
                return _allowedUsers;
            }
        }

        public string PaymentProviderToken { get; set; }

        public string PrivateChatId { get; set; }

        public string SuperGroupChatId { get; set; }

        private string[] _allowedUsers;
    }
}
