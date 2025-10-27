// GENERATED FILE - DO NOT MODIFY MANUALLY
#pragma warning disable CS0108
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>Upon receiving a message with this object, Telegram clients will display a reply interface to the user (act as if the user has selected the bot's message and tapped 'Reply'). This can be extremely useful if you want to create user-friendly step-by-step interfaces without having to sacrifice <a href="https://core.telegram.org/bots/features#privacy-mode">privacy mode</a>. Not supported in channels and for messages sent on behalf of a Telegram Business account.</summary>
/// <remarks><b>Example:</b> A <a href="https://t.me/PollBot">poll bot</a> for groups runs in privacy mode (only receives commands, replies to its messages and mentions). There could be two ways to create a new poll:<br/><ul><li>Explain the user how to send a command with parameters (e.g. /newpoll question answer1 answer2). May be appealing for hardcore users but lacks modern day polish.</li><li>Guide the user through a step-by-step process. 'Please send me your question', 'Cool, now let's add the first answer option', 'Great. Keep adding answer options, then send /done when you're ready'.</li></ul>The last option is definitely more attractive. And if you use <a href="https://core.telegram.org/bots/api#forcereply">ForceReply</a> in your bot's questions, it will receive the user's answers even if it only receives replies, commands and mentions - without any extra work for the user.<br/></remarks>
public partial class ForceReplyMarkup : ReplyMarkup
{
    /// <summary>Shows reply interface to the user, as if they manually selected the bot's message and tapped 'Reply'</summary>
    [JsonPropertyName("force_reply")]
    public bool ForceReply => true;

    /// <summary><em>Optional</em>. The placeholder to be shown in the input field when the reply is active; 1-64 characters</summary>
    [JsonPropertyName("input_field_placeholder")]
    public string? InputFieldPlaceholder { get; set; }

    /// <summary><em>Optional</em>. Use this parameter if you want to force reply from specific users only. Targets: 1) users that are @mentioned in the <em>text</em> of the <see cref="Message"/> object; 2) if the bot's message is a reply to a message in the same chat and forum topic, sender of the original message.</summary>
    public bool Selective { get; set; }
}
