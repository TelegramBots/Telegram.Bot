using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit only the reply markup of messages. On success the edited
/// <see cref="Message"/> is returned.
/// </summary>
public class EditMessageReplyMarkupRequest : RequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifier of the message to edit
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <inheritdoc cref="Documentation.InlineReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public EditMessageReplyMarkupRequest(ChatId chatId, int messageId)
        : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    public EditMessageReplyMarkupRequest()
        : base("editMessageReplyMarkup")
    { }
}
