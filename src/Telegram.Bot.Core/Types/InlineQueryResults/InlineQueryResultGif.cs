using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an animated GIF file.
    /// By default, this animated GIF file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    public class InlineQueryResultGif : InlineQueryResultBase,
                                        ICaptionInlineQueryResult,
                                        IThumbnailUrlInlineQueryResult,
                                        ITitleInlineQueryResult,
                                        IInputMessageContentResult
    {
        /// <summary>
        /// A valid URL for the GIF file. File size must not exceed 1MB
        /// </summary>
        public string GifUrl { get; set; }

        /// <summary>
        /// Optional. Width of the GIF.
        /// </summary>
        public int GifWidth { get; set; }

        /// <summary>
        /// Optional. Height of the GIF.
        /// </summary>
        public int GifHeight { get; set; }

        /// <summary>
        /// Optional. Duration of the GIF.
        /// </summary>
        public int GifDuration { get; set; }

        /// <inheritdoc />
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public string ThumbUrl { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultGif()
            : base(InlineQueryResultType.Gif)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="gifUrl">Width of the GIF</param>
        /// <param name="thumbUrl">Url of the thumbnail for the result.</param>
        public InlineQueryResultGif(string id, string gifUrl, string thumbUrl)
            : base(InlineQueryResultType.Gif, id)
        {
            GifUrl = gifUrl;
            ThumbUrl = thumbUrl;
        }
    }
}
