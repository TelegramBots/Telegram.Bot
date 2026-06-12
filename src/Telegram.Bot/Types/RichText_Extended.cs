using System.Diagnostics;

namespace Telegram.Bot.Types;

/// <summary>An array of <see cref="RichText"/></summary>
public class RichTextArray : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Array"/></summary>
    public override RichTextType Type => RichTextType.Array;

    /// <summary>The array of rich texts</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText[] Array { get; set; } = default!;
}

/// <summary>Plain text</summary>
[DebuggerDisplay("{Text}")]
public class RichTextText : RichText
{
    /// <summary>Type of the rich text, always <see cref="RichTextType.Text"/></summary>
    public override RichTextType Type => RichTextType.Text;

    /// <summary>The plain text</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;
}
