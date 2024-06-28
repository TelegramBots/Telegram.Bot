namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current bot short description for the given user language.<para>Returns: <see cref="BotShortDescription"/> on success.</para></summary>
public partial class GetMyShortDescriptionRequest : RequestBase<BotShortDescription>
{
    /// <summary>A two-letter ISO 639-1 language code or an empty string</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Instantiates a new <see cref="GetMyShortDescriptionRequest"/></summary>
    public GetMyShortDescriptionRequest() : base("getMyShortDescription") { }
}
