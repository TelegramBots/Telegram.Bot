namespace Telegram.Bot.Requests;

/// <summary>Use this method to set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class SetChatPhotoRequest : FileRequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>New chat photo, uploaded using <see cref="InputFileStream"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileStream Photo { get; set; }

    /// <summary>Initializes an instance of <see cref="SetChatPhotoRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="photo">New chat photo, uploaded using <see cref="InputFileStream"/></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetChatPhotoRequest(ChatId chatId, InputFileStream photo) : this()
    {
        ChatId = chatId;
        Photo = photo;
    }

    /// <summary>Instantiates a new <see cref="SetChatPhotoRequest"/></summary>
    public SetChatPhotoRequest() : base("setChatPhoto") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent("photo", Photo);
}
