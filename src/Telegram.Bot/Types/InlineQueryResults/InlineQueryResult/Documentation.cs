#nullable disable
#pragma warning disable 169
#pragma warning disable CA1823
// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

// ReSharper disable once UnusedType.Global
internal static class Documentation
{
    /// <summary>
    /// Content of the message to be sent instead of the result
    /// </summary>
    static object InputMessageContent;

    /// <summary>
    /// Caption of the result to be sent, 0-1024 characters after entities parsing
    /// </summary>
    static object Caption;

    /// <summary>
    /// Mode for parsing entities in the result caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a>
    /// for more details.
    /// </summary>
    static object ParseMode;

    /// <summary>
    /// List of special entities that appear in the caption, which can be specified
    /// instead of <see cref="ParseMode"/>
    /// </summary>
    static object CaptionEntities;

    /// <summary>
    /// Location latitude in degrees
    /// </summary>
    static object Latitude;

    /// <summary>
    /// Location longitude in degrees
    /// </summary>
    static object Longitude;

    /// <summary>
    /// Thumbnail width
    /// </summary>
    static object ThumbWidth;

    /// <summary>
    /// Thumbnail height
    /// </summary>
    static object ThumbHeight;

    /// <summary>
    /// Url of the thumbnail for the result
    /// </summary>
    static object ThumbUrl;
}
