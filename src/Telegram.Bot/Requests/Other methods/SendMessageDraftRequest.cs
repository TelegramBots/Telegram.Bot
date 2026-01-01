// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to stream a partial message to a user while the message is being generated; supported only for bots with forum topic mode enabled.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendMessageDraftRequest() : RequestBase<bool>("sendMessageDraft"), IChatTargetable
{
    /// <summary>Unique identifier for the target private chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier of the message draft; must be non-zero. Changes of drafts with the same identifier are animated</summary>
    [JsonPropertyName("draft_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DraftId { get; set; }

    /// <summary>Text of the message to be sent, 1-4096 characters after entities parsing</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary>Unique identifier for the target message thread</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
