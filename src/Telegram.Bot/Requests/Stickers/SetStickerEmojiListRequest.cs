using System.Collections.Generic;

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the list of emoji assigned to a regular or custom emoji sticker.
/// The sticker must belong to a sticker set created by the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerEmojiListRequest : RequestBase<bool>
{
    /// <summary>
    /// File identifier of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sticker { get; }

    /// <summary>
    /// A JSON-serialized list of 1-20 emoji associated with the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<string> EmojiList { get; }

    /// <summary>
    /// Initializes a new request with sticker and emojiList
    /// </summary>
    /// <param name="sticker">
    /// File identifier of the sticker
    /// </param>
    /// <param name="emojiList">
    /// A JSON-serialized list of 1-20 emoji associated with the sticker
    /// </param>
    public SetStickerEmojiListRequest(string sticker, IEnumerable<string> emojiList)
        : base("setStickerEmojiList")
    {
        Sticker = sticker;
        EmojiList = emojiList;
    }
}
