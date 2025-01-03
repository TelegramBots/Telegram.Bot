namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the bot's name.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMyNameRequest() : RequestBase<bool>("setMyName")
{
    /// <summary>New bot name; 0-64 characters. Pass an empty string to remove the dedicated name for the given language.</summary>
    public string? Name { get; set; }

    /// <summary>A two-letter ISO 639-1 language code. If empty, the name will be shown to all users for whose language there is no dedicated name.</summary>
    public string? LanguageCode { get; set; }
}
