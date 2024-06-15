using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to clear the list of pinned messages in a General forum topic. The bot must be an administrator in
/// the chat for this to work and must have the <see cref="ChatAdministratorRights.CanPinMessages"/> administrator
/// right in the supergroup. Returns <see langword="true"/> on success.
/// </summary>
[Obsolete("Use class UnpinAllGeneralForumTopicMessagesRequest")]
public class UnpinAllGeneralForumTopicMessages : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public UnpinAllGeneralForumTopicMessages(ChatId chatId)
        : this()
    {
        ChatId = chatId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public UnpinAllGeneralForumTopicMessages()
        : base("unpinAllGeneralForumTopicMessages")
    { }
}
