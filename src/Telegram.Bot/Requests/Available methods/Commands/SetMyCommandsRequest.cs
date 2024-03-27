using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the list of the bot’s commands. See
/// <a href="https://core.telegram.org/bots#commands"/> for more details about bot commands.
/// Returns <see langword="true"/> on success
/// </summary>
public class SetMyCommandsRequest : RequestBase<bool>
{
    /// <summary>
    /// A list of bot commands to be set as the list of the bot’s commands.
    /// At most 100 commands can be specified.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<BotCommand> Commands { get; init; }

    /// <summary>
    /// An object, describing scope of users for which the commands are relevant.
    /// Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BotCommandScope? Scope { get; set; }

    /// <summary>
    /// A two-letter ISO 639-1 language code. If empty, commands will be applied to all users
    /// from the given <see cref="Scope"/>, for whose language there are no dedicated commands
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request with commands
    /// </summary>
    /// <param name="commands">A list of bot commands to be set</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetMyCommandsRequest(IEnumerable<BotCommand> commands)
        : this()
    {
        Commands = commands;
    }

    /// <summary>
    /// Initializes a new request with commands
    /// </summary>
    public SetMyCommandsRequest()
        : base("setMyCommands")
    { }
}
