using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    /// <summary>
    /// Represents an inline query result with caption
    /// </summary>
    public interface ICaptionInlineQueryResult
    {
        /// <summary>
        /// Optional. Caption of the photo to be sent, 0-1024 characters after entities parsing
        /// </summary>
        string? Caption { get; set; }

        /// <summary>
        /// Optional. Mode for parsing entities in the photo caption. See <see href="https://core.telegram.org/bots/api#formatting-options">formatting options</see> for more details.
        /// </summary>
        ParseMode? ParseMode { get; set; }

        /// <summary>
        /// Optional. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode"/>
        /// </summary>
        MessageEntity[]? CaptionEntities { get; set; }
    }
}
