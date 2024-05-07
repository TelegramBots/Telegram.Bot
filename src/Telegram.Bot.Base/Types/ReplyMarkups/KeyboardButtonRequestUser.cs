namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object defines the criteria used to request a suitable user. The identifier of the selected user will be
/// shared with the bot when the corresponding button is pressed.
/// </summary>
[Obsolete($"This type is deprecated, {nameof(KeyboardButtonRequestUsers)} is used instead")]
public class KeyboardButtonRequestUser
{
    /// <summary>
    /// Signed 32-bit identifier of the request
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request a bot, pass <see langword="false" /> to request a regular user. If not specified, no additional
    /// restrictions are applied.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UserIsBot { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request a premium user, pass <see langword="false" /> to request a non-premium user. If not specified,
    /// no additional restrictions are applied.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UserIsPremium { get; set; }
}
