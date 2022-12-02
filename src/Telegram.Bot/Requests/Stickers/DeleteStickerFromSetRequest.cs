// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a sticker from a set created by the bot. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeleteStickerFromSetRequest : RequestBase<bool>
{
    /// <summary>
    /// File identifier of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sticker { get; }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    /// <param name="sticker">File identifier of the sticker</param>
    public DeleteStickerFromSetRequest(string sticker)
        : base("deleteStickerFromSet")
    {
        Sticker = sticker;
    }
}
