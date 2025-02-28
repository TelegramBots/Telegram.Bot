// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object defines the criteria used to request suitable users. Information about the selected users will be shared with the bot when the corresponding button is pressed. <a href="https://core.telegram.org/bots/features#chat-and-user-selection">More about requesting users »</a></summary>
public partial class KeyboardButtonRequestUsers
{
    /// <summary>Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object. Must be unique within the message</summary>
    [JsonPropertyName("request_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int RequestId { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request bots, pass <see langword="false"/> to request regular users. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("user_is_bot")]
    public bool? UserIsBot { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request premium users, pass <see langword="false"/> to request non-premium users. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("user_is_premium")]
    public bool? UserIsPremium { get; set; }

    /// <summary><em>Optional</em>. The maximum number of users to be selected; 1-10. Defaults to 1.</summary>
    [JsonPropertyName("max_quantity")]
    public int? MaxQuantity { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the users' first and last names</summary>
    [JsonPropertyName("request_name")]
    public bool RequestName { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the users' usernames</summary>
    [JsonPropertyName("request_username")]
    public bool RequestUsername { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the users' photos</summary>
    [JsonPropertyName("request_photo")]
    public bool RequestPhoto { get; set; }

    /// <summary>Initializes an instance of <see cref="KeyboardButtonRequestUsers"/></summary>
    /// <param name="requestId">Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object. Must be unique within the message</param>
    [SetsRequiredMembers]
    public KeyboardButtonRequestUsers(int requestId) => RequestId = requestId;

    /// <summary>Instantiates a new <see cref="KeyboardButtonRequestUsers"/></summary>
    public KeyboardButtonRequestUsers() { }
}
