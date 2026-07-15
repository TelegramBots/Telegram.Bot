// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>An item of a list to be sent.</summary>
public partial class InputRichBlockListItem
{
    /// <summary>The content of the item</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlock> Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the item has a checkbox</summary>
    [JsonPropertyName("has_checkbox")]
    public bool HasCheckbox { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the item has a checked checkbox</summary>
    [JsonPropertyName("is_checked")]
    public bool IsChecked { get; set; }

    /// <summary><em>Optional</em>. For ordered lists, the numeric value of the item label</summary>
    public int? Value { get; set; }

    /// <summary><em>Optional</em>. For ordered lists, the type of the item label; must be one of “a” for lowercase letters, “A” for uppercase letters, “i” for lowercase Roman numerals, “I” for uppercase Roman numerals, or “1” for decimal numbers</summary>
    public string? Type { get; set; }
}
