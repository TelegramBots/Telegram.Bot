// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes reply parameters for the message that is being sent.</summary>
public partial class ReplyParameters
{
    /// <summary>Identifier of the message that will be replied to in the current chat, or in the chat <see cref="ChatId">ChatId</see> if it is specified</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary><em>Optional</em>. If the message to be replied to is from a different chat, unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>). Not supported for messages sent on behalf of a business account and messages from channel direct messages chats.</summary>
    [JsonPropertyName("chat_id")]
    public ChatId? ChatId { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the message should be sent even if the specified message to be replied to is not found. Always <see langword="false"/> for replies in another chat or forum topic. Always <see langword="true"/> for messages sent on behalf of a business account.</summary>
    [JsonPropertyName("allow_sending_without_reply")]
    public bool AllowSendingWithoutReply { get; set; }

    /// <summary><em>Optional</em>. Quoted part of the message to be replied to; 0-1024 characters after entities parsing. The quote must be an exact substring of the message to be replied to, including <em>bold</em>, <em>italic</em>, <em>underline</em>, <em>strikethrough</em>, <em>spoiler</em>, and <em>CustomEmoji</em> entities. The message will fail to send if the quote isn't found in the original message.</summary>
    public string? Quote { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the quote. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("quote_parse_mode")]
    public ParseMode QuoteParseMode { get; set; }

    /// <summary><em>Optional</em>. A list of special entities that appear in the quote. It can be specified instead of <see cref="QuoteParseMode">QuoteParseMode</see>.</summary>
    [JsonPropertyName("quote_entities")]
    public MessageEntity[]? QuoteEntities { get; set; }

    /// <summary><em>Optional</em>. Position of the quote in the original message in UTF-16 code units</summary>
    [JsonPropertyName("quote_position")]
    public int? QuotePosition { get; set; }

    /// <summary><em>Optional</em>. Identifier of the specific checklist task to be replied to</summary>
    [JsonPropertyName("checklist_task_id")]
    public int? ChecklistTaskId { get; set; }
}
