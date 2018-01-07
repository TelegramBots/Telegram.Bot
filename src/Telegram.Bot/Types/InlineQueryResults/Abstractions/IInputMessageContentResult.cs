namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    public interface IInputMessageContentResult
    {
        /// <summary>
        /// Content of the message to be sent
        /// </summary>
        InputMessageContentBase InputMessageContent { get; set; }
    }
}
