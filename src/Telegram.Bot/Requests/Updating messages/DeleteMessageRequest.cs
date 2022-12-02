using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a message, including service messages, with the following limitations:
/// <list type="bullet">
/// <item>A message can only be deleted if it was sent less than 48 hours ago</item>
/// <item>A dice message in a private chat can only be deleted if it was sent more than 24 hours ago</item>
/// <item>Bots can delete outgoing messages in private chats, groups, and supergroups</item>
/// <item>Bots can delete incoming messages in private chats</item>
/// <item>Bots granted can_post_messages permissions can delete outgoing messages in channels</item>
/// <item>If the bot is an administrator of a group, it can delete any message there</item>
/// <item>
/// If the bot has can_delete_messages permission in a supergroup or a channel,
/// it can delete any message there
/// </item>
/// </list>
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeleteMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Identifier of the message to delete
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to delete</param>
    public DeleteMessageRequest(ChatId chatId, int messageId)
        : base("deleteMessage")
    {
        ChatId = chatId;
        MessageId = messageId;
    }
}
