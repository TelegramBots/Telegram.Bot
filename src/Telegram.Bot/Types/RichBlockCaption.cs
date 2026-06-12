// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Caption of a rich formatted block.</summary>
public partial class RichBlockCaption
{
    /// <summary>Block caption</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Block credit which corresponds to the HTML tag &lt;cite&gt;</summary>
    public RichText? Credit { get; set; }
}
