using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to stop updating a live location message before <see cref="Types.Location.LivePeriod"/> expires.
/// On success <see langword="true"/> is returned.
/// </summary>
public class StopInlineMessageLiveLocationRequest : RequestBase<bool>
{
    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; init; }

    /// <inheritdoc cref="Abstractions.Documentation.InlineReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with inlineMessageId
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public StopInlineMessageLiveLocationRequest(string inlineMessageId)
        : this()
    {
        InlineMessageId = inlineMessageId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public StopInlineMessageLiveLocationRequest()
        : base("stopMessageLiveLocation")
    { }
}
