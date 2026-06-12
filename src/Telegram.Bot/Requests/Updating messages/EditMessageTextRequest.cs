// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit text, rich and <a href="https://core.telegram.org/bots/api#games">game</a> messages.<para>Returns: The edited <see cref="Message"/> is returned</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditMessageTextRequest() : RequestBase<Message>("editMessageText"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target bot, supergroup or channel in the format <c>@username</c>.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to edit.</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>New text of the message, 1-4096 characters after entity parsing; required if <see cref="RichMessage">RichMessage</see> isn't specified</summary>
    public string? Text { get; set; }

    /// <summary>Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>Link preview generation options for the message</summary>
    [JsonPropertyName("link_preview_options")]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>New rich content of the message; required if <see cref="Text">Text</see> isn't specified</summary>
    [JsonPropertyName("rich_message")]
    public InputRichMessage? RichMessage { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a></summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }
}
