// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to send a native poll.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendPollRequest() : RequestBase<Message>("sendPoll"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>). Polls can't be sent to channel direct messages chats.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Poll question, 1-300 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Question { get; set; }

    /// <summary>A list of 2-12 answer options</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InputPollOption> Options { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of a forum; for forum supergroups and private chats of bots with forum topic mode enabled only</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Mode for parsing entities in the question. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Currently, only custom emoji entities are allowed</summary>
    [JsonPropertyName("question_parse_mode")]
    public ParseMode QuestionParseMode { get; set; }

    /// <summary>A list of special entities that appear in the poll question. It can be specified instead of <see cref="QuestionParseMode">QuestionParseMode</see></summary>
    [JsonPropertyName("question_entities")]
    public IEnumerable<MessageEntity>? QuestionEntities { get; set; }

    /// <summary><see langword="true"/>, if the poll needs to be anonymous, defaults to <see langword="true"/></summary>
    [JsonPropertyName("is_anonymous")]
    public bool? IsAnonymous { get; set; }

    /// <summary>Poll type, <see cref="PollType.Quiz">Quiz</see> or <see cref="PollType.Regular">Regular</see>, defaults to <see cref="PollType.Regular">Regular</see></summary>
    public PollType? Type { get; set; }

    /// <summary><see langword="true"/>, if the poll allows multiple answers, ignored for polls in quiz mode, defaults to <see langword="false"/></summary>
    [JsonPropertyName("allows_multiple_answers")]
    public bool AllowsMultipleAnswers { get; set; }

    /// <summary>0-based identifier of the correct answer option, required for polls in quiz mode</summary>
    [JsonPropertyName("correct_option_id")]
    public int? CorrectOptionId { get; set; }

    /// <summary>Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a quiz-style poll, 0-200 characters with at most 2 line feeds after entities parsing</summary>
    public string? Explanation { get; set; }

    /// <summary>Mode for parsing entities in the explanation. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("explanation_parse_mode")]
    public ParseMode ExplanationParseMode { get; set; }

    /// <summary>A list of special entities that appear in the poll explanation. It can be specified instead of <see cref="ExplanationParseMode">ExplanationParseMode</see></summary>
    [JsonPropertyName("explanation_entities")]
    public IEnumerable<MessageEntity>? ExplanationEntities { get; set; }

    /// <summary>Amount of time in seconds the poll will be active after creation, 5-600. Can't be used together with <see cref="CloseDate">CloseDate</see>.</summary>
    [JsonPropertyName("open_period")]
    public int? OpenPeriod { get; set; }

    /// <summary>Point in time when the poll will be automatically closed. Must be at least 5 and no more than 600 seconds in the future. Can't be used together with <see cref="OpenPeriod">OpenPeriod</see>.</summary>
    [JsonPropertyName("close_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? CloseDate { get; set; }

    /// <summary>Pass <see langword="true"/> if the poll needs to be immediately closed. This can be useful for poll preview.</summary>
    [JsonPropertyName("is_closed")]
    public bool IsClosed { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    [JsonPropertyName("disable_notification")]
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    [JsonPropertyName("protect_content")]
    public bool ProtectContent { get; set; }

    /// <summary>Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</summary>
    [JsonPropertyName("allow_paid_broadcast")]
    public bool AllowPaidBroadcast { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    [JsonPropertyName("message_effect_id")]
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    [JsonPropertyName("reply_parameters")]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</summary>
    [JsonPropertyName("reply_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }
}
