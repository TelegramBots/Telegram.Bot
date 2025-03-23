// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current bot description for the given user language.<para>Returns: <see cref="BotDescription"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetMyDescriptionRequest() : RequestBase<BotDescription>("getMyDescription")
{
    /// <summary>A two-letter ISO 639-1 language code or an empty string</summary>
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }
}
