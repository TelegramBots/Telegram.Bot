namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have <em>CanManageTopics</em> administrator rights, unless it is the creator of the topic.<para>Returns: </para></summary>
public partial class EditForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the target message thread of the forum topic</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageThreadId { get; set; }

    /// <summary>New topic name, 0-128 characters. If not specified or empty, the current name of the topic will be kept</summary>
    public string? Name { get; set; }

    /// <summary>New unique identifier of the custom emoji shown as the topic icon. Use <see cref="TelegramBotClientExtensions.GetForumTopicIconStickersAsync">GetForumTopicIconStickers</see> to get all allowed custom emoji identifiers. Pass an empty string to remove the icon. If not specified, the current icon will be kept</summary>
    public string? IconCustomEmojiId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditForumTopicRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditForumTopicRequest(ChatId chatId, int messageThreadId) : this()
    {
        ChatId = chatId;
        MessageThreadId = messageThreadId;
    }

    /// <summary>Instantiates a new <see cref="EditForumTopicRequest"/></summary>
    public EditForumTopicRequest() : base("editForumTopic") { }
}
