// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a block in a rich formatted message to be sent. Currently, it can be any of the following types:<br/><see cref="InputRichBlockParagraph"/>, <see cref="InputRichBlockSectionHeading"/>, <see cref="InputRichBlockPreformatted"/>, <see cref="InputRichBlockFooter"/>, <see cref="InputRichBlockDivider"/>, <see cref="InputRichBlockMathematicalExpression"/>, <see cref="InputRichBlockAnchor"/>, <see cref="InputRichBlockList"/>, <see cref="InputRichBlockBlockQuotation"/>, <see cref="InputRichBlockPullQuotation"/>, <see cref="InputRichBlockCollage"/>, <see cref="InputRichBlockSlideshow"/>, <see cref="InputRichBlockTable"/>, <see cref="InputRichBlockDetails"/>, <see cref="InputRichBlockMap"/>, <see cref="InputRichBlockAnimation"/>, <see cref="InputRichBlockAudio"/>, <see cref="InputRichBlockPhoto"/>, <see cref="InputRichBlockVideo"/>, <see cref="InputRichBlockVoiceNote"/>, <see cref="InputRichBlockThinking"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputRichBlock>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputRichBlockParagraph), "paragraph")]
[CustomJsonDerivedType(typeof(InputRichBlockSectionHeading), "heading")]
[CustomJsonDerivedType(typeof(InputRichBlockPreformatted), "pre")]
[CustomJsonDerivedType(typeof(InputRichBlockFooter), "footer")]
[CustomJsonDerivedType(typeof(InputRichBlockDivider), "divider")]
[CustomJsonDerivedType(typeof(InputRichBlockMathematicalExpression), "mathematical_expression")]
[CustomJsonDerivedType(typeof(InputRichBlockAnchor), "anchor")]
[CustomJsonDerivedType(typeof(InputRichBlockList), "list")]
[CustomJsonDerivedType(typeof(InputRichBlockBlockQuotation), "blockquote")]
[CustomJsonDerivedType(typeof(InputRichBlockPullQuotation), "pullquote")]
[CustomJsonDerivedType(typeof(InputRichBlockCollage), "collage")]
[CustomJsonDerivedType(typeof(InputRichBlockSlideshow), "slideshow")]
[CustomJsonDerivedType(typeof(InputRichBlockTable), "table")]
[CustomJsonDerivedType(typeof(InputRichBlockDetails), "details")]
[CustomJsonDerivedType(typeof(InputRichBlockMap), "map")]
[CustomJsonDerivedType(typeof(InputRichBlockAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputRichBlockAudio), "audio")]
[CustomJsonDerivedType(typeof(InputRichBlockPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputRichBlockVideo), "video")]
[CustomJsonDerivedType(typeof(InputRichBlockVoiceNote), "voice_note")]
[CustomJsonDerivedType(typeof(InputRichBlockThinking), "thinking")]
public abstract partial class InputRichBlock
{
    /// <summary>Type of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputRichBlockType Type { get; }
}

/// <summary>A text paragraph, corresponding to the HTML tag <c>&lt;p&gt;</c>.</summary>
public partial class InputRichBlockParagraph : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Paragraph"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Paragraph;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A section heading, corresponding to the HTML tags <c>&lt;h1&gt;</c>, <c>&lt;h2&gt;</c>, <c>&lt;h3&gt;</c>, <c>&lt;h4&gt;</c>, <c>&lt;h5&gt;</c>, or <c>&lt;h6&gt;</c>.</summary>
public partial class InputRichBlockSectionHeading : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Heading"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Heading;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>Relative size of the text font; 1-6, 1 is the largest, 6 is the smallest</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Size { get; set; }
}

/// <summary>A preformatted text block, corresponding to the nested HTML tags <c>&lt;pre&gt;</c> and <c>&lt;code&gt;</c>.</summary>
public partial class InputRichBlockPreformatted : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Pre"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Pre;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. The programming language of the text</summary>
    public string? Language { get; set; }
}

/// <summary>A footer, corresponding to the HTML tag <c>&lt;footer&gt;</c>.</summary>
public partial class InputRichBlockFooter : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Footer"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Footer;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A divider, corresponding to the HTML tag <c>&lt;hr/&gt;</c>.</summary>
public partial class InputRichBlockDivider : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Divider"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Divider;
}

/// <summary>A block with a mathematical expression in LaTeX format, corresponding to the custom HTML tag <c>&lt;tg-math-block&gt;</c>.</summary>
public partial class InputRichBlockMathematicalExpression : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.MathematicalExpression"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.MathematicalExpression;

    /// <summary>The mathematical expression in LaTeX format</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Expression { get; set; } = default!;
}

/// <summary>A block with an anchor, corresponding to the HTML tag <c>&lt;a&gt;</c> with the attribute <c>name</c>.</summary>
public partial class InputRichBlockAnchor : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Anchor"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Anchor;

    /// <summary>The name of the anchor</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}

/// <summary>A list of blocks, corresponding to the HTML tag <c>&lt;ul&gt;</c> or <c>&lt;ol&gt;</c> with multiple nested tags <c>&lt;li&gt;</c>.</summary>
public partial class InputRichBlockList : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.List"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.List;

    /// <summary>Items of the list</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlockListItem> Items { get; set; } = default!;
}

/// <summary>A block quotation, corresponding to the HTML tag <c>&lt;blockquote&gt;</c>.</summary>
public partial class InputRichBlockBlockQuotation : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Blockquote"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Blockquote;

    /// <summary>Content of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlock> Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Credit of the block</summary>
    public RichText? Credit { get; set; }
}

/// <summary>A quotation with centered text, loosely corresponding to the HTML tag <c>&lt;aside&gt;</c>.</summary>
public partial class InputRichBlockPullQuotation : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Pullquote"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Pullquote;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Credit of the block</summary>
    public RichText? Credit { get; set; }
}

/// <summary>A collage, corresponding to the custom HTML tag <c>&lt;tg-collage&gt;</c>.</summary>
public partial class InputRichBlockCollage : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Collage"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Collage;

    /// <summary>Elements of the collage</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlock> Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A slideshow, corresponding to the custom HTML tag <c>&lt;tg-slideshow&gt;</c>.</summary>
public partial class InputRichBlockSlideshow : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Slideshow"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Slideshow;

    /// <summary>Elements of the slideshow</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlock> Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A table, corresponding to the HTML tag <c>&lt;table&gt;</c>.</summary>
public partial class InputRichBlockTable : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Table"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Table;

    /// <summary>Cells of the table</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<IEnumerable<RichBlockTableCell>> Cells { get; set; } = default!;

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the table has borders</summary>
    [JsonPropertyName("is_bordered")]
    public bool IsBordered { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the table is striped</summary>
    [JsonPropertyName("is_striped")]
    public bool IsStriped { get; set; }

    /// <summary><em>Optional</em>. Caption of the table</summary>
    public RichText? Caption { get; set; }
}

/// <summary>An expandable block for details disclosure, corresponding to the HTML tag <c>&lt;details&gt;</c>.</summary>
public partial class InputRichBlockDetails : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Details"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Details;

    /// <summary>Always shown summary of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Summary { get; set; } = default!;

    /// <summary>Content of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IEnumerable<InputRichBlock> Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the content of the block is visible by default</summary>
    [JsonPropertyName("is_open")]
    public bool IsOpen { get; set; }
}

/// <summary>A block with a map, corresponding to the custom HTML tag <c>&lt;tg-map&gt;</c>. The map's width and height must not exceed 10000 in total. The width and height ratio must be at most 20.</summary>
public partial class InputRichBlockMap : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Map"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Map;

    /// <summary>Location of the center of the map</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Location Location { get; set; } = default!;

    /// <summary>Map zoom level; 0-24</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Zoom { get; set; }

    /// <summary>Map width; 0-10000</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Map height; 0-10000</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with an animation, corresponding to the HTML tag <c>&lt;video&gt;</c>.</summary>
public partial class InputRichBlockAnimation : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Animation"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Animation;

    /// <summary>The animation. Caption is ignored.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaAnimation Animation { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a music file, corresponding to the HTML tag <c>&lt;audio&gt;</c>.</summary>
public partial class InputRichBlockAudio : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Audio"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Audio;

    /// <summary>The audio. Caption is ignored.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaAudio Audio { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a photo, corresponding to the HTML tag <c>&lt;img&gt;</c>.</summary>
public partial class InputRichBlockPhoto : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Photo"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Photo;

    /// <summary>The photo. Caption is ignored.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaPhoto Photo { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a video, corresponding to the HTML tag <c>&lt;video&gt;</c>.</summary>
public partial class InputRichBlockVideo : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Video"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Video;

    /// <summary>The video. Caption is ignored.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaVideo Video { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a voice note, corresponding to the HTML tag <c>&lt;audio&gt;</c>.</summary>
public partial class InputRichBlockVoiceNote : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.VoiceNote"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.VoiceNote;

    /// <summary>The voice note. Caption is ignored.</summary>
    [JsonPropertyName("voice_note")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaVoiceNote VoiceNote { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a “Thinking…” placeholder, corresponding to the custom HTML tag <c>&lt;tg-thinking&gt;</c>. The block may be used only in <see cref="TelegramBotClientExtensions.SendRichMessageDraft">SendRichMessageDraft</see>, therefore it can't be received in messages. See <a href="https://t.me/addemoji/AIActions">https://t.me/addemoji/AIActions</a> for examples of custom emoji that are recommended for usage in the block.</summary>
public partial class InputRichBlockThinking : InputRichBlock
{
    /// <summary>Type of the block, always <see cref="InputRichBlockType.Thinking"/></summary>
    public override InputRichBlockType Type => InputRichBlockType.Thinking;

    /// <summary>Text of the block. See <a href="https://t.me/addemoji/AIActions">https://t.me/addemoji/AIActions</a> for examples of custom emoji that are recommended for usage in the block.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}
