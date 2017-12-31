namespace Telegram.Bot.Requests.Abstractions
{
    public interface IReplyMessage
    {
        /// <summary>
        /// Additional interface options. If the message is a reply, ID of the original message.
        /// </summary>
        int ReplyToMessageId { get; set; }
    }
}
