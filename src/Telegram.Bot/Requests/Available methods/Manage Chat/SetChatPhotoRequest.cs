using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set a new profile photo for the chat. Photos can't be changed for private
/// chats. The bot must be an administrator in the chat for this to work and must have the appropriate
/// admin rights. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetChatPhotoRequest : FileRequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// New chat photo, uploaded using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFileStream Photo { get; }

    /// <summary>
    /// Initializes a new request with chatId and photo
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="photo">New chat photo, uploaded using multipart/form-data</param>
    public SetChatPhotoRequest(ChatId chatId, InputFileStream photo)
        : base("setChatPhoto")
    {
        ChatId = chatId;
        Photo = photo;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Photo.FileType switch
        {
            FileType.Stream => ToMultipartFormDataContent("photo", Photo),
            _               => base.ToHttpContent()
        };
}