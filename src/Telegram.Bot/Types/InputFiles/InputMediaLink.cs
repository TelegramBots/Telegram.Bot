// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents an HTTP link to be sent.</summary>
public partial class InputMediaLink : InputPollOptionMedia
{
    /// <summary>Type of the media, must be <em>link</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaType Type => InputMediaType.Link;

    /// <summary>HTTP URL of the link</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaLink"/></summary>
    /// <param name="url">HTTP URL of the link</param>
    [SetsRequiredMembers]
    public InputMediaLink(string url) => Url = url;

    /// <summary>Instantiates a new <see cref="InputMediaLink"/></summary>
    public InputMediaLink() { }
}
