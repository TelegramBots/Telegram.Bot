namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the list of emoji assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetStickerEmojiListRequest() : RequestBase<bool>("setStickerEmojiList")
{
    /// <summary>File identifier of the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; set; }

    /// <summary>A list of 1-20 emoji associated with the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> EmojiList { get; set; }
}
