// ReSharper disable once CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a sticker from a set created by the bot. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeleteStickerFromSetRequest : RequestBase<bool>
{
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required parameters")]
    public DeleteStickerFromSetRequest(InputFileId sticker)
        : this()
    {
        Sticker = sticker;
    }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    public DeleteStickerFromSetRequest()
        : base("deleteStickerFromSet")
    { }
}
