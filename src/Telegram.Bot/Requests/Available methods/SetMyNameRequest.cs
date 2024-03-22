// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the bot's name. Returns <see langword="true"/> on success.
/// </summary>
public class SetMyNameRequest : RequestBase<bool>
{
    /// <summary>
    /// New bot name; 0-64 characters. Pass an empty string to remove the dedicated name for the given language.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// A two-letter ISO 639-1 language code. If empty, the name will be shown to all users for whose language
    /// there is no dedicated name.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetMyNameRequest()
        : base("setMyName")
    { }
}
