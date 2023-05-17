using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes a sticker to be added to a sticker set.
/// <a href="https://core.telegram.org/bots/api#inputsticker"/>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InputSticker
{
    /// <summary>
    /// The added sticker. Pass a <see cref="InputFileId"/> as a String to send a file that already exists
    /// on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file
    /// from the Internet, or upload a new one using multipart/form-data.
    /// <see cref="StickerFormat.Animated">Animated</see> and <see cref="StickerFormat.Video">video</see>
    /// stickers can't be uploaded via HTTP URL.
    /// If you are using a <see cref="InputFileStream"/>, then the property <see cref="InputFileStream.FileName"/> is required.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFile Sticker { get; }

    /// <summary>
    /// List of 1-20 emoji associated with the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<string> EmojiList { get; }

    /// <summary>
    /// Optional. Position where the mask should be placed on faces.
    /// For <see cref="StickerType.Mask"/> stickers only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MaskPosition? MaskPosition { get; set; }

    /// <summary>
    /// Optional. List of 0-20 search keywords for the sticker with total length of up to 64 characters.
    /// For <see cref="StickerType.Regular"/> and <see cref="StickerType.CustomEmoji"/> stickers only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<string>? KeyWords { get; set; }

    /// <summary>
    /// Initializes a new input sticker to create or add sticker sets
    /// with an <see cref="InputFile">sticker</see> and emojiList
    /// </summary>
    /// <param name="sticker">
    /// The added sticker. Pass a file_id as a String to send a file that already exists
    /// on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file
    /// from the Internet, or upload a new one using multipart/form-data.
    /// <see cref="StickerFormat.Animated">Animated</see> and <see cref="StickerFormat.Video">video</see>
    /// stickers can't be uploaded via HTTP URL.
    /// </param>
    /// <param name="emojiList">
    /// List of 1-20 emoji associated with the sticker
    /// </param>
    public InputSticker(InputFile sticker, IEnumerable<string> emojiList)
    {
        Sticker = sticker;
        EmojiList = emojiList;
    }
}
