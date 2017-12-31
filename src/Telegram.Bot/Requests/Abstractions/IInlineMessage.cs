namespace Telegram.Bot.Requests.Abstractions
{
    public interface IInlineMessage
    {
        /// <summary>
        /// Identifier of the inline message
        /// </summary>
        string InlineMessageId { get; }
    }
}
