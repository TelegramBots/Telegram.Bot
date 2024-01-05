namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object defines the criteria used to request a suitable user. The identifier of the selected user will be
/// shared with the bot when the corresponding button is pressed.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class KeyboardButtonRequestUsers
{
    /// <summary>
    /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
    /// Must be unique within the message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int RequestId { get; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request bots, pass <see langword="false" /> to request regular users.
    /// If not specified, no additional restrictions are applied.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? UserIsBot { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request premium users, pass <see langword="false" /> to request non-premium users.
    /// If not specified, no additional restrictions are applied.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? UserIsPremium { get; set; }

    /// <summary>
    /// Optional. The maximum number of users to be selected; 1-10. Defaults to 1.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MaxQuantity { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButtonRequestUsers"/> class with requestId
    /// </summary>
    /// <param name="requestId">
    /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
    /// Must be unique within the message
    /// </param>
    public KeyboardButtonRequestUsers(int requestId)
    {
        RequestId = requestId;
    }
}
