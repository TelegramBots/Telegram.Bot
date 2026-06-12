// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>An item of a list.</summary>
public partial class RichBlockListItem
{
    /// <summary>Label of the item</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Label { get; set; } = default!;

    /// <summary>The content of the item</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the item has a checkbox</summary>
    [JsonPropertyName("has_checkbox")]
    public bool HasCheckbox { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the item has a checked checkbox</summary>
    [JsonPropertyName("is_checked")]
    public bool IsChecked { get; set; }

    /// <summary><em>Optional</em>. For ordered lists, the numeric value of the item label</summary>
    public int? Value { get; set; }

    /// <summary><em>Optional</em>. For ordered lists, the type of the item label; must be one of “a” for lowercase letters, “A” for uppercase letters, “i” for lowercase Roman numerals, “I” for uppercase Roman numerals, or “1” for decimal numbers</summary>
    public string? Type { get; set; }
}
