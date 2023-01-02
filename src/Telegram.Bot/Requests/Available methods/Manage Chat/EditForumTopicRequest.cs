using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator
/// in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
/// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class EditForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Unique identifier for the target message thread of the forum topic
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageThreadId { get; }

    /// <summary>
    /// New topic name, 0-128 characters. If not specififed or empty, the current name of the topic will be kept
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// New unique identifier of the custom emoji shown as the topic icon. Use
    /// <see cref="GetForumTopicIconStickersRequest"/> to get all allowed custom emoji identifiers. Pass an empty string to remove the icon.
    /// If not specified, the current icon will be kept
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IconCustomEmojiId { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="name">Topic name</param>
    /// <param name="iconCustomEmojiId">Unique identifier of the custom emoji shown as the topic icon</param>
    public EditForumTopicRequest(ChatId chatId, int messageThreadId)
        : base("editForumTopic") =>
        (ChatId, MessageThreadId) = (chatId, messageThreadId);
}
