using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound).
    /// By default, this animated MPEG-4 file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    public class InlineQueryResultMpeg4Gif : InlineQueryResultBase,
        ICaptionInlineQueryResult,
        IThumbnailUrlInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult
    {
        /// <summary>
        /// A valid URL for the MP4 file. File size must not exceed 1MB.
        /// </summary>
        public string Mpeg4Url { get; set; }

        /// <inheritdoc />
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        public int Mpeg4Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        public int Mpeg4Height { get; set; }

        /// <summary>
        /// Optional. Duration of the Video
        /// </summary>
        public int Mpeg4Duration { get; set; }

        /// <inheritdoc />
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultMpeg4Gif()
            : base(InlineQueryResultType.Mpeg4Gif)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="mpeg4Url">A valid URL for the MP4 file. File size must not exceed 1MB.</param>
        /// <param name="thumbUrl">Url of the thumbnail for the result.</param>
        public InlineQueryResultMpeg4Gif(string id, string mpeg4Url, string thumbUrl)
            : base(InlineQueryResultType.Mpeg4Gif, id)
        {
            Mpeg4Url = mpeg4Url;
            ThumbUrl = thumbUrl;
        }
    }
}
