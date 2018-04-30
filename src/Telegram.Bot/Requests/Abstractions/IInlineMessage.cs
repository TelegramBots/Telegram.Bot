namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents an inline message
    /// </summary>
    public interface IInlineMessage
    {
        /// <summary>
        /// Identifier of the inline message
        /// </summary>
        string InlineMessageId { get; }
    }
}
