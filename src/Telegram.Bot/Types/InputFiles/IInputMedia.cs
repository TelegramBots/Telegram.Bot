using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A marker interface for input media content
/// </summary>
public interface IInputMedia
{
    /// <summary>
    /// Type of the media
    /// </summary>
    InputMediaType Type { get; }

    /// <summary>
    /// Media to send
    /// </summary>
    InputMedia Media { get; }

    /// <summary>
    /// Optional. Caption of the photo to be sent, 0-1024 characters after entities parsing
    /// </summary>
    string? Caption { get; }

    /// <summary>
    /// Optional. Mode for parsing entities in the photo caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.
    /// </summary>
    ParseMode? ParseMode { get; }

    /// <summary>
    /// Optional. List of special entities that appear in the caption, which can be specified
    /// instead of <see cref="ParseMode"/>
    /// </summary>
    MessageEntity[]? CaptionEntities { get; }
}
