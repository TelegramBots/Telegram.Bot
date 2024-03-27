using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the title of a chat. Titles can't be changed for private chats.
/// The bot must be an administrator in the chat for this to work and must have the appropriate
/// admin rights. Returns <see langword="true"/> on success.
/// </summary>
public class SetChatTitleRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// New chat title, 1-255 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and title
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="title">New chat title, 1-255 characters</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetChatTitleRequest(ChatId chatId, string title)
        : this()
    {
        ChatId = chatId;
        Title = title;
    }

    /// <summary>
    /// Initializes a new request with chatId and title
    /// </summary>
    public SetChatTitleRequest()
        : base("setChatTitle")
    { }
}
