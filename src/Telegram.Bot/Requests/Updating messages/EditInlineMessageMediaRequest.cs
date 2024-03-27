using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit animation, audio, document, photo, or video messages. If a message is
/// part of a message album, then it can be edited only to an audio for audio albums, only to a
/// document for document albums and to a photo or a video otherwise. Use a previously uploaded file
/// via its <see cref="InputFileId"/> or specify a URL. On success
/// <see langword="true"/> is returned.
/// </summary>
public class EditInlineMessageMediaRequest : RequestBase<bool>
{
    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; init; }

    /// <summary>
    /// A new media content of the message
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMedia Media { get; init; }

    /// <inheritdoc cref="Documentation.InlineReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with inlineMessageId and new media
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="media">A new media content of the message</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public EditInlineMessageMediaRequest(string inlineMessageId, InputMedia media)
        : this()
    {
        InlineMessageId = inlineMessageId;
        Media = media;
    }

    /// <summary>
    /// Initializes a new request with inlineMessageId and new media
    /// </summary>
    public EditInlineMessageMediaRequest()
        : base("editMessageMedia")
    { }
}
