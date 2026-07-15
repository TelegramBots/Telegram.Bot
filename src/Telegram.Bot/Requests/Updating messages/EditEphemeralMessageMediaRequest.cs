// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit the media of an ephemeral message. Note that it is not guaranteed that the user will receive the message edit event, especially if they are offline</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditEphemeralMessageMediaRequest() : FileRequestBase<bool>("editEphemeralMessageMedia"), IChatTargetable
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

    /// <summary>An object for the new media content of the message. A new file can't be uploaded; use a previously uploaded file via its FileId or specify a URL.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMedia Media { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a></summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }
}
