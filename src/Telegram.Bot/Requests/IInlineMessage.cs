namespace Telegram.Bot.Requests
{
    public interface IInlineMessage
    {
        /// <summary>
        /// Identifier of the inline message
        /// </summary>
        string InlineMessageId { get; set; }
    }
}
