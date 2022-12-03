// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to move a sticker in a set created by the bot to a specific position.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerPositionInSetRequest : RequestBase<bool>
{
    /// <summary>
    /// File identifier of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sticker { get; }

    /// <summary>
    /// New sticker position in the set, zero-based
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Position { get; }

    /// <summary>
    /// Initializes a new request with sticker and position
    /// </summary>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="position">New sticker position in the set, zero-based</param>
    public SetStickerPositionInSetRequest(string sticker, int position)
        : base("setStickerPositionInSet")
    {
        Sticker = sticker;
        Position = position;
    }
}
