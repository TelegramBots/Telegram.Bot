using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send audio files, if you want Telegram clients to display the file as a playable
/// voice message. For this to work, your audio must be in an .OGG file encoded with OPUS (other
/// formats may be sent as <see cref="Audio"/> or <see cref="Document"/>). On success, the sent
/// <see cref="Message"/> is returned. Bots can currently send voice messages of up to 50 MB in size,
/// this limit may be changed in the future.
/// </summary>
public class SendVoiceRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BusinessConnectionId { get; set; }

    /// <inheritdoc />
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
    /// Audio file to send. Pass a <see cref="InputFileId"/> as String to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get
    /// a file from the Internet, or upload a new one using multipart/form-data
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Voice { get; init; }

    /// <summary>
    /// Voice message caption, 0-1024 characters after entities parsing
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

    /// <summary>
    /// Duration of the voice message in seconds
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; set; }

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
    /// Initializes a new request with chatId and voice
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="voice">
    /// Audio file to send. Pass a <see cref="InputFileId"/> as String to send a file
    /// that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram
    /// to get a file from the Internet, or upload a new one using multipart/form-data
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SendVoiceRequest(ChatId chatId, InputFile voice)
        : this()
    {
        ChatId = chatId;
        Voice = voice;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SendVoiceRequest()
        : base("sendVoice", TelegramBotClientJsonSerializerContext.Instance.SendVoiceRequest)
    { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Voice switch
        {
            InputFileStream voice =>
                ToMultipartFormDataContent(TelegramBotClientJsonSerializerContext.Instance.SendVoiceRequest, fileParameterName: "voice", inputFile: voice),
            _               => base.ToHttpContent()
        };
}
