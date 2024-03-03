using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the mask position of a mask sticker.
/// The sticker must belong to a sticker set that was created by the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerMaskPositionRequest : RequestBase<bool>
{
    //
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// A JSON-serialized object with the position where the mask should be placed on faces.
    /// <see cref="Nullable">Omit</see> the parameter to remove the mask position.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MaskPosition? MaskPosition { get; set; }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetStickerMaskPositionRequest(InputFileId sticker)
        : this()
    {
        Sticker = sticker;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetStickerMaskPositionRequest()
        : base("setStickerMaskPosition")
    { }
}
