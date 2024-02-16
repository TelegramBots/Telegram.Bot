namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about the quoted part of a message that is replied to by the given message.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class TextQuote
{
    /// <summary>
    /// Text of the quoted part of a message that is replied to by the given message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Text { get; set; } = default!;

    /// <summary>
    /// Optional. Special entities that appear in the quote. Currently, only bold, italic, underline,
    /// strikethrough, spoiler, and custom_emoji entities are kept in quotes.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? Entities { get; set; }

    /// <summary>
    /// Approximate quote position in the original message in UTF-16 code units as specified by the sender
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Position { get; set; }

    /// <summary>
    /// Optional.True, if the quote was chosen manually by the message sender.
    /// Otherwise, the quote was added automatically by the server.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsManual { get; set; }
}
