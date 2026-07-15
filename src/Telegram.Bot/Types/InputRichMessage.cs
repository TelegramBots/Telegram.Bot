// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a rich message to be sent. Exactly <b>one</b> of the fields <see cref="Html">Html</see>, <see cref="Markdown">Markdown</see>, or <see cref="Blocks">Blocks</see> must be used.</summary>
public partial class InputRichMessage
{
    /// <summary><em>Optional</em>. Content of the rich message to send described as a list of blocks</summary>
    public IEnumerable<InputRichBlock>? Blocks { get; set; }

    /// <summary><em>Optional</em>. Content of the rich message to send described using HTML formatting. See <a href="https://core.telegram.org/bots/api#rich-message-formatting-options">rich message formatting options</a> for more details. Use <see cref="Media">Media</see> field to specify the media used in the message.</summary>
    public string? Html { get; set; }

    /// <summary><em>Optional</em>. Content of the rich message to send described using Markdown formatting. See <a href="https://core.telegram.org/bots/api#rich-message-formatting-options">rich message formatting options</a> for more details. Use <see cref="Media">Media</see> field to specify the media used in the message.</summary>
    public string? Markdown { get; set; }

    /// <summary><em>Optional</em>. List of media that are specified in the <see cref="Markdown">Markdown</see> or <see cref="Html">Html</see> fields using <c>tg://photo?id=</c>, <c>tg://video?id=</c>, and <c>tg://audio?id=</c> links</summary>
    public IEnumerable<InputRichMessageMedia>? Media { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the rich message must be shown right-to-left</summary>
    [JsonPropertyName("is_rtl")]
    public bool IsRtl { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to skip automatic detection of entities (e.g., URLs, email addresses, username mentions, hashtags, cashtags, bot commands, or phone numbers) in the text</summary>
    [JsonPropertyName("skip_entity_detection")]
    public bool SkipEntityDetection { get; set; }
}
