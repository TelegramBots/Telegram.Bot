// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Cell in a table.</summary>
public partial class RichBlockTableCell
{
    /// <summary><em>Optional</em>. Text in the cell. If omitted, then the cell is invisible.</summary>
    public RichText? Text { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the cell is a header cell</summary>
    [JsonPropertyName("is_header")]
    public bool IsHeader { get; set; }

    /// <summary><em>Optional</em>. The number of columns the cell spans if it is bigger than 1</summary>
    public int? Colspan { get; set; }

    /// <summary><em>Optional</em>. The number of rows the cell spans if it is bigger than 1</summary>
    public int? Rowspan { get; set; }

    /// <summary>Horizontal cell content alignment. Currently, must be one of <see cref="RichBlockTableCellAlign.Left">Left</see>, <see cref="RichBlockTableCellAlign.Center">Center</see>, or <see cref="RichBlockTableCellAlign.Right">Right</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlockTableCellAlign Align { get; set; }

    /// <summary>Vertical cell content alignment. Currently, must be one of <see cref="RichBlockTableCellValign.Top">Top</see>, <see cref="RichBlockTableCellValign.Middle">Middle</see>, or <see cref="RichBlockTableCellValign.Bottom">Bottom</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlockTableCellValign Valign { get; set; }
}
