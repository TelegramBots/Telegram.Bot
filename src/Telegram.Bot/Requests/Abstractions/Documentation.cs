#nullable disable
#pragma warning disable 169
#pragma warning disable CA1823

using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable InconsistentNaming
namespace Telegram.Bot.Requests.Abstractions;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Type used to store documentation for shared properties in request types, use it using
/// <code>&lt;inheritdoc cref="Telegram.Bot.Requests.Abstractions.Documentation.[PropertyName]"/&gt;</code> syntax
/// </summary>
internal static class Documentation
{
    /// <summary>
    /// List of special entities that appear in the caption, which can be specified instead of
    /// <see cref="Types.Enums.ParseMode"/>
    /// </summary>
    static readonly object CaptionEntities;

    /// <summary>
    /// List of special entities that appear in message text, which can be specified instead of
    /// <see cref="Types.Enums.ParseMode"/>
    /// </summary>
    static readonly object Entities;

    /// <summary>
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a>
    /// options for more details.
    /// </summary>
    static readonly object ParseMode;

    /// <summary>
    /// Identifier of the inline message
    /// </summary>
    static readonly object InlineMessageId;

    /// <summary>
    /// An <see cref="InlineKeyboardMarkup">inline keyboard</see>
    /// </summary>
    static readonly object InlineReplyMarkup;

    /// <summary>
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user.
    /// </summary>
    static readonly object ReplyMarkup;

    /// <summary>
    /// Sends the message silently. Users will receive a notification with no sound.
    /// </summary>
    static readonly object DisableNotification;

    /// <summary>
    /// If the message is a reply, ID of the original message
    /// </summary>
    static readonly object ReplyToMessageId;

    /// <summary>
    /// Pass <see langword="true"/>, if the message should be sent even if the specified replied-to message is not found
    /// </summary>
    static readonly object AllowSendingWithoutReply;

    /// <summary>
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported
    /// server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's
    /// width and height should not exceed 320. Ignored if the file is not uploaded using
    /// multipart/form-data. Thumbnails can't be reused and can be only uploaded as a new file, so
    /// you can pass "attach://&lt;file_attach_name&gt;" if the thumbnail was uploaded using
    /// multipart/form-data under &lt;file_attach_name&gt;
    /// </summary>
    static readonly object Thumb;

    /// <summary>
    /// Protects the contents of sent messages from forwarding and saving
    /// </summary>
    static readonly object ProtectContent;
}
