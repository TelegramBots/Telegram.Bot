using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send photos. On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendPhotoRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Photo to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send a photo that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to
    /// get a photo from the Internet, or upload a new photo using multipart/form-data. The photo
    /// must be at most 10 MB in size. The photo's width and height must not exceed 10000 in total.
    /// Width and height ratio must be at most 20
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputOnlineFile Photo { get; }

    /// <summary>
    /// Photo caption (may also be used when resending photos by <see cref="InputTelegramFile.FileId"/>),
    /// 0-1024 characters after entities parsing
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ParseMode"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.CaptionEntities"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

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
    /// Initializes a new request with chatId and photo
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="photo">
    /// Photo to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send a photo that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to
    /// get a photo from the Internet, or upload a new photo using multipart/form-data. The photo
    /// must be at most 10 MB in size. The photo's width and height must not exceed 10000 in total.
    /// Width and height ratio must be at most 20</param>
    public SendPhotoRequest(ChatId chatId, InputOnlineFile photo)
        : base("sendPhoto")
    {
        ChatId = chatId;
        Photo = photo;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Photo.FileType switch
        {
            FileType.Stream => ToMultipartFormDataContent(fileParameterName: "photo", inputFile: Photo),
            _               => base.ToHttpContent()
        };
}