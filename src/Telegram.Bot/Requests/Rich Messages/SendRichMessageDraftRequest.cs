// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to stream a partial rich message to a user while the message is being generated. Note that the streamed draft is ephemeral and acts as a temporary 30-second preview - once the output is finalized, you <b>must</b> call <see cref="TelegramBotClientExtensions.SendRichMessage">SendRichMessage</see> with the complete message to persist it in the user's chat.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendRichMessageDraftRequest() : FileRequestBase<bool>("sendRichMessageDraft"), IChatTargetable
{
    /// <summary>Unique identifier for the target private chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier of the message draft; must be non-zero. Changes to drafts with the same identifier are animated. Otherwise, the draft is replaced without animation.</summary>
    [JsonPropertyName("draft_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DraftId { get; set; }

    /// <summary>The partial message to be streamed. Direct upload of new files and explicit upload of files by a URL isn't supported.</summary>
    [JsonPropertyName("rich_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputRichMessage RichMessage { get; set; }

    /// <summary>Unique identifier for the target message thread</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Pass <see langword="true"/> to show the user a button to stop further drafts. The bot will receive an <see cref="Update"/> “StoppedMessageGeneration” if the user presses the button.</summary>
    [JsonPropertyName("can_stop")]
    public bool CanStop { get; set; }

    /// <summary>Pass <see langword="true"/> to keep the draft in the chat when the button is pressed. The draft will still disappear after a short time or if the bot sends a message. To fully preserve the partial draft, the bot should send it as a new message.</summary>
    [JsonPropertyName("keep_on_stop")]
    public bool KeepOnStop { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
