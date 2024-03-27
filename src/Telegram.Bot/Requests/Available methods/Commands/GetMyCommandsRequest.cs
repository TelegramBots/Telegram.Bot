// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the current list of the bot’s commands for the given <see cref="Scope">scope</see>
/// and <see cref="LanguageCode">user language</see>. Returns Array of <see cref="BotCommand"/> on success.
/// If commands aren't set, an empty list is returned.
/// </summary>
public class GetMyCommandsRequest : RequestBase<BotCommand[]>
{
    /// <summary>
    /// An object, describing scope of users. Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BotCommandScope? Scope { get; set; }

    /// <summary>
    /// A two-letter ISO 639-1 language code or an empty string
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetMyCommandsRequest()
        : base("getMyCommands")
    { }
}
