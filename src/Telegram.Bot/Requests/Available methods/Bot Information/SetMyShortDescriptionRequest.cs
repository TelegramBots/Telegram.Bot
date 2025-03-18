// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the bot's short description, which is shown on the bot's profile page and is sent together with the link when users share the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMyShortDescriptionRequest() : RequestBase<bool>("setMyShortDescription")
{
    /// <summary>New short description for the bot; 0-120 characters. Pass an empty string to remove the dedicated short description for the given language.</summary>
    [JsonPropertyName("short_description")]
    public string? ShortDescription { get; set; }

    /// <summary>A two-letter ISO 639-1 language code. If empty, the short description will be applied to all users for whose language there is no dedicated short description.</summary>
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }
}
