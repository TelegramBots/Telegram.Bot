// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get a sticker set. On success, a <see cref="StickerSet"/> object is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetStickerSetRequest : RequestBase<StickerSet>
{
    /// <summary>
    /// Name of the sticker set
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <summary>
    /// Initializes a new request with name
    /// </summary>
    /// <param name="name">Name of the sticker set</param>
    public GetStickerSetRequest(string name)
        : base("getStickerSet")
    {
        Name = name;
    }
}
