namespace Telegram.Bot.Types;

/// <summary>
/// Contains data sent from a <a href="https://core.telegram.org/bots/webapps"></a>Web App to the bot.
/// </summary>
public class WebAppData
{
    /// <summary>
    /// The data. Be aware that a bad client can send arbitrary data in this field.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Data { get; set; } = default!;

    /// <summary>
    /// Text of the web_app keyboard button, from which the Web App was opened. Be aware that a bad client can
    /// send arbitrary data in this field.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ButtonText { get; set; } = default!;
}
