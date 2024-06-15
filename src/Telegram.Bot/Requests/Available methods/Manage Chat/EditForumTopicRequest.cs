using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator
/// in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
/// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
/// </summary>
public class EditForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread of the forum topic
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageThreadId { get; init; }

    /// <summary>
    /// New topic name, 0-128 characters. If not specififed or empty, the current name of the topic will be kept
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// New unique identifier of the custom emoji shown as the topic icon. Use
    /// <see cref="GetForumTopicIconStickersRequest"/> to get all allowed custom emoji identifiers. Pass an empty string to remove the icon.
    /// If not specified, the current icon will be kept
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IconCustomEmojiId { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public EditForumTopicRequest(ChatId chatId, int messageThreadId)
        : this()
        => (ChatId, MessageThreadId) = (chatId, messageThreadId);

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public EditForumTopicRequest()
        : base("editForumTopic")
    { }
}
