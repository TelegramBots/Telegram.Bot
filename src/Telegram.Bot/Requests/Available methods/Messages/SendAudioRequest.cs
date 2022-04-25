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
/// Use this method to send audio files, if you want Telegram clients to display them in the music
/// player. Your audio must be in the .MP3 or .M4A format. On success, the sent <see cref="Message"/>
/// is returned. Bots can currently send audio files of up to 50 MB in size, this limit may be
/// changed in the future.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendAudioRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Audio file to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send an audio
    /// file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for
    /// Telegram to get an audio file from the Internet, or upload a new one using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputOnlineFile Audio { get; }

    /// <summary>
    /// Audio caption, 0-1024 characters after entities parsing
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
    /// Duration of the audio in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Duration { get; set; }

    /// <summary>
    /// Performer
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Performer { get; set; }

    /// <summary>
    /// Track name
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Title { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.Thumb"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMedia? Thumb { get; set; }

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
    /// Initializes a new request with chatId and audio
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="audio">
    /// Audio file to send. Pass a <see cref="InputTelegramFile.FileId"/> as String to send an audio
    /// file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for
    /// Telegram to get an audio file from the Internet, or upload a new one using multipart/form-data
    /// </param>
    public SendAudioRequest(ChatId chatId, InputOnlineFile audio)
        : base("sendAudio")
    {
        ChatId = chatId;
        Audio = audio;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
    {
        HttpContent? httpContent;
        if (Audio.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
        {
            var multipartContent = GenerateMultipartFormDataContent("audio", "thumb");
            if (Audio.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: Audio.Content!,
                    name: "audio",
                    fileName: Audio.FileName
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