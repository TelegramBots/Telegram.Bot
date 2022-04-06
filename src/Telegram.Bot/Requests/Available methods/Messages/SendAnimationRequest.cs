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
/// Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). On success,
/// the sent <see cref="Message"/> is returned. Bots can currently send animation files of up to
/// 50 MB in size, this limit may be changed in the future.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendAnimationRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Animation to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send an animation
    /// that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram
    /// to get an animation from the Internet, or upload a new animation using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputOnlineFile Animation { get; }

    /// <summary>
    /// Duration of sent animation in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Duration { get; set; }

    /// <summary>
    /// Animation width
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Width { get; set; }

    /// <summary>
    /// Animation height
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Height { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.Thumb"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMedia? Thumb { get; set; }

    /// <summary>
    /// Animation caption (may also be used when resending animation by
    /// <see cref="InputTelegramFile.FileId"/>), 0-1024 characters after entities parsing
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
    /// Initializes a new request with chatId and animation
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="animation">
    /// Animation to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send an animation
    /// that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to
    /// get an animation from the Internet, or upload a new animation using multipart/form-data
    /// </param>
    public SendAnimationRequest(ChatId chatId, InputOnlineFile animation)
        : base("sendAnimation")
    {
        ChatId = chatId;
        Animation = animation;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
    {
        HttpContent? httpContent;
        if (Animation.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
        {
            var multipartContent = GenerateMultipartFormDataContent("animation", "thumb");
            if (Animation.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: Animation.Content!,
                    name: "animation",
                    fileName: Animation.FileName
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