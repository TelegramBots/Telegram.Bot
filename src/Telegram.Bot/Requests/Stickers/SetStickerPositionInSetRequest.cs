using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to move a sticker in a set created by the bot to a specific position.
/// Returns <see langword="true"/> on success.
/// </summary>
public class SetStickerPositionInSetRequest : RequestBase<bool>
{
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// New sticker position in the set, zero-based
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Position { get; init; }

    /// <summary>
    /// Initializes a new request with sticker and position
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="position">New sticker position in the set, zero-based</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetStickerPositionInSetRequest(InputFileId sticker, int position)
        : this()
    {
        Sticker = sticker;
        Position = position;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetStickerPositionInSetRequest()
        : base("setStickerPositionInSet")
    { }
}
