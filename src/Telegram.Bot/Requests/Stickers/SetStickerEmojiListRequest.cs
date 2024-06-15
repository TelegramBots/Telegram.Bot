using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the list of emoji assigned to a regular or custom emoji sticker.
/// The sticker must belong to a sticker set created by the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
public class SetStickerEmojiListRequest : RequestBase<bool>
{
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// A list of 1-20 emoji associated with the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> EmojiList { get; init; }

    /// <summary>
    /// Initializes a new request with sticker and emojiList
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="emojiList">
    /// A list of 1-20 emoji associated with the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetStickerEmojiListRequest(InputFileId sticker, IEnumerable<string> emojiList)
        : this()
    {
        Sticker = sticker;
        EmojiList = emojiList;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetStickerEmojiListRequest()
        : base("setStickerEmojiList")
    { }
}
