using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to reopen a closed 'General' topic in a forum supergroup chat. The bot must be an administrator
/// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
/// rights. The topic will be automatically unhidden if it was hidden. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ReopenGeneralForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    public ReopenGeneralForumTopicRequest(ChatId chatId)
        : base("reopenGeneralForumTopic") => ChatId = chatId;
}
