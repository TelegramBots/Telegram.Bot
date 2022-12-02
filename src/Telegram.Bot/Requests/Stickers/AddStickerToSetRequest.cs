using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to add a new sticker to a set created by the bot. Static sticker sets
/// can have up to 120 stickers. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class AddStickerToSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

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
    /// Use this request to add a new sticker to a set created by the bot. Static sticker sets
    /// can have up to 120 stickers. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="name">Sticker set name</param>
    /// <param name="emojis">One or more emoji corresponding to the sticker</param>
    protected AddStickerToSetRequest(
        long userId,
        string name,
        string emojis)
        : base("addStickerToSet")
    {
        UserId = userId;
        Name = name;
        Emojis = emojis;
    }
}
