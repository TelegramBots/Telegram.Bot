using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send static .WEBP, animated .TGS, or video .WEBM stickers.
/// On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendStickerRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Optional. Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Sticker to send. Pass a <see cref="InputFileId"/> as String to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String
    /// for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP
    /// or .TGS sticker using multipart/form-data.
    /// Video stickers can only be sent by a <see cref="InputFileId"/>.
    /// Animated stickers can't be sent via an HTTP URL.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required InputFile Sticker { get; init; }

    /// <summary>
    /// Optional. Emoji associated with the sticker; only for just uploaded stickers
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Emoji { get; set; }

    /// <inheritdoc cref="Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Documentation.ReplyParameters"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <inheritdoc cref="Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request chatId and sticker
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="sticker">
    /// Sticker to send. Pass a <see cref="InputFileId"/> as String to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String
    /// for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP
    /// or .TGS sticker using multipart/form-data.
    /// Video stickers can only be sent by a <see cref="InputFileId"/>.
    /// Animated stickers can't be sent via an HTTP URL.
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required parameters")]
    public SendStickerRequest(ChatId chatId, InputFile sticker)
        : this()
    {
        ChatId = chatId;
        Sticker = sticker;
    }

    /// <summary>
    /// Initializes a new request chatId and sticker
    /// </summary>
    public SendStickerRequest()
        : base("sendSticker")
    { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Sticker switch
        {
            InputFileStream sticker => ToMultipartFormDataContent(fileParameterName: "sticker", inputFile: sticker),
            _                       => base.ToHttpContent()
        };
}
