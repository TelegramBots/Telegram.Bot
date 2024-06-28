namespace Telegram.Bot.Requests;

/// <summary>Use this method to get a list of administrators in a chat, which aren't bots.<para>Returns: An Array of <see cref="ChatMember"/> objects.</para></summary>
public partial class GetChatAdministratorsRequest : RequestBase<ChatMember[]>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetChatAdministratorsRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetChatAdministratorsRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="GetChatAdministratorsRequest"/></summary>
    public GetChatAdministratorsRequest() : base("getChatAdministrators") { }
}
