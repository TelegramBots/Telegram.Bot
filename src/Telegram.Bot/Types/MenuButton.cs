using Telegram.Bot.Converters;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the bot’s menu button in a private chat. It should be one of:
/// <list type="bullet">
/// <item>MenuButtonCommands</item>
/// <item>MenuButtonWebApp</item>
/// <item>MenuButtonDefault</item>
/// </list>
/// If a menu button other than MenuButtonDefault is set for a private chat, then it is applied in the chat.
/// Otherwise the default menu button is applied. By default, the menu button opens the list of bot commands.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
[JsonConverter(typeof(MenuButtonConverter))]
public abstract class MenuButton
{
    /// <summary>
    /// Type of the button
    /// </summary>
    [JsonProperty]
    public abstract MenuButtonType Type { get; }
}

/// <summary>
/// Represents a menu button, which opens the bot’s list of commands.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MenuButtonCommands : MenuButton
{
    /// <inheritdoc />
    public override MenuButtonType Type => MenuButtonType.Commands;
}

/// <summary>
/// Represents a menu button, which launches a <a href="https://core.telegram.org/bots/webapps">Web App</a>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MenuButtonWebApp : MenuButton
{
    /// <inheritdoc />
    public override MenuButtonType Type => MenuButtonType.WebApp;

    /// <summary>
    /// Text on the button
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Text { get; set; } = default!;

    /// <summary>
    /// Description of the Web App that will be launched when the user presses the button. The Web App will be able
    /// to send an arbitrary message on behalf of the user using the method <see cref="AnswerWebAppQueryRequest"/>.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public WebAppInfo WebApp { get; set; } = default!;
}

/// <summary>
/// Describes that no specific value for the menu button was set.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MenuButtonDefault : MenuButton
{
    /// <inheritdoc />
    public override MenuButtonType Type => MenuButtonType.Default;
}
