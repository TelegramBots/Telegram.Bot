using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a sticker from a set created by the bot. Returns <see langword="true"/> on success.
/// </summary>
public class DeleteStickerFromSetRequest : RequestBase<bool>
{
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
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
