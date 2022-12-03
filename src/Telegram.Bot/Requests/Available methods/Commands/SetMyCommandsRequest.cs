using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the list of the bot’s commands. See
/// <a href="https://core.telegram.org/bots#commands"/> for more details about bot commands.
/// Returns <see langword="true"/> on success
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetMyCommandsRequest : RequestBase<bool>
{
    /// <summary>
    /// A list of bot commands to be set as the list of the bot’s commands.
    /// At most 100 commands can be specified.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<BotCommand> Commands { get; }

    /// <summary>
    /// An object, describing scope of users for which the commands are relevant.
    /// Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public BotCommandScope? Scope { get; set; }

    /// <summary>
    /// A two-letter ISO 639-1 language code. If empty, commands will be applied to all users
    /// from the given <see cref="Scope"/>, for whose language there are no dedicated commands
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Initializes a new request with commands
    /// </summary>
    /// <param name="commands">A list of bot commands to be set</param>
    public SetMyCommandsRequest(IEnumerable<BotCommand> commands)
        : base("setMyCommands")
    {
        Commands = commands;
    }
}
