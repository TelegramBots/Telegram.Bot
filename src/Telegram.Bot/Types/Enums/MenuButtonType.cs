// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the button</summary>
[JsonConverter(typeof(EnumConverter<MenuButtonType>))]
public enum MenuButtonType
{
    /// <summary>Describes that no specific value for the menu button was set.<br/><br/><i>(<see cref="MenuButton"/> can be cast into <see cref="MenuButtonDefault"/>)</i></summary>
    Default = 1,
    /// <summary>Represents a menu button, which opens the bot's list of commands.<br/><br/><i>(<see cref="MenuButton"/> can be cast into <see cref="MenuButtonCommands"/>)</i></summary>
    Commands,
    /// <summary>Represents a menu button, which launches a <a href="https://core.telegram.org/bots/webapps">Web App</a>.<br/><br/><i>(<see cref="MenuButton"/> can be cast into <see cref="MenuButtonWebApp"/>)</i></summary>
    WebApp,
}
