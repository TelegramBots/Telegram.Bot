using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send general files. On success, the sent <see cref="Message"/>
/// is returned. Bots can currently send files of any type of up to 50 MB in size,
/// this limit may be changed in the future.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendDocumentRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// File to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram
    /// to get a file from the Internet, or upload a new one using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputOnlineFile Document { get; }

    /// <inheritdoc cref="Abstractions.Documentation.Thumb"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMedia? Thumb { get; set; }

    /// <summary>
    /// Document caption (may also be used when resending documents by file_id), 0-1024 characters
    /// after entities parsing
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ParseMode"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.CaptionEntities"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>
    /// Disables automatic server-side content type detection for files uploaded using multipart/form-data
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableContentTypeDetection { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ReplyToMessageId { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.AllowSendingWithoutReply"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? AllowSendingWithoutReply { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and document
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="document">
    /// File to send. Pass a <see cref="InputTelegramFile.FileId"/> as string to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram
    /// to get a file from the Internet, or upload a new one using multipart/form-data
    /// </param>
    public SendDocumentRequest(ChatId chatId, InputOnlineFile document)
        : base("sendDocument")
    {
        ChatId = chatId;
        Document = document;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
    {
        HttpContent? httpContent;
        if (Document.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
        {
            var multipartContent = GenerateMultipartFormDataContent("document", "thumb");
            if (Document.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: Document.Content!,
                    name: "document",
                    fileName: Document.FileName
                );
            }

            if (Thumb?.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: Thumb.Content!,
                    name: "thumb",
                    fileName: Thumb.FileName
                );
            }

            httpContent = multipartContent;
        }
        else
        {
            httpContent = base.ToHttpContent();
        }

        return httpContent;
    }
}