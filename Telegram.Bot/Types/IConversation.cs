namespace Telegram.Bot.Types
{
    public interface IConversation
    {
        int Id { get; set; }
        ConversationType Type { get; }
    }
}
