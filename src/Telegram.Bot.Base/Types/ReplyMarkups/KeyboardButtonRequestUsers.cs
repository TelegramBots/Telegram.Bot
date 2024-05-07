using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object defines the criteria used to request a suitable user. The identifier of the selected user will be
/// shared with the bot when the corresponding button is pressed.
/// </summary>
public class KeyboardButtonRequestUsers
{
    /// <summary>
    /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
    /// Must be unique within the message
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int RequestId { get; init; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request bots, pass <see langword="false" /> to request regular users.
    /// If not specified, no additional restrictions are applied.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UserIsBot { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request premium users, pass <see langword="false" /> to request non-premium users.
    /// If not specified, no additional restrictions are applied.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UserIsPremium { get; set; }

    /// <summary>
    /// Optional. The maximum number of users to be selected; 1-10. Defaults to 1.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxQuantity { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request the users' first and last name
    /// </summary>
    public bool? RequestName { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request the users' username
    /// </summary>
    public bool? RequestUsername { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true" /> to request the users' photo
    /// </summary>
    public bool? RequestPhoto { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButtonRequestUsers"/> class with requestId
    /// </summary>
    /// <param name="requestId">
    /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
    /// Must be unique within the message
    /// </param>
    [SetsRequiredMembers]
    public KeyboardButtonRequestUsers(int requestId)
        => RequestId = requestId;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButtonRequestUsers"/> class
    /// </summary>
    public KeyboardButtonRequestUsers() {}
}
