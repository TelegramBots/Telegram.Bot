using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to copy messages of any kind. Service messages and invoice messages can't be copied.
/// The method is analogous to the method <see cref="ForwardMessageRequest"/>, but the copied message
/// doesn't have a link to the original message. Returns the <see cref="Types.MessageId"/> of the
/// sent <see cref="Message"/> on success.
/// </summary>
public class CopyMessageRequest : RequestBase<MessageId>, IChatTargetable

{
    /// <summary>
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Unique identifier for the chat where the original message was sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId FromChatId { get; init; }

    /// <summary>
    /// Message identifier in the chat specified in <see cref="FromChatId"/>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <summary>
    /// New caption for media, 0-1024 characters after entities parsing.
    /// If not specified, the original caption is kept
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ParseMode"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.CaptionEntities"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyParameters"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [Obsolete($"This property is deprecated, use {nameof(ReplyParameters)} instead")]
    [JsonIgnore]
    public int? ReplyToMessageId
    {
        get => ReplyParameters?.MessageId;
        set
        {
            if (value is null)
            {
                ReplyParameters = null;
            }
            else
            {
                ReplyParameters ??= new();
                ReplyParameters.MessageId = value.Value;
            }
        }
    }

    /// <summary>
    /// Initializes a new request with chatId, fromChatId and messageId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original message was sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">
    /// Message identifier in the chat specified in <see cref="FromChatId"/>
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public CopyMessageRequest(ChatId chatId, ChatId fromChatId, int messageId)
        : this()
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public CopyMessageRequest()
        : base("copyMessage")
    { }
}
