namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message that is a reply to another message
    /// </summary>
    public interface IReplyMessage
    {
        /// <summary>
        /// Additional interface options. If the message is a reply, ID of the original message.
        /// </summary>
        int ReplyToMessageId { get; set; }
    }
}
