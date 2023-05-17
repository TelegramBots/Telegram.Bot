using System.Collections.Generic;
using System.Net.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new sticker set owned by a user.
/// The bot will be able to edit the sticker set thus created.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CreateNewStickerSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only English letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <summary>
    /// Sticker set title, 1-64 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// A JSON-serialized list of 1-50 initial stickers to be added to the sticker set
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<InputSticker> Stickers { get; }

    /// <summary>
    /// Format of stickers in the set.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public StickerFormat StickerFormat { get; }

    /// <summary>
    /// Type of stickers in the set.
    /// By default, a regular sticker set is created.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public StickerType? StickerType { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if stickers in the sticker set must be repainted to the
    /// color of text when used in messages, the accent color if used as emoji status, white
    /// on chat photos, or another appropriate color based on context;
    /// for <see cref="StickerType.CustomEmoji">custom emoji</see> sticker sets only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? NeedsRepainting { get; set; }

    /// <summary>
    /// Initializes a new request with userId, name, title, stickers and stickerFormat
    /// </summary>
    /// <param name="userId">
    /// User identifier of sticker set owner
    /// </param>
    /// <param name="name">
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </param>
    /// <param name="title">
    /// Sticker set title, 1-64 characters
    /// </param>
    /// <param name="stickers">
    /// A JSON-serialized list of 1-50 initial stickers to be added to the sticker set
    /// </param>
    /// <param name="stickerFormat">
    /// Format of stickers in the set.
    /// </param>
    public CreateNewStickerSetRequest(
        long userId,
        string name,
        string title,
        IEnumerable<InputSticker> stickers,
        StickerFormat stickerFormat)
        : base("createNewStickerSet")
    {
        UserId = userId;
        Name = name;
        Title = title;
        Stickers = stickers;
        StickerFormat = stickerFormat;
    }

    /// <inheritdoc/>
    public override HttpContent ToHttpContent()
    {
        var multipartContent = GenerateMultipartFormDataContent();

        foreach (var inputSticker in Stickers)
        {
            if (inputSticker is { Sticker: InputFileStream file })
            {
                multipartContent.AddContentIfInputFile(file, file.FileName!);
            }
        }

        return multipartContent;
    }
}
