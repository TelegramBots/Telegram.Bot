namespace Telegram.Bot.Requests;

/// <summary>Use this method to send phone contacts.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendContactRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Contact's phone number</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PhoneNumber { get; set; }

    /// <summary>Contact's first name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FirstName { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Contact's last name</summary>
    public string? LastName { get; set; }

    /// <summary>Additional data about the contact in the form of a <a href="https://en.wikipedia.org/wiki/VCard">vCard</a>, 0-2048 bytes</summary>
    public string? Vcard { get; set; }

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

    /// <summary>Initializes an instance of <see cref="SendContactRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendContactRequest(ChatId chatId, string phoneNumber, string firstName) : this()
    {
        ChatId = chatId;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
    }

    /// <summary>Instantiates a new <see cref="SendContactRequest"/></summary>
    public SendContactRequest() : base("sendContact") { }
}
