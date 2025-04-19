// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Delete messages on behalf of a business account. Requires the <em>CanDeleteSentMessages</em> business bot right to delete messages sent by the bot itself, or the <em>CanDeleteAllMessages</em> business bot right to delete any message.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteBusinessMessagesRequest() : RequestBase<bool>("deleteBusinessMessages"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection on behalf of which to delete the messages</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>A list of 1-100 identifiers of messages to delete. All messages must be from the same chat. See <see cref="TelegramBotClientExtensions.DeleteMessage">DeleteMessage</see> for limitations on which messages can be deleted</summary>
    [JsonPropertyName("message_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; set; }
}
