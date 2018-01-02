namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    public interface ICaptionInlineQueryResult
    {
        /// <summary>
        /// Optional. Caption of the result to be sent, 0-200 characters.
        /// </summary>
        string Caption { get; set; }
    }
}
