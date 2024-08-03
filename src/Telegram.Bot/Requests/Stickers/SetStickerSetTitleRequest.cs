namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the title of a created sticker set.<para>Returns: </para></summary>
public partial class SetStickerSetTitleRequest : RequestBase<bool>
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Sticker set title, 1-64 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Instantiates a new <see cref="SetStickerSetTitleRequest"/></summary>
    public SetStickerSetTitleRequest() : base("setStickerSetTitle") { }
}
