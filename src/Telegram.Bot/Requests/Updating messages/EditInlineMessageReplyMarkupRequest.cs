using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit only the reply markup of messages. On success <see langword="true"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class EditInlineMessageReplyMarkupRequest : RequestBase<bool>
{
    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonProperty(Required = Required.Always)]
    public string InlineMessageId { get; }

    /// <inheritdoc cref="Documentation.InlineReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with inlineMessageId and new inline keyboard
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    public EditInlineMessageReplyMarkupRequest(string inlineMessageId)
        : base("editMessageReplyMarkup")
    {
        InlineMessageId = inlineMessageId;
    }
}
