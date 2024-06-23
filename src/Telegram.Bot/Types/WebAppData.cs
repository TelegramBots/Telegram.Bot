namespace Telegram.Bot.Types;

/// <summary>Describes data sent from a <a href="https://core.telegram.org/bots/webapps">Web App</a> to the bot.</summary>
public partial class WebAppData
{
    /// <summary>The data. Be aware that a bad client can send arbitrary data in this field.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Data { get; set; } = default!;

    /// <summary>Text of the <em>WebApp</em> keyboard button from which the Web App was opened. Be aware that a bad client can send arbitrary data in this field.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ButtonText { get; set; } = default!;
}
