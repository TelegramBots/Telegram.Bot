namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// Upon receiving a <see cref="Message"/> with this object, Telegram clients will display a reply interface to the
/// user (act as if the user has selected the bot’s message and tapped 'Reply'). This can be extremely useful if you
/// want to create user-friendly step-by-step interfaces without having to sacrifice
/// <a href="https://core.telegram.org/bots#privacy-mode">privacy mode</a>.
/// </summary>
public class ForceReplyMarkup : IReplyMarkup
{
    /// <summary>
    /// Shows reply interface to the user, as if they manually selected the bot’s message and tapped 'Reply'
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool ForceReply => true;

    /// <summary>
    /// Optional. The placeholder to be shown in the input field when the reply is active; 1-64 characters
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InputFieldPlaceholder { get; set; }

    /// <summary>
    /// <em>Optional</em>. Use this parameter if you want to force reply from specific users only. Targets: 1) users that are @mentioned in the <em>text</em> of the <see cref="Message"/> object; 2) if the bot's message is a reply to a message in the same chat and forum topic, sender of the original message.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Selective { get; set; }
}
