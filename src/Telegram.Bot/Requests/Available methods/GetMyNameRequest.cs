namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current bot name for the given user language.<para>Returns: <see cref="BotName"/> on success.</para></summary>
public partial class GetMyNameRequest : RequestBase<BotName>
{
    /// <summary>A two-letter ISO 639-1 language code or an empty string</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Instantiates a new <see cref="GetMyNameRequest"/></summary>
    public GetMyNameRequest() : base("getMyName") { }
}
