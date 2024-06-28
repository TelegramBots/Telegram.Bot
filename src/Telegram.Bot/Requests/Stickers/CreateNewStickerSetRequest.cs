namespace Telegram.Bot.Requests;

/// <summary>Use this method to create a new sticker set owned by a user. The bot will be able to edit the sticker set thus created.<para>Returns: </para></summary>
public partial class CreateNewStickerSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <summary>User identifier of created sticker set owner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <em>animals</em>). Can contain only English letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in <c>"_by_&lt;BotUsername&gt;"</c>. <c>&lt;BotUsername&gt;</c> is case insensitive. 1-64 characters.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Sticker set title, 1-64 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>A list of 1-50 initial stickers to be added to the sticker set</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InputSticker> Stickers { get; set; }

    /// <summary>Type of stickers in the set, pass <see cref="StickerType.Regular">Regular</see>, <see cref="StickerType.Mask">Mask</see>, or <see cref="StickerType.CustomEmoji">CustomEmoji</see>. By default, a regular sticker set is created.</summary>
    public StickerType? StickerType { get; set; }

    /// <summary>Pass <see langword="true"/> if stickers in the sticker set must be repainted to the color of text when used in messages, the accent color if used as emoji status, white on chat photos, or another appropriate color based on context; for custom emoji sticker sets only</summary>
    public bool NeedsRepainting { get; set; }

    /// <summary>Initializes an instance of <see cref="CreateNewStickerSetRequest"/></summary>
    /// <param name="userId">User identifier of created sticker set owner</param>
    /// <param name="name">Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <em>animals</em>). Can contain only English letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in <c>"_by_&lt;BotUsername&gt;"</c>. <c>&lt;BotUsername&gt;</c> is case insensitive. 1-64 characters.</param>
    /// <param name="title">Sticker set title, 1-64 characters</param>
    /// <param name="stickers">A list of 1-50 initial stickers to be added to the sticker set</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public CreateNewStickerSetRequest(long userId, string name, string title, IEnumerable<InputSticker> stickers) : this()
    {
        UserId = userId;
        Name = name;
        Title = title;
        Stickers = stickers;
    }

    /// <summary>Instantiates a new <see cref="CreateNewStickerSetRequest"/></summary>
    public CreateNewStickerSetRequest() : base("createNewStickerSet") { }
}
