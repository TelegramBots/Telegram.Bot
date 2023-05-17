namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the current <see cref="BotDescription">bot description</see>
/// for the given <see cref="LanguageCode">user language</see>.
/// Returns <see cref="BotDescription"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetMyDescriptionRequest : RequestBase<BotDescription>
{
    /// <summary>
    /// A two-letter ISO 639-1 language code or an empty string
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetMyDescriptionRequest()
        : base("getMyDescription")
    { }
}
