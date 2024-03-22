using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Types;

/// <summary>
/// Contains information about a <a href="https://core.telegram.org/bots/webapps ">Web App</a>
/// </summary>
public class WebAppInfo
{
    /// <summary>
    /// An HTTPS URL of a Web App to be opened with additional data as specified in
    /// <a href="https://core.telegram.org/bots/webapps#initializing-web-apps">Initializing Web Apps</a>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebAppInfo"/> class with url
    /// </summary>
    /// <param name="url">
    /// An HTTPS URL of a Web App to be opened with additional data as specified in
    /// <a href="https://core.telegram.org/bots/webapps#initializing-web-apps">Initializing Web Apps</a>
    /// </param>
    [SetsRequiredMembers]
    public WebAppInfo(string url)
        => Url = url;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebAppInfo"/> class
    /// </summary>
    public WebAppInfo()
    {}
}
