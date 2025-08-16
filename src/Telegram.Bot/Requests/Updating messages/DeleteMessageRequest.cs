// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a message, including service messages, with the following limitations:<br/>- A message can only be deleted if it was sent less than 48 hours ago.<br/>- Service messages about a supergroup, channel, or forum topic creation can't be deleted.<br/>- A dice message in a private chat can only be deleted if it was sent more than 24 hours ago.<br/>- Bots can delete outgoing messages in private chats, groups, and supergroups.<br/>- Bots can delete incoming messages in private chats.<br/>- Bots granted <em>CanPostMessages</em> permissions can delete outgoing messages in channels.<br/>- If the bot is an administrator of a group, it can delete any message there.<br/>- If the bot has <em>CanDeleteMessages</em> administrator right in a supergroup or a channel, it can delete any message there.<br/>- If the bot has <em>CanManageDirectMessages</em> administrator right in a channel, it can delete any message in the corresponding direct messages chat.<br/>Returns <em>True</em> on success.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteMessageRequest() : RequestBase<bool>("deleteMessage"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to delete</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }
}
