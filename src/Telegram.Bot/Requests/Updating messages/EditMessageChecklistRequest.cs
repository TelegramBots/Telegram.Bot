// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit a checklist on behalf of a connected business account.<para>Returns: The edited <see cref="Message"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditMessageChecklistRequest() : RequestBase<Message>("editMessageChecklist"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier for the target chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier for the target message</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>An object for the new checklist</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputChecklist Checklist { get; set; }

    /// <summary>An object for the new inline keyboard for the message</summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
