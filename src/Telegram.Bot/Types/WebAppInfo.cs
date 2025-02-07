// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a <a href="https://core.telegram.org/bots/webapps">Web App</a>.</summary>
public partial class WebAppInfo
{
    /// <summary>An HTTPS URL of a Web App to be opened with additional data as specified in <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">Initializing Web Apps</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; set; }

    /// <summary>Initializes an instance of <see cref="WebAppInfo"/></summary>
    /// <param name="url">An HTTPS URL of a Web App to be opened with additional data as specified in <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">Initializing Web Apps</a></param>
    [SetsRequiredMembers]
    public WebAppInfo(string url) => Url = url;

    /// <summary>Instantiates a new <see cref="WebAppInfo"/></summary>
    public WebAppInfo() { }

    /// <summary>Implicit conversion to string (Url)</summary>
    public static implicit operator string(WebAppInfo self) => self.Url;
    /// <summary>Implicit conversion from string (Url)</summary>
    public static implicit operator WebAppInfo(string url) => new() { Url = url };
}
