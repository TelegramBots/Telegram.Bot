namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current bot description for the given user language.<para>Returns: <see cref="BotDescription"/> on success.</para></summary>
public partial class GetMyDescriptionRequest : RequestBase<BotDescription>
{
    /// <summary>A two-letter ISO 639-1 language code or an empty string</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Instantiates a new <see cref="GetMyDescriptionRequest"/></summary>
    public GetMyDescriptionRequest() : base("getMyDescription") { }
}
