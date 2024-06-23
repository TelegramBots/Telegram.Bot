namespace Telegram.Bot.Requests;

/// <summary>Use this method to send a native poll.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendPollRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Poll question, 1-300 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Question { get; set; }

    /// <summary>A list of 2-10 answer options</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InputPollOption> Options { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Mode for parsing entities in the question. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Currently, only custom emoji entities are allowed</summary>
    public ParseMode QuestionParseMode { get; set; }

    /// <summary>A list of special entities that appear in the poll question. It can be specified instead of <see cref="QuestionParseMode">QuestionParseMode</see></summary>
    public IEnumerable<MessageEntity>? QuestionEntities { get; set; }

    /// <summary><see langword="true"/>, if the poll needs to be anonymous, defaults to <see langword="true"/></summary>
    public bool? IsAnonymous { get; set; }

    /// <summary>Poll type, <see cref="PollType.Quiz">Quiz</see> or <see cref="PollType.Regular">Regular</see>, defaults to <see cref="PollType.Regular">Regular</see></summary>
    public PollType? Type { get; set; }

    /// <summary><see langword="true"/>, if the poll allows multiple answers, ignored for polls in quiz mode, defaults to <see langword="false"/></summary>
    public bool AllowsMultipleAnswers { get; set; }

    /// <summary>0-based identifier of the correct answer option, required for polls in quiz mode</summary>
    public int? CorrectOptionId { get; set; }

    /// <summary>Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a quiz-style poll, 0-200 characters with at most 2 line feeds after entities parsing</summary>
    public string? Explanation { get; set; }

    /// <summary>Mode for parsing entities in the explanation. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ExplanationParseMode { get; set; }

    /// <summary>A list of special entities that appear in the poll explanation. It can be specified instead of <see cref="ExplanationParseMode">ExplanationParseMode</see></summary>
    public IEnumerable<MessageEntity>? ExplanationEntities { get; set; }

    /// <summary>Amount of time in seconds the poll will be active after creation, 5-600. Can't be used together with <see cref="CloseDate">CloseDate</see>.</summary>
    public int? OpenPeriod { get; set; }

    /// <summary>Point in time when the poll will be automatically closed. Must be at least 5 and no more than 600 seconds in the future. Can't be used together with <see cref="OpenPeriod">OpenPeriod</see>.</summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? CloseDate { get; set; }

    /// <summary>Pass <see langword="true"/> if the poll needs to be immediately closed. This can be useful for poll preview.</summary>
    public bool IsClosed { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</summary>
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="SendPollRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="question">Poll question, 1-300 characters</param>
    /// <param name="options">A list of 2-10 answer options</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendPollRequest(ChatId chatId, string question, IEnumerable<InputPollOption> options) : this()
    {
        ChatId = chatId;
        Question = question;
        Options = options;
    }

    /// <summary>Instantiates a new <see cref="SendPollRequest"/></summary>
    public SendPollRequest() : base("sendPoll") { }
}
