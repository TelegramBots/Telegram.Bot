// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit the caption of an ephemeral message. Note that it is not guaranteed that the user will receive the message edit event, especially if they are offline</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditEphemeralMessageCaptionRequest() : RequestBase<bool>("editEphemeralMessageCaption"), IChatTargetable
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

    /// <summary>New caption of the message, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a></summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }
}
