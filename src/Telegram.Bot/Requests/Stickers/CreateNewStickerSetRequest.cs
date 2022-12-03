using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new sticker set owned by a user.
/// </summary>
public abstract class CreateNewStickerSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only English letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <summary>
    /// Sticker set title, 1-64 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// Type of stickers in the set, pass <see cref="StickerType.Regular"/> or <see cref="StickerType.Mask"/>.
    /// Custom emoji sticker sets can't be created via the Bot API at the moment.
    /// By default, a regular sticker set is created.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public StickerType? StickerType { get; set; }

    /// <summary>
    /// One or more emoji corresponding to the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Emojis { get; }

    /// <summary>
    /// An object for position where the mask should be placed on faces
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MaskPosition? MaskPosition { get; set; }

    /// <summary>
    /// Initializes a new request with userId, name and emojis
    /// </summary>
    /// <param name="userId">User identifier of sticker set owner</param>
    /// <param name="name">
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </param>
    /// <param name="title">Sticker set title, 1-64 characters</param>
    /// <param name="emojis">One or more emoji corresponding to the sticker</param>
    protected CreateNewStickerSetRequest(
        long userId,
        string name,
        string title,
        string emojis)
        : base("createNewStickerSet")
    {
        UserId = userId;
        Name = name;
        Title = title;
        Emojis = emojis;
    }
}
