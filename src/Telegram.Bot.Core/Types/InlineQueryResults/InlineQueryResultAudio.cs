using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an mp3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    public class InlineQueryResultAudio : InlineQueryResultBase,
        ICaptionInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult
    {
        /// <summary>
        /// A valid URL for the audio file
        /// </summary>
        public string AudioUrl { get; set; }

        /// <summary>
        /// Optional. Performer
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// Optional. Audio duration in seconds
        /// </summary>
        public int AudioDuration { get; set; }

        /// <inheritdoc />
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultAudio()
            : base(InlineQueryResultType.Audio)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="audioUrl">A valid URL for the audio file</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultAudio(string id, string audioUrl, string title)
            : base(InlineQueryResultType.Audio, id)
        {
            AudioUrl = audioUrl;
            Title = title;
        }
    }
}
