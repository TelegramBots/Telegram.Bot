namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the list of the bot's commands. See <a href="https://core.telegram.org/bots/features#commands">this manual</a> for more details about bot commands.<para>Returns: </para></summary>
public partial class SetMyCommandsRequest : RequestBase<bool>
{
    /// <summary>A list of bot commands to be set as the list of the bot's commands. At most 100 commands can be specified.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<BotCommand> Commands { get; set; }

    /// <summary>An object, describing scope of users for which the commands are relevant. Defaults to <see cref="BotCommandScopeDefault"/>.</summary>
    public BotCommandScope? Scope { get; set; }

    /// <summary>A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given scope, for whose language there are no dedicated commands</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Initializes an instance of <see cref="SetMyCommandsRequest"/></summary>
    /// <param name="commands">A list of bot commands to be set as the list of the bot's commands. At most 100 commands can be specified.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetMyCommandsRequest(IEnumerable<BotCommand> commands) : this() => Commands = commands;

    /// <summary>Instantiates a new <see cref="SetMyCommandsRequest"/></summary>
    public SetMyCommandsRequest() : base("setMyCommands") { }
}
