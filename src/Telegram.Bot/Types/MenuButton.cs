namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the bot's menu button in a private chat. It should be one of<br/><see cref="MenuButtonDefault"/>, <see cref="MenuButtonCommands"/>, <see cref="MenuButtonWebApp"/><br/>If a menu button other than <see cref="MenuButtonDefault"/> is set for a private chat, then it is applied in the chat. Otherwise the default menu button is applied. By default, the menu button opens the list of bot commands.
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(MenuButtonDefault), "default")]
[CustomJsonDerivedType(typeof(MenuButtonCommands), "commands")]
[CustomJsonDerivedType(typeof(MenuButtonWebApp), "web_app")]
public abstract partial class MenuButton
{
    /// <summary>
    /// Type of the button
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract Enums.MenuButtonType Type { get; }
}

/// <summary>
/// Represents a menu button, which opens the bot's list of commands.
/// </summary>
public partial class MenuButtonCommands : MenuButton
{
    /// <summary>
    /// Type of the button, always <see cref="Enums.MenuButtonType.Commands"/>
    /// </summary>
    public override Enums.MenuButtonType Type => Enums.MenuButtonType.Commands;
}

/// <summary>
/// Represents a menu button, which launches a <a href="https://core.telegram.org/bots/webapps">Web App</a>.
/// </summary>
public partial class MenuButtonWebApp : MenuButton
{
    /// <summary>
    /// Type of the button, always <see cref="Enums.MenuButtonType.WebApp"/>
    /// </summary>
    public override Enums.MenuButtonType Type => Enums.MenuButtonType.WebApp;

    /// <summary>
    /// Text on the button
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary>
    /// Description of the Web App that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQueryAsync">AnswerWebAppQuery</see>.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public WebAppInfo WebApp { get; set; } = default!;
}

/// <summary>
/// Describes that no specific value for the menu button was set.
/// </summary>
public partial class MenuButtonDefault : MenuButton
{
    /// <summary>
    /// Type of the button, always <see cref="Enums.MenuButtonType.Default"/>
    /// </summary>
    public override Enums.MenuButtonType Type => Enums.MenuButtonType.Default;
}
