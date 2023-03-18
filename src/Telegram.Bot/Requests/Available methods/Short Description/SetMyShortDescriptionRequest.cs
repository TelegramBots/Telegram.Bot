namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the bot's short description,which is shown on
/// the bot's profile page and is sent together with the link when users share the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetMyShortDescriptionRequest : RequestBase<bool>
{
    /// <summary>
    /// New short description for the bot; 0-120 characters.
    /// Pass an empty string to remove the dedicated short description for the given language.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ShortDescription { get; set; }

    /// <summary>
    /// A two-letter ISO 639-1 language code. If empty, the short description will be
    /// applied to all users for whose language there is no dedicated short description.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetMyShortDescriptionRequest()
        : base("setMyShortDescription")
    { }
}
