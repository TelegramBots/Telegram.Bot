namespace Telegram.Bot.Requests;

/// <summary>Use this method to send paid media to channel chats.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendPaidMediaRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The number of Telegram Stars that must be paid to buy access to the media</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int StarCount { get; set; }

    /// <summary>A array describing the media to be sent; up to 10 items</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InputPaidMedia> Media { get; set; }

    /// <summary>Media caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the media caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</summary>
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>Initializes an instance of <see cref="SendPaidMediaRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="starCount">The number of Telegram Stars that must be paid to buy access to the media</param>
    /// <param name="media">A array describing the media to be sent; up to 10 items</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendPaidMediaRequest(ChatId chatId, int starCount, IEnumerable<InputPaidMedia> media) : this()
    {
        ChatId = chatId;
        StarCount = starCount;
        Media = media;
    }

    /// <summary>Instantiates a new <see cref="SendPaidMediaRequest"/></summary>
    public SendPaidMediaRequest() : base("sendPaidMedia") { }
}
