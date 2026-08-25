// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object represents an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a> that appears right next to the message it belongs to.</summary>
public partial class InlineKeyboardMarkup : ReplyMarkup
{
    /// <summary>Array of button rows, each represented by an Array of <see cref="InlineKeyboardButton"/> objects</summary>
    [JsonPropertyName("inline_keyboard")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKeyboard { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the reply interface must be shown to the user, as if they had manually selected the bot's message and tapped 'Reply'. The value of the field can't be changed when the inline keyboard is edited.</summary>
    [JsonPropertyName("force_reply")]
    public new bool ForceReply { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineKeyboardMarkup"/></summary>
    /// <param name="inlineKeyboard">Array of button rows, each represented by an Array of <see cref="InlineKeyboardButton"/> objects</param>
    [SetsRequiredMembers]
    public InlineKeyboardMarkup(IEnumerable<IEnumerable<InlineKeyboardButton>> inlineKeyboard) => InlineKeyboard = inlineKeyboard;

    /// <summary>Instantiates a new <see cref="InlineKeyboardMarkup"/></summary>
    [SetsRequiredMembers]
    public InlineKeyboardMarkup() => InlineKeyboard = new List<List<InlineKeyboardButton>>();
}
