using System.Diagnostics.CodeAnalysis;
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
public class DeleteMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifier of the message to delete
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to delete</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public DeleteMessageRequest(ChatId chatId, int messageId)
        : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public DeleteMessageRequest()
        : base("deleteMessage")
    { }
}
