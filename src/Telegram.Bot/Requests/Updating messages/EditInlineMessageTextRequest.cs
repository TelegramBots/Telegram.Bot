using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit text and game messages. On success <see langword="true"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class EditInlineMessageTextRequest : RequestBase<bool>
{
    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonProperty(Required = Required.Always)]
    public required string InlineMessageId { get; init; }

    /// <summary>
    /// New text of the message, 1-4096 characters after entities parsing
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required string Text { get; init; }

    /// <inheritdoc cref="Documentation.ParseMode"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.Entities"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>
    /// Link preview generation options for the message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <inheritdoc cref="Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

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
    /// Initializes a new request with inlineMessageId and new text
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public EditInlineMessageTextRequest(string inlineMessageId, string text)
        : this()
    {
        InlineMessageId = inlineMessageId;
        Text = text;
    }

    /// <summary>
    /// Initializes a new request with inlineMessageId and new text
    /// </summary>
    public EditInlineMessageTextRequest()
        : base("editMessageText")
    { }
}
