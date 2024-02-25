using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a sticker set that was created by the bot.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]public class DeleteStickerSetRequest : RequestBase<bool>
{
    //
    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required string Name { get; init; }

    /// <summary>
    /// Initializes a new request with name
    /// </summary>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required parameters")]
    public DeleteStickerSetRequest(string name)
        : this()
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public DeleteStickerSetRequest()
        : base("deleteStickerSet")
    { }
}
