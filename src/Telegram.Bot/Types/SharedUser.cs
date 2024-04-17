namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about a user that was shared with the bot using
/// a <see cref="ReplyMarkups.KeyboardButtonRequestUser"/> button.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SharedUser
{
    /// <summary>
    /// Identifier of the shared user. The bot may not have access to the user and could be unable to use this
    /// identifier, unless the user is already known to the bot by some other means.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; set; }

    /// <summary>
    /// Optional. First name of the user, if the name was requested by the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Optional. Last name of the user, if the name was requested by the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. Username of the user, if the username was requested by the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Username { get; set; }

    /// <summary>
    ///
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize[]? Photo { get; set; }
}
