// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the block</summary>
[JsonConverter(typeof(EnumConverter<InputRichBlockType>))]
public enum InputRichBlockType
{
    /// <summary>A text paragraph, corresponding to the HTML tag <c>&lt;p&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockParagraph"/>)</i></summary>
    Paragraph = 1,
    /// <summary>A section heading, corresponding to the HTML tags <c>&lt;h1&gt;</c>, <c>&lt;h2&gt;</c>, <c>&lt;h3&gt;</c>, <c>&lt;h4&gt;</c>, <c>&lt;h5&gt;</c>, or <c>&lt;h6&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockSectionHeading"/>)</i></summary>
    Heading,
    /// <summary>A preformatted text block, corresponding to the nested HTML tags <c>&lt;pre&gt;</c> and <c>&lt;code&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockPreformatted"/>)</i></summary>
    Pre,
    /// <summary>A footer, corresponding to the HTML tag <c>&lt;footer&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockFooter"/>)</i></summary>
    Footer,
    /// <summary>A divider, corresponding to the HTML tag <c>&lt;hr/&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockDivider"/>)</i></summary>
    Divider,
    /// <summary>A block with a mathematical expression in LaTeX format, corresponding to the custom HTML tag <c>&lt;tg-math-block&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockMathematicalExpression"/>)</i></summary>
    MathematicalExpression,
    /// <summary>A block with an anchor, corresponding to the HTML tag <c>&lt;a&gt;</c> with the attribute <c>name</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockAnchor"/>)</i></summary>
    Anchor,
    /// <summary>A list of blocks, corresponding to the HTML tag <c>&lt;ul&gt;</c> or <c>&lt;ol&gt;</c> with multiple nested tags <c>&lt;li&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockList"/>)</i></summary>
    List,
    /// <summary>A block quotation, corresponding to the HTML tag <c>&lt;blockquote&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockBlockQuotation"/>)</i></summary>
    Blockquote,
    /// <summary>A quotation with centered text, loosely corresponding to the HTML tag <c>&lt;aside&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockPullQuotation"/>)</i></summary>
    Pullquote,
    /// <summary>A collage, corresponding to the custom HTML tag <c>&lt;tg-collage&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockCollage"/>)</i></summary>
    Collage,
    /// <summary>A slideshow, corresponding to the custom HTML tag <c>&lt;tg-slideshow&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockSlideshow"/>)</i></summary>
    Slideshow,
    /// <summary>A table, corresponding to the HTML tag <c>&lt;table&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockTable"/>)</i></summary>
    Table,
    /// <summary>An expandable block for details disclosure, corresponding to the HTML tag <c>&lt;details&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockDetails"/>)</i></summary>
    Details,
    /// <summary>A block with a map, corresponding to the custom HTML tag <c>&lt;tg-map&gt;</c>. The map's width and height must not exceed 10000 in total. The width and height ratio must be at most 20.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockMap"/>)</i></summary>
    Map,
    /// <summary>A block with an animation, corresponding to the HTML tag <c>&lt;video&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockAnimation"/>)</i></summary>
    Animation,
    /// <summary>A block with a music file, corresponding to the HTML tag <c>&lt;audio&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockAudio"/>)</i></summary>
    Audio,
    /// <summary>A block with a photo, corresponding to the HTML tag <c>&lt;img&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockPhoto"/>)</i></summary>
    Photo,
    /// <summary>A block with a video, corresponding to the HTML tag <c>&lt;video&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockVideo"/>)</i></summary>
    Video,
    /// <summary>A block with a voice note, corresponding to the HTML tag <c>&lt;audio&gt;</c>.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockVoiceNote"/>)</i></summary>
    VoiceNote,
    /// <summary>A block with a “Thinking…” placeholder, corresponding to the custom HTML tag <c>&lt;tg-thinking&gt;</c>. The block may be used only in <see cref="TelegramBotClientExtensions.SendRichMessageDraft">SendRichMessageDraft</see>, therefore it can't be received in messages. See <a href="https://t.me/addemoji/AIActions">https://t.me/addemoji/AIActions</a> for examples of custom emoji that are recommended for usage in the block.<br/><br/><i>(<see cref="InputRichBlock"/> can be cast into <see cref="InputRichBlockThinking"/>)</i></summary>
    Thinking,
}
