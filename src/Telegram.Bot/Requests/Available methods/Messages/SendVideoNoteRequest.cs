using System.Net.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// As of <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>,
/// Telegram clients support rounded square mp4 videos of up to 1 minute long. Use this method
/// to send video messages. On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendVideoNoteRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Video note to send. Pass a <see cref="InputFileId"/> as String to send a video
    /// note that exists on the Telegram servers (recommended) or upload a new video using
    /// multipart/form-data. Sending video notes by a URL is currently unsupported
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IInputFile VideoNote { get; }

    /// <summary>
    /// Duration of sent video in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Duration { get; set; }

    /// <summary>
    /// Video width and height, i.e. diameter of the video message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Length { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.Thumb"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IInputFile? Thumb { get; set; }

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
    /// Initializes a new request with chatId and videoNote
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="videoNote">
    /// Video note to send. Pass a <see cref="InputFileId"/> as String to send a video
    /// note that exists on the Telegram servers (recommended) or upload a new video using
    /// multipart/form-data. Sending video notes by a URL is currently unsupported
    /// </param>
    public SendVideoNoteRequest(ChatId chatId, IInputFile videoNote)
        : base("sendVideoNote")
    {
        ChatId = chatId;
        VideoNote = videoNote;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
    {
        HttpContent? httpContent;

        if (VideoNote is InputFile || Thumb is InputFile)
        {
            httpContent = GenerateMultipartFormDataContent("video_note", "thumb")
                .AddContentIfInputFile(media: VideoNote, name: "video_note")
                .AddContentIfInputFile(media: Thumb, name: "thumb");
        }
        else
        {
            httpContent = base.ToHttpContent();
        }

        return httpContent;
    }
}
