using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get a list of administrators in a chat. On success, returns an Array of
/// <see cref="ChatMember"/> objects that contains information about all chat administrators
/// except other bots. If the chat is a group or a supergroup and no administrators were appointed,
/// only the creator will be returned.
/// </summary>
public class GetChatAdministratorsRequest : RequestBase<ChatMember[]>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Initializes a new request with chatId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public GetChatAdministratorsRequest(ChatId chatId)
        : this()
    {
        ChatId = chatId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetChatAdministratorsRequest()
        : base("getChatAdministrators")
    { }
}
