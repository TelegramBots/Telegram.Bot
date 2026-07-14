// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a block in a rich formatted message. Currently, it can be any of the following types:<br/><see cref="RichBlockParagraph"/>, <see cref="RichBlockSectionHeading"/>, <see cref="RichBlockPreformatted"/>, <see cref="RichBlockFooter"/>, <see cref="RichBlockDivider"/>, <see cref="RichBlockMathematicalExpression"/>, <see cref="RichBlockAnchor"/>, <see cref="RichBlockList"/>, <see cref="RichBlockBlockQuotation"/>, <see cref="RichBlockPullQuotation"/>, <see cref="RichBlockCollage"/>, <see cref="RichBlockSlideshow"/>, <see cref="RichBlockTable"/>, <see cref="RichBlockDetails"/>, <see cref="RichBlockMap"/>, <see cref="RichBlockAnimation"/>, <see cref="RichBlockAudio"/>, <see cref="RichBlockPhoto"/>, <see cref="RichBlockVideo"/>, <see cref="RichBlockVoiceNote"/>, <see cref="RichBlockThinking"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<RichBlock>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(RichBlockParagraph), "paragraph")]
[CustomJsonDerivedType(typeof(RichBlockSectionHeading), "heading")]
[CustomJsonDerivedType(typeof(RichBlockPreformatted), "pre")]
[CustomJsonDerivedType(typeof(RichBlockFooter), "footer")]
[CustomJsonDerivedType(typeof(RichBlockDivider), "divider")]
[CustomJsonDerivedType(typeof(RichBlockMathematicalExpression), "mathematical_expression")]
[CustomJsonDerivedType(typeof(RichBlockAnchor), "anchor")]
[CustomJsonDerivedType(typeof(RichBlockList), "list")]
[CustomJsonDerivedType(typeof(RichBlockBlockQuotation), "blockquote")]
[CustomJsonDerivedType(typeof(RichBlockPullQuotation), "pullquote")]
[CustomJsonDerivedType(typeof(RichBlockCollage), "collage")]
[CustomJsonDerivedType(typeof(RichBlockSlideshow), "slideshow")]
[CustomJsonDerivedType(typeof(RichBlockTable), "table")]
[CustomJsonDerivedType(typeof(RichBlockDetails), "details")]
[CustomJsonDerivedType(typeof(RichBlockMap), "map")]
[CustomJsonDerivedType(typeof(RichBlockAnimation), "animation")]
[CustomJsonDerivedType(typeof(RichBlockAudio), "audio")]
[CustomJsonDerivedType(typeof(RichBlockPhoto), "photo")]
[CustomJsonDerivedType(typeof(RichBlockVideo), "video")]
[CustomJsonDerivedType(typeof(RichBlockVoiceNote), "voice_note")]
[CustomJsonDerivedType(typeof(RichBlockThinking), "thinking")]
public abstract partial class RichBlock
{
    /// <summary>Type of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract RichBlockType Type { get; }
}

/// <summary>A text paragraph, corresponding to the HTML tag <c>&lt;p&gt;</c>.</summary>
public partial class RichBlockParagraph : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Paragraph"/></summary>
    public override RichBlockType Type => RichBlockType.Paragraph;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A section heading, corresponding to the HTML tags <c>&lt;h1&gt;</c>, <c>&lt;h2&gt;</c>, <c>&lt;h3&gt;</c>, <c>&lt;h4&gt;</c>, <c>&lt;h5&gt;</c>, or <c>&lt;h6&gt;</c>.</summary>
public partial class RichBlockSectionHeading : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Heading"/></summary>
    public override RichBlockType Type => RichBlockType.Heading;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary>Relative size of the text font; 1-6, 1 is the largest, 6 is the smallest</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Size { get; set; }
}

/// <summary>A preformatted text block, corresponding to the nested HTML tags <c>&lt;pre&gt;</c> and <c>&lt;code&gt;</c>.</summary>
public partial class RichBlockPreformatted : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Pre"/></summary>
    public override RichBlockType Type => RichBlockType.Pre;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. The programming language of the text</summary>
    public string? Language { get; set; }
}

/// <summary>A footer, corresponding to the HTML tag <c>&lt;footer&gt;</c>.</summary>
public partial class RichBlockFooter : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Footer"/></summary>
    public override RichBlockType Type => RichBlockType.Footer;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}

/// <summary>A divider, corresponding to the HTML tag <c>&lt;hr/&gt;</c>.</summary>
public partial class RichBlockDivider : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Divider"/></summary>
    public override RichBlockType Type => RichBlockType.Divider;
}

/// <summary>A block with a mathematical expression in LaTeX format, corresponding to the custom HTML tag <c>&lt;tg-math-block&gt;</c>.</summary>
public partial class RichBlockMathematicalExpression : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.MathematicalExpression"/></summary>
    public override RichBlockType Type => RichBlockType.MathematicalExpression;

    /// <summary>The mathematical expression in LaTeX format</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Expression { get; set; } = default!;
}

/// <summary>A block with an anchor, corresponding to the HTML tag <c>&lt;a&gt;</c> with the attribute <c>name</c>.</summary>
public partial class RichBlockAnchor : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Anchor"/></summary>
    public override RichBlockType Type => RichBlockType.Anchor;

    /// <summary>The name of the anchor</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}

/// <summary>A list of blocks, corresponding to the HTML tag <c>&lt;ul&gt;</c> or <c>&lt;ol&gt;</c> with multiple nested tags <c>&lt;li&gt;</c>.</summary>
public partial class RichBlockList : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.List"/></summary>
    public override RichBlockType Type => RichBlockType.List;

    /// <summary>Items of the list</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlockListItem[] Items { get; set; } = default!;
}

/// <summary>A block quotation, corresponding to the HTML tag <c>&lt;blockquote&gt;</c>.</summary>
public partial class RichBlockBlockQuotation : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Blockquote"/></summary>
    public override RichBlockType Type => RichBlockType.Blockquote;

    /// <summary>Content of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Credit of the block</summary>
    public RichText? Credit { get; set; }
}

/// <summary>A quotation with centered text, loosely corresponding to the HTML tag <c>&lt;aside&gt;</c>.</summary>
public partial class RichBlockPullQuotation : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Pullquote"/></summary>
    public override RichBlockType Type => RichBlockType.Pullquote;

    /// <summary>Text of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Credit of the block</summary>
    public RichText? Credit { get; set; }
}

/// <summary>A collage, corresponding to the custom HTML tag <c>&lt;tg-collage&gt;</c>.</summary>
public partial class RichBlockCollage : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Collage"/></summary>
    public override RichBlockType Type => RichBlockType.Collage;

    /// <summary>Elements of the collage</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A slideshow, corresponding to the custom HTML tag <c>&lt;tg-slideshow&gt;</c>.</summary>
public partial class RichBlockSlideshow : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Slideshow"/></summary>
    public override RichBlockType Type => RichBlockType.Slideshow;

    /// <summary>Elements of the slideshow</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A table, corresponding to the HTML tag <c>&lt;table&gt;</c>.</summary>
public partial class RichBlockTable : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Table"/></summary>
    public override RichBlockType Type => RichBlockType.Table;

    /// <summary>Cells of the table</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlockTableCell[][] Cells { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the table has borders</summary>
    [JsonPropertyName("is_bordered")]
    public bool IsBordered { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the table is striped</summary>
    [JsonPropertyName("is_striped")]
    public bool IsStriped { get; set; }

    /// <summary><em>Optional</em>. Caption of the table</summary>
    public RichText? Caption { get; set; }
}

/// <summary>An expandable block for details disclosure, corresponding to the HTML tag <c>&lt;details&gt;</c>.</summary>
public partial class RichBlockDetails : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Details"/></summary>
    public override RichBlockType Type => RichBlockType.Details;

    /// <summary>Always shown summary of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Summary { get; set; } = default!;

    /// <summary>Content of the block</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichBlock[] Blocks { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the content of the block is visible by default</summary>
    [JsonPropertyName("is_open")]
    public bool IsOpen { get; set; }
}

/// <summary>A block with a map, corresponding to the custom HTML tag <c>&lt;tg-map&gt;</c>.</summary>
public partial class RichBlockMap : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Map"/></summary>
    public override RichBlockType Type => RichBlockType.Map;

    /// <summary>Location of the center of the map</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Location Location { get; set; } = default!;

    /// <summary>Map zoom level; 13-20</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Zoom { get; set; }

    /// <summary>Expected width of the map</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Expected height of the map</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with an animation, corresponding to the HTML tag <c>&lt;video&gt;</c>.</summary>
public partial class RichBlockAnimation : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Animation"/></summary>
    public override RichBlockType Type => RichBlockType.Animation;

    /// <summary>The animation</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Animation Animation { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the media preview is covered by a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a music file, corresponding to the HTML tag <c>&lt;audio&gt;</c>.</summary>
public partial class RichBlockAudio : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Audio"/></summary>
    public override RichBlockType Type => RichBlockType.Audio;

    /// <summary>The audio</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Audio Audio { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a photo, corresponding to the HTML tag <c>&lt;img&gt;</c>.</summary>
public partial class RichBlockPhoto : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Photo"/></summary>
    public override RichBlockType Type => RichBlockType.Photo;

    /// <summary>Available sizes of the photo</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PhotoSize[] Photo { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the media preview is covered by a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a video, corresponding to the HTML tag <c>&lt;video&gt;</c>.</summary>
public partial class RichBlockVideo : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Video"/></summary>
    public override RichBlockType Type => RichBlockType.Video;

    /// <summary>The video</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Video Video { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the media preview is covered by a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a voice note, corresponding to the HTML tag <c>&lt;audio&gt;</c>.</summary>
public partial class RichBlockVoiceNote : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.VoiceNote"/></summary>
    public override RichBlockType Type => RichBlockType.VoiceNote;

    /// <summary>The voice note</summary>
    [JsonPropertyName("voice_note")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Voice VoiceNote { get; set; } = default!;

    /// <summary><em>Optional</em>. Caption of the block</summary>
    public RichBlockCaption? Caption { get; set; }
}

/// <summary>A block with a “Thinking…” placeholder, corresponding to the custom HTML tag <c>&lt;tg-thinking&gt;</c>. The block may be used only in <see cref="TelegramBotClientExtensions.SendRichMessageDraft">SendRichMessageDraft</see>, therefore it can't be received in messages. See <a href="https://t.me/addemoji/AIActions">https://t.me/addemoji/AIActions</a> for examples of custom emoji that are recommended for usage in the block.</summary>
public partial class RichBlockThinking : RichBlock
{
    /// <summary>Type of the block, always <see cref="RichBlockType.Thinking"/></summary>
    public override RichBlockType Type => RichBlockType.Thinking;

    /// <summary>Text of the block. See <a href="https://t.me/addemoji/AIActions">https://t.me/addemoji/AIActions</a> for examples of custom emoji that are recommended for usage in the block.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;
}
