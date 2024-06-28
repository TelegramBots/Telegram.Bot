namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the list of emoji assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.<para>Returns: </para></summary>
public partial class SetStickerEmojiListRequest : RequestBase<bool>
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>A list of 1-20 emoji associated with the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> EmojiList { get; set; }

    /// <summary>Initializes an instance of <see cref="SetStickerEmojiListRequest"/></summary>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="emojiList">A list of 1-20 emoji associated with the sticker</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetStickerEmojiListRequest(InputFileId sticker, IEnumerable<string> emojiList) : this()
    {
        Sticker = sticker;
        EmojiList = emojiList;
    }

    /// <summary>Instantiates a new <see cref="SetStickerEmojiListRequest"/></summary>
    public SetStickerEmojiListRequest() : base("setStickerEmojiList") { }
}
