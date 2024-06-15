using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to create a topic in a forum supergroup chat. The bot must be an administrator in the chat for
/// this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights.
/// Returns information about the created topic as a <see cref="ForumTopic"/> object.
/// </summary>
public class CreateForumTopicRequest : RequestBase<ForumTopic>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Topic name, 1-128 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; init; }

    /// <summary>
    /// Optional. Color of the topic icon in RGB format. Currently, must be one of 0x6FB9F0, 0xFFD67E, 0xCB86DB,
    /// 0x8EEE98, 0xFF93B2, or 0xFB6F5F
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Color? IconColor { get; set; }

    /// <summary>
    /// Optional. Unique identifier of the custom emoji shown as the topic icon.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IconCustomEmojiId { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
    /// <param name="name">Topic name</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public CreateForumTopicRequest(ChatId chatId, string name)
        : this()
    {
        ChatId = chatId;
        Name = name;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public CreateForumTopicRequest()
        : base("createForumTopic")
    { }
}
