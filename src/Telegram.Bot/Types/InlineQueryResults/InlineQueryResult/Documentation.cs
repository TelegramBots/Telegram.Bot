#nullable disable
#pragma warning disable 169
// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults
{
    // ReSharper disable once UnusedType.Global
    internal abstract class Documentation
    {
        Documentation()
        { }

        /// <summary>
        /// Content of the message to be sent instead of the result
        /// </summary>
        object InputMessageContent { get; set; }

        /// <summary>
        /// Caption of the result to be sent, 0-1024 characters after entities parsing
        /// </summary>
        object Caption;

        /// <summary>
        /// Mode for parsing entities in the result caption. See
        /// <see href="https://core.telegram.org/bots/api#formatting-options">formatting options</see>
        /// for more details.
        /// </summary>
        object ParseMode;

        /// <summary>
        /// List of special entities that appear in the caption, which can be specified
        /// instead of <see cref="ParseMode"/>
        /// </summary>
        object CaptionEntities;

        /// <summary>
        /// Location latitude in degrees
        /// </summary>
        object Latitude;

        /// <summary>
        /// Location longitude in degrees
        /// </summary>
        object Longitude;

        /// <summary>
        /// Thumbnail width
        /// </summary>
        object ThumbWidth;

        /// <summary>
        /// Thumbnail height
        /// </summary>
        object ThumbHeight;

        /// <summary>
        /// Url of the thumbnail for the result
        /// </summary>
        object ThumbUrl;
    }
}
