using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a photo.
    /// By default, this photo will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of photo.
    /// </summary>
    public class InlineQueryResultPhoto : InlineQueryResultBase,
        ICaptionInlineQueryResult,
        IThumbnailUrlInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult
    {
        /// <inheritdoc />
        public string ThumbUrl { get; set; }

        /// <summary>
        /// A valid URL of the photo. Photo size must not exceed 5MB.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Optional. Width of the photo
        /// </summary>
        public int PhotoWidth { get; set; }

        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        public int PhotoHeight { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultPhoto()
            : base(InlineQueryResultType.Photo)
        {
        }

        /// <summary>
        /// Initializes a new inline query representing a link to a photo
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="photoUrl">A valid URL of the photo. Photo size must not exceed 5MB.</param>
        /// <param name="thumbUrl">Optional. Url of the thumbnail for the result.</param>
        public InlineQueryResultPhoto(string id, string photoUrl, string thumbUrl)
            : base(InlineQueryResultType.Photo, id)
        {
            PhotoUrl = photoUrl;
            ThumbUrl = thumbUrl;
        }
    }
}
