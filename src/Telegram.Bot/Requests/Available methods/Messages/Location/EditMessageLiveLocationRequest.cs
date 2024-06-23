namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit live location messages. A location can be edited until its <see cref="LivePeriod">LivePeriod</see> expires or editing is explicitly disabled by a call to <see cref="TelegramBotClientExtensions.StopMessageLiveLocationAsync">StopMessageLiveLocation</see>.<para>Returns: The edited <see cref="Message"/> is returned</para></summary>
public partial class EditMessageLiveLocationRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to edit</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Latitude of new location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; set; }

    /// <summary>Longitude of new location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; set; }

    /// <summary>New period in seconds during which the location can be updated, starting from the message send date. If 0x7FFFFFFF is specified, then the location can be updated forever. Otherwise, the new value must not exceed the current <see cref="LivePeriod">LivePeriod</see> by more than a day, and the live location expiration date must remain within the next 90 days. If not specified, then <see cref="LivePeriod">LivePeriod</see> remains unchanged</summary>
    public int? LivePeriod { get; set; }

    /// <summary>The radius of uncertainty for the location, measured in meters; 0-1500</summary>
    public double? HorizontalAccuracy { get; set; }

    /// <summary>Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</summary>
    public int? Heading { get; set; }

    /// <summary>The maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</summary>
    public int? ProximityAlertRadius { get; set; }

    /// <summary>An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditMessageLiveLocationRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditMessageLiveLocationRequest(ChatId chatId, int messageId, double latitude, double longitude) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>Instantiates a new <see cref="EditMessageLiveLocationRequest"/></summary>
    public EditMessageLiveLocationRequest() : base("editMessageLiveLocation") { }
}
