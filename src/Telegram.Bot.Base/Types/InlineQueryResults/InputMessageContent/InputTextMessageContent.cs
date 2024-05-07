using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents the content of a text message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputTextMessageContent : InputMessageContent
{
    /// <summary>
    /// Text of the message to be sent, 1-4096 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string MessageText { get; init; }

    /// <summary>
    /// Optional. Mode for
    /// <a href="https://core.telegram.org/bots/api#formatting-options">parsing entities</a> in the message
    /// text. See formatting options for more details.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <summary>
    /// Optional. List of special entities that appear in message text, which can be specified
    /// instead of <see cref="ParseMode"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? Entities { get; set; } // ToDo: add test

    /// <summary>
    /// Optional. Link preview generation options for the message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>
    /// Disables link previews for links in this message
    /// </summary>
    [Obsolete($"This property is deprecated, use {nameof(LinkPreviewOptions)} instead")]
    [JsonIgnore]
    public bool? DisableWebPagePreview
    {
        get => LinkPreviewOptions?.IsDisabled;
        set
        {
            LinkPreviewOptions ??= new();
            LinkPreviewOptions.IsDisabled = value;
        }
    }

    /// <summary>
    /// Initializes a new input text message content
    /// </summary>
    /// <param name="messageText">The text of the message</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputTextMessageContent(string messageText)
    {
        MessageText = messageText;
    }

    /// <summary>
    /// Initializes a new input text message content
    /// </summary>
    public InputTextMessageContent()
    { }
}
