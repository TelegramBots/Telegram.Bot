using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change search keywords assigned to a regular or custom emoji sticker.
/// The sticker must belong to a sticker set created by the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerKeywordsRequest : RequestBase<bool>
{
    /// <summary>
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required InputFileId Sticker { get; init; }

    /// <summary>
    /// Optional. A JSON-serialized list of 0-20 search keywords for the sticker
    /// with total length of up to 64 characters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<string>? Keywords { get; set; }

    /// <summary>
    /// Initializes a new request with sticker
    /// </summary>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required parameters")]
    public SetStickerKeywordsRequest(InputFileId sticker)
        : this()
    {
        Sticker = sticker;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetStickerKeywordsRequest()
        : base("setStickerKeywords")
    { }
}
