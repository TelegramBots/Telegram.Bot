// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to stream a partial message to a user while the message is being generated. Note that the streamed draft is ephemeral and acts as a temporary 30-second preview - once the output is finalized, you <b>must</b> call <see cref="TelegramBotClientExtensions.SendMessage">SendMessage</see> with the complete message to persist it in the user's chat.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendMessageDraftRequest() : RequestBase<bool>("sendMessageDraft"), IChatTargetable
{
    /// <summary>Unique identifier for the target private chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier of the message draft; must be non-zero. Changes to drafts with the same identifier are animated. Otherwise, the draft is replaced without animation.</summary>
    [JsonPropertyName("draft_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DraftId { get; set; }

    /// <summary>Unique identifier for the target message thread</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Text of the message to be sent, 0-4096 characters after entities parsing. Pass an empty text to show a “Thinking…” placeholder.</summary>
    public string? Text { get; set; }

    /// <summary>Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>Pass <see langword="true"/> to show the user a button to stop further drafts. The bot will receive an <see cref="Update"/> “StoppedMessageGeneration” if the user presses the button.</summary>
    [JsonPropertyName("can_stop")]
    public bool CanStop { get; set; }

    /// <summary>Pass <see langword="true"/> to keep the draft in the chat when the button is pressed. The draft will still disappear after a short time or if the bot sends a message. To fully preserve the partial draft, the bot should send it as a new message.</summary>
    [JsonPropertyName("keep_on_stop")]
    public bool KeepOnStop { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
