using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator
/// in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
/// rights, unless it is the creator of the topic. Returns <c>true</c> on success.
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
    /// Topic name, 1-128 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <summary>
    /// Unique identifier of the custom emoji shown as the topic icon.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string IconCustomEmojiId { get; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="name">Topic name</param>
    /// <param name="iconCustomEmojiId">Unique identifier of the custom emoji shown as the topic icon</param>
    public EditForumTopicRequest(ChatId chatId, int messageThreadId, string name, string iconCustomEmojiId)
        : base("editForumTopic")
    {
        ChatId = chatId;
        MessageThreadId = messageThreadId;
        Name = name;
        IconCustomEmojiId = iconCustomEmojiId;
    }
}
