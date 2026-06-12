// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Rich formatted message.</summary>
public partial class RichMessage
{
    /// <summary>Content of the message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the rich message must be shown right-to-left</summary>
    [JsonPropertyName("is_rtl")]
    public bool IsRtl { get; set; }
}
