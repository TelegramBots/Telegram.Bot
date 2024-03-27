using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set a new profile photo for the chat. Photos can't be changed for private
/// chats. The bot must be an administrator in the chat for this to work and must have the appropriate
/// admin rights. Returns <see langword="true"/> on success.
/// </summary>
public class SetChatPhotoRequest : FileRequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// New chat photo, uploaded using multipart/form-data
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileStream Photo { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and photo
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="photo">New chat photo, uploaded using multipart/form-data</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetChatPhotoRequest(ChatId chatId, InputFileStream photo)
        : this()
    {
        ChatId = chatId;
        Photo = photo;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetChatPhotoRequest()
        : base("setChatPhoto")
    { }

    /// <inheritdoc />
    public override HttpContent ToHttpContent()
        => ToMultipartFormDataContent("photo", Photo);
}
