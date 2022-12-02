namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of the <see cref="MenuButton"/>
/// </summary>
[JsonConverter(typeof(MenuButtonTypeConverter))]
public enum MenuButtonType
{
    /// <summary>
    /// Describes that no specific value for the menu button was set.
    /// </summary>
    Default = 1,

    /// <summary>
    /// Represents a menu button, which opens the bot’s list of commands.
    /// </summary>
    Commands,

    /// <summary>
    /// Represents a menu button, which launches a <a href="https://core.telegram.org/bots/webapps">Web App</a>.
    /// </summary>
    WebApp
}
