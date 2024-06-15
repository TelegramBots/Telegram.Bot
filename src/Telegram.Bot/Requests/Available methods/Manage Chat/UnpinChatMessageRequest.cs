using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to remove a message from the list of pinned messages in a chat. If the chat is not
/// a private chat, the bot must be an administrator in the chat for this to work and must have the
/// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
/// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel.
/// Returns <see langword="true"/> on success.
/// </summary>
public class UnpinChatMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifier of a message to unpin. If not specified, the most recent pinned message
    /// (by sending date) will be unpinned.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageId { get; set; }

    /// <summary>
    /// Initializes a new request with chatId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public UnpinChatMessageRequest(ChatId chatId)
        : this()
    {
        ChatId = chatId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public UnpinChatMessageRequest()
        : base("unpinChatMessage")
    { }
}
