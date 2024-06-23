namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete the list of the bot's commands for the given scope and user language. After deletion, <a href="https://core.telegram.org/bots/api#determining-list-of-commands">higher level commands</a> will be shown to affected users.<para>Returns: </para></summary>
public partial class DeleteMyCommandsRequest : RequestBase<bool>
{
    /// <summary>An object, describing scope of users for which the commands are relevant. Defaults to <see cref="BotCommandScopeDefault"/>.</summary>
    public BotCommandScope? Scope { get; set; }

    /// <summary>A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given scope, for whose language there are no dedicated commands</summary>
    public string? LanguageCode { get; set; }

    /// <summary>Instantiates a new <see cref="DeleteMyCommandsRequest"/></summary>
    public DeleteMyCommandsRequest() : base("deleteMyCommands") { }
}
