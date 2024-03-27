using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get information about custom emoji stickers by their identifiers.
/// Returns an Array of <see cref="Sticker"/> objects.
/// </summary>
public class GetCustomEmojiStickersRequest : RequestBase<Sticker[]>
{
    /// <summary>
    /// List of custom emoji identifiers. At most 200 custom emoji identifiers can be specified.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> CustomEmojiIds { get; init; }

    /// <summary>
    /// Initializes a new request with name
    /// </summary>
    /// <param name="customEmojiIds">
    /// List of custom emoji identifiers. At most 200 custom emoji identifiers can be specified.
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public GetCustomEmojiStickersRequest(IEnumerable<string> customEmojiIds)
        : this()
    {
        CustomEmojiIds = customEmojiIds;
    }

    /// <summary>
    /// Initializes a new request with name
    /// </summary>
    public GetCustomEmojiStickersRequest()
        : base("getCustomEmojiStickers")
    { }
}
