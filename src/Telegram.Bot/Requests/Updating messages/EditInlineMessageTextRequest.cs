using System.Collections.Generic;
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
    public string InlineMessageId { get; }

    /// <summary>
    /// New text of the message, 1-4096 characters after entities parsing
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Text { get; }

    /// <inheritdoc cref="Documentation.ParseMode"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.Entities"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>
    /// Disables link previews for links in this message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableWebPagePreview { get; set; }

    /// <inheritdoc cref="Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with inlineMessageId and new text
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    public EditInlineMessageTextRequest(string inlineMessageId, string text)
        : base("editMessageText")
    {
        InlineMessageId = inlineMessageId;
        Text = text;
    }
}
