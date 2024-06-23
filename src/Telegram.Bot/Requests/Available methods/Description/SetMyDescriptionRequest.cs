namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the bot's description, which is shown in the chat with the bot if the chat is empty.<para>Returns: </para></summary>
public partial class SetMyDescriptionRequest : RequestBase<bool>
{
    /// <summary>New bot description; 0-512 characters. Pass an empty string to remove the dedicated description for the given language.</summary>
    public string? Description { get; set; }

    /// <summary>A two-letter ISO 639-1 language code. If empty, the description will be applied to all users for whose language there is no dedicated description.</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Instantiates a new <see cref="SetMyDescriptionRequest"/></summary>
    public SetMyDescriptionRequest() : base("setMyDescription") { }
}
