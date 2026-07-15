// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit an ephemeral text message. Note that it is not guaranteed that the user will receive the message edit event, especially if they are offline</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditEphemeralMessageTextRequest() : RequestBase<bool>("editEphemeralMessageText"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup in the format <c>@username</c></summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the user who received the message</summary>
    [JsonPropertyName("receiver_user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ReceiverUserId { get; set; }

    /// <summary>Identifier of the ephemeral message to edit</summary>
    [JsonPropertyName("ephemeral_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int EphemeralMessageId { get; set; }

    /// <summary>New text of the message, 1-4096 characters after entity parsing</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary>Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>Link preview generation options for the message</summary>
    [JsonPropertyName("link_preview_options")]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a></summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }
}
