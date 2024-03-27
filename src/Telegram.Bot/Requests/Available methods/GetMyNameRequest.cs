// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the current bot name for the given user language.
/// Returns <see cref="BotName"/> on success.
/// </summary>
public class GetMyNameRequest : RequestBase<BotName>
{
    /// <summary>
    /// A two-letter ISO 639-1 language code or an empty string
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetMyNameRequest()
        : base("getMyName")
    { }
}
