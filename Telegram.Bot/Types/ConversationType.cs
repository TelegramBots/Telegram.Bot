namespace Telegram.Bot.Types
{
    public class ConversationType
    {

        private ConversationType(string type)
        {
            Type = type;
        }

        public static ConversationType UserChat => new ConversationType("user");
        public static ConversationType GroupChat => new ConversationType("group");

        public string Type { get; }
    }
}
