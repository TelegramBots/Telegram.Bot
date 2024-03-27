using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to uhhide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the
/// chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights.
/// Returns <see langword="true"/> on success.
/// </summary>
public class UnhideGeneralForumTopicRequest : RequestBase<bool>, IChatTargetable
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
    public UnhideGeneralForumTopicRequest(ChatId chatId)
        : this()
        => ChatId = chatId;

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public UnhideGeneralForumTopicRequest()
        : base("unhideGeneralForumTopic")
    { }
}
