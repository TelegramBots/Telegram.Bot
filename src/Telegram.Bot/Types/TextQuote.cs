// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about the quoted part of a message that is replied to by the given message.</summary>
public partial class TextQuote
{
    /// <summary>Text of the quoted part of a message that is replied to by the given message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the quote. Currently, only <em>bold</em>, <em>italic</em>, <em>underline</em>, <em>strikethrough</em>, <em>spoiler</em>, and <em>CustomEmoji</em> entities are kept in quotes.</summary>
    public MessageEntity[]? Entities { get; set; }

    /// <summary>Approximate quote position in the original message in UTF-16 code units as specified by the sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Position { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the quote was chosen manually by the message sender. Otherwise, the quote was added automatically by the server.</summary>
    [JsonPropertyName("is_manual")]
    public bool IsManual { get; set; }
}
