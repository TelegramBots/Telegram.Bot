// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to stream a partial rich message to a user while the message is being generated. Note that the streamed draft is ephemeral and acts as a temporary 30-second preview - once the output is finalized, you <b>must</b> call <see cref="TelegramBotClientExtensions.SendRichMessage">SendRichMessage</see> with the complete message to persist it in the user's chat.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendRichMessageDraftRequest() : RequestBase<bool>("sendRichMessageDraft"), IChatTargetable
{
    /// <summary>Unique identifier for the target private chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Unique identifier of the message draft; must be non-zero. Changes to drafts with the same identifier are animated.</summary>
    [JsonPropertyName("draft_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DraftId { get; set; }

    /// <summary>The partial message to be streamed. Direct upload of new files isn't supported.</summary>
    [JsonPropertyName("rich_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputRichMessage RichMessage { get; set; }

    /// <summary>Unique identifier for the target message thread</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
