using System.Net.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit animation, audio, document, photo, or video messages. If a message is part
/// of a message album, then it can be edited only to an audio for audio albums, only to a
/// document for document albums and to a photo or a video otherwise. Use a previously uploaded
/// file via its <see cref="InputFileId"/> or specify a URL.
/// On success the edited <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class EditMessageMediaRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Identifier of the message to edit
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <summary>
    /// A new media content of the message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputMedia Media { get; }

    /// <inheritdoc cref="Documentation.InlineReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, messageId and new media
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="media">A new media content of the message</param>
    public EditMessageMediaRequest(ChatId chatId, int messageId, InputMedia media)
        : base("editMessageMedia")
    {
        ChatId = chatId;
        MessageId = messageId;
        Media = media;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
    {
        if (Media.Media.FileType is not FileType.Stream &&
            Media is not IInputMediaThumb { Thumb.FileType: FileType.Stream })
        {
            return base.ToHttpContent();
        }

        var multipartContent = GenerateMultipartFormDataContent();

        if (Media.Media is InputFile file)
        {
            multipartContent.AddContentIfInputFile(file, file.FileName!);
        }
        if (Media is IInputMediaThumb { Thumb: InputFile thumb })
        {
            multipartContent.AddContentIfInputFile(thumb, thumb.FileName!);
        }

        return multipartContent;
    }
}
