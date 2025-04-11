// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a sticker to be added to a sticker set.</summary>
public partial class InputSticker
{
    /// <summary>The added sticker. Pass a <em>FileId</em> as a String to send a file that already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. Animated and video stickers can't be uploaded via HTTP URL. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Sticker { get; set; }

    /// <summary>Format of the added sticker, must be one of <see cref="StickerFormat.Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="StickerFormat.Animated">Animated</see> for a <b>.TGS</b> animation, <see cref="StickerFormat.Video">Video</see> for a <b>.WEBM</b> video</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required StickerFormat Format { get; set; }

    /// <summary>List of 1-20 emoji associated with the sticker</summary>
    [JsonPropertyName("emoji_list")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> EmojiList { get; set; }

    /// <summary><em>Optional</em>. Position where the mask should be placed on faces. For “mask” stickers only.</summary>
    [JsonPropertyName("mask_position")]
    public MaskPosition? MaskPosition { get; set; }

    /// <summary><em>Optional</em>. List of 0-20 search keywords for the sticker with total length of up to 64 characters. For “regular” and “CustomEmoji” stickers only.</summary>
    public IEnumerable<string>? Keywords { get; set; }

    /// <summary>Initializes an instance of <see cref="InputSticker"/></summary>
    /// <param name="sticker">The added sticker. Pass a <em>FileId</em> as a String to send a file that already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. Animated and video stickers can't be uploaded via HTTP URL. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="format">Format of the added sticker, must be one of <see cref="StickerFormat.Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="StickerFormat.Animated">Animated</see> for a <b>.TGS</b> animation, <see cref="StickerFormat.Video">Video</see> for a <b>.WEBM</b> video</param>
    /// <param name="emojiList">List of 1-20 emoji associated with the sticker</param>
    [SetsRequiredMembers]
    public InputSticker(InputFile sticker, StickerFormat format, IEnumerable<string> emojiList)
    {
        Sticker = sticker;
        Format = format;
        EmojiList = emojiList;
    }

    /// <summary>Instantiates a new <see cref="InputSticker"/></summary>
    public InputSticker() { }
}
