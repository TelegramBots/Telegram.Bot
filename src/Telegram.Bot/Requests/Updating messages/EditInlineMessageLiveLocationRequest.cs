// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit live location messages. A location can be edited until its <see cref="LivePeriod">LivePeriod</see> expires or editing is explicitly disabled by a call to <see cref="TelegramBotClientExtensions.StopMessageLiveLocation">StopMessageLiveLocation</see>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditInlineMessageLiveLocationRequest() : RequestBase<bool>("editMessageLiveLocation"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Identifier of the inline message</summary>
    [JsonPropertyName("inline_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>Latitude of new location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; set; }

    /// <summary>Longitude of new location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; set; }

    /// <summary>New period in seconds during which the location can be updated, starting from the message send date. If 0x7FFFFFFF is specified, then the location can be updated forever. Otherwise, the new value must not exceed the current <see cref="LivePeriod">LivePeriod</see> by more than a day, and the live location expiration date must remain within the next 90 days. If not specified, then <see cref="LivePeriod">LivePeriod</see> remains unchanged</summary>
    [JsonPropertyName("live_period")]
    public int? LivePeriod { get; set; }

    /// <summary>The radius of uncertainty for the location, measured in meters; 0-1500</summary>
    [JsonPropertyName("horizontal_accuracy")]
    public double? HorizontalAccuracy { get; set; }

    /// <summary>Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</summary>
    public int? Heading { get; set; }

    /// <summary>The maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</summary>
    [JsonPropertyName("proximity_alert_radius")]
    public int? ProximityAlertRadius { get; set; }

    /// <summary>An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }
}
